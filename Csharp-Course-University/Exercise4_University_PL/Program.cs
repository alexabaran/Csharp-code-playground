//Napisz program, który pozwoli użytkownikowi wpisanie liczb całkowitych do tablicy o wymiarach n:m, wprowadzonych przez użytkownika. Program ma w ostatnim wierszu zapisać sumę elementów z wierszy powyżej, a w ostatniej kolumnie sumę elementów z kolumn po lewej oraz wyświetlić zawartość tabeli.
//Należy zastosować podprogramy i kontrolować poprawność wprowadzanych danych.
//---------------------------------------------------------

//Przykładowe działanie programu:

//Wprowadź liczbę wierszy: 3
//Wprowadź liczbę kolumn: 2
//Wprowadź elementy 1 wiersza, po każdej liczbie wciśnij enter: 

//1

//12
//Wprowadź elementy 2 wiersza, po każdej liczbie wciśnij enter: 

//14

//55
//Wprowadź elementy 3 wiersza, po każdej liczbie wciśnij enter: 

//5

//9

//1   12 | 13
//14  55 | 69
//5    9 | 14
//----------------
//20  76 | 96

Console.WriteLine("*** Funkcja sumujaca wartosci tablicy ***");

try
{
    // Podanie wielkości tabeli
    Console.WriteLine("Proszę podać liczbę wierszy tablicy: ");
    int n = int.Parse(Console.ReadLine()) + 1;

    Console.WriteLine("Proszę podać liczbę kolumn tablicy: ");
    int m = int.Parse(Console.ReadLine()) + 1;

    // Uwzględnienie wyjątku dla złej wartości rozmiaru tabeli 
    if (n <= 0 || m <= 0)
    {
        Console.WriteLine("Błąd: Wartości muszą być liczbą całkowitą dodatnią różną od 0!");
        return;
    }


    // Utworzenie i wypełnienie tablicy
    int[,] tablica = BudujTablice(n, m);

    // Dodanie wiersza i kolumny na sumy
    int[,] rozszerzonaTablica = ObliczSumy(tablica, n, m);

    // Wyświetlenie wynikowej tablicy
    WyswietlTablice(rozszerzonaTablica);

    //int sum1 = 0;
    //int sum2 = 0;
    //for (int i = 0; i < tablica.Length; i++)
    //{
    //    Console.WriteLine($"{tablica[i, 0]}  {tablica[i, 1]} | {tablica[i, 0] + tablica[i, 0]}");
    //    sum1 += tablica[i, 0];
    //    sum2 += tablica[0, i];
    //}
    //Console.WriteLine($"{sum1}  {sum2} | {sum1 + sum2}");

}
catch (Exception ex)
{
    Console.WriteLine("Błąd: wprowadź poprawną liczbę dodatnią cąłkowitą");
}


//Funkcja budowania tablicy nxm:
static int[,] BudujTablice(int n, int m)
{
    int[,] tablica = new int[n, m];

    for (int i = 0; i < (n - 1); i++)
    {
        for (int j = 0; j < (m - 1); j++)
        {
            Console.WriteLine($"Tablicja pozycja (wiersz,kolumna) [{i}, {j}] - proszę Podać wartość: ");
            int wartosc = int.Parse(Console.ReadLine());
            tablica[i, j] = wartosc;
        }
    }

    return tablica;
}

// Funkcja obliczająca sumy i rozszerzająca tablicę
static int[,] ObliczSumy(int[,] tablica, int n, int m)
{
    int[,] rozszerzonaTablica = new int[n + 1, m + 1];

    // Kopiowanie oryginalnych wartości do rozszerzonej tablicy
    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < m; j++)
        {
            rozszerzonaTablica[i, j] = tablica[i, j];
        }
    }

    // Obliczanie sum wierszy
    for (int i = 0; i < n; i++)
    {
        int sumaWiersza = 0;
        for (int j = 0; j < m; j++)
        {
            sumaWiersza += tablica[i, j];
        }
        rozszerzonaTablica[i, m] = sumaWiersza; // Dodanie sumy wiersza do ostatniej kolumny
    }

    // Obliczanie sum kolumn
    for (int j = 0; j < m; j++)
    {
        int sumaKolumny = 0;
        for (int i = 0; i < n; i++)
        {
            sumaKolumny += tablica[i, j];
        }
        rozszerzonaTablica[n, j] = sumaKolumny; // Dodanie sumy kolumny do ostatniego wiersza
    }

    // Obliczanie sumy całej tablicy
    int sumaCalkowita = 0;
    for (int i = 0; i < n; i++)
    {
        sumaCalkowita += rozszerzonaTablica[i, m];
    }
    rozszerzonaTablica[n, m] = sumaCalkowita;

    return rozszerzonaTablica;
}

// Funkcja wyświetlająca tablicę
static void WyswietlTablice(int[,] tablica)
{
    int n = tablica.GetLength(0);
    int m = tablica.GetLength(1);

    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < m; j++)
        {
            Console.Write(tablica[i, j].ToString().PadLeft(5));
        }
        Console.WriteLine();
    }
}