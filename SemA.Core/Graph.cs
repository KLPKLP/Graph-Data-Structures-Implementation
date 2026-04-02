using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemA.Core
{
    public class Graph<KV, DV, DE>

    {
        private class Vertex
        {
            public KV Key { get; }
            public DV Data { get; private set; }

            //  kolekce Edge ve Vertex

            // public List<Edge> OutgoingEdges { get; } = new();
            public Dictionary<KV, Edge> OutgoingEdges { get; } = new();
            public Vertex(KV key, DV data)
            {
                Key = key;
                Data = data;
            }
        }

        private class Edge
        {
            public Vertex VertexA { get; }
            public Vertex VertexB { get; }
            public DE Data { get; set; }

            public Edge(Vertex vertexA, Vertex vertexB, DE data)
            {
                VertexA = vertexA;
                VertexB = vertexB;
                Data = data;
            }
        }

        private readonly Dictionary<KV, Vertex> vertices = new(); // slovník pro rychlý přístup k vrcholům podle klíče.

        public IEnumerable<KV> VertexKeys => vertices.Keys; // umožní iterovat přes všechny klíče vrcholů

        public bool AddVertex(KV key, DV data)
        {
            if (vertices.ContainsKey(key)) return false;
            vertices[key] = new Vertex(key, data);
            return true;
        }


        public bool TryGetVertexData(KV key, out DV data)
        {
            if (vertices.TryGetValue(key, out var vertex))
            {
                data = vertex.Data;
                return true;
            }

            data = default;
            return false;
        }

        public bool ContainsVertex(KV key) => vertices.ContainsKey(key);

        public bool AddEdge(KV a, KV b, DE data)
        {
            if (EqualityComparer<KV>.Default.Equals(a, b))
                throw new ArgumentException("Hrana nesmí vést z vrcholu do sebe sama.");



            if (!vertices.TryGetValue(a, out var va) || !vertices.TryGetValue(b, out var vb))
                throw new KeyNotFoundException("Jeden nebo oba vrcholy neexistují.");

            if (ContainsEdge(a, b))
                return false;

            // neorientovaný graf => 2 orientované hrany
            va.OutgoingEdges.Add(b, new Edge(va, vb, data));
            vb.OutgoingEdges.Add(a, new Edge(vb, va, data));
            return true;
        }

        public bool RemoveEdge(KV a, KV b)
        {
            if (!vertices.TryGetValue(a, out var va) || !vertices.TryGetValue(b, out var vb))
                return false;

            bool removedFromA = va.OutgoingEdges.Remove(b);
            bool removedFromB = vb.OutgoingEdges.Remove(a);

            return removedFromA || removedFromB;
        }




        public bool TryGetEdgeData(KV fromKey, KV toKey, out DE data)
        {
            data = default;

            if (!vertices.TryGetValue(fromKey, out Vertex fromVertex))
                return false;

            if (!fromVertex.OutgoingEdges.TryGetValue(toKey, out Edge edge))
                return false;

            data = edge.Data;
            return true;
        }

        public bool ContainsEdge(KV a, KV b)
        {
            if (!vertices.TryGetValue(a, out var va) || !vertices.ContainsKey(b))
                return false;

            return va.OutgoingEdges.ContainsKey(b);
        }

        public IEnumerable<(KV NeighborKey, DE EdgeData)> GetNeighbors(KV key)
        {
            if (!vertices.TryGetValue(key, out var v))
                throw new KeyNotFoundException("Vrchol neexistuje.");

            foreach (Edge edge in v.OutgoingEdges.Values)
                yield return (edge.VertexB.Key, edge.Data);
        }
    }
}
