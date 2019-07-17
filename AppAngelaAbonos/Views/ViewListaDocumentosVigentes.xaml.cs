using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppAngelaAbonos.Models;
using AppAngelaAbonos.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppAngelaAbonos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewListaDocumentosVigentes : ContentPage
    {
        DocumentoViewModel viewModel;
    
        public ViewListaDocumentosVigentes()
        {
            InitializeComponent();
            BindingContext = viewModel = new DocumentoViewModel(1);
        }
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Documento;
            if (item == null)
                return;

            await Navigation.PushAsync(new ViewDocumentoDetalle(new DocuemntoDetalleViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ViewDocumento()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();


            viewModel.LoadItemsCommand.Execute(null);
        }
    }
}