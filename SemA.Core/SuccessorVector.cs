using System;
using System.Collections.Generic;

namespace SemA.Core
{
    public class SuccessorVector<TKey>
        where TKey : notnull
    {
        private readonly List<TKey> orderedSourceVertices = new();
        private readonly Dictionary<TKey, List<TKey>> successorsByVertex = new();

        public IReadOnlyList<TKey> OrderedSourceVertices => orderedSourceVertices;

        public IReadOnlyList<TKey> GetSuccessors(TKey sourceVertexKey)
        {
            if (!successorsByVertex.TryGetValue(sourceVertexKey, out List<TKey>? successorList))
            {
                return Array.Empty<TKey>();
            }

            return successorList;
        }

        public void AddRoute(IReadOnlyList<TKey> route)
        {
            if (route is null)
            {
                throw new ArgumentNullException(nameof(route));
            }

            if (route.Count < 2)
            {
                return;
            }

            for (int routeIndex = 0; routeIndex < route.Count - 1; routeIndex++)
            {
                TKey sourceVertexKey = route[routeIndex];
                TKey successorVertexKey = route[routeIndex + 1];

                if (!successorsByVertex.TryGetValue(sourceVertexKey, out List<TKey>? successorList))
                {
                    successorList = new List<TKey>();
                    successorsByVertex[sourceVertexKey] = successorList;
                    orderedSourceVertices.Add(sourceVertexKey);
                }

                bool successorAlreadyExists = successorList.Contains(successorVertexKey);
                if (!successorAlreadyExists)
                {
                    successorList.Add(successorVertexKey);
                }
            }
        }

        public string FormatSuccessors(TKey sourceVertexKey, string separator = " | ")
        {
            IReadOnlyList<TKey> successors = GetSuccessors(sourceVertexKey);
            return string.Join(separator, successors);
        }

        public void Clear()
        {
            orderedSourceVertices.Clear();
            successorsByVertex.Clear();
        }
    }
}