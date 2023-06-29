using MyProtocolsApp_Jeanca.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace MyProtocolsApp_Jeanca.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}