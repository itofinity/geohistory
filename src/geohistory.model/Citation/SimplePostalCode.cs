namespace Uk.Co.Itofinity.Geohistory.Model.Citation
{
    public class SimplePostalCode : IPostalCode
    {
        public static IPostalCode Empty = new SimplePostalCode(string.Empty);

        public SimplePostalCode(string code)
        {
            this.Code = Code;
        }

        public string Code { get; }
    }
}