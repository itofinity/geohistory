using System.Threading.Tasks;
using FluentAssertions;
using UK.CO.Itofinity.GeoHistory.Client.Api.Model.Domain;
using UK.CO.Itofinity.GeoHistory.Client.Api.Service;
using UK.CO.Itofinity.GeoHistory.Spi.Domain.Publication;
using Xunit;

namespace UK.CO.Itofinity.GeoHistory.Client.Api.Test.Integration.Service
{
    public class DefaultPublicationServiceIT
    {
        private string apiKey = System.Environment.GetEnvironmentVariable("GOOGLE_API_KEY");

        [Fact]
        public async Task VerifySearchFindsRealBook()
        {
            var service = new DefaultPublicationService(new GremlinStorageService(), new GoogleBooksSearchService(apiKey));
            IPublication publication = new PublicationDto(null, "The Tunisian Campaign", "", null);
            var result = await service.Search(publication);

            result.Should().NotBeNull();
        }
    }
}
