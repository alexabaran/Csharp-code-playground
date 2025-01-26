/*Przeprowadzić analizę średniej oraz maksymalnej złożoności obliczeniowej i czasowej algorytmów wyszukiwania linowego oraz binarnego. Do badań wykorzystaj tablice liczb całkowitych o rozmiarze rzędu 10-100mln elementów.
Podpowiedzi:

-Maksymalną złożoność liczymy dla maksymalnej wartości w tablicy, natomiast średnia jest wartością średnią wyszukiwań(proszę średnią wyznaczyć z 10 losowo wybranych elemetów ab.).

-W przypadku analizy złożoności czasowej zawsze wykonaj 12 pomiarów, następnie odejmij wartości max i min czasu wykonania oraz podziel przez 10.

-Wykorzystaj algorytmy z zadania 3

-Do złożonosci obliczeniowej zlicz operacje porównań

-Wyniki przedstaw w formie wykresów (złożoności średniej/maks) w zal. od wielkości tablicy z krokiem 10 000 000
Wgrany plik powinien zawierać kody wykorzystane do badań, wyniki w formie wykresów zależności oraz proponowane wnioski.
 */

using OfficeOpenXml; // NuGet package: EPPlus - do eksportowania excela
using System.Diagnostics;

class Program
{
    public static void Main()
    {
        // Ustawienie licencji "NonCommercial"
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        // Stworzenie tablicy rozmiarów tablic: 10 - 100 mln, krok: 10 mln
        int[] arraySizes = {10000000, 20000000, 30000000, 40000000, 50000000, 60000000, 70000000, 80000000, 90000000, 100000000};

        // Inicjalizacja EPPlus
        using var package = new ExcelPackage();
        var worksheet = package.Workbook.Worksheets.Add("AnalysisResults");

        // Nazwy kolumn
        worksheet.Cells[1, 1].Value = "Array Size";

        worksheet.Cells[1, 2].Value = "Target Value (Random)";
        worksheet.Cells[1, 3].Value = "Avg Time Linear (Random, ns)";
        worksheet.Cells[1, 4].Value = "Avg Time Binary (Random, ns)";
        worksheet.Cells[1, 5].Value = "Avg Comparisons Linear (Random)";
        worksheet.Cells[1, 6].Value = "Avg Comparisons Binary (Random)";

        worksheet.Cells[1, 7].Value = "Target Value (Max)";
        worksheet.Cells[1, 8].Value = "Avg Time Linear (Max, ns)";
        worksheet.Cells[1, 9].Value = "Avg Time Binary (Max, ns)";
        worksheet.Cells[1, 10].Value = "Avg Comparisons Linear (Max)";
        worksheet.Cells[1, 11].Value = "Avg Comparisons Binary (Max)";


        int row = 2;

        // Dla każdego rozmiaru generujemy nową tablicę
        foreach (int size in arraySizes)
        {
            // Losowanie 10 różnych elementów z tablicy
            int[] array = GenerateArray(size);
            Console.WriteLine($"Rozmiar: {size}");
            Random random = new Random();
            int targetValueRandom = array[random.Next(size)];
            Console.WriteLine($"Wartosc losowa: {targetValueRandom}");
            int targetValueMax = array[array.Length - 1];
            Console.WriteLine($"Wartość maxymalna: {targetValueMax}");

            // Tablice dla czasów i porównań (12 pomiarów)
            long[] timesLinearRandom = new long[12];
            long[] timesBinaryRandom = new long[12];
            long[] timesLinearMax = new long[12];
            long[] timesBinaryMax = new long[12];

            int totalComparisonsLinearRandom = 0;
            int totalComparisonsBinaryRandom = 0;
            int totalComparisonsLinearMax = 0;
            int totalComparisonsBinaryMax = 0;

            // Mierzymy czasy i liczymy porównania
            for (int i = 0; i < 12; i++)
            {
                // Czas wykonania wyszukiwania liniowego w ns i zwraca liczbę porównań dla losowego elementu
                int comparisonsLRandom = 0;
                timesLinearRandom[i] = MeasureTimeLinear(array, targetValueRandom, out comparisonsLRandom);
                totalComparisonsLinearRandom += comparisonsLRandom;

                // Czas wykonania wyszukiwania binarnego w ns i zwraca liczbę porównań dla losowego elementu
                int comparisonsBRandom = 0;
                timesBinaryRandom[i] = MeasureTimeBinary(array, targetValueRandom, out comparisonsBRandom);
                totalComparisonsBinaryRandom += comparisonsBRandom;

                // Czas wykonania wyszukiwania liniowego w ns i zwraca liczbę porównań dla maksymalnej wartości
                int comparisonsLMax = 0;
                timesLinearMax[i] = MeasureTimeLinear(array, targetValueMax, out comparisonsLMax);
                totalComparisonsLinearMax += comparisonsLMax;

                // Czas wykonania wyszukiwania binarnego w ns i zwraca liczbę porównań dla maksymalnej wartości
                int comparisonsBMax = 0;
                timesBinaryMax[i] = MeasureTimeBinary(array, targetValueMax, out comparisonsBMax);
                totalComparisonsBinaryMax += comparisonsBMax;
            }

            // Obliczanie średnich czasów i porównań dla losowego elementu
            long avgTimeLinearRandom = CalculateAverageExcludingMinMax(timesLinearRandom);
            long avgTimeBinaryRandom = CalculateAverageExcludingMinMax(timesBinaryRandom);
            double avgComparisonsLinearRandom = totalComparisonsLinearRandom / 12.0;
            double avgComparisonsBinaryRandom = totalComparisonsBinaryRandom / 12.0;

            // Obliczanie średnich czasów i porównań dla maksymalnej wartości
            long avgTimeLinearMax = CalculateAverageExcludingMinMax(timesLinearMax);
            long avgTimeBinaryMax = CalculateAverageExcludingMinMax(timesBinaryMax);
            double avgComparisonsLinearMax = totalComparisonsLinearMax / 12.0;
            double avgComparisonsBinaryMax = totalComparisonsBinaryMax / 12.0;

            // Zapisywanie wyników do arkusza
            worksheet.Cells[row, 1].Value = size;

            worksheet.Cells[row, 2].Value = targetValueRandom;
            worksheet.Cells[row, 3].Value = avgTimeLinearRandom;
            worksheet.Cells[row, 4].Value = avgTimeBinaryRandom;
            worksheet.Cells[row, 5].Value = avgComparisonsLinearRandom;
            worksheet.Cells[row, 6].Value = avgComparisonsBinaryRandom;

            worksheet.Cells[row, 7].Value = targetValueMax;
            worksheet.Cells[row, 8].Value = avgTimeLinearMax;
            worksheet.Cells[row, 9].Value = avgTimeBinaryMax;
            worksheet.Cells[row, 10].Value = avgComparisonsLinearMax;
            worksheet.Cells[row, 11].Value = avgComparisonsBinaryMax;

            row++;
        }

        FileInfo fi = new FileInfo("AnalysisResults.xlsx");
        package.SaveAs(fi);
        Console.WriteLine("Wyniki zapisane do pliku Excel.");
    }

    // Algorytm wyszukiwania liniowego
    public static int LinearSearch(int[] array, int targetValue, out int comparisonCount)
    {
        comparisonCount = 0;
        for (int i = 0; i < array.Length; i++)
        {
            comparisonCount++;
            if (array[i] == targetValue)
                return i;
        }
        return -1;
    }

    // Algorytm wyszukiwania binarnego
    public static int BinarySearch(int[] array, int targetValue, out int comparisonCount)
    {
        comparisonCount = 0;
        int left = 0, right = array.Length - 1;

        while (left <= right)
        {
            int middle = (left + right) / 2;
            comparisonCount++;

            if (array[middle] == targetValue) return middle;
            else if (array[middle] < targetValue) left = middle + 1;
            else right = middle - 1;
        }

        return -1;
    }

    // Generujemy tablice
    public static int[] GenerateArray(int size)
    {
        int[] array = new int[size];
        for (int i = 0; i < size; i++)
        {
            array[i] = i;
        }
        return array;
    }

    // Mierzymy czas wykonania wyszukiwania liniowego w ns
    public static long MeasureTimeLinear(int[] array, int targetValue, out int comparisons)
    {
        var stopwatch = Stopwatch.StartNew();
        LinearSearch(array, targetValue, out comparisons);
        stopwatch.Stop();
        return stopwatch.ElapsedTicks * 100; // system x64 1 tick = 100 ns
    }

    // Mierzymy czas wykonania wyszukiwania binarnego w ns
    public static long MeasureTimeBinary(int[] array, int targetValue, out int comparisons)
    {
        var stopwatch = Stopwatch.StartNew();
        BinarySearch(array, targetValue, out comparisons);
        stopwatch.Stop();
        return stopwatch.ElapsedTicks * 100; // system x64 1 tick = 100 ns
    }

    // Średnia czasu (12 wyników z odjęciem MIN i MAX podzielone na 10)
    public static long CalculateAverageExcludingMinMax(long[] times)
    {
        Array.Sort(times);
        long sum = times.Skip(1).Take(10).Sum(); // Pomiń pierwszy (min) i weź kolejne 10
        return sum / 10;
    }
}