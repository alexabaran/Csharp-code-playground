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

        // Definicja grafów - macierze sasiedztwa
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

        int row = 2;

        foreach (var (graph, graphName) in graphs)
        {
            // Testowanie DFS
            var dfsTimes = MeasureAlgorithm(graph, DFS);
            long avgDfsTime = CalculateAverageExcludingMinMax(dfsTimes);
            worksheet.Cells[row, 1].Value = graphName;
            worksheet.Cells[row, 2].Value = "DFS";
            worksheet.Cells[row, 3].Value = avgDfsTime;
            row++;

            // Testowanie BFS
            var bfsTimes = MeasureAlgorithm(graph, BFS);
            long avgBfsTime = CalculateAverageExcludingMinMax(bfsTimes);
            worksheet.Cells[row, 1].Value = graphName;
            worksheet.Cells[row, 2].Value = "BFS";
            worksheet.Cells[row, 3].Value = avgBfsTime;
            row++;
        }

        // Zapis wyników do pliku Excel
        FileInfo fi = new FileInfo("Wyniki.xlsx");
        package.SaveAs(fi);
        Console.WriteLine("Wyniki zapisane do pliku Excel.");
    }

    // Pomiar czasu dla algorytmu
    public static long[] MeasureAlgorithm(int[,] graph, Action<int[,], int> algorithm)
    {
        var times = new long[12];
        for (int i = 0; i < 12; i++)
        {
            var stopwatch = Stopwatch.StartNew();
            algorithm(graph, 0);
            stopwatch.Stop();
            times[i] = stopwatch.ElapsedTicks * 100; // Przeliczanie na ns
        }
        return times;
    }

    // Obliczanie średniej bez min i max
    public static long CalculateAverageExcludingMinMax(long[] times)
    {
        Array.Sort(times);
        return times.Skip(1).Take(10).Sum() / 10; // Pominięcie najwolniejszego i najszybszego
    }

    // Algorytm DFS
    public static void DFS(int[,] graph, int start)
    {
        bool[] visited = new bool[graph.GetLength(0)];
        Stack<int> stack = new Stack<int>();
        stack.Push(start);

        while (stack.Count > 0)
        {
            int node = stack.Pop();
            if (!visited[node])
            {
                visited[node] = true;
                for (int i = 0; i < graph.GetLength(1); i++)
                {
                    if (graph[node, i] == 1 && !visited[i])
                        stack.Push(i);
                }
            }
        }
    }

    // Algorytm BFS
    public static void BFS(int[,] graph, int start)
    {
        bool[] visited = new bool[graph.GetLength(0)];
        Queue<int> queue = new Queue<int>();
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            int node = queue.Dequeue();
            if (!visited[node])
            {
                visited[node] = true;
                for (int i = 0; i < graph.GetLength(1); i++)
                {
                    if (graph[node, i] == 1 && !visited[i])
                        queue.Enqueue(i);
                }
            }
        }
    }
}