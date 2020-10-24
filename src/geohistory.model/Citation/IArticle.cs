namespace UK.CO.Itofinity.GeoHistory.Model.Citation
{
    public interface IArticle : IPublication
    {
        string Title { get; }
        IPublication HostPublication { get; }
    }
}