using System;
using Mathe.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mathe
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new TreePage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}