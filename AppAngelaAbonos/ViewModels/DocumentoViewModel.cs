using AppAngelaAbonos.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using AppAngelaAbonos.Views;

namespace AppAngelaAbonos.ViewModels
{
    public class DocumentoViewModel : BaseViewModel
    {
        #region propiedades
        public int IDTipoDocumento;
        public Documento documento { get; set; }
        public ObservableCollection<Cliente> Clientes { get; set; }
        public ObservableCollection<Documento>DocumentoLista{ get; set; }
        private Cliente _cliente;
        public Cliente Cliente
        {
            get { return _cliente; }
            set
            {

                _cliente = value;
                if (value != null)
                    documento.IdCliente = value.Id;
            }
        }
        public Command LoadItemsCommand { get; set; }
        #endregion
        public DocumentoViewModel(int idTipoDocumento)
        {
            Title = "Clientes";
            Clientes = new ObservableCollection<Cliente>();
            DocumentoLista = new ObservableCollection<Documento>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            documento = new Documento() {
                IdDocumento=0,
                IdTipoDocumento= idTipoDocumento
            };
            IDTipoDocumento = idTipoDocumento;
            MessagingCenter.Subscribe<ViewDocumento, Documento>(this, "AddItem", async (obj, item) =>
            {
                if (item.Id > 0)
                    return;
                var newItem = item as Documento;
                Color = "Blue";
                Mensaje = "";
                await DocumentoDatos.AddItemAsync(item);

              
            });
            MessagingCenter.Subscribe<ViewAbonos, Documento>(this, "AddItemAbono", async (obj, item) =>
            {
                if (item.Id > 0)
                    return;
                var newItem = item as Documento;
                Color = "Blue";
                Mensaje = "";
                item.IdDocumento = 1;
                await DocumentoDatos.AddItemAsync(item);


            });
            MessagingCenter.Subscribe<ViewDocumentoDetalle, Documento>(this, "DeleteItem", async (obj, item) =>
            {
                var newItem = item as Documento;

                if (!await DocumentoDatos.DeleteItemAsync(newItem))
                {

                    Color = "Red";
                    Mensaje = "***!!!!Este Documento No se puede Eliminar porque tiene Abonos o ya esta abonado...!!!!***";
                    return;
                }
            });

        }
        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                DocumentoLista.Clear();
                Clientes.Clear();
                var items = await ClienteDatos.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Clientes.Add(item);
                }


                var documentos =await DocumentoDatos.GetItemsAsync(this.IDTipoDocumento);
                foreach (var item in documentos)
                {
                    DocumentoLista.Add(item);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
