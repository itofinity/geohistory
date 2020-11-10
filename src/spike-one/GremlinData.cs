using System;
using System.Collections.Generic;
using tinkerpop.scripts.Engine;
using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin;
using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Core.Time;
using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Audit;
using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Location;
using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Organisation.Commercial;
using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.People;
using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Publication;
using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Time;

using static tinkerpop.scripts.ScriptBuilder;
using static tinkerpop.scripts.ScriptClauses;

namespace UK.CO.Itofinity.GeoHistory.SpikeOne
{
    public class GremlinData
    {
        // Azure Cosmos DB Configuration variables
        // Replace the values in these variables to your own.
        private const string DEFAULT_HOSTNAME = "localhost";
        private const int DEFAULT_PORT = 8901;
        private const string DEFAULT_AUTHKEY = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        private const string DEFAULT_DATABASE = "sandbox1 NOT USED";
        private const string DEFAULT_COLLECTION = "container1";

        public static void run()
        {
            /*foreach(var key in gremlinQueriesOG.Keys)
            {
                var ogQuery = gremlinQueriesOG[key];
                var newQuery = gremlinQueries[key];

                if(!ogQuery.Equals(newQuery))
                {
                    throw new Exception($"{ogQuery} != {newQuery}");
                }

            }*/

            var clean = DataResetQueries();
            var populate = DataPopulationQueries();
            var search = DataSearchQueries();
            var all = new List<string>();
            all.AddRange(clean);
            all.AddRange(populate);
            all.AddRange(search);

            var gremlinService = new GremlinService(DEFAULT_HOSTNAME, DEFAULT_PORT, DEFAULT_AUTHKEY, DEFAULT_DATABASE, DEFAULT_COLLECTION);


            gremlinService.RunGremlinQueries(all);
        }

        public static List<string> DataResetQueries()
        {
            var entries = new List<string>();

            entries.AddRange(GremlinService.DropScript);

            return entries;
        }

        public static List<string> DataSearchQueries()
        {
            var entries = new List<string>();

            //entries.Add(g.V().count().Build());
            //entries.Add(g.V().hasLabel("publication").values("title").Build());
            //entries.Add(g.V("Bemrose & Sons Ltd").@out("hostedby").Build());
            entries.Add(g.V().hasLabel(Municipality.Label).Build());

            entries.Add(g.V("london").@out("hosts").hasLabel("publisher").values("title").Build());

            //entries.Add(g.V().Build());
            //entries.Add(g.E().Build());


            //entries.Add(g.V().hasLabel("unit").Build());

            entries.Add(g.V().hasLabel(FuzzyDateTime.Label).has("ticks", lt(new DateTime(1945, 1, 1).Ticks)).@out("happenedon").Build());
            entries.Add(g.V().hasLabel(FuzzyDateTime.Label).has("ticks", lt(new DateTime(1945, 1, 1).Ticks)).@out("dateof").@out("happenedon").Build());

            entries.Add($"g.V().hasLabel('{FuzzyDateTime.Label}').has('ticks', lt({new DateTime(1945, 1, 1).Ticks})).out('dateof').as('x').out('happenedon').as('y').select('x').select('y', 'x')");

            return entries;
        }

        public static List<string> DataPopulationQueries()
        {
            var entries = new List<string>();

            var auditSessionAndQueries = CreateAuditingSession("Mike Minns", "mminns@itofinity.co.uk", "Session 1", DateTime.Now);
            entries.AddRange(auditSessionAndQueries.Queries);
            var auditSession = auditSessionAndQueries.Instance;

            var scrapbook = new Publication("1st Derbyshire Yeomanry Scrapbook 1939 - 1947", null, null);
            entries.AddRange(scrapbook.ToInsertQueries());

            var citation1 = new Citation(scrapbook.Name, 1, auditSession.Id);
            entries.AddRange(citation1.ToInsertQueries());


            var uk = new Country("en-UK", citation1.Id, auditSession.Id);
            entries.AddRange(uk.ToInsertQueries());

            var derby = new Municipality("Derby", citation1.Id, auditSession.Id);
            entries.AddRange(derby.ToInsertQueries());
            entries.AddRange(new HostedBy(derby.Id, uk.Id, citation1.Id, auditSession.Id).ToInsertQueries());

            var catterick = new Municipality("Catterick", citation1.Id, auditSession.Id);
            entries.AddRange(catterick.ToInsertQueries());
            entries.AddRange(new HostedBy(catterick.Id, uk.Id, citation1.Id, auditSession.Id).ToInsertQueries());

            var london = new Municipality("London", citation1.Id, auditSession.Id);
            entries.AddRange(london.ToInsertQueries());
            entries.AddRange(new HostedBy(london.Id, uk.Id, citation1.Id, auditSession.Id).ToInsertQueries());


            

            /*
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
            */


            var variousPerson = new Person("Various", citation1.Id, auditSession.Id);
            entries.AddRange(variousPerson.ToInsertQueries());
            entries.AddRange(new Authored(variousPerson.Id, scrapbook.Id, citation1.Id, auditSession.Id).ToInsertQueries());

            var unknownPerson = new Person("Unknown", citation1.Id, auditSession.Id);
            entries.AddRange(unknownPerson.ToInsertQueries());
            entries.AddRange(new Edited(unknownPerson.Id, scrapbook.Id, citation1.Id, auditSession.Id).ToInsertQueries());


            var bemrosePublisher = new Publisher("Bemrose & Sons Ltd", citation1.Id, auditSession.Id);
            entries.AddRange(bemrosePublisher.ToInsertQueries());

            entries.AddRange(new HostedBy(bemrosePublisher.Id, derby.Id, citation1.Id, auditSession.Id).ToInsertQueries());
            entries.AddRange(new Hosts(derby.Id, bemrosePublisher.Id, citation1.Id, auditSession.Id).ToInsertQueries());

            entries.AddRange(new HostedBy(bemrosePublisher.Id, london.Id, citation1.Id, auditSession.Id).ToInsertQueries());
            entries.AddRange(new Hosts(london.Id, bemrosePublisher.Id, citation1.Id, auditSession.Id).ToInsertQueries());

            entries.AddRange(new Published(bemrosePublisher.Id, scrapbook.Id, citation1.Id, auditSession.Id).ToInsertQueries());
            entries.AddRange(new PublishedBy(scrapbook.Id, bemrosePublisher.Id, citation1.Id, auditSession.Id).ToInsertQueries());


            /*

            var mobilisationDate = new FuzzyDateTime(new DateTime(1939, 7, 29), "yyyy/MM/dd");
            var veDay = new FuzzyDateTime(new DateTime(1945, 5, 8), "yyyy/MM/dd");
            */

            var mobilisationDate = new FuzzyDateTime(new DateTime(1939, 7, 29), "yyyy/MM/dd", citation1.Id, auditSession.Id);
            entries.AddRange(mobilisationDate.ToInsertQueries());
            var mobilisationDay = new KeyDateTime("UK Mobilisation Day", citation1.Id, auditSession.Id);
            entries.AddRange(mobilisationDay.ToInsertQueries());
            entries.AddRange(new WhatHappenedWhen(mobilisationDay.Id, mobilisationDate.Id, citation1.Id, auditSession.Id).ToInsertQueries());
            entries.AddRange(new WhenWhatHappened(mobilisationDate.Id, mobilisationDay.Id, citation1.Id, auditSession.Id).ToInsertQueries());

            /*
            entries.AddRange(mobilisationDay.ToInsertQueries());
            entries.AddRange(new AuditedBy(mobilisationDay.Id, auditSession.Id).ToInsertQueries());

            var veDate = new FuzzyDateTime(new DateTime(1945, 5, 8), "yyyy/MM/dd", citation1.Id, auditSession.Id);
            entries.AddRange(veDate.ToInsertQueries());
            var veDay = new KeyDateTime("VE Day", citation1.Id, auditSession.Id);
            entries.AddRange(new WhatHappenedWhen(veDay.Id, veDate.Id, citation1.Id, auditSession.Id).ToInsertQueries());
            entries.AddRange(new WhenWhatHappened(veDate.Id, veDay.Id, citation1.Id, auditSession.Id).ToInsertQueries());
            */
            /*
            var armouredCar = new TemporalRole(
                new Reconnaissance(),
                new FuzzyDateRange(
                    mobilisationDate),
                GetCitationPage(1, scrapBook),
                audit);

            */
            

            //var firstDerbyshireYeomanry = new Unit("1st Derbyshire Yeomanry", "regiment", citation1.Id, auditSession.Id);
            /*
            //var firstDerbyshireYeomanry = new Regiment("1st Derbyshire Yeomanry", citation1, auditSession);
            var reconnaissance = new OrganisationRole("reconnaissance");

            var actedAs = new ActedAsDuring(firstDerbyshireYeomanry.Id, reconnaissance.Id, mobilisationDate.Id, citation1.Id, auditSession.Id);
            entries.AddRange(firstDerbyshireYeomanry.ToInsertQueries());
            entries.AddRange(reconnaissance.ToInsertQueries());
            entries.AddRange(actedAs.ToInsertQueries());
            */

            /*
            var siddalsRoad = new SimplePostalAddress(null, new SimpleStreetAddress(91, "Siddals Road"), derby, null, null, uk, null, null);
            var siddalsRoadPosting = new TemporalLocation(siddalsRoad, new FuzzyDateRange(mobilisationDate),
                                    GetCitationPage(1, scrapBook),
                                    audit);
            */
            /*
            var ninetyOne = new Building(91, citation1.Id, auditSession.Id);
            entries.AddRange(ninetyOne.ToInsertQueries());

            var siddalsRoad = new Street("Siddals Road", citation1.Id, auditSession.Id);
            entries.AddRange(siddalsRoad.ToInsertQueries());

            entries.AddRange(new Hosts(siddalsRoad.Id, ninetyOne.Id, citation1.Id, auditSession.Id).ToInsertQueries());
            entries.AddRange(new HostedBy(ninetyOne.Id, siddalsRoad.Id, citation1.Id, auditSession.Id).ToInsertQueries());
            entries.AddRange(new Hosts(derby.Id, siddalsRoad.Id, citation1.Id, auditSession.Id).ToInsertQueries());
            entries.AddRange(new HostedBy(siddalsRoad.Id, derby.Id, citation1.Id, auditSession.Id).ToInsertQueries());



            var latLong = new LatitudeLongtitude(0.0, 0.0, citation1.Id, auditSession.Id);
            entries.AddRange(new Hosts(ninetyOne.Id, latLong.Id, citation1.Id, auditSession.Id).ToInsertQueries());
            entries.AddRange(new HostedBy(latLong.Id, ninetyOne.Id, citation1.Id, auditSession.Id).ToInsertQueries());
            entries.AddRange(new Hosts(siddalsRoad.Id, latLong.Id, citation1.Id, auditSession.Id).ToInsertQueries());
            entries.AddRange(new HostedBy(latLong.Id, siddalsRoad.Id, citation1.Id, auditSession.Id).ToInsertQueries());
            entries.AddRange(new Hosts(derby.Id, latLong.Id, citation1.Id, auditSession.Id).ToInsertQueries());
            entries.AddRange(new HostedBy(latLong.Id, derby.Id, citation1.Id, auditSession.Id).ToInsertQueries());
            entries.AddRange(new Hosts(uk.Id, latLong.Id, citation1.Id, auditSession.Id).ToInsertQueries());
            entries.AddRange(new HostedBy(latLong.Id, uk.Id, citation1.Id, auditSession.Id).ToInsertQueries());

            var address = new Address(citation1.Id, auditSession.Id);
            entries.AddRange(new Hosts(address.Id, ninetyOne.Id, citation1.Id, auditSession.Id).ToInsertQueries());
            entries.AddRange(new HostedBy(ninetyOne.Id, address.Id, citation1.Id, auditSession.Id).ToInsertQueries());
            entries.AddRange(new Hosts(address.Id, siddalsRoad.Id, citation1.Id, auditSession.Id).ToInsertQueries());
            entries.AddRange(new HostedBy(siddalsRoad.Id, address.Id, citation1.Id, auditSession.Id).ToInsertQueries());
            entries.AddRange(new Hosts(address.Id, derby.Id, citation1.Id, auditSession.Id).ToInsertQueries());
            entries.AddRange(new HostedBy(derby.Id, address.Id, citation1.Id, auditSession.Id).ToInsertQueries());
            entries.AddRange(new Hosts(address.Id, uk.Id, citation1.Id, auditSession.Id).ToInsertQueries());
            entries.AddRange(new HostedBy(uk.Id, address.Id, citation1.Id, auditSession.Id).ToInsertQueries());
            entries.AddRange(new Hosts(address.Id, latLong.Id, citation1.Id, auditSession.Id).ToInsertQueries());
            entries.AddRange(new HostedBy(latLong.Id, address.Id, citation1.Id, auditSession.Id).ToInsertQueries());

            entries.AddRange(new LocatedInDuring(firstDerbyshireYeomanry.Id, address.Id, mobilisationDate.Id, citation1.Id, auditSession.Id).ToInsertQueries());
            */

            /*

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

*/
            return entries;
        }

        private static ItemAndQueries CreateAuditingSession(string name, string email, string title, DateTime date)
        {
            var queries = new List<string>();

            var auditSession = new AuditSession(title);
            queries.AddRange(auditSession.ToInsertQueries());

            var citation = new Citation("Audit Session_" + title, auditSession.Id);
            queries.AddRange(citation.ToInsertQueries());

            var auditor = new Auditor(name, name, new List<char>(), email, citation.Id, auditSession.Id);
            queries.AddRange(auditor.ToInsertQueries());

            var auditDate = new FuzzyDateTime(date, citation.Id, auditSession.Id);
            queries.AddRange(auditDate.ToInsertQueries());

            queries.AddRange(new WhatHappenedWhen(auditSession.Id, auditDate.Id, citation.Id, auditSession.Id).ToInsertQueries());
            queries.AddRange(new WhoDidWhat(auditor.Id, auditSession.Id, citation.Id, auditSession.Id).ToInsertQueries());

            return new ItemAndQueries(auditSession, queries);
        }
    }

    internal class ItemAndQueries
    {
        public ItemAndQueries(IIdentifiable instance, List<string> queries)
        {
            Instance = instance ?? throw new ArgumentNullException(nameof(instance));
            Queries = queries ?? throw new ArgumentNullException(nameof(queries));
        }

        public IIdentifiable Instance { get; }
        public List<string> Queries { get; }
    }
}
