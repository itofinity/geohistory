using GoogleMaps.LocationServices;
using SharpKml.Base;
using SharpKml.Dom;
using SharpKml.Engine;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Uk.Co.Itofinity.GeoHistory.Render.Kml;
using System.CommandLine;
using System.CommandLine.Invocation;
using Microsoft.Extensions.Configuration;
using System.IO;
using Uk.Co.Itofinity.Geohistory.Model.Role.Military;
using System.Drawing;
using Uk.Co.Itofinity.Geohistory.Model.Citation;
using Uk.Co.Itofinity.Geohistory.Model.Audit;
using Uk.Co.Itofinity.Geohistory.Model.Organisation.Military;
using Uk.Co.Itofinity.Geohistory.Model.Time;
using Uk.Co.Itofinity.Geohistory.Model;
using Uk.Co.Itofinity.Geohistory.Model.People;
using Uk.Co.Itofinity.Geohistory.Model.Role;
using Uk.Co.Itofinity.Geohistory.Model.Location;
using Uk.Co.Itofinity.Geohistory.Model.Organisation;

namespace Uk.Co.Itofinity.GeoHistory.SpikeOne
{
    class Program
    {
        private static Dictionary<string, ICitation> citations = new Dictionary<string, ICitation>();

        static async Task<int> Main(string[] args)
        {
            // Create a root command with some options
            var rootCommand = new RootCommand
            {
                new Option<string>(
                    "--apikey",
                    getDefaultValue: () => "undefined",
                    description: "the ApiKey to use to interact with the google maps service"),
                 new Option<string>(
                    "--output",
                    getDefaultValue: () => "ringo",
                    description: "the root filename to use for output"),
            };
            rootCommand.Description = "Generate KML!";

            rootCommand.Handler = CommandHandler.Create<string, string>((apikey, output) =>
            {
                run(apikey, output);
            });

            // Parse the incoming args and invoke the handler
            return rootCommand.InvokeAsync(args).Result;

        }

        private static void run(string apikey, string output)
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
            var talbotArmsPostingRotation1 = new TemporalLocation(talbotArms, new FuzzyDateRange(new FuzzyDateTime(rotationStart, "yyyy/MM/dd"), 14),
                GetCitationPage(2, scrapBook),
                audit);
            var talbotArmsPostingRotation2 = new TemporalLocation(talbotArms, new FuzzyDateRange(new FuzzyDateTime(rotationStart.AddDays(14), "yyyy/MM/dd"), 14),
                GetCitationPage(2, scrapBook),
                audit);
            var talbotArmsPostingRotation3 = new TemporalLocation(talbotArms, new FuzzyDateRange(new FuzzyDateTime(rotationStart.AddDays(28), "yyyy/MM/dd"), 14),
                GetCitationPage(2, scrapBook),
                audit);
            var talbotArmsPostingRotation4 = new TemporalLocation(talbotArms, new FuzzyDateRange(new FuzzyDateTime(rotationStart.AddDays(42), "yyyy/MM/dd"), 14),
                GetCitationPage(2, scrapBook),
                audit);

            aSquadron.AddLocation(talbotArmsPostingRotation1);
            bSquadron.AddLocation(talbotArmsPostingRotation2);
            cSquadron.AddLocation(talbotArmsPostingRotation3);
            dSquadron.AddLocation(talbotArmsPostingRotation4);



            entries.Add(GetEntry(firstDerbyshireYeomanry, firstDerbyshireYeomanry.Locations[0]));
            entries.Add(GetEntry(firstDerbyshireYeomanry, firstDerbyshireYeomanry.Locations[1]));
            entries.Add(GetEntry(mccHarrison, mccHarrison.Locations[0]));
            entries.Add(GetEntry(firstCavalryDivision, firstCavalryDivision.Locations[0]));
            entries.Add(GetEntry(aSquadron, aSquadron.Locations[0]));
            entries.Add(GetEntry(bSquadron, bSquadron.Locations[0]));
            entries.Add(GetEntry(cSquadron, cSquadron.Locations[0]));
            entries.Add(GetEntry(dSquadron, dSquadron.Locations[0]));
            /*

                        /*
                                    entries.Add(new Entry(firstDerbyshireYeomanry,
                                                            siddalsRoad,
                                                            new FuzzyDateRange(mobilisationDate),
                                                            GetCitationPage(1, scrapBook),
                                                            audit));

                                    entries.Add(new Entry(firstDerbyshireYeomanry,
                                       ashbourneRoad,
                                       new FuzzyDateRange(new FuzzyDateTime(new DateTime(1939, 11, 1), "yyyy/MM"), new FuzzyDateTime(new DateTime(1940, 05, 1), "yyyy/MM")),
                                       GetCitationPage(2, scrapBook),
                                       audit));

                                    entries.Add(new Entry(mccHarrison,
                                        ashbourneRoad,
                                        new FuzzyDateRange(new FuzzyDateTime(new DateTime(1939, 11, 1), "yyyy/MM"), new FuzzyDateTime(new DateTime(1940, 05, 1), "yyyy/MM")),
                                        GetCitationPage(2, scrapBook),
                                        audit));
                                        */

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

