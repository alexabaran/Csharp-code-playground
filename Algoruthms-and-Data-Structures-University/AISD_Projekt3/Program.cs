/*Zaimplementuj algorytm DFS  i BFS (w zależności od wyboru użytkownika - dwie metody muszą być zaimplementowane!). Program powinien zwracać kolejno odwiedzane wierzchołki oraz średni czas działania (z uwzg. 12 pomiarów).
Projekt powinien zawierać:
- kody źródłowe algorytmów.
- porównanie wydajności obu algorytmów dla 3 wybranych grafów.*/


using System.Diagnostics;
using OfficeOpenXml; // NuGet package: EPPlus - do eksportowania excela

class Program
{
    public static void Main()
    {
        // Ustawienie licencji "NonCommercial"
        ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

        // Definicja grafów - macierze sąsiedztwa
        int[,] graphA = {
            { 0, 1, 0, 1, 0, 1, 0, 0 },
            { 1, 0, 1, 1, 1, 0, 0, 0 },
            { 0, 1, 0, 0, 1, 0, 0, 1 },
            { 1, 1, 0, 0, 0, 1, 1, 0 },
            { 0, 1, 1, 0, 0, 0, 1, 1 },
            { 1, 0, 0, 1, 0, 0, 1, 0 },
            { 0, 0, 0, 1, 1, 1, 0, 1 },
            { 0, 0, 1, 0, 1, 0, 1, 0 }
        };

        int[,] graphB = {
            { 0, 1, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
            { 1, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0 },
            { 1, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0 },
            { 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
            { 1, 1, 1, 1, 1, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 1, 0, 1, 1, 1, 1, 0 },
            { 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 1 },
            { 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0 },
            { 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 1 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0 }
        };

        int[,] graphC = {
            { 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
            { 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1 },
            { 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 1 },
            { 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 0, 0, 0, 0, 1, 1, 0, 1, 0, 1, 0 }
        };

        // Tablica grafów
        var graphs = new (int[,] Graph, string Name)[]
        {
            (graphA, "GraphA"),
            (graphB, "GraphB"),
            (graphC, "GraphC")
        };

        // Wyniki będą eksportowane do Excela
        using var package = new ExcelPackage();
        var worksheet = package.Workbook.Worksheets.Add("Wyniki");
        worksheet.Cells[1, 1].Value = "Graf";
        worksheet.Cells[1, 2].Value = "Algorytm";
        worksheet.Cells[1, 3].Value = "Średni czas [ns]";
        worksheet.Cells[1, 4].Value = "Kroki";
        worksheet.Cells[1, 5].Value = "Kolejność odwiedzania";

        int row = 2;

        foreach (var (graph, graphName) in graphs)
        {
            // Testowanie DFS
            var (dfsTimes, dfsSteps, dfsOrder) = MeasureAlgorithm(graph, DFS);
            long avgDfsTime = CalculateAverageExcludingMinMax(dfsTimes);
            worksheet.Cells[row, 1].Value = graphName;
            worksheet.Cells[row, 2].Value = "DFS";
            worksheet.Cells[row, 3].Value = avgDfsTime;
            worksheet.Cells[row, 4].Value = dfsSteps;
            worksheet.Cells[row, 5].Value = string.Join(", ", dfsOrder);
            row++;

            // Testowanie BFS
            var (bfsTimes, bfsSteps, bfsOrder) = MeasureAlgorithm(graph, BFS);
            long avgBfsTime = CalculateAverageExcludingMinMax(bfsTimes);
            worksheet.Cells[row, 1].Value = graphName;
            worksheet.Cells[row, 2].Value = "BFS";
            worksheet.Cells[row, 3].Value = avgBfsTime;
            worksheet.Cells[row, 4].Value = bfsSteps;
            worksheet.Cells[row, 5].Value = string.Join(", ", bfsOrder);
            row++;
        }

        // Zapis wyników do pliku Excel
        FileInfo fi = new FileInfo("Wyniki.xlsx");
        package.SaveAs(fi);
        Console.WriteLine("Wyniki zapisane do pliku Excel.");
    }

    public static (long[], int, List<int>) MeasureAlgorithm(int[,] graph, Func<int[,], int, (int, List<int>)> algorithm)
    {
        var times = new long[12];
        int totalSteps = 0;
        List<int> lastOrder = new List<int>();

        for (int i = 0; i < 12; i++)
        {
            var stopwatch = Stopwatch.StartNew();
            var (steps, order) = algorithm(graph, 1); // Zaczynamy od węzła 1
            stopwatch.Stop();
            times[i] = stopwatch.ElapsedTicks * 100; // Przeliczanie na ns

            if (i == 11) // Zapisujemy dane tylko dla ostatniej iteracji
            {
                totalSteps = steps;
                lastOrder = order;
            }
        }
        return (times, totalSteps, lastOrder);
    }

    public static long CalculateAverageExcludingMinMax(long[] times)
    {
        Array.Sort(times);
        return times.Skip(1).Take(10).Sum() / 10; // Pominięcie najwolniejszego i najszybszego
    }

    public static (int, List<int>) DFS(int[,] graph, int start)
    {
        bool[] visited = new bool[graph.GetLength(0)];
        Stack<int> stack = new Stack<int>();
        List<int> order = new List<int>();
        int steps = 0;

        stack.Push(start - 1); // Przesunięcie indeksów o 1

        while (stack.Count > 0)
        {
            int node = stack.Pop();
            if (!visited[node])
            {
                visited[node] = true;
                order.Add(node + 1); // Przesunięcie indeksów o 1
                steps++;
                for (int i = graph.GetLength(1) - 1; i >= 0; i--) // Odwiedzanie w odwrotnej kolejności
                {
                    if (graph[node, i] == 1 && !visited[i])
                        stack.Push(i);
                }
            }
        }

        return (steps, order);
    }

    public static (int, List<int>) BFS(int[,] graph, int start)
    {
        bool[] visited = new bool[graph.GetLength(0)];
        Queue<int> queue = new Queue<int>();
        List<int> order = new List<int>();
        int steps = 0;

        queue.Enqueue(start - 1); // Przesunięcie indeksów o 1

        while (queue.Count > 0)
        {
            int node = queue.Dequeue();
            if (!visited[node])
            {
                visited[node] = true;
                order.Add(node + 1); // Przesunięcie indeksów o 1
                steps++;
                for (int i = 0; i < graph.GetLength(1); i++)
                {
                    if (graph[node, i] == 1 && !visited[i])
                        queue.Enqueue(i);
                }
            }
        }

        return (steps, order);
    }
}
