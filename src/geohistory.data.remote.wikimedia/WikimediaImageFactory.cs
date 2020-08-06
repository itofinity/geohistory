using HtmlAgilityPack;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Uk.Co.Itofinity.GeoHistory.Data.Remote.Wikimedia
{
    public class WikimediaImageFactory
    {
        private static readonly HttpClient client = new HttpClient();
        private IEnumerable<string> _images;

        public WikimediaImageFactory()
        {
        }

        private async Task<IEnumerable<string>> GetImageCollection()
        {
            var response = await client.GetAsync("https://commons.wikimedia.org/w/api.php?action=parse&page=NATO_Military_Map_Symbols&format=json");

            var responseString = await response.Content.ReadAsStringAsync();


            var definition = new
            {
                Parse = new
                {
                    Images = new List<string>()
                }
            };

            var page = JsonConvert.DeserializeAnonymousType(responseString, definition);

            return page.Parse.Images;
        }

        public async Task<string> GetUnitSymbolUrl(string type)
        {
            var imageName = $"NATO_Map_Symbol_-_{type}.svg";

            var url = $"https://commons.wikimedia.org/wiki/File:{imageName}";
            var web = new HtmlWeb();
            var htmlDoc = web.Load(url);

            string href = htmlDoc.DocumentNode
                .SelectSingleNode("//div[@class='fullImageLink']/a")
                .Attributes["href"].Value;

            return href;
        }

        public async Task<string> GetUnitSizeUrl(string type)
        {
            var imageName = $"NATO_Map_Symbol_-_Unit_Size_-_{type}.svg";

            var url = $"https://commons.wikimedia.org/wiki/File:{imageName}";
            var web = new HtmlWeb();
            var htmlDoc = web.Load(url);

            string href = htmlDoc.DocumentNode
                .SelectSingleNode("//div[@class='fullImageLink']/a")
                .Attributes["href"].Value;

            return href;
        }

        public async Task<IEnumerable<string>> GetImages()
        {
            if (_images == null)
            {
                _images = await GetImageCollection();
            }

            return _images;

        }
    }
}
