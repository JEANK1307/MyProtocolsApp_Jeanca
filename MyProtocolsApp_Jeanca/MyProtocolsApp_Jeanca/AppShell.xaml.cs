﻿using MyProtocolsApp_Jeanca.ViewModels;
using MyProtocolsApp_Jeanca.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MyProtocolsApp_Jeanca
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            //Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
