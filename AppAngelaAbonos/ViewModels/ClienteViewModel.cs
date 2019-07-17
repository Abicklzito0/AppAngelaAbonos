using AppAngelaAbonos.Models;
using AppAngelaAbonos.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
namespace AppAngelaAbonos.ViewModels
{
    public class ClienteViewModel : BaseViewModel
    {
        public ObservableCollection<Cliente> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
      
        public  ClienteViewModel()
        {
            Title = "Clientes";
            Items = new ObservableCollection<Cliente>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            Mensaje = "";
            Color = "Blue";
            MessagingCenter.Subscribe<ViewCliente, Cliente>(this, "AddItem", async (obj, item) =>
            {
                Color = "Blue";
                Mensaje = "";
                var newItem = item as Cliente;
                Items.Add(newItem);
                await ClienteDatos.AddItemAsync(newItem);
            });

            MessagingCenter.Subscribe<ItemDetailPage, Cliente>(this, "DeleteItem", async (obj, item) =>
            {
                var newItem = item as Cliente;
                Items.Add(newItem);
                if (!await ClienteDatos.DeleteItemAsync(newItem))
                {
                    Mensaje = "***!!!!Este Cliente No se puede Eliminar...!!!!***";
                    Color = "Red";
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

            
                Items.Clear();
                var items = await ClienteDatos.GetItemsAsync(1);
                foreach (var item in items)
                {
                    Items.Add(item);
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
