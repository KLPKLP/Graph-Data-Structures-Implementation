using System;
using System.Globalization;
using System.IO;

namespace SemA.Core
{
    public class GraphFileLoader
    {
        public Graph<string, Town, Road> LoadFromFile(string filePath)
        {
            ValidateFilePath(filePath);

            Graph<string, Town, Road> graph = new();

            string[] lines = File.ReadAllLines(filePath);

            for (int lineIndex = 0; lineIndex < lines.Length; lineIndex++)
            {
                string originalLine = lines[lineIndex];
                string trimmedLine = originalLine.Trim();

                if (string.IsNullOrWhiteSpace(trimmedLine))
                {
                    continue;
                }

                if (trimmedLine.StartsWith("#"))
                {
                    continue;
                }

                string[] parts = trimmedLine.Split(';');

                if (parts.Length == 0)
                {
                    continue;
                }

                string recordType = parts[0].Trim().ToUpperInvariant();

                switch (recordType)
                {
                    case "TOWN":
                        ParseTownLine(graph, parts, lineIndex);
                        break;

                    case "ROAD":
                        ParseRoadLine(graph, parts, lineIndex);
                        break;

                    default:
                        throw new FormatException(
                            $"Neznámý typ záznamu na řádku {lineIndex + 1}: '{parts[0]}'.");
                }
            }

            return graph;
        }

        private void ValidateFilePath(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("Cesta k souboru nesmí být prázdná.", nameof(filePath));
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Zadaný soubor neexistuje.", filePath);
            }
        }

        private void ParseTownLine(
            Graph<string, Town, Road> graph,
            string[] parts,
            int lineIndex)
        {
            if (parts.Length != 4)
            {
                throw new FormatException(
                    $"Řádek {lineIndex + 1}: záznam TOWN musí mít formát TOWN;nazev;x;y");
            }

            string townName = parts[1].Trim();

            if (string.IsNullOrWhiteSpace(townName))
            {
                throw new FormatException(
                    $"Řádek {lineIndex + 1}: název města nesmí být prázdný.");
            }

            double x = ParseDouble(parts[2], lineIndex, "x");
            double y = ParseDouble(parts[3], lineIndex, "y");

            Town town = new(townName, new Coordinates(x, y));

            bool added = graph.AddVertex(townName, town);

            if (!added)
            {
                throw new InvalidOperationException(
                    $"Řádek {lineIndex + 1}: město '{townName}' už v grafu existuje.");
            }
        }

        private void ParseRoadLine(
            Graph<string, Town, Road> graph,
            string[] parts,
            int lineIndex)
        {
            if (parts.Length != 5)
            {
                throw new FormatException(
                    $"Řádek {lineIndex + 1}: záznam ROAD musí mít formát ROAD;od;do;cas;problematicka");
            }

            string fromTown = parts[1].Trim();
            string toTown = parts[2].Trim();

            if (string.IsNullOrWhiteSpace(fromTown) || string.IsNullOrWhiteSpace(toTown))
            {
                throw new FormatException(
                    $"Řádek {lineIndex + 1}: počáteční i cílové město musí být vyplněné.");
            }

            double time = ParseDouble(parts[3], lineIndex, "cas");
            bool isProblematic = ParseBool(parts[4], lineIndex, "problematicka");

            if (!graph.ContainsVertex(fromTown))
            {
                throw new InvalidOperationException(
                    $"Řádek {lineIndex + 1}: město '{fromTown}' neexistuje. " +
                    "Nejdřív musí být definovány všechny TOWN záznamy.");
            }

            if (!graph.ContainsVertex(toTown))
            {
                throw new InvalidOperationException(
                    $"Řádek {lineIndex + 1}: město '{toTown}' neexistuje. " +
                    "Nejdřív musí být definovány všechny TOWN záznamy.");
            }

            Road road = new(time, isProblematic);

            bool added = graph.AddEdge(fromTown, toTown, road);

            if (!added)
            {
                throw new InvalidOperationException(
                    $"Řádek {lineIndex + 1}: silnice '{fromTown}' - '{toTown}' už v grafu existuje.");
            }
        }

        private double ParseDouble(string value, int lineIndex, string fieldName)
        {
            string trimmedValue = value.Trim();

            bool parsedSuccessfully = double.TryParse(
                trimmedValue,
                NumberStyles.Float | NumberStyles.AllowThousands,
                CultureInfo.InvariantCulture,
                out double parsedValue);

            if (!parsedSuccessfully)
            {
                throw new FormatException(
                    $"Řádek {lineIndex + 1}: hodnota '{trimmedValue}' v poli '{fieldName}' není platné číslo. " +
                    "Použij tečku jako desetinný oddělovač.");
            }

            return parsedValue;
        }

        private bool ParseBool(string value, int lineIndex, string fieldName)
        {
            string trimmedValue = value.Trim().ToLowerInvariant();

            if (trimmedValue == "true")
            {
                return true;
            }

            if (trimmedValue == "false")
            {
                return false;
            }

            throw new FormatException(
                $"Řádek {lineIndex + 1}: hodnota '{value}' v poli '{fieldName}' není platná logická hodnota. " +
                "Použij true nebo false.");
        }
    }
}