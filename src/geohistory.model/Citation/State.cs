namespace Uk.Co.Itofinity.Geohistory.Model.Citation
{
    public class State : IAdministrativeRegion
    {
        public State(string name, string acronym)
        {
            Name = name;
            Acronym = acronym;
        }

        public State(string name) : this(name, null)
        {

        }

        public string Name { get; }
        public string Acronym { get; }

        public override string ToString()
        {
            return Name;
        }

        public string ToShortString()
        {
            return Acronym;
        }
    }
}