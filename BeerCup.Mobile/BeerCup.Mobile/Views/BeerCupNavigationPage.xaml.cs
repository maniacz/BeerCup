using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BeerCup.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BeerCupNavigationPage : NavigationPage
    {
        public BeerCupNavigationPage()
        {
            InitializeComponent();
        }

        public BeerCupNavigationPage(Page root) : base(root)
        {
            InitializeComponent();
        }
    }
}