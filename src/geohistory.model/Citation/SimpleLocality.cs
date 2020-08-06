namespace Uk.Co.Itofinity.GeoHistory.Model.Citation
{
    public class SimpleLocality : ILocality
    {
        public static ILocality Empty = new SimpleLocality();

        public string Name => null;
    }
}