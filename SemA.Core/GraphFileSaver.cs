using System.Globalization;
using System.Text;

namespace SemA.Core
{
    public class GraphFileSaver
    {
        public void SaveToFile(Graph<string, Town, Road> graph, string filePath)
        {
            if (graph is null)
            {
                throw new ArgumentNullException(nameof(graph));
            }

            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("Cesta k souboru nesmí být prázdná.", nameof(filePath));
            }

            StringBuilder fileContentBuilder = new();

            fileContentBuilder.AppendLine("# Města");

            foreach (string townKey in graph.VertexKeys)
            {
                if (!graph.TryGetVertexData(townKey, out Town town))
                {
                    continue;
                }

                fileContentBuilder.AppendLine(
                    $"TOWN;{townKey};" +
                    $"{town.Position.X.ToString(CultureInfo.InvariantCulture)};" +
                    $"{town.Position.Y.ToString(CultureInfo.InvariantCulture)}");
            }

            fileContentBuilder.AppendLine();
            fileContentBuilder.AppendLine("# Silnice");

            foreach (string fromTownKey in graph.VertexKeys)
            {
                foreach ((string toTownKey, Road road) in graph.GetNeighbors(fromTownKey))
                {
                    if (string.Compare(fromTownKey, toTownKey, StringComparison.Ordinal) > 0)
                    {
                        continue;
                    }

                    fileContentBuilder.AppendLine(
                        $"ROAD;{fromTownKey};{toTownKey};" +
                        $"{road.Time.ToString(CultureInfo.InvariantCulture)};" +
                        $"{road.IsProblematic.ToString().ToLowerInvariant()}");
                }
            }

            File.WriteAllText(filePath, fileContentBuilder.ToString(), Encoding.UTF8);
        }
    }
}