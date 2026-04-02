using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SemA.Core
{
    public class AlternativeRouteFinder<KV, DV, DE>
        where DV : IHasPosition
            where DE : IHasCost
    {
        private readonly AStarPathFinder<KV, DV, DE> aStarPathFinder = new();

        //private class RouteProcessingState
        //{
        //    public List<KV> Route { get; }
        //    public List<(KV FromVertexKey, KV ToVertexKey)> BlockedEdges { get; }

        //    public RouteProcessingState(List<KV> route, List<(KV FromVertexKey, KV ToVertexKey)> blockedEdges)
        //    {
        //        Route = route;
        //        BlockedEdges = blockedEdges;
        //    }

        //}





        public List<List<KV>> FindAlternativeRoutes(Graph<KV, DV, DE> graph, KV start, KV goal, Func<DE, bool> isProblematicEdge)
        {
            List<List<KV>> alternativeRoutes = new();
            List<List<Tuple<KV, KV, DE>>> forbiddenEdges = new(); // seznam seznamů problematických hran, které jsme již zkoušeli blokovat
            List<KV> shortestRoute = aStarPathFinder.FindPath(graph, start, goal);
            forbiddenEdges.Add(new()); // přidávám prázdný seznam - první položka odpovídá tomu, že zatím neblokujeme žádné hrany
            if (shortestRoute.Count == 0)
            {
                return alternativeRoutes; // není žádná cesta, takže nejsou ani alternativní cesty
            }

            List<List<KV>> routesToProcess = new();




            routesToProcess.Add(shortestRoute); // začínáme s nejkratší cestou, ze které budeme hledat alternativy

            while (routesToProcess.Count > 0)
            {
                List<KV> currentRoute = routesToProcess[0]; // vezmeme první cestu k zpracování
                routesToProcess.RemoveAt(0); // odstraníme ji ze seznamu cest k zpracování

                var currentForbiddenEdges = forbiddenEdges[0]; // vezmeme seznam blokovaných hran, které odpovídají této cestě
                forbiddenEdges.RemoveAt(0); // a také odstraníme odpovídající seznam blokovaných hran, protože se budeme snažit najít alternativy založené na této cestě

                for (int i = 0; i < currentRoute.Count - 1; i++) //-1 protože pro poslední vrchol není hrana vedoucí k dalšímu vrcholu (beru vždy dvojici vrcholů, takže potřebuji jít jen do předposledního vrcholu)
                {
                    KV fromVertexKey = currentRoute[i]; // aktuální vrchol
                    KV toVertexKey = currentRoute[i + 1]; // následující vrchol

                    if (!graph.TryGetEdgeData(fromVertexKey, toVertexKey, out DE edgeData))
                    {
                        throw new InvalidOperationException($"Nepodařilo se načíst data hrany mezi vrcholy {fromVertexKey} a {toVertexKey}.");
                    }

                    if (!isProblematicEdge(edgeData))
                    {
                        continue; // tato hrana není problematická, takže ji nemusíme řešit
                    }

                    graph.RemoveEdge(fromVertexKey, toVertexKey); // odstraníme problematickou hranu z grafu
                    foreach (var blockedEdge in currentForbiddenEdges)
                    {
                        graph.RemoveEdge(blockedEdge.Item1, blockedEdge.Item2); // odstraníme i všechny hrany, které jsme již dříve blokovali pro tuto cestu, abychom hledali skutečně nové alternativy
                    }

                    List<KV> alternativeRoute = aStarPathFinder.FindPath(graph, start, goal);
                    if (alternativeRoute.Count > 0 && !ContainsRoute(alternativeRoutes, alternativeRoute))
                    {
                        alternativeRoutes.Add(alternativeRoute); // pokud existuje alternativa, přidáme ji do seznamu alternativních cest

                        routesToProcess.Add(alternativeRoute); // a také ji přidáme do seznamu cest k zpracování, abychom mohli hledat další alternativy založené na této nové cestě
                        forbiddenEdges.Add(currentForbiddenEdges.Concat(new List<Tuple<KV, KV, DE>> { Tuple.Create(fromVertexKey, toVertexKey, edgeData) }).ToList()); // přidáme nový seznam blokovaných hran pro tuto novou cestu, který bude obsahovat všechny blokované hrany z původní cesty plus tu nově blokovanou hranu
                    }

                    graph.AddEdge(fromVertexKey, toVertexKey, edgeData); // znovu přidáme hranu zpět do grafu
                    // také znovu přidáme všechny hrany, které jsme dříve blokovali pro tuto cestu, abychom mohli hledat další alternativy založené na původní cestě
                    foreach (var blockedEdge in currentForbiddenEdges)
                    {
                       
                        graph.AddEdge(blockedEdge.Item1, blockedEdge.Item2, blockedEdge.Item3);

                    }
                }

            }


            return alternativeRoutes;

        }

        private bool ContainsRoute(List<List<KV>> existingRoutes, List<KV> candidateRoute)
        {
            foreach (List<KV> existingRoute in existingRoutes)
            {
                if (existingRoute.Count != candidateRoute.Count)
                {
                    continue; // pokud mají různé délky, nemohou být stejné
                }

                bool routesAreEqual = true;

                for (int i = 0; i < existingRoute.Count; i++)
                {
                    if (!EqualityComparer<KV>.Default.Equals(existingRoute[i], candidateRoute[i]))
                    {
                        routesAreEqual = false;
                        break;
                    }
                }

                if (routesAreEqual)
                {
                    return true; // našli jsme shodnou cestu
                }

            }
            return false;
        }
    }
}
