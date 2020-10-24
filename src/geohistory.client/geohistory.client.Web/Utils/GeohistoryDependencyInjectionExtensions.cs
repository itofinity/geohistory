using Microsoft.Extensions.DependencyInjection;
using UK.CO.Itofinity.GeoHistory.Client.Spi.Service;
using UK.CO.Itofinity.GeoHistory.Client.Web.Models;
using UK.CO.Itofinity.GeoHistory.Client.Web.Services.Gremlin;

namespace UK.CO.Itofinity.GeoHistory.Client.Web.Utils
{
    public static class GeohistoryDependencyInjectionExtensions
    {
        public static IMvcBuilder AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IItemRepository, ItemRepository>();
            services.AddSingleton<IStorageService, GremlinPublicationService>();

            return services
                .AddMvc()
                .AddNewtonsoftJson();
        }
    }
}
