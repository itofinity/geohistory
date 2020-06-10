namespace Uk.Co.Itofinity.Geohistory.Model.Citation
{
    public class Municipality : ILocality
    {
        public Municipality(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public override string ToString()
        {
            return Name;
        }
    }
}