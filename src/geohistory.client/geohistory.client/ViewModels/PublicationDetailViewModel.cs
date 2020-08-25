using System;

using geohistory.client.Models;

namespace geohistory.client.ViewModels
{
    public class PublicationDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public PublicationDetailViewModel(Item item = null)
        {
            Title = item?.Text;
            Item = item;
        }
    }
}
