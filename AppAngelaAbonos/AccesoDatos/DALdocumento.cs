using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using AppAngelaAbonos.Models;
using System.Linq;

namespace AppAngelaAbonos.AccesoDatos
{
    public class DALdocumento
    {
        public DALdocumento()
        {

        }

        string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        public bool Insertar(Documento doc)
        {

            try
            {
                using (var conexion = new SQLiteConnection(System.IO.Path.Combine(folder, "documento.db")))
                {
                    conexion.Insert(doc);
                    return true;
                }

            }
            catch (SQLiteException ex)
            {

                return false;
            }
        }
        public bool Modificar(Documento doc)
        {

            try
            {
                using (var conexion = new SQLiteConnection(System.IO.Path.Combine(folder, "documento.db")))
                {
                    conexion.Query<Documento>("update documento set importe=? ,idcliente=? where Id=? ", doc.Total, doc.IdCliente, doc.Id);
                    return true;
                }

            }
            catch (SQLiteException ex)
            {

                return false;
            }
        }
        public bool Eliminar(Documento doc)
        {

            try
            {

                if (!ObtenerDocumentoPendiente(doc.Id))
                    using (var conexion = new SQLiteConnection(System.IO.Path.Combine(folder, "documento.db")))
                    {
                        conexion.Query<Documento>("update documento set estado=? where Id=? ", 3, doc.Id);
                        return true;
                    }
                else
                    return false;

            }
            catch (SQLiteException ex)
            {

                return false;
            }
        }
        public List<Documento> Listar()
        {

            try
            {
                using (var conexion = new SQLiteConnection(System.IO.Path.Combine(folder, "documento.db")))
                {

                    using (var conexionCliente = new SQLiteConnection(System.IO.Path.Combine(folder, "cliente.db")))
                    {
                        var resultList = (from mas in conexion.Table<Documento>().Where(c => c.Estado == 0 )
                                          join det in conexionCliente.Table<Cliente>()
                                                   on mas.IdCliente equals det.Id
                                          orderby det.Nombre
                                          select new Documento()
                                          {
                                              // Master
                                              Id = mas.Id,
                                              Cliente = new Cliente() { Nombre = det.Nombre },
                                              Total = mas.Total,
                                              IdTipoDocumento = mas.IdTipoDocumento,
                                              IdCliente = mas.IdCliente,
                                              IdDocumento = mas.IdDocumento


                                          }

                               ).ToList();

                        return resultList;
                    }
                }

            }
            catch (SQLiteException ex)
            {

                return new List<Documento>();
            }
        }
        public List<Documento> Listar(int IdtipoDocumento)
        {

            try
            {
                using (var conexion = new SQLiteConnection(System.IO.Path.Combine(folder, "documento.db")))
                {

                    using (var conexionCliente = new SQLiteConnection(System.IO.Path.Combine(folder, "cliente.db")))
                    {
                        var resultList = (from mas in conexion.Table<Documento>().Where(c => c.Estado != 3 && c.IdTipoDocumento== IdtipoDocumento)
                                          join det in conexionCliente.Table<Cliente>()
                                                   on mas.IdCliente equals det.Id
                                          orderby det.Nombre
                                          select new Documento()
                                          {
                                              // Master
                                              Id = mas.Id,
                                              Cliente = new Cliente() { Nombre = det.Nombre },
                                              Total = mas.Total,
                                              IdTipoDocumento = mas.IdTipoDocumento,
                                              IdCliente = mas.IdCliente,
                                              IdDocumento = mas.IdDocumento,
                                              Descripcion=mas.Descripcion


                                          }

                               ).ToList();

                        return resultList;
                    }
                }

            }
            catch (SQLiteException ex)
            {

                return new List<Documento>();
            }
        }
        public Documento ObtenerDocumento(int id)
        {

            try
            {
                using (var conexion = new SQLiteConnection(System.IO.Path.Combine(folder, "documento.db")))
                {
                    return conexion.Query<Documento>("Select * from documento where id=?", id).FirstOrDefault();
                     
                }

            }
            catch (SQLiteException ex)
            {

                return new Documento() ;
            }
        }
        //****Estados***///
        //0 vigente
        //1 pendiente
        //2 pagado
        //3 cancelado
        public bool ObtenerDocumentoPendiente(int id)
        {

            try
            {
                using (var conexion = new SQLiteConnection(System.IO.Path.Combine(folder, "documento.db")))
                {
                    List<Documento> lista = conexion.Query<Documento>("Select * from documento where idDocumento=?", id);
                    if (lista.Count > 0)
                        return true;
                    else
                        return false;
                }

            }
            catch (SQLiteException ex)
            {

                return true;
            }
        }

        public bool CrearBaseDatos()
        {
            try
            {
                using (var conexion = new SQLiteConnection(System.IO.Path.Combine(folder, "documento.db")))
                {
                   
                    conexion.CreateTable<Documento>();
                    return true;
                }

            }
            catch (SQLiteException ex)
            {

                return false;
            }
        }
    }
}
