namespace UK.CO.Itofinity.GeoHistory.Model.Citation
{
    public interface IAdministrativeRegion
    {
        string Name { get; }
        string Acronym { get; }
        string ToShortString();
    }
}