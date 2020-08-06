using System.Collections.Generic;
using System.Linq;

namespace Uk.Co.Itofinity.GeoHistory.Model.Citation
{
    public class SimplePublisher : IPublisher
    {
        public SimplePublisher(string name, IPostalAddress postalAddress)
        {
            Name = name;
            PostalAddresses = new List<IPostalAddress>() { postalAddress };
        }

        public SimplePublisher(string name, List<IPostalAddress> postalAddresses)
        {
            Name = name;
            PostalAddresses = postalAddresses;
        }

        public string Name { get; }

        public IPostalAddress PostalAddress
        {
            get
            {
                if (!PostalAddresses.Any())
                {
                    return SimplePostalAddress.Empty;
                }

                return PostalAddresses[0];
            }
        }
        public List<IPostalAddress> PostalAddresses { get; }

        public override string ToString()
        {
            return $"{PostalAddress.ToShortString()}: {Name}.";
        }
    }
}