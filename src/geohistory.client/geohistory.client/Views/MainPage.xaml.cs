using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using geohistory.client.Models;

namespace geohistory.client.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public MainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;
        }

        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                LoadMenuPage(id);
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }

        private void LoadMenuPage(int id)
        {
            switch (id)
            {
                case (int)MenuItemType.Browse:
                    MenuPages.Add(id, new NavigationPage(new ItemsPage()));
                    break;
                // MMINNS Add new menu items here. Part 2
                case (int)MenuItemType.PublicationsList:
                    MenuPages.Add(id, new NavigationPage(new PublicationsListPage()));
                    break;
                //case (int)MenuItemType.PublicationsSearch:
                //    MenuPages.Add(id, new NavigationPage(new PublicationsListPage()));
                //    break;
                case (int)MenuItemType.About:
                    MenuPages.Add(id, new NavigationPage(new AboutPage()));
                    break;
            }
        }
    }
}