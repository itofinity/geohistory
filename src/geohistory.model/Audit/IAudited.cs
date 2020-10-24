namespace UK.CO.Itofinity.GeoHistory.Model.Audit
{
    public interface IAudited
    {
        IAudit Audit { get; }
    }
}