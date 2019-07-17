using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AppAngelaAbonos.Models;
using AppAngelaAbonos.ViewModels;

namespace AppAngelaAbonos.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

        

          
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            var yesSelected = await DisplayAlert("Question", "Desea Eliminar el Registro?", "Yes", "No");
            if (yesSelected)  // compile error: Can't convert Task<bool> to bool
            {
                MessagingCenter.Send(this, "DeleteItem", this.viewModel.Item);
                await Navigation.PopAsync();
            }
        }
    }
}