namespace Uk.Co.Itofinity.Geohistory.Model.Time
{
    public interface ITemporal
    {
        IFuzzyDateTime StartDateTime { get; }
        IFuzzyDateTime EndDateTime { get; }
    }
}