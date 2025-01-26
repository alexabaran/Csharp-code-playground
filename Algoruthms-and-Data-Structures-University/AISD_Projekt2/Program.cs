/*Przeprowadzić analizę szybkości działania poznanych algorytmów sortowania. Badania przeprowadź z wykorzystaniem tablic o rozmiarze 100.000 do 1.000.000 z krokiem 100.000 - 10 punktów pomiarowych reprezentujących średnie czasy trwania sortowania (w razie pracy na szybszym/wolniejszym sprzęcie zmniejsz/zwiększ rozmiar o rząd wielkości). Tablice wygeneruj w postaci: losowej, rosnącej, malejącej, stałej, v-kształtnej.

Projekt powinien zawierać:

- kod źródłowy projektu,
- wykresy w formie zależności t = f(n) (t- czas wykonania, n- wielkość tablicy) dla każdej z metod oraz każdej postaci tablicy,
-własne wnioski otrzymane na podstawie analizy otrzymanych rezultatów.

 */

using System.Diagnostics;
using OfficeOpenXml; // NuGet package: EPPlus - do eksportowania excela

class Program
{
    public static void Main()
    {
        // Ustawienie licencji "NonCommercial"
        ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

        // Stworzenie tablicy rozmiarów tablic: 10000 - 100000, krok: 10000  ---- Zmieniany rzad wielkosci ze wzgledu na slaby sprzet
        int[] arraySizes = { 10000, 20000, 30000, 40000, 50000, 60000, 70000, 80000, 90000, 100000 };

        // Inicjalizacja EPPlus
        using var package = new ExcelPackage();
        var worksheet = package.Workbook.Worksheets.Add("AnalysisResults");

        int row = 2; // Wiersz startowy w arkuszu Excel
        worksheet.Cells[1, 1].Value = "Array Type";
        worksheet.Cells[1, 2].Value = "Size";
        worksheet.Cells[1, 3].Value = "SelectionSort (ns)";
        worksheet.Cells[1, 4].Value = "InsertionSort (ns)";
        worksheet.Cells[1, 5].Value = "BubbleSort (ns)";

        // Dla kazdego rozmiaru generujemy nowa tablice dla wszystkich typow.
        foreach (int size in arraySizes)
        {
            foreach (var (arrayType, generateArray) in new (string, Func<int, int[]>)[]
            {
                ("Constant", s => GenerateConstantArray(s, s)),
                ("Increasing", GenerateIncreasingArray),
                ("Decreasing", GenerateDecreasingArray),
                ("V-shaped", GenerateVArray),
                ("Random", GenerateRandomArray)
            })
            {
                // Tworzenie tablicy
                int[] originalArray = generateArray(size);

                // Przygotowanie tablic na wyniki pomiarow
                long[] selectionTimes = new long[12];
                long[] insertionTimes = new long[12];
                long[] bubbleTimes = new long[12];

                for (int i = 0; i < 12; i++)
                {
                    // Kopie tablic do niezależnych pomiarow
                    int[] arrayForSelectionSort = (int[])originalArray.Clone();
                    int[] arrayForInsertionSort = (int[])originalArray.Clone();
                    int[] arrayForBubbleSort = (int[])originalArray.Clone();

                    // Pomiar czasow sortowania
                    selectionTimes[i] = MeasureSelectionSort(arrayForSelectionSort);
                    insertionTimes[i] = MeasureInsertionSort(arrayForInsertionSort);
                    bubbleTimes[i] = MeasureBubbleSort(arrayForBubbleSort);
                }

                // Obliczenie srednich czasow z pominięciem wartosci ekstremalnych
                long avgSelectionTime = CalculateAverageExcludingMinMax(selectionTimes);
                long avgInsertionTime = CalculateAverageExcludingMinMax(insertionTimes);
                long avgBubbleTime = CalculateAverageExcludingMinMax(bubbleTimes);

                // Zapis wynikow do Excela
                SaveResultsToExcel(worksheet, row++, arrayType, size, avgSelectionTime, avgInsertionTime, avgBubbleTime);

                Console.WriteLine($"Array: {arrayType}, Size: {size}, Avg SelectionSort: {avgSelectionTime} ns, Avg InsertionSort: {avgInsertionTime} ns, Avg BubbleSort: {avgBubbleTime} ns");

                FileInfo fi = new FileInfo("AnalysisResults.xlsx");
                package.SaveAs(fi);
                Console.WriteLine("Wyniki zapisane do pliku Excel.");
            }
        }
    }

    // Algorytm sortowania przez wybieranie
    static void SelectionSort(int[] array)
    {
        for (int i = 0; i < array.Length - 1; i++)
        {
            int minIndex = i; // Indeks najmniejszego elementu

            for (int j = i + 1; j < array.Length; j++)
            {
                if (array[j] < array[minIndex])
                {
                    minIndex = j; // Aktualizacja indeksu najmniejszego elementu
                }
            }

            // Zamiana elementów miejscami
            if (minIndex != i)
            {
                (array[i], array[minIndex]) = (array[minIndex], array[i]); //swap
            }
        }
    }


    // Algorytm sortowania przez wstawianie
    static void InsertionSort(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int key = array[i];
            int j = i - 1;

            while (j >= 0 && array[j] > key)
            {
                array[j + 1] = array[j];
                j--;
            }
            array[j + 1] = key;
        }
    }

    // Algorytm sortowania babelkowego
    static void BubbleSort(int[] array)
    {
        int end = array.Length - 1;

        for (int i = 0; i < array.Length; i++)
        {
            for (int j = 0; j < end; j++)
            {
                if (array[j] > array[j + 1])
                {
                    (array[j + 1], array[j]) = (array[j], array[j + 1]); //swap
                }
            }
            end--;
        }
    }
    
    // Pokaz array
    static void ShowArray(int[] array)
    {
        foreach (int item in array) Console.WriteLine(item);
    }

    // Generujemy tablice stala
    public static int[] GenerateConstantArray(int size, int constant)
    {
        int[] array = new int[size];
        for (int i = 0; i < size; i++)
        {
            array[i] = constant;
        }
        return array;
    }

    // Generujemy tablice rosnaca
    public static int[] GenerateIncreasingArray(int size)
    {
        int[] array = new int[size];
        for (int i = 0; i < size; i++)
        {
            array[i] = i;
        }
        return array;
    }

    // Generujemy tablice malejaca
    public static int[] GenerateDecreasingArray(int size)
    {
        int[] array = new int[size];
        for (int i = 0; i < size; i++)
        {
            array[i] = size - i;
        }
        return array;
    }

    // Generujemy tablice V-ksztaltna
    public static int[] GenerateVArray(int size)
    {
        int[] array = new int[size];
        for (int i = 0; i < size/2; i++)
        {
            array[i] = (size / 2) - i;
        }
        for (int i = size / 2; i < size; i++)
        {
            array[i] = i - (size / 2);
        }
        return array;
    }

    // Generujemy tablice losowa
    public static int[] GenerateRandomArray(int size)
    {
        int[] array = new int[size];
        for (int i = 0; i < size; i++)
        {
            array[i] = new Random().Next(size);
        }
        return array;
    }

    // Mierzymy czas wykonania sortowania przez wybieranie w ns
    public static long MeasureSelectionSort(int[] array)
    {
        var stopwatch = Stopwatch.StartNew();
        SelectionSort(array);
        stopwatch.Stop();
        return stopwatch.ElapsedTicks * 100; // system x64 1 tick = 100 ns
    }

    // Mierzymy czas wykonania sortowania przez wstawianie w ns
    public static long MeasureInsertionSort(int[] array)
    {
        var stopwatch = Stopwatch.StartNew();
        InsertionSort(array);
        stopwatch.Stop();
        return stopwatch.ElapsedTicks * 100; // system x64 1 tick = 100 ns
    }

    // Mierzymy czas wykonania sortowania babelkowego w ns
    public static long MeasureBubbleSort(int[] array)
    {
        var stopwatch = Stopwatch.StartNew();
        BubbleSort(array);
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

    // Funkcja zapisująca wyniki do pliku Excel
    static void SaveResultsToExcel(ExcelWorksheet worksheet, int row, string arrayType, int size, long selectionTime, long insertionTime, long bubbleTime)
    {
        worksheet.Cells[row, 1].Value = arrayType; // Typ tablicy
        worksheet.Cells[row, 2].Value = size; // Rozmiar tablicy
        worksheet.Cells[row, 3].Value = selectionTime; // Czas SelectionSort
        worksheet.Cells[row, 4].Value = insertionTime; // Czas InsertionSort
        worksheet.Cells[row, 5].Value = bubbleTime; // Czas BubbleSort
    }

}