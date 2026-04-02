using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemA.Core
{
    public class AStarPathFinder<KV, DV, DE>
        where DV : IHasPosition
        where DE : IHasCost
    {
        public List<KV> FindPath(Graph<KV, DV, DE> graph, KV start, KV goal)
        {
            ValidateGraphAndRequiredVertices(graph, start, goal);

            LoadStartAndGoalVertexData(graph, start, goal, out DV startVertexData, out DV goalVertexData);



            PriorityQueue<KV, double> verticesToExplore = new();
            Dictionary<KV, KV> cameFrom = new();
            Dictionary<KV, double> costFromStart = new();

            double startPriority = CalculateHeuristic(startVertexData, goalVertexData);


            costFromStart[start] = 0; // náklady z startu do startu jsou 0

            Dictionary<KV, double> estimatedTotalCostByVertex = new(); // odhadované celkové náklady z startu přes daný vrchol do cíle (g + h)
            estimatedTotalCostByVertex[start] = startPriority; // pro startovní vrchol jsou odhadované celkové náklady stejné jako heuristická hodnota, protože g = 0

            verticesToExplore.Enqueue(start, startPriority); // přidáme startovní vrchol do fronty s jeho prioritou

            while (verticesToExplore.Count > 0)
            {
                // získáme vrchol s nejnižší prioritou (nejmenší odhadované náklady do cíle)

                bool dequeueSucceeded = verticesToExplore.TryDequeue(out KV currentVertexKey, out double currentEstimatedTotalCost);

                if (!dequeueSucceeded)
                {
                    break;
                }

                if (estimatedTotalCostByVertex.TryGetValue(currentVertexKey, out double bestKnownEstimatedTotalCost) &&
                    currentEstimatedTotalCost > bestKnownEstimatedTotalCost)
                {
                    continue;
                }

                if (EqualityComparer<KV>.Default.Equals(currentVertexKey, goal)) // pokud jsme dosáhli cíle, můžeme složit a vrátit cestu
                {
                    return ReconstructPath(cameFrom, currentVertexKey);
                }

                foreach ((KV neighborKey, DE edgeData) in graph.GetNeighbors(currentVertexKey))
                {
                    double newCostToNeighbor = costFromStart[currentVertexKey] + edgeData.Cost;

                    if (!costFromStart.TryGetValue(neighborKey, out double currentKnownCostToNeighbor) ||
                        newCostToNeighbor < currentKnownCostToNeighbor)
                    {
                        costFromStart[neighborKey] = newCostToNeighbor;
                        cameFrom[neighborKey] = currentVertexKey;

                        if (!graph.TryGetVertexData(neighborKey, out DV neighborVertexData))
                        {
                            throw new InvalidOperationException("Nepodařilo se načíst data sousedního vrcholu.");
                        }

                        double estimatedTotalCost =  newCostToNeighbor + CalculateHeuristic(neighborVertexData, goalVertexData);
                        estimatedTotalCostByVertex[neighborKey] = estimatedTotalCost;

                        verticesToExplore.Enqueue(neighborKey, estimatedTotalCost);
                    }
                }

            }


            return new List<KV>();
        }

        private void ValidateGraphAndRequiredVertices(Graph<KV, DV, DE> graph, KV start, KV goal)
        {
            if (graph == null)
            {
                throw new ArgumentNullException(nameof(graph));
            }

            if (!graph.ContainsVertex(start))
            {
                throw new ArgumentException("Startovní vrchol v grafu neexistuje.", nameof(start));
            }

            if (!graph.ContainsVertex(goal))
            {
                throw new ArgumentException("Cílový vrchol v grafu neexistuje.", nameof(goal));
            }
        }

        private void LoadStartAndGoalVertexData(
            Graph<KV, DV, DE> graph,
            KV start,
            KV goal,
            out DV startVertexData,
            out DV goalVertexData)
        {
            if (!graph.TryGetVertexData(start, out startVertexData))
            {
                throw new InvalidOperationException("Nepodařilo se načíst data startovního vrcholu.");
            }

            if (!graph.TryGetVertexData(goal, out goalVertexData))
            {
                throw new InvalidOperationException("Nepodařilo se načíst data cílového vrcholu.");
            }
        }

        private double CalculateHeuristic(DV currentVertexData, DV goalVertexData)
        {
            double xDifference = currentVertexData.Position.X - goalVertexData.Position.X;
            double yDifference = currentVertexData.Position.Y - goalVertexData.Position.Y;

            double straightLineDistance = Math.Sqrt(xDifference * xDifference + yDifference * yDifference);

            return straightLineDistance;
        }

        private List<KV> ReconstructPath(Dictionary<KV, KV> cameFrom, KV currentVertexKey)
        {
            List<KV> path = new();
            path.Add(currentVertexKey);

            while (cameFrom.ContainsKey(currentVertexKey))
            {
                currentVertexKey = cameFrom[currentVertexKey];
                path.Add(currentVertexKey);
            }

            path.Reverse();
            return path;
        }
    }
}
