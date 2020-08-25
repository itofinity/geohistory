using geohistory.data.remote.google.books;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace GeoHistory.Data.Books.Remote.Google.Test
{
    [TestClass]
    public class PublicationServiceIT
    {
        private string apiKey = "AIzaSyB7nquw9e91qk-dITttKiUJib3cAhOTdxY";

        [TestMethod]
        public async Task VerifyQueryReturnsResultsForKnownBook()
        {
            var query = "mailed fist";
            var service = new BookService(apiKey);
            var results = await service.Query(query);
            results.Items.Select(i => i.VolumeInfo).ToList().ForEach(vi => System.Console.Out.WriteLine($"{vi.Title}:{vi.Subtitle}"));
        }
    }
}
