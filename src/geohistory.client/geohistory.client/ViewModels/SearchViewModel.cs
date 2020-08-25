using geohistory.client.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace geohistory.client.ViewModels
{
    public class SearchViewModel : BaseViewModel
    {
        public ICommand PerformSearch => new Command<string>(async (string query) =>
        {
            SearchResults = await SearchService?.Query(query);
        });

        List<string> searchResults = new List<string>();

        public List<string> SearchResults
        {
            get
            {
                return searchResults;
            }
            set
            {
                SetProperty(ref searchResults, value);
            }
        }

        public ISearchService SearchService => DependencyService.Get<ISearchService>();
    }
}
