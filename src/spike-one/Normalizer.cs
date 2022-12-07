using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Uk.Co.Itofinity.GeoHistory.Data.Remote.Google.Maps;
using Uk.Co.Itofinity.GeoHistory.Model;
using Uk.Co.Itofinity.GeoHistory.Model.Citation;
using Uk.Co.Itofinity.GeoHistory.Model.Location;

namespace Uk.Co.Itofinity.GeoHistory.SpikeOne
{
    public class Normalizer
    {
        public static IEnumerable<RenderEntry> ForRendering(List<Entry> entries, string mapsApiKey)
        {
            Sort(entries);
            return Extend(entries, mapsApiKey);
        }

        private static IEnumerable<RenderEntry> Extend(List<Entry> entries, string mapsApiKey)
        {
            var geo = new CachingGeoLocationService(mapsApiKey);

            return entries
                    .Select(e =>
                    {
                        var index = entries.IndexOf(e);
                        var postalAddress = e.Where as IPostalAddress;
                        if (postalAddress != null && (postalAddress.Latitude == null || postalAddress.Longtitude == null))
                        {
                            var latlong = geo.GetLatitudeLongtitude(e);
                            e.Where.AddLatitudeLongtitude(latlong.Latitude, latlong.Longtitude);
                        }

                        // route to
                        IEnumerable<LatitudeLongtitude> routeTo = null;
                        if (index > 0)
                        {
                            var previous = entries[index - 1];
                            if (previous != null && previous.What.Equals(e.What))
                            {
                                routeTo = geo.GetRoute(previous, e);
                            }
                        }

                        // end date?
                        FixEndDate(entries, e, index);

                        // friendly or not ?
                        var color = GetColor(e);

                        // hierarchy locations
                        e.What.Hierarchy.ToList().ForEach(h =>
                        {
                            var hindex = e.What.Hierarchy.ToList().IndexOf(h);

                            h.Inferior.Locations.ForEach(l =>
                            {
                                var postalAddress = l.Location as IPostalAddress;
                                if (postalAddress != null && (postalAddress.Latitude == null || postalAddress.Longtitude == null))
                                {
                                    var latlong = geo.GetLatitudeLongtitude(l.Location);
                                    l.Location.AddLatitudeLongtitude(latlong.Latitude, latlong.Longtitude);
                                }
                            });

                            FixEndDate(h.Inferior.Locations);
                        });




                        return new RenderEntry(e, color, routeTo);
                    });
        }

        private static void FixEndDate(List<TemporalLocation> locations)
        {
            locations.ForEach(l =>
            {
                var index = locations.IndexOf(l);
                if (l.EndDateTime == null && locations.Count >= index + 1)
                {
                    var next = locations[index + 1];
                    l.DateTimeRange.FixEndDate(next.StartDateTime);

                }
            });
        }
        private static void FixEndDate(List<Entry> entries, Entry e, int index)
        {
            if (e.When.EndDateTime == null
                                        && entries.Count >= index + 1)
            {
                var next = entries[index + 1];
                if (next.What.Equals(e.What))
                {
                    e.When.FixEndDate(next.When.StartDateTime);
                }
            }
        }

        private static Color GetColor(Entry e)
        {
            switch (e.What.Country.Name.ToLower())
            {
                case "uk":
                    return Color.Blue;
                    break;
                default:
                    return Color.Yellow;
            }
        }

        private static void Sort(List<Entry> entries)
        {
            var entryComparator = new EntryComparator();
            entries.Sort(entryComparator);
        }
    }
}
