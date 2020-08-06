using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Threading.Tasks;
using static tinkerpop.scripts.ScriptBuilder;
using static tinkerpop.scripts.ScriptClauses;

namespace Uk.Co.Itofinity.GeoHistory.SpikeOne
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            // Create a root command with some options
            var rootCommand = new RootCommand
            {
                new Option<string>(
                    "--apikey",
                    getDefaultValue: () => "undefined",
                    description: "the ApiKey to use to interact with the google maps service"),
                 new Option<string>(
                    "--output",
                    getDefaultValue: () => "ringo",
                    description: "the root filename to use for output"),
                 new Option<string>(
                    "--approach",
                    getDefaultValue: () => "gremlin",
                    description: "the approach to test"),
            };
            rootCommand.Description = "Generate KML!";

            rootCommand.Handler = CommandHandler.Create<string, string, string>(async (apikey, output, approach) =>
            {
                //runV1(apikey, output);
                if ("gremlin".Equals(approach))
                {
                    GremlinData.run();
                }

                if ("memory".Equals(approach))
                {
                    MemoryData.run(apikey, output);
                }

                // Will Not Work
                //await runV3Async();
            });

            // Parse the incoming args and invoke the handler
            return rootCommand.InvokeAsync(args).Result;

        }










        private static Dictionary<string, string> gremlinQueries = new Dictionary<string, string>
        {
            { "Cleanup",        g.V().drop().Build() },
            { "AddVertex 1",    g.AddV("person").property("id", "thomas").property("firstName", "Thomas").property("age", 44).property("pk", "pk").Build() },
            { "AddVertex 2",    g.addV("person").property("id", "mary").property("firstName", "Mary").property("lastName", "Andersen").property("age", 39).property("pk", "pk").Build() },
            { "AddVertex 3",    g.addV("person").property("id", "ben").property("firstName", "Ben").property("lastName", "Miller").property("pk", "pk").Build() },
            { "AddVertex 4",    g.addV("person").property("id", "robin").property("firstName", "Robin").property("lastName", "Wakefield").property("pk", "pk").Build() },
            { "AddVertex 5",    g.addV("person").property("id", "robin2").property("firstName", "Robin").property("lastName", "Wakefield").property("pk", "pk").Build() },
            { "AddVertex 6",    g.addV("person").property("id", "robin2").property("firstName", "Robin").property("lastName", "Wakefield").property("pk", "pk2").Build() },
            { "AddEdge 1",      g.V("thomas").addE("knows").to(g.V("mary").Build()).Build() },
            { "AddEdge 2",      g.V("thomas").addE("knows").to(g.V("ben").Build()).Build() },
            { "AddEdge 3",      g.V("ben").addE("knows").to(g.V("robin").Build()).Build() },
            { "UpdateVertex",   g.V("thomas").property("age", 44).Build() },
            { "CountVertices",  g.V().count().Build() },
            { "Filter Range",   g.V().hasLabel("person").has("age", gt(40)).Build() },
            { "Project",        g.V().hasLabel("person").values("firstName").Build() },
            { "Sort",           g.V().hasLabel("person").order().by("firstName", decr).Build() },
            { "Traverse",       g.V("thomas").@out("knows").hasLabel("person").Build() },
            { "Traverse 2x",    g.V("thomas").@out("knows").hasLabel("person").@out("knows").hasLabel("person").Build() },
            { "Loop",           g.V("thomas").repeat(@out()).until(has("id", "robin")).path().Build() },
            { "DropEdge",       g.V("thomas").outE("knows").where(inV().has("id", "'mary'").Build()).drop().Build() },
            { "CountEdges",     g.E().count().Build() },
            { "DropVertex",     g.V("thomas").drop().Build() },
        };

        private static Dictionary<string, string> gremlinQueriesOG = new Dictionary<string, string>
        {
            { "Cleanup",        "g.V().drop()" },
            { "AddVertex 1",    "g.addV('person').property('id', 'thomas').property('firstName', 'Thomas').property('age', 44).property('pk', 'pk')" },
            { "AddVertex 2",    "g.addV('person').property('id', 'mary').property('firstName', 'Mary').property('lastName', 'Andersen').property('age', 39).property('pk', 'pk')" },
            { "AddVertex 3",    "g.addV('person').property('id', 'ben').property('firstName', 'Ben').property('lastName', 'Miller').property('pk', 'pk')" },
            { "AddVertex 4",    "g.addV('person').property('id', 'robin').property('firstName', 'Robin').property('lastName', 'Wakefield').property('pk', 'pk')" },
            { "AddVertex 5",    "g.addV('person').property('id', 'robin2').property('firstName', 'Robin').property('lastName', 'Wakefield').property('pk', 'pk')" },
            { "AddVertex 6",    "g.addV('person').property('id', 'robin2').property('firstName', 'Robin').property('lastName', 'Wakefield').property('pk', 'pk2')" },
            { "AddEdge 1",      "g.V('thomas').addE('knows').to(g.V('mary'))" },
            { "AddEdge 2",      "g.V('thomas').addE('knows').to(g.V('ben'))" },
            { "AddEdge 3",      "g.V('ben').addE('knows').to(g.V('robin'))" },
            { "UpdateVertex",   "g.V('thomas').property('age', 44)" },
            { "CountVertices",  "g.V().count()" },
            { "Filter Range",   "g.V().hasLabel('person').has('age', gt(40))" },
            { "Project",        "g.V().hasLabel('person').values('firstName')" },
            { "Sort",           "g.V().hasLabel('person').order().by('firstName', decr)" },
            { "Traverse",       "g.V('thomas').out('knows').hasLabel('person')" },
            { "Traverse 2x",    "g.V('thomas').out('knows').hasLabel('person').out('knows').hasLabel('person')" },
            { "Loop",           "g.V('thomas').repeat(out()).until(has('id', 'robin')).path()" },
            { "DropEdge",       "g.V('thomas').outE('knows').where(inV().has('id', 'mary')).drop()" },
            { "CountEdges",     "g.E().count()" },
            { "DropVertex",     "g.V('thomas').drop()" },
        };





    }
}

