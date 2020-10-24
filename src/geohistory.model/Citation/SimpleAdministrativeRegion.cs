namespace UK.CO.Itofinity.GeoHistory.Model.Citation
{
    public class SimpleAdministrativeRegion : IAdministrativeRegion
    {
        public SimpleAdministrativeRegion()
        {
            Name = string.Empty;
            Acronym = string.Empty;
        }
        public static IAdministrativeRegion Empty = new SimpleAdministrativeRegion();

        public string Name { get; }

        public string Acronym { get; }

        public string ToShortString()
        {
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}