
using AppAngelaAbonos.Models;

namespace AppAngelaAbonos.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Cliente Item { get; set; }
        public ItemDetailViewModel(Cliente item = null)
        {
            Title = item?.Nombre;
            Item = item;
        }
    }
}
