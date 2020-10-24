namespace UK.CO.Itofinity.GeoHistory.Model.Citation
{
    public class SimplePageRange : IPageRange
    {
        private int v1;
        private int v2;

        /// a range specifying a single page
        public SimplePageRange(int number)
        {
            Start = number;
            End = number;
        }

        public SimplePageRange(int start, int end)
        {
            Start = start;
            End = end;
        }

        public int? Start { get; }
        public int? End { get; }

        public override string ToString()
        {
            var start = Start.HasValue ? $"{Start.Value}" : null;
            var end = End.HasValue
                && End != Start // single page range
                ? $"-{End.Value}" : null;
            var indicator = End.HasValue
                && End != Start // single page range
                ? "pp" : "p";
            return $"{indicator}. {start}{end}";
        }

        public static string GetPageRangeString(IPageRange pageRange)
        {
            return pageRange != null ? pageRange.ToString() : string.Empty;
        }
    }
}