using AppAngelaAbonos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppAngelaAbonos.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewCliente : ContentPage
	{
        public Cliente Item { get; set; }
        public ViewCliente ()
		{
			InitializeComponent ();
            Item = new Cliente() { Id=0};
            
            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Item.Nombre))
            {
                await DisplayAlert("Mensaje", "Debe de Capturar el nombre del cliente...", "OK");
                return;
            }
            MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopModalAsync();
        }
    }
}