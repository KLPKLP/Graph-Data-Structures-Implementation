using System.Collections.Generic;

namespace SemA.Core
{
    public static class SuccessorVectorBuilder
    {
        public static SuccessorVector<TKey> Build<TKey>(
            IReadOnlyList<TKey> shortestRoute,
            IEnumerable<IReadOnlyList<TKey>> alternativeRoutes)
            where TKey : notnull
        {
            SuccessorVector<TKey> successorVector = new();

            successorVector.AddRoute(shortestRoute);

            foreach (IReadOnlyList<TKey> alternativeRoute in alternativeRoutes)
            {
                successorVector.AddRoute(alternativeRoute);
            }

            return successorVector;
        }
    }
}