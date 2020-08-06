using System.Globalization;
using Xunit;

namespace Uk.Co.Itofinity.GeoHistory.Model.test.unit.citation
{
    public class EntryTest
    {
        [Fact]
        public void VerifyXXXXX()
        {
            /*
            
            Citation:

            Crete. The Battle and The Resistence (/)
            Antony Beevor (/)
            1991 (/)
            p59 (/)
            publisher (/)
            publisher addresses (/)

            */
            /*
            var citation = new ApaCitation(
                    new SimplePageRange(59),
                    new SimplePublication("Crete. The Battle and The Resistence",
                        new List<IAuthor>() {
                            new SimpleAuthor( "Beevor", new List<string>() {"Antony"})
                        },
                        new List<IEditor>(),
                        new SimplePublicationDateTime(new DateTime(1991, 1, 1), "yyyy"),
                        new SimplePublisher("Penguin Group", new List<IPostalAddress>()
                            {
                                new SimplePostalAddress(
                                    "Penguin Books Ltd",
                                    new SimpleStreetAddress(27, "Wrights Lane"), 
                                    new Municipality("London"), 
                                    SimpleAdministrativeRegion.Empty, 
                                    new SimplePostalCode("W8 5TZ"),
                                    new RegionInfo("GB")
                                    ),
                                new SimplePostalAddress(
                                    "Penguin Putnam Inc.",
                                    new SimpleStreetAddress(375, "Hudson Street"), 
                                    new Municipality("New York"), 
                                    new State("New York"),
                                    new SimplePostalCode("10014"),
                                    new RegionInfo("US")
                                    ),
                                new SimplePostalAddress(
                                    "Penguin Books Australia Ltd",
                                    SimpleStreetAddress.Empty, 
                                    new Municipality("Ringwood"), 
                                    new State("Victoria"),
                                    SimplePostalCode.Empty,
                                    new RegionInfo("AU")
                                    ),
                                new SimplePostalAddress(
                                    "Penguin Books Canada Ltd",
                                    new SimpleStreetAddress(10, "Alcorn Avenue"), 
                                    new Municipality("Toronto"), 
                                    new State("Ontario"),
                                    new SimplePostalCode("M4V 3B2"),
                                    new RegionInfo("CA")
                                    ),
                                new SimplePostalAddress(
                                    "Penguin Books (NZ) Ltd",
                                    new SimpleStreetAddress(182, 190, "Wairau Road"), 
                                    new Municipality("Auckland"), 
                                    SimpleAdministrativeRegion.Empty, 
                                    new SimplePostalCode("10"),
                                    new RegionInfo("NZ")
                                    ),
                            })
                    )
                );

                citation.ToString().Should().Be("Beevor, A. (1991). Crete. The Battle and The Resistence. London: Penguin Group.");
            */
            /*
                        Battalion 1?
                        Battalion 2?
                        Battalion 3?
                        Battalion 4?
                        2nd New Zealand Div
                        5th New Zealand Brigade
                        Brigadier James Hargest
                        Suda Bay

                        25/04/1941
             */
            var newZealand = new RegionInfo("nz");
            //var brigadierJamesHargest = new Person("Hargest", "James", new DateTime(1891, 9, 4));
            //var brigadier = new Brigadier(newZealand);
            //brigadierJamesHargest.AddRole(new TemporalRole(brigadier, new FuzzyDateTime(new DateTime(1940, 5, 1), "yyyy/MM")));

            //var secondNewZealandDivision = new Division(newZealand);

            //var fifthNewZealandBrigade = new Brigade(newZealand);
            //secondNewZealandDivision.AddSubordinate(new TemporalChainOfCommand(fifthNewZealandBrigade, new FuzzyDateTime(new DateTime(1940, 5, 1), "yyyy/MM")));

            //var battalion1 = new Battalion(newZealand);
            //battalion1.AddSuperior(new TemporalChainOfCommand(secondNewZealandDivision, new FuzzyDateTime(new DateTime(1940, 5, 1), "yyyy/MM")));
            /*
            var battalion4 = new Unit<Battalion>() {
                country = newZealand,
                Parent = secondNewZealandDivision 
            };
            var battalion4 = new Unit<Battalion>() {
                country = newZealand,
                Parent = secondNewZealandDivision 
            };
            var battalion4 = new Unit<Battalion>() {
                country = newZealand,
                Parent = secondNewZealandDivision 
            };
            */

        }
    }
}