using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using geohistory.client.Models;
using geohistory.client.Views;
using geohistory.client.ViewModels;

namespace geohistory.client.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class PublicationDetailPage : ContentPage
    {
        PublicationDetailViewModel viewModel;

        public PublicationDetailPage(PublicationDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public PublicationDetailPage()
        {
            InitializeComponent();

            var item = new Item
            {
                Text = "Item 1",
                Description = "This is an item description."
            };

            viewModel = new PublicationDetailViewModel(item);
            BindingContext = viewModel;
        }
    }
}