﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BeerCup.Mobile.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuItemTemplate : ContentView
    {
        public MenuItemTemplate()
        {
            InitializeComponent();
        }
    }
}