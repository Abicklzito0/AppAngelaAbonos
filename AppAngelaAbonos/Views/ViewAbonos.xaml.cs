using AppAngelaAbonos.Models;
using AppAngelaAbonos.ViewModels;
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
    public partial class ViewAbonos : ContentPage
    {

        DocumentoViewModel viewModel;

   
        public ViewAbonos()
        {
            InitializeComponent();
            BindingContext = viewModel = new DocumentoViewModel(2);// tipo de documento 1  es la venta y el 2 es el abono

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();


            viewModel.LoadItemsCommand.Execute(null);
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            Documento doc = viewModel.documento;
            MessagingCenter.Send(this, "AddItemAbono", doc);
            await Navigation.PopModalAsync();
        }
    }
}