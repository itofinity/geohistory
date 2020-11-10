using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using UK.CO.Itofinity.GeoHistory.Client.Api.Service;
using UK.CO.Itofinity.GeoHistory.Client.Spi.Service;
using UK.CO.Itofinity.GeoHistory.Spi.Domain.Organisation.Commercial;
using UK.CO.Itofinity.GeoHistory.Spi.Domain.Publication;
using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Publication;
using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Organisation.Commercial;
using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Audit;
using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Core.Time;
using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.Time;
using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin.Domain.People;
using UK.CO.Itofinity.GeoHistory.Model.Graph.Gremlin;
using UK.CO.Itofinity.GeoHistory.Spi.Domain.People;
using System.ComponentModel.Composition;

namespace UK.CO.GeoHistory.Client.Commands.Search.Google
{
    [Export(typeof(ISearchCommand))]
    internal class SearchGoogleBooksCommand : Command, ISearchCommand
    {
        public const string NAME = "publication";
        public const string DESCRIPTION = "err search to do with publications";

        [ImportingConstructor]
        public SearchGoogleBooksCommand(IStorageService storageService) :base(NAME, DESCRIPTION)
        {
            StorageService = storageService ?? throw new ArgumentNullException(nameof(storageService));

            this.Add(new Option<string>("--apikey"));

            this.Add(new Option<string>("--term"));
            this.Add(new Option<int>("--startIndex"));
            this.Add(new Option<int>("--maxCount"));
            this.Add(new Option<string>("--auditSessionId"));

            this.Handler = CommandHandler.Create<string, string, int, int, string>(async (apikey, term, startIndex, maxCount, auditSessionId) =>
            {
                // TODOencapsulate along with user config check
                if(string.IsNullOrWhiteSpace(apikey))
                {
                    apikey = Environment.GetEnvironmentVariable("GOOGLE_API_KEY");
                }

                if(string.IsNullOrWhiteSpace(auditSessionId))
                {
                    throw new ArgumentNullException("auditSessionId");
                }

                if(maxCount <= 0)
                {
                    maxCount = 10;
                }

                Console.WriteLine($"called {NAME} with \"{apikey}\" \"{term}\"");

                var service = new GoogleBooksSearchService(apikey);
                var results = await service.Search(term, startIndex, maxCount);
                var index = 0;
                var indexedResults = new List<object>();
                results.ForEach(r => {

                    Console.WriteLine($"[{index++}]{GetPublicationText(r)}");
                    indexedResults.Add(r); 
                });
                Console.WriteLine($"Select publication by index [0 -{index}] an hit [ENTER]");
                var searchResponse = Console.ReadLine();
                int requestedIndex;
                if ( Int32.TryParse(searchResponse, out requestedIndex) )
                {
                    var tuple = (Tuple<IPublication, IPublisher, IEnumerable<IPerson>>)indexedResults[requestedIndex];
                    Console.WriteLine($"Store [{GetPublicationJson(tuple)}] [Y/n] ?");
                    var storeResponse = Console.ReadLine();
                    if("y".Equals(storeResponse.ToLower()))
                    {
                        var items = new List<IQuery>();

                        var publication = new Publication(tuple.Item1.Name, auditSessionId);
                        items.Add(publication);

                        var publisher = new Publisher(tuple.Item2.Name, publication.Id, auditSessionId);
                        items.Add(publisher);

                        tuple.Item3.ToList().ForEach(a =>
                        {
                            var author = new Person(a.Name, publication.Id, auditSessionId);
                            items.Add(author);
                        });

                        await storageService.StoreAsync(items);

                        Directory.CreateDirectory(@".\data\publication");
                        File.WriteAllText($@".\data\publication\{GetFileName(tuple.Item1.Name)}.json", JsonConvert.SerializeObject(tuple.Item1, Formatting.Indented));
                        Directory.CreateDirectory(@".\data\publisher");
                        File.WriteAllText($@".\data\publisher\{GetFileName(tuple.Item2.Name)}.json", JsonConvert.SerializeObject(tuple.Item2, Formatting.Indented));
                        Directory.CreateDirectory(@".\data\author");
                        tuple.Item3.ToList().ForEach(a =>
                        {
                            var author = JsonConvert.SerializeObject(a, Formatting.Indented);
                            File.WriteAllText($@".\data\author\{GetFileName(a.Name)}.json", JsonConvert.SerializeObject(a, Formatting.Indented));
                        });
                    }
                }
                else { Console.WriteLine("oops"); }
            });
        }

        public IStorageService StorageService { get; }

        private object GetFileName(string name)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                return Guid.NewGuid().ToString();
            }

            foreach (char c in System.IO.Path.GetInvalidFileNameChars())
            {
                name = name.Replace(c, '_');
            }

            return name.Replace(" ", "_");
        }

        private string GetPublicationJson(Tuple<IPublication, IPublisher, IEnumerable<IPerson>> tuple)
        {
            var buffer = new StringBuilder();
            var publication = JsonConvert.SerializeObject(tuple.Item1, Formatting.Indented);
            buffer.AppendLine(publication);
            var publisher = JsonConvert.SerializeObject(tuple.Item2, Formatting.Indented);
            buffer.AppendLine(publisher);
            tuple.Item3.ToList().ForEach(a =>
            {
                var author = JsonConvert.SerializeObject(a, Formatting.Indented);
                buffer.AppendLine(author);
            });

            return buffer.ToString();
        }

        private string GetPublicationText(Tuple<IPublication, IPublisher, IEnumerable<IPerson>> v)
        {
            var title = v.Item1.Name;
            var authorList = v.Item3?.Select(p => p.Name);
            var authors = authorList != null ? string.Join(",", authorList) : "[UNDEFINED]";
            var publishers = v.Item2.Name;
            var year = "UNDEFINED"; // v.Item1.PublishedDate?.DateTime.Year;
            return $"{title}/{authors}/{year}/{publishers}";
        }
    }
}
