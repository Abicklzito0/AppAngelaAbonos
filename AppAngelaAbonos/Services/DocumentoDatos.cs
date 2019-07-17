using AppAngelaAbonos.AccesoDatos;
using AppAngelaAbonos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAngelaAbonos.Services
{
    public class DocumentoDatos : IDataStore<Documento>
    {

        DALdocumento DAL;
        List<Documento> Datos;
        public DocumentoDatos()
        {
            DAL = new DALdocumento();
            Datos = new List<Documento>();
            DAL.CrearBaseDatos();
        }
        public async Task<bool> AddItemAsync(Documento item)
        {
            DAL.Insertar(item);
            Datos = DAL.Listar(item.IdTipoDocumento);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Documento item)
        {
          bool bandera=  DAL.Eliminar(item);
            Datos = DAL.Listar(item.IdTipoDocumento);
            return await Task.FromResult(bandera);
        }

        public async Task<Documento> GetItemAsync(string id)
        {
       
            return await Task.FromResult(DAL.ObtenerDocumento(Convert.ToInt32(id)));
        }

        public async Task<IEnumerable<Documento>> GetItemsAsync(int idtipodocumento)
        {
            Datos = DAL.Listar(idtipodocumento);
            return await Task.FromResult(Datos);

        }
        public async Task<IEnumerable<Documento>> GetItemsAsync( bool forceRefresh = false)
        {
            Datos = DAL.Listar();
            return await Task.FromResult(Datos);

        }
        public async Task<bool> UpdateItemAsync(Documento item)
        {
            DAL.Modificar(item);
            return await Task.FromResult(true);
        }
    }
}
