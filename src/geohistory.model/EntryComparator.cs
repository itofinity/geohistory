using System.Collections.Generic;

namespace Uk.Co.Itofinity.GeoHistory.Model
{
    public class EntryComparator : IComparer<Entry>
    {
        public int Compare(Entry x, Entry y)
        {
            if (x == null && y == null)
            {
                return 0;
            }

            if (x != null && y == null)
            {
                return 1;
            }

            if (x == null && y != null)
            {
                return -1;
            }

            if (!x.What.Equals(y.What))
            {
                return x.What.Name.CompareTo(y.What.Name);
            }

            //if (!x.When.Equals(y.When))
            //{
            return x.What.Locations[0].StartDateTime.DateTime.CompareTo(y.What.Locations[0].StartDateTime.DateTime);
            //}
        }
    }
}