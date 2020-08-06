namespace Uk.Co.Itofinity.GeoHistory.Model.Organisation
{
    public class SimpleZone : IZone
    {
        public SimpleZone(double range, double density)
        {
            Range = range;
            Density = density;
        }

        public double Range { get; }
        public double Density { get; }
    }
}
