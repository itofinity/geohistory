namespace Uk.Co.Itofinity.GeoHistory.Model.Citation
{
    public class SimpleStreetAddress : IStreetAddress
    {
        public static IStreetAddress Empty = new SimpleStreetAddress(null, string.Empty);
        private int v1;
        private int v2;
        private string v3;

        public int? Number { get; }
        public int? EndNumber { get; }
        private string StreetOne { get; }

        public SimpleStreetAddress(string street)
        {
            this.StreetOne = street;
        }

        public SimpleStreetAddress(int? number, string street)
        {
            this.Number = number;
            this.StreetOne = street;
        }

        public SimpleStreetAddress(int? startNumber, int? endNumber, string street) : this(startNumber, street)
        {
            this.EndNumber = endNumber;
        }

        public override string ToString()
        {
            var numbers = (Number.HasValue ? Number.Value.ToString() : "") + (EndNumber.HasValue ? "-" + EndNumber.Value.ToString() : "");
            return $"{numbers} {StreetOne}";
        }
    }
}