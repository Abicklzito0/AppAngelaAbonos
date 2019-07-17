using AppAngelaAbonos.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppAngelaAbonos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewDocumentoDetalle : ContentPage
    {
        DocuemntoDetalleViewModel viewModel;

        public ViewDocumentoDetalle(DocuemntoDetalleViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ViewDocumentoDetalle()
        {
            InitializeComponent();

            string sss = "";


        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            var yesSelected = await DisplayAlert("Mensaje", "Desea Eliminar el Registro?", "Yes", "No");
            if (yesSelected)  // compile error: Can't convert Task<bool> to bool
            {
                MessagingCenter.Send(this, "DeleteItem", this.viewModel.Documento);
                await Navigation.PopAsync();
            }
        }
    }
}