using System.Collections.Generic;
using System.Globalization;

namespace Uk.Co.Itofinity.Geohistory.Model.Citation
{
    public class SimplePostalAddress : IPostalAddress
    {
        public SimplePostalAddress(string addressee, IStreetAddress streetAddress, ILocality locality, IAdministrativeRegion administrativeRegion, IPostalCode postalCode, RegionInfo country, double? latitude, double? longtitude)
        {
            Addressee = addressee;
            StreetAddress = streetAddress;
            Locality = locality;
            AdministrativeRegion = administrativeRegion;
            PostalCode = postalCode;
            Country = country;
            Latitude = latitude;
            Longtitude = longtitude;
        }

        public SimplePostalAddress(IPostalAddress postalAddress, double latitude, double longtitude) : this(postalAddress.Addressee, postalAddress.StreetAddress, postalAddress.Locality,
            postalAddress.AdministrativeRegion, postalAddress.PostalCode, postalAddress.Country, latitude, longtitude)
        {

        }

        public SimplePostalAddress(ILocality locality, RegionInfo country) : this(string.Empty,
            SimpleStreetAddress.Empty,
            locality,
            SimpleAdministrativeRegion.Empty,
            SimplePostalCode.Empty,
            country,
            null,
            null)
        {
        }

        public static IPostalAddress Empty = new SimplePostalAddress(string.Empty,
            SimpleStreetAddress.Empty,
            SimpleLocality.Empty,
            SimpleAdministrativeRegion.Empty,
            SimplePostalCode.Empty,
            null,
            null,
            null);

        public string Addressee { get; }

        public IStreetAddress StreetAddress { get; }

        public ILocality Locality { get; }

        public IAdministrativeRegion AdministrativeRegion { get; }

        public IPostalCode PostalCode { get; }

        public RegionInfo Country { get; }

        public string Name => ToString();

        public string ShortName => ToShortString();

        public double? Latitude { get; private set; }

        public double? Longtitude { get; private set; }

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

        public override string ToString()
        {
            var parts = new List<string>();
            if (Addressee != null
                && !string.IsNullOrWhiteSpace(Addressee.ToString()))
            {
                parts.Add(Addressee.ToString());
            }
            if (StreetAddress != null
                && !string.IsNullOrWhiteSpace(StreetAddress.ToString()))
            {
                parts.Add(StreetAddress.ToString());
            }
            if (Locality != null
                && !string.IsNullOrWhiteSpace(Locality.ToString()))
            {
                parts.Add(Locality.ToString());
            }
            if (AdministrativeRegion != null
                && !string.IsNullOrWhiteSpace(AdministrativeRegion.ToString()))
            {
                parts.Add(AdministrativeRegion.ToString());
            }
            if (PostalCode != null
                && !string.IsNullOrWhiteSpace(PostalCode.ToString()))
            {
                parts.Add(PostalCode.ToString());
            }
            if (Country != null
                && !string.IsNullOrWhiteSpace(Country.ToString()))
            {
                parts.Add(Country.ToString());
            }

            return string.Join(", ", parts);
        }

        public void AddLatitudeLongtitude(double latitude, double longtitude)
        {
            Latitude = latitude;
            Longtitude = longtitude;
        }
    }
}