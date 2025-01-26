/**************** Wersja  dla  chętnych - dodatkowo 10 pkt ******************
Zaimplementuj algorytm Dijkstry dla dowolnego grafu ważonego, skierowanego. Program powinien po wprowadzeniu grafu oraz dwóch wybranych wierzchołków zwrócić najkrótszą ścieżkę oraz jej koszt.*/

using System;
using System.Collections.Generic;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        // Macierz sąsiedztwa wprowadzona w kodzie
        int[,] adjacencyMatrix = {
            { 0, 1, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 5, 0, 0, 0, 1, 6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 4, 0, 0, 1, 0, 0, 7, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 6, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 9, 6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 6, 0, 0, 0, 0, 0, 0, 6, 11, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 4, 0, 0, 0, 0, 0, 5, 11, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 4, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 9, 0, 0, 0, 0, 0, 0, 0, 9, 9, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 13, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 13, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 9, 0, 0, 1, 0, 0, 0, 0, 3, 11, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 13, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 9, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 9, 0, 0, 0, 0, 9, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 3, 9, 9 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 9, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 9, 0, 0, 0, 0 }
        };

        int verticesCount = adjacencyMatrix.GetLength(0);

        // Ścieżka do obrazu grafu
        string graphImagePath = "GraphImage.png";

        // Sprawdzenie, czy obraz istnieje
        if (File.Exists(graphImagePath))
        {
            Console.WriteLine("Wyświetlanie grafu w domyślnej przeglądarce obrazów...");
            Process.Start(new ProcessStartInfo
            {
                FileName = graphImagePath,
                UseShellExecute = true
            });
        }
        else
        {
            Console.WriteLine("Nie znaleziono obrazu grafu w ścieżce: " + graphImagePath);
        }


        // Tutaj możesz kontynuować algorytm Dijkstry
        Console.WriteLine("Kontynuuję algorytm Dijkstry...");

        Console.WriteLine("Podaj wierzchołek początkowy (od 1 do " + (verticesCount) + "):");
        int startVertex = int.Parse(Console.ReadLine())-1;
        if (startVertex < 0 || startVertex >= verticesCount)
        {
            Console.WriteLine("Wprowadzono nieprawidłowy wierzchołek początkowy.");
            return;
        }

        Console.WriteLine("Podaj wierzchołek końcowy (od 1 do " + (verticesCount) + "):");
        int endVertex = int.Parse(Console.ReadLine())-1;
        if (endVertex < 0 || endVertex >= verticesCount)
        {
            Console.WriteLine("Wprowadzono nieprawidłowy wierzchołek końcowy.");
            return;
        }

        var result = Dijkstra(adjacencyMatrix, verticesCount, startVertex, endVertex);

        // Zwiększanie indeksów ścieżki o 1, aby pasowały do numeracji użytkownika
        var adjustedPath = result.Item1.ConvertAll(v => v + 1);

        Console.WriteLine($"Najkrótsza ścieżka: {string.Join(" -> ", adjustedPath)}");
        Console.WriteLine($"Koszt ścieżki: {result.Item2}");
    }

    static Tuple<List<int>, int> Dijkstra(int[,] graph, int verticesCount, int startVertex, int endVertex)
    {
        int[] distances = new int[verticesCount];
        bool[] shortestPathTreeSet = new bool[verticesCount];
        int[] parents = new int[verticesCount];

        for (int i = 0; i < verticesCount; i++)
        {
            distances[i] = int.MaxValue;
            shortestPathTreeSet[i] = false;
            parents[i] = -1;
        }

        distances[startVertex] = 0;

        for (int count = 0; count < verticesCount - 1; count++)
        {
            int u = MinDistance(distances, shortestPathTreeSet, verticesCount);
            shortestPathTreeSet[u] = true;

            for (int v = 0; v < verticesCount; v++)
            {
                if (!shortestPathTreeSet[v] &&
                    graph[u, v] != 0 &&
                    distances[u] != int.MaxValue &&
                    distances[u] + graph[u, v] < distances[v])
                {
                    distances[v] = distances[u] + graph[u, v];
                    parents[v] = u;
                }
            }
        }

        return Tuple.Create(GetPath(parents, endVertex), distances[endVertex]);
    }

    static int MinDistance(int[] distances, bool[] shortestPathTreeSet, int verticesCount)
    {
        int min = int.MaxValue, minIndex = -1;

        for (int v = 0; v < verticesCount; v++)
        {
            if (!shortestPathTreeSet[v] && distances[v] <= min)
            {
                min = distances[v];
                minIndex = v;
            }
        }

        return minIndex;
    }

    static List<int> GetPath(int[] parents, int targetVertex)
    {
        List<int> path = new List<int>();

        for (int vertex = targetVertex; vertex != -1; vertex = parents[vertex])
        {
            path.Insert(0, vertex);
        }

        return path;
    }
}