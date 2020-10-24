using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using UK.CO.Itofinity.GeoHistory.Client.Api.Model.Core;
using UK.CO.Itofinity.GeoHistory.Client.Api.Model.Domain;
using UK.CO.Itofinity.GeoHistory.Spi.Core;
using UK.CO.Itofinity.GeoHistory.Spi.Domain.Organisation.Commercial;
using UK.CO.Itofinity.GeoHistory.Spi.Domain.Publication;
using UK.CO.Itofinity.GeoHistory.Client.Spi.Service;
using UK.CO.Itofinity.GeoHistory.Data.Books.Remote.Google;
using static Google.Apis.Books.v1.Data.Volume;
using UK.CO.Itofinity.GeoHistory.Spi.Domain.People;

namespace UK.CO.Itofinity.GeoHistory.Client.Api.Service
{
    public class GoogleBooksSearchService : ISearchService<Tuple<IPublication, IPublisher, IEnumerable<IPerson>>>
    {
        private IMapper mapper;

        public GoogleBooksSearchService(string apiKey)
        {
            ApiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));

            remoteService = new MemoryCachingBookService(new BookService(apiKey));
            mapper = GetMapper();
        }

        private IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<VolumeInfoData, PublicationDto>()
                    .ForMember(dest => dest.Title,
                        opt => opt.MapFrom(s => s.Title))
                    .ForMember(dest => dest.Description,
                        opt => opt.MapFrom(s => s.Description))
                .ForMember(dest => dest.PublishedDate,
                        opt => opt.MapFrom(s => GetFuzzyDateTime(s.PublishedDate)));

                cfg.CreateMap<VolumeInfoData, PublisherDto>().ForMember(dest => dest.Name,
                        opt => opt.MapFrom(s => s.Publisher));

            });
            return config.CreateMapper();
        }

        private static IFuzzyDateTime GetFuzzyDateTime(string date)
        {
            if(string.IsNullOrWhiteSpace(date))
            {
                return null;
            }

            var parts = date.Split('-');
            var year = GetDatePart(parts, 0);
            var month = GetDatePart(parts, 1);
            var day = GetDatePart(parts, 2);
            var yearFormat = year > 0 ? "yyyy" : null;
            var monthFormat = month > 0 ? "MM" : null;
            var dayFormat = day > 0 ? "dd" : null;
            var format = $"{yearFormat}/{monthFormat}/{dayFormat}";
            return new FuzzyDateTime(new DateTime(NormalizeDatePart(year), NormalizeDatePart(month), NormalizeDatePart(day)), format);
        }

        private static int NormalizeDatePart(int value)
        {
            return value > 0 ? value : 1;
        }

        private static int GetDatePart(string[] parts, int i)
        {
            return parts.Length > i ? int.Parse(parts[i]) : 0;
        }

        public string ApiKey { get; }
        public IBookService remoteService { get; }
        public string Name => "Google Books Api";

        public async Task<List<Tuple<IPublication, IPublisher, IEnumerable<IPerson>>>> Search(string term, int startIndex, int maxResults)
        {
            var volumes = await remoteService.Query(term, startIndex, maxResults);
            if(volumes.Items == null)
            {
                return new List<Tuple<IPublication, IPublisher, IEnumerable<IPerson>>>();
            }

            return volumes.Items.Select(i => {
                    var publication = mapper.Map<PublicationDto>(i.VolumeInfo);
                    var publisher = mapper.Map<PublisherDto>(i.VolumeInfo);
                    var authors = i.VolumeInfo.Authors?.Select(a => new PersonDto(null, a));
                return new Tuple<IPublication, IPublisher, IEnumerable<IPerson>> (publication, publisher, authors?.ToList<IPerson>());
                }).ToList<Tuple<IPublication, IPublisher, IEnumerable<IPerson>>>();
        }
    }
}
