namespace Uk.Co.Itofinity.GeoHistory.Model.Time
{
    public interface IFuzzyDateTimeRange : INamed
    {
        IFuzzyDateTime StartDateTime { get; }
        IFuzzyDateTime EndDateTime { get; }

        void FixEndDate(IFuzzyDateTime startDateTime);
    }
}
