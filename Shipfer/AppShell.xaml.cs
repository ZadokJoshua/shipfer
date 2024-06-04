﻿using Shipfer.Views;

namespace Shipfer
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(AuthPage), typeof(AuthPage));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        }
    }
}
