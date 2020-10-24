using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UK.CO.Itofinity.GeoHistory.Model;
using UK.CO.Itofinity.GeoHistory.Model.Audit;
using UK.CO.Itofinity.GeoHistory.Model.Citation;
using UK.CO.Itofinity.GeoHistory.Model.Location;
using UK.CO.Itofinity.GeoHistory.Model.Organisation;
using UK.CO.Itofinity.GeoHistory.Model.Organisation.Military;
using UK.CO.Itofinity.GeoHistory.Model.People;
using UK.CO.Itofinity.GeoHistory.Model.Role;
using UK.CO.Itofinity.GeoHistory.Model.Role.Military;
using UK.CO.Itofinity.GeoHistory.Model.Time;
using UK.CO.Itofinity.GeoHistory.Render.Kml;

namespace UK.CO.Itofinity.GeoHistory.SpikeOne
{
    public class MemoryData
    {
        private static Dictionary<string, ICitation> citations = new Dictionary<string, ICitation>();

        public static void run(string apikey, string output)
        {
            Console.WriteLine("Generate KML!");
            List<Entry> entries = SearchDataModel();

            //TODO normalize the data set
            // e.g. if there is no enddate for an entry take the startdate of the next entry for the same unit etc ....
            var kmlData = Normalizer.ForRendering(entries, apikey);

            using (var writer = KmlWriter.Create(output + "-" + DateTime.Now.Ticks))
            {
                kmlData.ToList().ForEach(async re => await writer.Write(re));

            }
        }

        private static List<Entry> SearchDataModel()
        {
            var entries = new List<Entry>();

            var uk = new RegionInfo("en-UK");
            var derby = new Municipality("Derby");
            var catterick = new Municipality("Catterick");
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

            var mobilisationDate = new FuzzyDateTime(new DateTime(1939, 7, 29), "yyyy/MM/dd");
            var veDay = new FuzzyDateTime(new DateTime(1945, 5, 8), "yyyy/MM/dd");

            var armouredCar = new TemporalRole(
                new Reconnaissance(),
                new FuzzyDateRange(
                    mobilisationDate),
                GetCitationPage(1, scrapBook),
                audit);



            var firstDerbyshireYeomanry = new Regiment("1st Derbyshire Yeomanry", armouredCar, uk, GetCitationPage(1, scrapBook),
                audit);

            var siddalsRoad = new SimplePostalAddress(null, new SimpleStreetAddress(91, "Siddals Road"), derby, null, null, uk, null, null);
            var siddalsRoadPosting = new TemporalLocation(siddalsRoad, new FuzzyDateRange(mobilisationDate),
                                    GetCitationPage(1, scrapBook),
                                    audit);

            firstDerbyshireYeomanry.AddLocation(siddalsRoadPosting);

            var mccHarrison = new Person("M.C.C", "Harrision", null, uk, GetCitationPage(2, scrapBook),
                audit);

            var commandFirstDerbyshireYeomanryPosting = new TemporalRole(new LieutenantColonel(firstDerbyshireYeomanry, mccHarrison),
                        new FuzzyDateRange(
                            new FuzzyDateTime(new DateTime(1939, 11, 7), "yyy/MM"),
                            new FuzzyDateTime(new DateTime(1941, 4, 1), "yyy/MM")
                        ),
                        GetCitationPage(1, scrapBook),
                        audit);

            firstDerbyshireYeomanry.AddPersonel(commandFirstDerbyshireYeomanryPosting);
            mccHarrison.AddAppointment(commandFirstDerbyshireYeomanryPosting);







            var ashbourneRoad = new SimplePostalAddress(null, new SimpleStreetAddress("Ashbourne Road"), derby, null, null, uk, null, null);
            var ashbourneRoadPosting = new TemporalLocation(ashbourneRoad, new FuzzyDateRange(new FuzzyDateTime(new DateTime(1939, 11, 1), "yyyy/MM"), new FuzzyDateTime(new DateTime(1940, 05, 1), "yyyy/MM")),
                GetCitationPage(2, scrapBook),
                audit);

            firstDerbyshireYeomanry.AddLocation(ashbourneRoadPosting);
            mccHarrison.AddLocation(ashbourneRoadPosting);





            var cavalryDivison = new TemporalRole(
                new Cavalry(),
                new FuzzyDateRange(
                    mobilisationDate),
                GetCitationPage(4, scrapBook),
                audit);
            var catterickGarrision = new SimplePostalAddress(null, null, catterick, null, null, uk, null, null);
            var catterickGarrisionPosting = new TemporalLocation(catterickGarrision, new FuzzyDateRange(mobilisationDate, new FuzzyDateTime(new DateTime(1940, 05, 1), "yyyy/MM")),
                GetCitationPage(4, scrapBook),
                audit);

            var firstCavalryDivision = new Division("1st Cavalry", armouredCar, uk,
                GetCitationPage(4, scrapBook),
                audit);
            firstCavalryDivision.AddLocation(catterickGarrisionPosting);

            var cocFirstDerbyshireYeomanry = new TemporalChainOfCommand(firstCavalryDivision, firstDerbyshireYeomanry, new FuzzyDateRange(mobilisationDate, new FuzzyDateTime(new DateTime(1940, 05, 1), "yyyy/MM")),
                GetCitationPage(4, scrapBook),
                audit);
            firstCavalryDivision.AddHierarchy(cocFirstDerbyshireYeomanry);
            firstDerbyshireYeomanry.AddHierarchy(cocFirstDerbyshireYeomanry);

            var search = new FuzzyDateRange(new FuzzyDateTime(new DateTime(1939, 1, 1), "yyyy/MM/dd"), new FuzzyDateTime(new DateTime(1940, 12, 31), "yyyy/MM/dd"));


            var aSquadron = new Squadron("A", armouredCar, uk, GetCitationPage(7, scrapBook), audit);
            var bSquadron = new Squadron("B", armouredCar, uk, GetCitationPage(7, scrapBook), audit);
            var cSquadron = new Squadron("C", armouredCar, uk, GetCitationPage(7, scrapBook), audit);
            var dSquadron = new Squadron("D", armouredCar, uk, GetCitationPage(7, scrapBook), audit);


            EstablishChainOfCommand(firstDerbyshireYeomanry, aSquadron, mobilisationDate, veDay, GetCitationPage(4, scrapBook), audit);
            EstablishChainOfCommand(firstDerbyshireYeomanry, bSquadron, mobilisationDate, veDay, GetCitationPage(4, scrapBook), audit);
            EstablishChainOfCommand(firstDerbyshireYeomanry, cSquadron, mobilisationDate, veDay, GetCitationPage(4, scrapBook), audit);
            EstablishChainOfCommand(firstDerbyshireYeomanry, dSquadron, mobilisationDate, veDay, GetCitationPage(4, scrapBook), audit);

            var caistor = new Municipality("Caistor");
            var talbotArms = new SimplePostalAddress(null, new SimpleStreetAddress("16 High Street"), caistor, null, new SimplePostalCode("LN7 6QF"), uk, null, null);
            var rotationStart = new DateTime(1939, 11, 27);
            var rotationEnd = new DateTime(1940, 5, 24);
            int rotation = 0;
            while (rotationStart < rotationEnd)
            {
                var talbotArmsPostingRotation = new TemporalLocation(talbotArms, new FuzzyDateRange(new FuzzyDateTime(rotationStart, "yyyy/MM/dd"), 14),
                   GetCitationPage(2, scrapBook),
                   audit);
                rotationStart = rotationStart.AddDays(14);
                int i = rotation % 4;
                switch (i)
                {
                    case 0:
                        aSquadron.AddLocation(talbotArmsPostingRotation);
                        entries.Add(GetEntry(aSquadron, talbotArmsPostingRotation));
                        break;
                    case 1:
                        bSquadron.AddLocation(talbotArmsPostingRotation);
                        entries.Add(GetEntry(bSquadron, talbotArmsPostingRotation));
                        break;
                    case 2:
                        cSquadron.AddLocation(talbotArmsPostingRotation);
                        entries.Add(GetEntry(cSquadron, talbotArmsPostingRotation));
                        break;
                    case 3:
                        dSquadron.AddLocation(talbotArmsPostingRotation);
                        entries.Add(GetEntry(dSquadron, talbotArmsPostingRotation));
                        break;
                }

                rotation++;
            }


            entries.Add(GetEntry(firstDerbyshireYeomanry, firstDerbyshireYeomanry.Locations[0]));
            entries.Add(GetEntry(firstDerbyshireYeomanry, firstDerbyshireYeomanry.Locations[1]));
            entries.Add(GetEntry(mccHarrison, mccHarrison.Locations[0]));
            entries.Add(GetEntry(firstCavalryDivision, firstCavalryDivision.Locations[0]));


            return entries;
        }

        private static void EstablishChainOfCommand(AbstractOrganisation superior, AbstractOrganisation inferior, FuzzyDateTime from, FuzzyDateTime to, ICitation citation, IAudit audit)
        {
            var coc = new TemporalChainOfCommand(superior, inferior, new FuzzyDateRange(from, to),
              citation,
              audit);
            inferior.AddHierarchy(coc);
            superior.AddHierarchy(coc);
        }

        private static Entry GetEntry(IEntity entity, TemporalLocation temporalLocation)
        {
            return new Entry(entity, temporalLocation.Location, temporalLocation.DateTimeRange, temporalLocation.Citation, temporalLocation.Audit);
        }

        private static ICitation GetCitationPage(int page, IPublication publication)
        {
            return GetCitationPage(page, page, publication);
        }

        private static ICitation GetCitationPage(int firstPage, int lastPage, IPublication publication)
        {
            var key = $"{firstPage}-{lastPage}";
            if (!citations.Keys.Contains(key))
            {
                citations[key] = new ApaCitation(new SimplePageRange(firstPage, lastPage), publication);
            }

            return citations[key];
        }



    }
}
