using System.Collections.Generic;
using System.Globalization;

namespace Uk.Co.Itofinity.Geohistory.Model.Citation
{
    public class SimplePostalAddress: IPostalAddress
    {
        public SimplePostalAddress(string addressee, IStreetAddress streetAddress, ILocality locality, IAdministrativeRegion administrativeRegion, IPostalCode postalCode, RegionInfo country)
        {
            Addressee = addressee;
            StreetAddress = streetAddress;
            Locality = locality;
            AdministrativeRegion = administrativeRegion;
            PostalCode = postalCode;
            Country = country;
        }

        public SimplePostalAddress(ILocality locality, RegionInfo country) : this(string.Empty,
            SimpleStreetAddress.Empty,
            locality,
            SimpleAdministrativeRegion.Empty,
            SimplePostalCode.Empty,
            country)
        {
        }

        public static IPostalAddress Empty = new SimplePostalAddress(string.Empty, 
            SimpleStreetAddress.Empty, 
            SimpleLocality.Empty, 
            SimpleAdministrativeRegion.Empty, 
            SimplePostalCode.Empty, 
            null);

        public string Addressee { get; }

        public RegionInfo Country { get; }
        public IPostalCode PostalCode { get; }
        
        public IAdministrativeRegion AdministrativeRegion { get; }

        public ILocality Locality { get;}

        public IStreetAddress StreetAddress { get; }

        public string ToShortString()
        {
            var parts = new List<string>();
            if(Locality != null
                && !string.IsNullOrWhiteSpace(Locality.ToString()))
            {
                parts.Add(Locality.ToString());
            }
            if(AdministrativeRegion != null
                && !string.IsNullOrWhiteSpace(AdministrativeRegion.ToString()))
            {
                parts.Add(AdministrativeRegion.ToString());
            }

            return string.Join(", ", parts);
        }
    }
}