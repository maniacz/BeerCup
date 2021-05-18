using BeerCup.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace BeerCup.Views
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