using System.Globalization;

namespace UK.CO.Itofinity.GeoHistory.Model.Citation
{
    public interface IPostalAddress : IAddress
    {
        string Addressee { get; }

        RegionInfo Country { get; }

        IPostalCode PostalCode { get; }

        IAdministrativeRegion AdministrativeRegion { get; }

        ILocality Locality { get; }

        IStreetAddress StreetAddress { get; }

        string ToShortString();
    }
}