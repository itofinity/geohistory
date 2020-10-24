using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using UK.CO.Itofinity.GeoHistory.Data.Books.Remote.Google;

namespace GeoHistory.Data.Books.Remote.Google.Test
{
    [TestClass]
    public class BookServiceIT
    {
        private string apiKey = System.Environment.GetEnvironmentVariable("GOOGLE_API_KEY");

        [TestMethod]
        public async Task VerifyQueryReturnsResultsForKnownBook()
        {
            var query = "mailed fist 6";
            var service = new BookService(apiKey);
            var results = await service.Query(query, 0, 10);
            results.Items.Select(i => i.VolumeInfo).ToList().ForEach(vi => System.Console.Out.WriteLine($"{vi.Title}:{vi.Subtitle}"));
        }
    }
}
