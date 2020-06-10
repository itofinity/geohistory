namespace Uk.Co.Itofinity.Geohistory.Model.Citation
{
    public interface IAdministrativeRegion
    {
        string Name { get; }
        string Acronym { get; }
        string ToShortString();
    }
}