namespace Uk.Co.Itofinity.Geohistory.Model.Citation
{
    public interface IArticle : IPublication
    {
        string Title { get; }
        IPublication HostPublication { get; }
    }
}