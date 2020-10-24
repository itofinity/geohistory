using GoogleMaps.LocationServices;
using SharpKml.Base;
using SharpKml.Dom;
using SharpKml.Engine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using UK.CO.Itofinity.GeoHistory.Data.Remote.Wikimedia;
using UK.CO.Itofinity.GeoHistory.Model;
using UK.CO.Itofinity.GeoHistory.Model.Citation;
using UK.CO.Itofinity.GeoHistory.Model.Location;
using UK.CO.Itofinity.GeoHistory.Model.Organisation;
using UK.CO.Itofinity.GeoHistory.Model.Time;

namespace UK.CO.Itofinity.GeoHistory.Render.Kml
{
    public class KmlWriter : IDisposable
    {
        private string _filePath;
        private Document _document;
        private static Color32 unknownColorFull = new Color32(255, 255, 255, 0);
        private static Color32 unknownColorPale = new Color32(100, 255, 255, 0);
        private static Color32 unknownColorStrong = new Color32(200, 255, 255, 0);

        private Dictionary<string, Style> _defaultStyles = new Dictionary<string, Style>()
        {
            { "zoneofinfluence",
                new Style() {
                    Id = "zoneofinfluence",
                    Polygon = new PolygonStyle() { Fill = true, Color = unknownColorPale },
                    Line = new LineStyle() { Color = unknownColorPale, Width = 5 },
                }
            },
            { "zoneofcontrol",
                new Style() {
                    Id = "zoneofcontrol",
                    Polygon = new PolygonStyle() { Fill = true, Color = unknownColorStrong },
                    Line = new LineStyle() { Color = unknownColorStrong, Width = 5 },
                }
            },
            { "routeto",
                new Style() {
                    Id = "routeto",
                    Line = new LineStyle() { Color = unknownColorFull, Width = 5 }
                }
            },
            { "location",
                new Style() {
                    Id = "location",
                    Icon = new IconStyle() { Icon = new IconStyle.IconLink(new Uri("http://maps.google.com/mapfiles/kml/pushpin/ylw-pushpin.png")) }
                }
            },
            { "lineofcommand",
                new Style() {
                    Id = "lineofcommand",
                    Line = new LineStyle() { Color = unknownColorFull, Width = 2 }
                }
            },
        };

        private KmlWriter(string filePath)
        {
            this._filePath = filePath;
            this._document = new Document();

            _document.AddFeature(new Folder() { Id = "Units", Name = "Units" });
            _document.AddFeature(new Folder() { Id = "ZonesOfControl", Name = "ZonesOfControl" });
            _document.AddFeature(new Folder() { Id = "ZonesOfInfluence", Name = "ZonesOfInfluence" });
            _document.AddFeature(new Folder() { Id = "LinesOfCommand", Name = "LinesOfCommand" });
            _document.AddFeature(new Folder() { Id = "RoutesTo", Name = "RoutesTo" });
            //_defaultStyles.Values.ToList().ForEach(s => this._document.AddStyle(s));
        }

        public static KmlWriter Create(string filePath)
        {
            return new KmlWriter(filePath);
        }

        public void Dispose()
        {
            if (_document == null)
            {
                return;
            }

            var kmlFile = KmlFile.Create(_document, false);
            var filePath = $"{_filePath}.kml";
            using (var stream = System.IO.File.OpenWrite(filePath))
            {
                kmlFile.Save(stream);
            }
            Console.WriteLine("saved to " + filePath);
        }

        public async Task Write(RenderEntry re)
        {
            var units = _document.FindFeature("Units") as Folder;
            var location = await GetUnit(re);
            if (location != null)
            {
                units.AddFeature(location);
            }


            var zoneOfControl = await GetZoneOfControl(re);
            var zonesOfControl = _document.FindFeature("ZonesOfControl") as Folder;
            zonesOfControl.AddFeature(zoneOfControl);

            var zoneOfInfluence = await GetZoneOfInfluence(re);
            var zonesOfInfluence = _document.FindFeature("ZonesOfInfluence") as Folder;
            zonesOfInfluence.AddFeature(zoneOfInfluence);

            var linesOfCommand = await GetLinesOfCommand(re);
            var linesOfCommands = _document.FindFeature("LinesOfCommand") as Folder;
            linesOfCommand.ForEach(loc => linesOfCommands.AddFeature(loc));

            var routesTo = _document.FindFeature("RoutesTo") as Folder;
            var pathTo = await GetRouteTo(re);
            if (pathTo != null)
            {
                routesTo.AddFeature(pathTo);
            }
        }

        private async Task<List<Placemark>> GetLinesOfCommand(RenderEntry data)
        {
            var placemarks = new List<Placemark>();

            if (!data.Entry.What.Hierarchy.Any())
            {
                return placemarks;
            }





            data.Entry.What.Hierarchy
                .Where(h => h.Superior.Equals(data.Entry.What))
                .Where(h => WasPresent(data.Entry.When.StartDateTime.DateTime, data.Entry.When.EndDateTime?.DateTime, h.StartDateTime.DateTime, h.EndDateTime?.DateTime))
                .ToList()
                .ForEach(h =>
                {
                    var superiorLocation = data.Entry.Where;
                    if (!superiorLocation.Latitude.HasValue || !superiorLocation.Longtitude.HasValue)
                    {
                        return;
                    }


                    var inferiorLocations = h.Inferior.Locations.Where(l => WasPresent(l.StartDateTime.DateTime, l.EndDateTime?.DateTime, h.StartDateTime.DateTime, h.EndDateTime?.DateTime)).ToList();
                    inferiorLocations.ForEach(il =>
                    {
                        if (!il.Location.Latitude.HasValue || !il.Location.Longtitude.HasValue)
                        {
                            return;
                        }
                        var lineString = new LineString();
                        lineString.Coordinates = new CoordinateCollection();
                        lineString.Coordinates.Add(new SharpKml.Base.Vector(superiorLocation.Latitude.Value, superiorLocation.Longtitude.Value));
                        lineString.Coordinates.Add(new SharpKml.Base.Vector(il.Location.Latitude.Value, il.Location.Longtitude.Value));


                        var placemark = new Placemark
                        {
                            Geometry = lineString,
                            Name = data.Entry.ToString() + " Line of Command",
                            Time = GetTimespan(GetActiveDateRange(h, il)),
                            StyleUrl = GetStyle("lineofcommand", 1.0, data.Color)
                        };

                        placemarks.Add(placemark);

                    });


                });




            return placemarks;

        }

        private static FuzzyDateRange GetActiveDateRange(TemporalChainOfCommand h, TemporalLocation il)
        {
            // max start date
            var startDateTime = MaxDateTime(h.StartDateTime.DateTime, il.StartDateTime.DateTime);
            // min end date
            var endDateTime = MinDateTime(h.EndDateTime.DateTime, il.EndDateTime.DateTime);
            var timeSpan = new FuzzyDateRange(new FuzzyDateTime(startDateTime, "yyyy/MM/dd"), new FuzzyDateTime(endDateTime, "yyyy/MM/dd"));
            return timeSpan;
        }

        public static DateTime MaxDateTime(DateTime a, DateTime b)
        {
            return a > b ? a : b;
        }

        public static DateTime MinDateTime(DateTime a, DateTime b)
        {
            return a < b ? a : b;
        }

        private async Task<Placemark> GetRouteTo(RenderEntry data)
        {
            if (data.RouteTo == null)
            {
                return null;
            }

            var lineString = new LineString();
            lineString.Coordinates = new CoordinateCollection();

            data.RouteTo.ToList().ForEach(ll => lineString.Coordinates.Add(new SharpKml.Base.Vector(ll.Latitude, ll.Longtitude)));

            var placemark = new Placemark
            {
                Geometry = lineString,
                Name = data.Entry.ToString() + " Path To",
                Time = GetTimespan(data.Entry.When),
                StyleUrl = GetStyle("routeto", 1.0, data.Color)
            };

            return placemark;
        }

        private async Task<Placemark> GetZoneOfControl(RenderEntry data)
        {
            Polygon polygon = GetInfluencePolygon(data.Entry.Where.Latitude, data.Entry.Where.Longtitude, data.Entry.What.ZoneOfControl.Range);

            var placemark = new Placemark
            {
                Geometry = polygon,
                Name = data.Entry.ToString() + " Zone of Control",
                Time = GetTimespan(data.Entry.When),
                StyleUrl = GetStyle("zoneofcontrol", data.Entry.What.ZoneOfControl.Density, data.Color)
            };

            return placemark;
        }

        private async Task<Placemark> GetZoneOfInfluence(RenderEntry data)
        {

            Polygon polygon = GetInfluencePolygon(data.Entry.Where.Latitude, data.Entry.Where.Longtitude, data.Entry.What.ZoneOfInfluence.Range);

            var placemark = new Placemark
            {
                Geometry = polygon,
                Name = data.Entry.ToString() + " Zone of Influence",
                Time = GetTimespan(data.Entry.When),
                StyleUrl = GetStyle("zoneofinfluence", data.Entry.What.ZoneOfInfluence.Density, data.Color)
            };

            return placemark;
        }

        private Uri GetStyle(string type, double density, Color color)
        {
            if (!_document.Styles.Any(s => s.Id.Equals(type)))
            {
                var style = _defaultStyles[type];
                if (style.Line != null)
                {
                    style.Line.Color = new Color32((byte)(int)(255 * density), color.B, color.G, color.R);
                }
                if (style.Polygon != null)
                {
                    style.Polygon.Color = new Color32((byte)(int)(255 * density), color.B, color.G, color.R);
                }
                if (style.Icon != null)
                {
                    switch (color.Name.ToLower())
                    {
                        case "blue":
                            style.Icon = new IconStyle() { Icon = new IconStyle.IconLink(new Uri("http://maps.google.com/mapfiles/kml/pushpin/blue-pushpin.png")) };
                            break;
                        case "red":
                            style.Icon = new IconStyle() { Icon = new IconStyle.IconLink(new Uri("http://maps.google.com/mapfiles/kml/pushpin/red-pushpin.png")) };
                            break;
                        case "white":
                            style.Icon = new IconStyle() { Icon = new IconStyle.IconLink(new Uri("http://maps.google.com/mapfiles/kml/pushpin/white-pushpin.png")) };
                            break;
                        case "pink":
                            style.Icon = new IconStyle() { Icon = new IconStyle.IconLink(new Uri("http://maps.google.com/mapfiles/kml/pushpin/pink-pushpin.png")) };
                            break;
                        case "purple":
                            style.Icon = new IconStyle() { Icon = new IconStyle.IconLink(new Uri("http://maps.google.com/mapfiles/kml/pushpin/purple-pushpin.png")) };
                            break;
                        case "lightblue":
                            style.Icon = new IconStyle() { Icon = new IconStyle.IconLink(new Uri("http://maps.google.com/mapfiles/kml/pushpin/ltblu-pushpin.png")) };
                            break;
                        case "green":
                            style.Icon = new IconStyle() { Icon = new IconStyle.IconLink(new Uri("http://maps.google.com/mapfiles/kml/pushpin/grn-pushpin.png")) };
                            break;
                    }
                }
                _document.AddStyle(style);
            }

            return new Uri($"#{type}", UriKind.Relative);
        }

        private static SharpKml.Dom.TimeSpan GetTimespan(IFuzzyDateTimeRange temporal)
        {
            var beginning = temporal.StartDateTime.DateTime;
            var ending = temporal.EndDateTime?.DateTime;

            return new SharpKml.Dom.TimeSpan
            {
                Begin = beginning,
                End = ending,
            };
        }

        private static Polygon GetInfluencePolygon(double? possibleLatitude, double? possibleLongtitude, double radius)
        {
            if (!possibleLatitude.HasValue || !possibleLongtitude.HasValue)
            {
                return null;
            }

            var latitude = possibleLatitude.Value;
            var longtitude = possibleLongtitude.Value;

            Gps centre = new Gps(latitude, longtitude);

            // In metres
            double worldRadius = 6371000;
            // In metres

            Gps[] points = new Gps[20];
            CirclePoints(points, centre, worldRadius, radius);
            var polygon = new Polygon();
            polygon.Extrude = true;
            polygon.AltitudeMode = AltitudeMode.RelativeToGround;
            polygon.OuterBoundary = new OuterBoundary();
            polygon.OuterBoundary.LinearRing = new LinearRing();
            polygon.OuterBoundary.LinearRing.Coordinates = new CoordinateCollection();
            points.ToList<Gps>().ForEach(g =>
            {
                polygon.OuterBoundary.LinearRing.Coordinates.Add(new SharpKml.Base.Vector(g.Latitude, g.Longtitude, 30));
            });
            // close the circle
            polygon.OuterBoundary.LinearRing.Coordinates.Add(new SharpKml.Base.Vector(points.First().Latitude, points.First().Longtitude, 30));
            return polygon;
        }

        private async Task<Placemark> GetUnit(RenderEntry data)
        {
            if (!data.Entry.Where.Latitude.HasValue || !data.Entry.Where.Longtitude.HasValue)
            {
                return null;
            }

            var latitude = data.Entry.Where.Latitude.Value;
            var longtitude = data.Entry.Where.Longtitude.Value;

            var point = new SharpKml.Dom.Point
            {
                Coordinate = new SharpKml.Base.Vector(latitude, longtitude),
            };


            var placemark = new Placemark
            {
                Geometry = point,
                Name = data.Entry.ToString() + " Location",
                Time = GetTimespan(data.Entry.When),
                Description = await GetDescription(data),
                StyleUrl = GetStyle("location", 1.0, data.Color)
            };

            return placemark;
        }

        private DateTime? GetEndDateTime(RenderEntry data)
        {
            return data.Entry.When.EndDateTime.DateTime;// != null ? data.Entry.When.EndDateTime.DateTime : data.FixedEndDateTime != null ? data.FixedEndDateTime.DateTime : (DateTime?)null;
        }

        private static bool WasPresent(DateTime appointmentStarted, DateTime? appointmentEnded, DateTime locationStarted, DateTime? locationEnded)
        {
            if (appointmentEnded.HasValue
                && appointmentEnded.Value < locationStarted)
            {
                return false;
            }

            if (locationEnded.HasValue
                && appointmentStarted > locationEnded.Value)
            {
                return false;
            }

            return true;
        }

        private async Task<Description> GetDescription(RenderEntry data)
        {
            return new Description
            {
                Text = $"<![CDATA[" +
                                $"<h1>{data.Entry.What.Name}</h1>\r\n" +
                                GetRolesLine(data) +
                                GetTimelineLine(data) +
                                GetPersonelDescriptionLine(data) +
                                await GetUnitSymbolLine(data) +
                        $"]]>"
            };
        }

        private string GetRolesLine(RenderEntry data)
        {
            var rolesOfInterest = data.Entry.What.Roles.Where(r => WasPresent(r.StartDateTime.DateTime, r.EndDateTime?.DateTime, data.Entry.When.StartDateTime.DateTime, GetEndDateTime(data))).Select(p => p.ToString());

            return $"{String.Join(" ", rolesOfInterest.ToArray())}<br/>\r\n";
        }

        private static string GetTimelineLine(RenderEntry data)
        {
            var when = data.Entry.When;
            var beginningFormatted = when.StartDateTime.DateTime.ToString(when.StartDateTime.Format);
            var endingFormatted = when.EndDateTime?.DateTime.ToString(when.EndDateTime?.Format);
            if (endingFormatted == null)
            {
                endingFormatted = "unknown";
            }

            return $"{beginningFormatted}-{endingFormatted}<br/>\r\n";
        }

        private static async Task<string> GetUnitSymbolLine(RenderEntry data)
        {
            var symbolFactory = new WikimediaImageFactory();

            var organisation = data.Entry.What as IOrganisation;
            if (organisation == null)
            {
                return string.Empty;
            }

            var unitSymbolUrl = await symbolFactory.GetUnitSymbolUrl(organisation.Roles[0].Role.Name);
            // "Reconnaissance");

            var unitSizeUrl = await symbolFactory.GetUnitSizeUrl(organisation.Size); // "Regiment_or_Group");

            return $"<img src=\"{unitSymbolUrl}\" width=\"100\"><br/>\r\n<img src=\"{unitSizeUrl}\"  width=\"100\" ><br/>\r\n";
        }

        private string GetPersonelDescriptionLine(RenderEntry data)
        {
            var organisation = data.Entry.What as IOrganisation;
            if (organisation == null)
            {
                return string.Empty;
            }

            var peopleOfInterest = organisation.Personel.Where(a => WasPresent(a.StartDateTime.DateTime, a.EndDateTime?.DateTime, data.Entry.When.StartDateTime.DateTime, GetEndDateTime(data))).Select(p => p.ToString());

            return $"{String.Join(" ", peopleOfInterest.ToArray())}<br/>\r\n";
        }


        private static AddressData GetAddressData(Entry entry)
        {
            var postalAddress = entry.Where as IPostalAddress;
            if (postalAddress != null)
            {
                return new AddressData()
                {
                    Address = postalAddress.StreetAddress.ToString(),
                    Country = postalAddress.Country.Name,
                    City = postalAddress.Locality.Name,
                };
            }

            return null;
        }


        private static void CirclePoints(Gps[] points, Gps centre, double R, double r)
        {
            int count = points.Length;

            Vector C = centre.ToUnitVector();
            double t = r / R;
            Vector K = Math.Cos(t) * C;
            double s = Math.Sin(t);

            Vector U = K.Orthogonal();
            Vector V = K.Cross(U);
            // Improve orthogonality
            U = K.Cross(V);

            for (int point = 0; point != count; ++point)
            {
                double a = 2 * Math.PI * point / count;
                Vector P = K + s * (Math.Sin(a) * U + Math.Cos(a) * V);
                points[point] = P.ToGps();
            }
        }
    }
}
