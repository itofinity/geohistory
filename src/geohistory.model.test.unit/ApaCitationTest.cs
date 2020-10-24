using FluentAssertions;
using System;
using System.Collections.Generic;
using UK.CO.Itofinity.GeoHistory.Model.Citation;
using Xunit;

namespace UK.CO.Itofinity.GeoHistory.Model.test.unit.citation
{
    /// taken from https://pitt.libguides.com/c.php?g=12108&p=64730
    public class ApaCitationTest
    {
        [Fact]
        public void VerifyInTextCitation()
        {
            AssertInTextCitation(
                "(Crockatt, 1995)",
                new ApaCitation(
                    null,
                    new SimplePublication(null,
                        new List<IAuthor>() {
                            new SimpleAuthor("Crockatt")
                        },
                        new List<IEditor>(),
                        new SimplePublicationDateTime(new DateTime(1995, 1, 1), "yyyy"),
                        null
                    )
                )
            );
        }

        [Fact]
        public void VerifyDirectQuoteFromTheText()
        {
            AssertDirectQuoteCitation(
                "(Crockatt, 1995, p. 1)",
                new ApaCitation(
                new SimplePageRange(1),
                new SimplePublication(null,
                    new List<IAuthor>() {
                        new SimpleAuthor("Crockatt")
                    },
                    new List<IEditor>(),
                    new SimplePublicationDateTime(new DateTime(1995, 1, 1), "yyyy"),
                    null
                )
            ));
        }
        /*
        [Fact]
        public void VerifyABookChapterPrintVersion()
        {
            AssertCitation(
                "Baxter, C. (1997). Race equality in health care and education. Philadelphia: Ballière Tindall.",
                new ApaCitation(
                    null,
                    new SimplePublication("Race equality in health care and education",
                        new List<IAuthor>() {
                            new SimpleAuthor( "Baxter", new List<string>() {"C"})
                        },
                        new List<IEditor>(){
                                new SimpleEditor("Eid", new List<string>() {"M"}),
                                new SimpleEditor("Larsen", new List<string>() {"R", "J"})
                            },
                        new SimplePublicationDateTime(new DateTime(1997, 1, 1), "yyyy"),
                        new SimplePublisher("Ballière Tindall", new SimplePostalAddress(string.Empty, null, new Municipality("Philadelphia"), null, null, new RegionInfo( "US" )))
                    )
                )
            );
        }
        [Fact]
        public void VerifyABookInPrint()
        {
            AssertCitation(
                "Haybron, D. M. (2008). Philosophy and the science of subjective well-being. In M. Eid & R. J. Larsen (Eds.), The science of subjective well-being (pp. 17-43). New York, NY: Guilford Press.", 
                new ApaCitation(
                    null,
                    new SimpleArticle("Philosophy and the science of subjective well-being",
                        new SimplePublication("The science of subjective well-being",
                            new List<IAuthor>() {
                                new SimpleAuthor( "Haybron", new List<string>() {"D", "M"})
                            },
                            new List<IEditor>(){
                                    new SimpleEditor("Eid", new List<string>() {"M"}),
                                    new SimpleEditor("Larsen", new List<string>() {"R", "J"})
                                },
                            new SimplePublicationDateTime(new DateTime(2008, 1, 1), "yyyy"),
                            new SimplePublisher("Guilford Press", new SimplePostalAddress(string.Empty, null, new Municipality("New York"), new State("NY"), null, new RegionInfo("US")))
                        ),
                        new SimplePageRange(17, 43)
                    )
                )
            );
        }

        [Fact]
        public void VerifyAnEBook()
        {
            AssertCitation(
               "Millbower, L. (2003). Show biz training: Fun and effective business training techniques from the worlds of stage, screen, and song. Retrieved from http://www.amacombooks.org/", 
                new ApaCitation(
                    null,
                    new SimplePublication("Show biz training: Fun and effective business training techniques from the worlds of stage, screen, and song",
                        new List<IAuthor>() {
                            new SimpleAuthor( "Millbower", new List<string>() {"L"})
                        },
                        new List<IEditor>(),
                        new SimplePublicationDateTime(new DateTime(2003, 1, 1), "yyyy"),
                        new SimpleWebPublisher(new Uri("http://www.amacombooks.org/"))
                    )
                )
           );
        }
        */
        private static void AssertInTextCitation(string expectedLine, ApaCitation citation)
        {
            var actualLine = citation.ToInTextString();

            AssertCitation(expectedLine, actualLine);
        }

        private static void AssertDirectQuoteCitation(string expectedLine, ApaCitation citation)
        {
            var actualLine = citation.ToDirectQuoteString();

            AssertCitation(expectedLine, actualLine);
        }

        private static void AssertCitation(string expectedLine, ApaCitation citation)
        {
            var actualLine = citation.ToString();

            AssertCitation(expectedLine, actualLine);
        }

        private static void AssertCitation(string expectedLine, string actualLine)
        {
            System.Console.WriteLine("[" + actualLine + "]");
            System.Console.WriteLine("[" + expectedLine + "]");

            actualLine.Should().Be(expectedLine);
        }
    }
}

/*
Material Type
Reference List/Bibliography
A book in print
Baxter, C. (1997). Race equality in health care and education. Philadelphia: Ballière Tindall.
A book chapter, print version
Haybron, D. M. (2008). Philosophy and the science of subjective well-being. In M. Eid & R. J. Larsen (Eds.), The science of subjective well-being (pp. 17-43). New York, NY: Guilford Press.
An eBook
Millbower, L. (2003). Show biz training: Fun and effective business training techniques from the worlds of stage, screen, and song. Retrieved from http://www.amacombooks.org/
An article in a print journal
Alibali, M. W. (1999). How children change their minds: Strategy change can be gradual or abrupt. Developmental Psychology, 35, 127-145.
An article in a journal without DOI
Carter, S., & Dunbar-Odom, D. (2009). The converging literacies center: An integrated model for writing programs. Kairos: A Journal of Rhetoric, Technology, and Pedagogy, 14(1), 38-48. Retrieved from http://kairos.technorhetoric.net/
An article in a journal with DOI
Gaudio, J. L., & Snowdon, C. T. (2008). Spatial cues more salient than color cues in cotton-top tamarins (saguinus oedipus) reversal learning. Journal of Comparative Psychology, 122, 441-444. doi: 10.1037/0735-7036.122.4.441
Websites - professional or personal sites
The World Famous Hot Dog Site. (1999, July 7). Retrieved January 5, 2008, from http://www.xroads.com/~tcs/hotdog/hotdog.html
Websites - online government publications
U.S. Department of Justice. (2006, September 10). Trends in violent victimization by age, 1973-2005. Retrieved from http://www.ojp.usdoj.gov/bjs/glance/vage.htm
Emails (cited in-text only)
According to preservationist J. Mohlhenrich (personal communication, January 5, 2008).
Mailing Lists (listserv)
Stein, C.(2006, January 5).  Chessie rescue - Annapolis, MD [Message posted to Chessie-L electronic mailing list]. Retrieved from  http://chessie-l-owner@lists.best.com
Radio and TV episodes - from library databases
DeFord, F. (Writer). (2007, August 8). Beyond Vick: Animal cruelty for sport [Television series episode]. In NPR (Producer), Morning Edition. Retrieved from Academic OneFile database.
Radio and TV episodes - from website
Sepic, M. (Writer). (2008). Federal prosecutors eye MySpace bullying case [Television series episode]. In NPR (Producer), All Things Considered. Retrieved from http://www.npr.org/templates/story/
Film Clips from website
Kaufman, J.C. (Producer), Lacy, L. (Director), & Hawkey, P. (Writer). (1979). Mean Joe Greene [video file]. Retrieved from http://memory.loc.gov/mbrs/ccmp/meanjoe_01g.ram
Film
Greene, C. (Producer), del Toro, G.(Director). (2015). Crimson peak [Motion picture]. United States: Legendary Pictures.
Photograph (from book, magazine or webpage)
Close, C. (2002). Ronald. [photograph]. Museum of Modern Art, New York, NY. Retrieved from http://www.moma.org/collection/object.php?object_id=108890
Artwork - from library database
Clark, L. (c.a. 1960's). Man with Baby. [photograph]. George Eastman House, Rochester, NY. Retrieved from ARTstor
Artwork - from website
Close, C. (2002). Ronald. [photograph]. Museum of Modern Art, New York. Retrieved from http://www.moma.org/collection/browse_results.php?
object_id=108890
 */
