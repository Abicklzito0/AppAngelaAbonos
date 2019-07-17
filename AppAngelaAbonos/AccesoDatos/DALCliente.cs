using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using AppAngelaAbonos.Models;
using System.Linq;

namespace AppAngelaAbonos.AccesoDatos
{
    public class DALCliente
    {
        public DALCliente()
        {
            //EliminarTablaBaseDatos("cliente.db");
            //EliminarTablaBaseDatos("documento.db");
        }

      static  string folderEliminar = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        public bool Insertar(Cliente cliente)
        {

            try
            {
                using (var conexion = new SQLiteConnection(System.IO.Path.Combine(folder, "cliente.db")))
                {
                    conexion.Insert(cliente);
                    return true;
                }

            }
            catch (SQLiteException ex)
            {

                return false;
            }
        }
        public bool Modificar(Cliente cliente)
        {

            try
            {
                using (var conexion = new SQLiteConnection(System.IO.Path.Combine(folder, "cliente.db")))
                {
                    conexion.Query<Cliente>("update cliente set nombre=? ,Direccion=?,Telefono=? where Id=? ", cliente.Nombre, cliente.Direccion, cliente.Telefono, cliente.Id);
                    return true;
                }

            }
            catch (SQLiteException ex)
            {

                return false;
            }
        }
        public bool Eliminar(Cliente cliente)
        {

            try
            {
                if (!ObtenerClienteConDocumentos(cliente.Id))
                    using (var conexion = new SQLiteConnection(System.IO.Path.Combine(folder, "cliente.db")))
                    {
                        conexion.Delete(cliente);
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
        public List<Cliente> Listar(int IdtipoDocumento)
        {

            try
            {
                using (var conexion = new SQLiteConnection(System.IO.Path.Combine(folder, "documento.db")))
                {

                    using (var conexionCliente = new SQLiteConnection(System.IO.Path.Combine(folder, "cliente.db")))
                    {
                        using (var conexionAbonos = new SQLiteConnection(System.IO.Path.Combine(folder, "documento.db")))
                        {

                           
                            var resultListAux = (from mas in conexion.Table<Documento>().Where(c => c.Estado != 3
                                                 )
                                            
                                              join clie in conexionCliente.Table<Cliente>()
                                                       on mas.IdCliente equals clie.Id
                                                 orderby clie.Nombre
                                              select new{
                                                  Id=clie.Id,
                                                  Nombre=clie.Nombre,
                                                  Total= mas.IdTipoDocumento==1 ? mas.Total:mas.Total*-1
                                              }

                                             
                                              
                                              

                               ).ToList();

                            var resultList = (from reg in resultListAux
                                             
                                              group reg by new{reg.Id,reg.Nombre}
                                              into g
                                              select new Cliente()
                                              {
                                                  Id = g.Key.Id,
                                                  Nombre = g.Key.Nombre,
                                                  Deuda =g.Sum(c=>c.Total)

                                              }
                                            ).ToList();

                            return resultList;
                        }
                    }
                }

            }
            catch (SQLiteException ex)
            {

                return new List<Cliente>();
            }
        }
        public List<Cliente> Listar()
        {

            try
            {
                using (var conexion = new SQLiteConnection(System.IO.Path.Combine(folder, "cliente.db")))
                {
                    return conexion.Table<Cliente>().ToList();
                }

            }
            catch (SQLiteException ex)
            {

                return new List<Cliente>();
            }
        }
        public bool ObtenerCliente(int id)
        {

            try
            {
                using (var conexion = new SQLiteConnection(System.IO.Path.Combine(folder, "cliente.db")))
                {
                    conexion.Query<Cliente>("Select * from cliente where id=?", id);
                    return true;
                }

            }
            catch (SQLiteException ex)
            {

                return true;
            }
        }
        //****Estados***///
        //0 vigente
        //1 pendiente
        //2 pagado
        //3 cancelado
        public bool ObtenerClienteConDocumentos(int id)
        {

            try
            {
                using (var conexion = new SQLiteConnection(System.IO.Path.Combine(folder, "documento.db")))
                {
                    List<Documento> lista = conexion.Query<Documento>("Select * from documento where estado<>3 and idcliente=?", id);
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
                using (var conexion = new SQLiteConnection(System.IO.Path.Combine(folder, "cliente.db")))
                {
                    conexion.CreateTable<Cliente>();
                    return true;
                }

            }
            catch (SQLiteException ex)
            {

                return false;
            }
        }
        public static bool EliminarTablaBaseDatos(string tabla)
        {
            try
            {
                using (var conexion = new SQLiteConnection(System.IO.Path.Combine(folderEliminar, tabla)))
                {
                    conexion.DropTable<Cliente>();
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
