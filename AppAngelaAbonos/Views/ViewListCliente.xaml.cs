using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AppAngelaAbonos.Models;
using AppAngelaAbonos.Views;
using AppAngelaAbonos.ViewModels;

namespace AppAngelaAbonos.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewListCliente : ContentPage
    {
        ClienteViewModel viewModel;
        public ViewListCliente ()
		{
			InitializeComponent ();
            BindingContext = viewModel = new ClienteViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Cliente;
            if (item == null)
                return;

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ViewCliente()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

          
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}