using AppAngelaAbonos.AccesoDatos;
using AppAngelaAbonos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppAngelaAbonos.Services
{
    public class ClienteDatos : IDataStore<Cliente>
    {
        DALCliente DAL;
        List<Cliente> Datos;

        public ClienteDatos()
        {
            DAL = new DALCliente();
            Datos = new List<Cliente>();
            DAL.CrearBaseDatos();
        }
      
        public async Task<bool> AddItemAsync(Cliente item)
        {
            DAL.Insertar(item);
            Datos = DAL.Listar();
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Cliente item)
        {
            bool bandera=  DAL.Eliminar(item);
            Datos = DAL.Listar();
            return await Task.FromResult(bandera);
        }

        public async Task<Cliente> GetItemAsync(string id)
        {
            Datos = DAL.Listar();
            return await Task.FromResult(Datos.FirstOrDefault(s => s.Id ==Convert.ToInt32( id)));
        }

        public async Task<IEnumerable<Cliente>> GetItemsAsync(bool forceRefresh = false)
        {
            Datos = DAL.Listar();
            return await Task.FromResult(Datos);
        }

        public async Task<IEnumerable<Cliente>> GetItemsAsync(int IdTipoDocumento)
        {
            Datos = DAL.Listar(IdTipoDocumento);
            return await Task.FromResult(Datos);
        }

        public async Task<bool> UpdateItemAsync(Cliente item)
        {
            DAL.Modificar(item);
            return await Task.FromResult(true);
        }
    }
}
