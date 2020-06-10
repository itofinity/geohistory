using GoogleMaps.LocationServices;
using SharpKml.Base;
using SharpKml.Dom;
using SharpKml.Engine;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Uk.Co.Itofinity.Geohistory.Model;
using Uk.Co.Itofinity.Geohistory.Model.Audit;
using Uk.Co.Itofinity.Geohistory.Model.Citation;
using Uk.Co.Itofinity.Geohistory.Model.Organisation.Military;
using Uk.Co.Itofinity.Geohistory.Model.Time;

namespace Uk.Co.Itofinity.GeoHistory.SpikeOne
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var uk = new RegionInfo("en-UK");
            var derby = new Municipality("Derby");
            var london = new Municipality("London");
            var audit = new SimpleAudit("mminns@itofinity.co.uk");

            var scrapBook = new SimplePublication("1st Derbyshire Yeomanry Scrapbook 1939 - 1947",
                new VariousAuthors(),
                new UnknownEditors(),
                new UnknownPublicationDateTime(),
                new SimplePublisher("Bemrose & Sons Ltd",
                    new List<IPostalAddress>() {
                        new SimplePostalAddress(derby, uk),
                        new SimplePostalAddress(london, uk)
                    }
                )
            );

            var citation1 = new ApaCitation(new SimplePageRange(1), scrapBook);

            var what = new Regiment("1st Derbyshire Yeomanry", Glossary.Reconnaissance, uk);

            var where1 = new SimplePostalAddress(null, new SimpleStreetAddress(91, "Siddals Road"), derby, new SimpleAdministrativeRegion(), null, uk);

            var when1 = new FuzzyDateRange(new FuzzyDateTime(new DateTime(1939, 7, 29), "yyyy/MM/dd"));

            var citation2 = new ApaCitation(new SimplePageRange(2), scrapBook);
            var where2 = new SimplePostalAddress(null, new SimpleStreetAddress("Ashbourne Road"), derby, new SimpleAdministrativeRegion(), null, uk);
            var when2 = new FuzzyDateRange(new FuzzyDateTime(new DateTime(1939, 11, 1), "yyyy/MM"), new FuzzyDateTime(new DateTime(1940, 05, 1), "yyyy/MM"));


            var entries = new List<Entry>{
                new Entry(what, where1, when1, citation1, audit),
                new Entry(what, where2, when2, citation2, audit)
            };
            await WriteToKml(entries);

        }

        private static async Task WriteToKml(IEnumerable<Entry> entries)
        {
            var document = new Document();

            // TODO Style/PolyStyle

            entries.OrderBy(e => e.What.Name).OrderBy(e => e.When.StartDateTime.DateTime);

            entries.ToList().ForEach(async e =>
            {
                Placemark placemark = await GetPlaceMark(e);
                document.AddFeature(placemark);
            });

            // TODO date overlaps
            // TODO routing
            // TODO polygon/influence

            Export(document);
        }

        private static void Export(Document document)
        {
            /*
                        var beginningName = entry.When.StartDateTime.DateTime.ToString("yyyy-MM-dd");
                        var endingName = entry.When.EndDateTime?.DateTime.ToString("yyyy-MM-dd");
                        if (endingName == null)
                        {
                            endingName = beginningName;
                        }

                        var entryName = $"{entry.What.Name.Replace(" ", "_")}-{beginningName}-{endingName}";
                        */
            var entryName = $"out";


            // This allows us to save and Element easily.
            KmlFile kml = KmlFile.Create(document, false);
            using (var stream = System.IO.File.OpenWrite($"{entryName}.kml"))
            {
                kml.Save(stream);
            }
        }

        private static async Task<Placemark> GetPlaceMark(Entry entry)
        {
            // https://console.cloud.google.com/google/maps-apis/overview;onboard=true?authuser=1&project=maximal-radius-279712

            var gls = new GoogleLocationService(apikey: "AIzaSyD5wjy1Ili-biytep1ni_4sAOMBBepPSTg");
            var address = GetAddressData(entry);

            var latlong = gls.GetLatLongFromAddress(address);
            var Latitude = latlong.Latitude;
            var Longitude = latlong.Longitude;

            var point = new Point
            {
                Coordinate = new SharpKml.Base.Vector(Latitude, Longitude)
            };


            var symbolFactory = new WikimediaImageFactory();
            var unitSymbolUrl = await symbolFactory.GetUnitSymbolUrl(entry.What.Purpose); // "Reconnaissance");
            var unitSizeUrl = await symbolFactory.GetUnitSizeUrl(entry.What.Size); // "Regiment_or_Group");



            var beginning = entry.When.StartDateTime.DateTime;
            DateTime? ending = entry.When.EndDateTime?.DateTime;

            var beginningFormatted = entry.When.StartDateTime.DateTime.ToString(entry.When.StartDateTime.Format);
            var endingFormatted = entry.When.EndDateTime?.DateTime.ToString(entry.When.EndDateTime?.Format);
            if (endingFormatted == null)
            {
                endingFormatted = beginningFormatted;
            }

            var placemark = new Placemark
            {
                Geometry = point,
                Name = address.ToString(),
                Time = new SharpKml.Dom.TimeSpan
                {
                    Begin = beginning,
                    End = ending,
                },
                Description = Render(entry, unitSymbolUrl, unitSizeUrl, beginningFormatted, endingFormatted),
                // TODO StyleUrl
            };

            Gps centre = new Gps(Latitude, Longitude);

            // In metres
            double worldRadius = 6371000;
            // In metres
            double circleRadius = 1000;
            Gps[] points = new Gps[20];
            CirclePoints(points, centre, worldRadius, circleRadius);
            var polygon = new Polygon();
            polygon.Extrude = true;
            polygon.AltitudeMode = AltitudeMode.RelativeToGround;
            polygon.OuterBoundary = new OuterBoundary();
            polygon.OuterBoundary.LinearRing = new LinearRing();
            polygon.OuterBoundary.LinearRing.Coordinates = new CoordinateCollection();
            points.ToList<Gps>().ForEach(g => {
                polygon.OuterBoundary.LinearRing.Coordinates.Add(new SharpKml.Base.Vector(g.Latitude, g.Longtitude, 30));
            });
            polygon

            placemark.Geometry = polygon;

            return placemark;
        }

        private static Description Render(Entry entry, string unitSymbolUrl, string unitSizeUrl, string beginningFormatted, string endingFormatted)
        {
            return new Description
            {
                Text = $"<![CDATA[" +
                                $"<h1>{entry.What.Name}</h1>\r\n" +
                                $"<h2>{beginningFormatted}-{endingFormatted}</h2>\r\n" +
                                $"<img src=\"{unitSymbolUrl}\" width=\"100\"><br/>\r\n" +
                                $"<img src=\"{unitSizeUrl}\"  width = \"100\" >" +
                                $"]]>"
            };
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


    struct Gps
    {
        // In degrees
        public readonly double Latitude;
        public readonly double Longtitude;

        public Gps(double latitude, double longtitude)
        {
            Latitude = latitude;
            Longtitude = longtitude;
        }

        public override string ToString()
        {
            return string.Format("({0},{1})", Latitude, Longtitude);
        }

        public Vector ToUnitVector()
        {
            double lat = Latitude / 180 * Math.PI;
            double lng = Longtitude / 180 * Math.PI;

            // Z is North
            // X points at the Greenwich meridian
            return new Vector(Math.Cos(lng) * Math.Cos(lat), Math.Sin(lng) * Math.Cos(lat), Math.Sin(lat));
        }
    }

    struct Vector
    {
        public readonly double X;
        public readonly double Y;
        public readonly double Z;

        public Vector(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public double MagnitudeSquared()
        {
            return X * X + Y * Y + Z * Z;
        }

        public double Magnitude()
        {
            return Math.Sqrt(MagnitudeSquared());
        }

        public Vector ToUnit()
        {
            double m = Magnitude();

            return new Vector(X / m, Y / m, Z / m);
        }

        public Gps ToGps()
        {
            Vector unit = ToUnit();
            // Rounding errors
            double z = unit.Z;
            if (z > 1)
                z = 1;

            double lat = Math.Asin(z);

            double lng = Math.Atan2(unit.Y, unit.X);

            return new Gps(lat * 180 / Math.PI, lng * 180 / Math.PI);
        }

        public static Vector operator *(double m, Vector v)
        {
            return new Vector(m * v.X, m * v.Y, m * v.Z);
        }

        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public override string ToString()
        {
            return string.Format("({0},{1},{2})", X, Y, Z);
        }

        public double Dot(Vector that)
        {
            return X * that.X + Y * that.Y + Z * that.Z;
        }

        public Vector Cross(Vector that)
        {
            return new Vector(Y * that.Z - Z * that.Y, Z * that.X - X * that.Z, X * that.Y - Y * that.X);
        }

        // Pick a random orthogonal vector
        public Vector Orthogonal()
        {
            double minNormal = Math.Abs(X);
            int minIndex = 0;
            if (Math.Abs(Y) < minNormal)
            {
                minNormal = Math.Abs(Y);
                minIndex = 1;
            }
            if (Math.Abs(Z) < minNormal)
            {
                minNormal = Math.Abs(Z);
                minIndex = 2;
            }

            Vector B;
            switch (minIndex)
            {
                case 0:
                    B = new Vector(1, 0, 0);
                    break;
                case 1:
                    B = new Vector(0, 1, 0);
                    break;
                default:
                    B = new Vector(0, 0, 1);
                    break;
            }

            return (B - minNormal * this).ToUnit();
        }
    }
}

