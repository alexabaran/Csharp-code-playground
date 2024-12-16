using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;
taskD3();

//Podstawy programowania – laboratorium 
//02. Podstawy języka C# 
//Zadania 
//Rozwiązując zadania pamiętaj o tym, żeby działały poprawnie niezależnie od tego co wpisze użytkownik! 
//1. Napisz program sprawdzający, czy podana liczba jest parzysta czy nie. 

static void task01()
{
    try
    {
        Console.WriteLine("*** Sprawdzenie czy liczba jest liczbą parzysta ***");
        Console.WriteLine("Prosze podac liczbę całkowitą: ");
        int a = int.Parse(Console.ReadLine());

        if (a % 2 == 0)
        {
            Console.WriteLine($"Liczba {a} jest liczbą parzystą");
        }
        else
        {
            Console.WriteLine($"Liczba {a} nie jest liczbą parzystą");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}


//2. Napisz program obliczający wartość wyrażenia 𝑎𝑏/𝑎+𝑏, gdzie a i b to parametry podane przez użytkownika. 
static void task02()
{
    try
    {
        Console.WriteLine("*** Program obliczający wartość wyrażenia 𝑎𝑏/𝑎+𝑏 ***");

        Console.WriteLine("Prosze podac wartosc a: ");
        float a = float.Parse(Console.ReadLine());

        Console.WriteLine("Prosze podac wartosc b: ");
        float b = float.Parse(Console.ReadLine());

        float licznik = (a * b);
        float mianownik = (a + b);
        if (mianownik == 0)
        {
            Console.WriteLine("Nie można dzielić przez 0");
        }
        else
        {
            float wyrazenie = licznik / mianownik;
            Console.WriteLine($"wartość wyrażenia: (a*b)/(a+b) wynosi: {wyrazenie}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}


//3. Napisz program, który pobierze od użytkownika 3 liczby i wypisze je w porządku niemalejącym. 

static void task03()
{
    try
    {
        Console.WriteLine("*** Program sortujący 3 liczby ***");

        Console.WriteLine("Prosze podac wartosc a: ");
        float a = float.Parse(Console.ReadLine());

        Console.WriteLine("Prosze podac wartosc b: ");
        float b = float.Parse(Console.ReadLine());

        Console.WriteLine("Prosze podac wartosc c: ");
        float c = float.Parse(Console.ReadLine());

        Console.WriteLine("Nieposortowany zestaw liczb: ");
        float[] array = { a, b, c };
        ShowArray(array);

        Console.WriteLine("Posortowany zestaw liczb: ");
        Sortowanie3Liczb(array);
        ShowArray(array);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

// Funkcja sortujaca - sortowanie babelkowe z algorytmow
static void Sortowanie3Liczb(float[] array)
{
    int end = 2;

    for (int i = 0; i < array.Length - 1; i++)
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
// Funkcja drukujaca array
static void ShowArray(float[] array)
{
    foreach (float item in array) Console.WriteLine(item);
}


//4. Napisz program kalkulator, który wykona podane przez użytkownika działanie arytmetyczne (+, -, *,/) na dwóch liczbach. 

static void task04()
{
    string choice = string.Empty;

    while (choice != "koniec")
    {
        try
        {
            PrintMenu();

            choice = Console.ReadLine();

            if (choice == "koniec") break;

            float answer = 0;
            switch (choice)
            {
                case "1":
                    {
                        (float a, float b) = wartosci();
                        answer = suma(a, b);
                    }
                    break;
                case "2":
                    {
                        (float a, float b) = wartosci();
                        answer = roznica(a, b);
                    }
                    break;
                case "3":
                    {
                        (float a, float b) = wartosci();
                        answer = mnozenie(a, b);
                    }
                    break;
                case "4":
                    {
                        (float a, float b) = wartosci();
                        if (b == 0) throw new DivideByZeroException();
                        answer = dzielenie(a, b);
                    }
                    break;
                default:
                    throw new Exception("Niewlasciwy numer menu.");
            }
            Console.WriteLine($"Wynik to: {answer}");
        }
        catch (DivideByZeroException)
        {
            Console.WriteLine("Nie mozna dzielic przez 0!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            Console.WriteLine("Nacisnij dowolny przycisk by kontynuowac.");
            Console.ReadLine();
        }
    }
}

static (float,float) wartosci()
{
    Console.WriteLine("Prosze podac wartosc a: ");
    float a = float.Parse(Console.ReadLine());

    Console.WriteLine("Prosze podac wartosc b: ");
    float b = float.Parse(Console.ReadLine());

    return (a,b);
}
static float suma(float a, float b)
{
    return a + b;
}
static float roznica(float a, float b)
{
    return a - b;
}
static float mnozenie(float a, float b)
{
    return a * b;
}
static float dzielenie(float a, float b)
{
    return a / b;
}

static void PrintMenu()
{
    Console.Clear();
    Console.WriteLine("*** - Prosty kalkulator - ***");
    Console.WriteLine("Prosze wybrac zadanie ( koniec - zeby wyjsc z programu) ");
    Console.WriteLine("1. Suma");
    Console.WriteLine("2. Odejmowanie");
    Console.WriteLine("3. Mnozenie");
    Console.WriteLine("4. Dzielenie");
}


//5. Usprawnij kalkulator tak, aby działał dopóki użytkownik zamiast działania nie poda komendy "koniec". 

// Rozwiazanie powyzej w zadaniu 4

//6. Napisz program, który dla podanej przez użytkownika liczby z przedziału <1,10> wyliczy silnię. 


static void task06()
{
    try
    {
        Console.WriteLine("*** Silnia liczb 1 do 10 ***");

        Console.WriteLine("Prosze podac wartosc a z przedziału 1 do 10: ");
        int a = int.Parse(Console.ReadLine());

        if (a < 1 || a > 10) 
        { 
            Console.WriteLine("Nie poprawna wartość!"); 
        }
        else
        {
            int result = 1;
            for (int i = 1; i < a; i++)
            {
                result = result * i;
            }
            Console.WriteLine($"Wartosc silni = {result}");
        }
        
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}


//7. Napisz program rysujący prostokąt z gwiazdek o zadanych przez użytkownika wymiarach. 
//Przykład dla a=3, b = 6: 
// ******
// ******
// ******

static void task07()
{
    try
    {

        Console.WriteLine("*** Funkcja wyswietlajaca figure axb ***");

        Console.WriteLine("Prosze podac wysokosc figury a: ");
        int a = int.Parse(Console.ReadLine());

        Console.WriteLine("Prosze podac szerokosc figury b: ");
        int b = int.Parse(Console.ReadLine());

        if (a <= 0 || b <= 0)
        {
            Console.WriteLine("Obie liczby muszą być dodatnie całkowite i różne od 0");
            return;
        }

        RysujFigure(a, b);

    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

static void RysujFigure(int a, int b)
{
    for (int i = 0; i < a; i++)
    {
        Console.WriteLine(new string('*', b));
    }
}

//8.Napisz program rysujący wieżę z gwiazdek o zadanej przez użytkownika wysokości. 
//Przykład dla wysokości 4. 
// * 
// *** 
// ***** 
// ******* 

static void task08()
{
    try
    {
        Console.WriteLine("*** Program rysujacy wieże z gwiazdek ***");

        Console.WriteLine("Prosze podac wysokosc wieży: ");
        int a = int.Parse(Console.ReadLine());

        if (a <= 0)
        {
            Console.WriteLine("Wysokość wieży musi być większa od 0");
            return;
        }

        for (int i = 1; i <= a; i++)
        {
            Console.WriteLine(new string('*', i));
        }
    }
    catch (FormatException)
    {
        Console.WriteLine("Błąd: Wprowadź poprawna liczbe dodatnia całkowitą.");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Błąd: Wprowadź poprawna liczbe dodatnia całkowitą.");
    }
}

//9. Napisz program sprawdzający czy podana liczba jest liczbą pierwszą. 

static void task09()
{
    try
    {
        Console.WriteLine("*** Program sprawdzający czy liczba jest liczbą pierwszą ***");

        Console.WriteLine("Prosze podac liczbę całkowitą większą od 0: ");
        int a = int.Parse(Console.ReadLine());

        if (a <= 0)
        {
            Console.WriteLine("liczba ujemna nie może być liczbą pierwszą");
            return;
        }

        int divider;
        var result = JestLiczbaPierwsza(a, out divider);
        if (!result)
        {
            Console.WriteLine($"Liczba {a} nie jest liczbą pierwszą i jest podzielna przez {divider}");
        }
        else
        {
            Console.WriteLine($"Liczba {a} jest liczba pierwsza");
        }
    }
    catch (FormatException)
    {
        Console.WriteLine("Błąd: Wprowadź poprawna liczbe dodatnia całkowitą.");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Błąd: Wprowadź poprawna liczbe dodatnia całkowitą.");
    }
}

static bool JestLiczbaPierwsza(int number, out int divider)
{
    divider = 0;
    if (number <= 3) return true;
    for (int i = 2; i < number; i++)
    {
        divider = i;
        int b = number % i;
        if(b == 0)
        {
            return false;
        }
    }
    return true;
}

//10. Rozwiń grę w kółko i krzyżyk z poprzedniego zestawu zadań. Teraz, zamiast prosić użytkownika 
//o podanie całego stanu planszy, użytkownik wprowadza po jednym ruchu na raz – tak jak w trakcie 
//normalnej rozgrywki – na zmianę krzyżyki i kółka. Na razie nie musisz sprawdzać, czy ktoś wygrał czy 
//nie – po wypełnieniu przez graczy wszystkich pól po prostu wypisz na ekran komunikat „Koniec gry”. 


//Pamiętaj aby uniemożliwić użytkownikowi wykonanie niedozwolonego ruchu! Poniżej przykładowe 
//uruchomienie. 

//Witaj w programie Kółko i Krzyżyk! 
//   |   | 
//---+---+--- 
//   |   | 
//---+---+--- 
//   |   | 

//Ruch X > 2 

//   | X |  
//---+---+--- 
//   |   | 
//---+---+--- 
//   |   | 

//Ruch O > 1 

// O | X |  
//---+---+--- 
//   |   | 
//---+---+--- 
//   |   | 

//Ruch X > 1 
//Niedozwolony ruch! Spróbuj jeszcze raz. 
//Ruch X > 23 
//Niedozwolony ruch! Spróbuj jeszcze raz. 
//Ruch X > 5 

// O | X |  
//---+---+--- 
//   | X | 
//---+---+--- 
//   |   | 

//Ruch O > 1 
//... 


static void task10()
{
    try
    {
        Console.WriteLine("Witaj w programie Kółko i Krzyżyk!");
        Console.WriteLine("Poniżej numeracja pozycji:");
        Console.WriteLine($" 1 | 2 | 3 ");
        Console.WriteLine("---+---+---");
        Console.WriteLine($" 4 | 5 | 6 ");
        Console.WriteLine("---+---+---");
        Console.WriteLine($" 7 | 8 | 9 ");
        Console.WriteLine("Powodzenia!!!");


        // Plansza do gry, początkowo pusta
        char[] board = { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' };
        char currentPlayer = 'X'; // Pierwszy gracz to 'X'

        int moves = 0; // Licznik ruchów (maksymalnie 9)

        while (moves < 9) // Maksymalnie 9 ruchów (pełna plansza)
        {
            // Wyświetlenie planszy
            WyswietlPlansze(board);

            // Wczytaj ruch od gracza
            Console.Write($"Ruch {currentPlayer} > ");
            string input = Console.ReadLine();

            if (!int.TryParse(input, out int position) || position < 1 || position > 9)
            {
                Console.WriteLine("Niedozwolony ruch! Spróbuj jeszcze raz.");
                continue;
            }

            // Indeks na planszy (0-bazowyindex)
            position -= 1;

            // Sprawdź, czy wybrane pole jest puste
            if (board[position] != ' ')
            {
                Console.WriteLine("Niedozwolony ruch! Spróbuj jeszcze raz.");
                continue;
            }

            // Wykonanie ruchu
            board[position] = currentPlayer;

            // Zmiana gracza
            currentPlayer = currentPlayer == 'X' ? 'O' : 'X';

            // Zwiększ liczbe ruchów
            moves++;
        }

        // Wyświetlenie planszy po zakończeniu gry
        WyswietlPlansze(board);
        Console.WriteLine("Koniec gry.");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

// Funkcja do wyświetlenia planszy
static void WyswietlPlansze(char[] board)
{
    Console.WriteLine($" {board[0]} | {board[1]} | {board[2]} ");
    Console.WriteLine("---+---+---");
    Console.WriteLine($" {board[3]} | {board[4]} | {board[5]} ");
    Console.WriteLine("---+---+---");
    Console.WriteLine($" {board[6]} | {board[7]} | {board[8]} ");
}


//Zadania dodatkowe 

//1. Program obliczający największy wspólny dzielnik. 
static void taskD1()
{
    try
    {
        Console.WriteLine("*** Program obliczający największy wspólny dzielnik ***");
        Console.WriteLine("Prosze podac pierwsza liczbe calkowita a: ");
        int a = int.Parse(Console.ReadLine());

        Console.WriteLine("Prosze podac druga liczbe calkowita b: ");
        int b = int.Parse(Console.ReadLine());

        if (a <= 0 || b <= 0)
        {
            Console.WriteLine("Obie liczby muszą być dodatnie całkowite i różne od 0");
            return;
        }

        int wynik = NWD(a, b);
        Console.WriteLine($"Największy wspólny dzielnik ({a}, {b}) to: {wynik}");

    }
    catch (FormatException)
    {
        Console.WriteLine("Błąd: Wprowadź poprawne liczby całkowite.");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}
// funkcja liczaca najwiekszy wspolny dzielnik
static int NWD(int a, int b)
{
    while (a != b)
    {
        if (a > b)
        {
            a = a - b;
        }
        else
        {
            b = b - a;
        }
    }
    return a;
}

//2. Program obliczający najmniejszą wspólną wielokrotność. 
// jest to iloczyn a i b dzielone przez NWD - użyjemy kodu z powyższego programu

static void taskD2()
{
    try
    {
        Console.WriteLine("*** Program obliczający najmniejszą wspólną wielokrotność. ***");

        Console.WriteLine("Prosze podac pierwsza liczbe calkowita a: ");
        int a = int.Parse(Console.ReadLine());

        Console.WriteLine("Prosze podac druga liczbe calkowita b: ");
        int b = int.Parse(Console.ReadLine());

        if (a <= 0 || b <= 0)
        {
            Console.WriteLine("Obie liczby muszą być dodatnie całkowite i różne od 0");
            return;
        }

        int wynik = NWW(a, b);
        Console.WriteLine($"Najmniejsza wspólna wielokrotnosc ({a}, {b}) to: {wynik}");

    }
    catch (FormatException)
    {
        Console.WriteLine("Błąd: Wprowadź poprawne liczby całkowite.");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}
// funkcja liczaca najmniejsza wspolna wielokrotnosc
static int NWW(int a, int b)
{
    return (a * b) / NWD(a, b);
}

//3. Programy wypisujący trójkąt Pascala o podanej wysokości 

//              1
//             1 1 
//            1 2 1
//           1 3 3 1 

static void taskD3()
{
    try
    {
        Console.WriteLine("*** Programy wypisujący trójkąt Pascala o podanej wysokości ***");

        Console.WriteLine("Prosze podac wysokość trójkąta h: ");
        int h = int.Parse(Console.ReadLine());

        TrojkatPascala(h);

    }
    catch (FormatException)
    {
        Console.WriteLine("Błąd: Wprowadź poprawne liczby całkowite.");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

//Funkcja wypisujaca trojkat
//static void TrojkatPascala(int h)
//{
//    for (int i = 0; i < h; i++)
//    {
//        Console.WriteLine(new string(' ', (h - i)) + string.Concat(Enumerable.Repeat("* ", i + 1)));
//    }
//}
static void TrojkatPascala(int height)
{
    for (int i = 0; i < height; i++)
    {
        int value = 1; // Pierwsza wartość w każdym wierszu to 1

        // Dodajemy odpowiednią ilość spacji dla wyśrodkowania
        for (int j = 0; j < height - i - 1; j++)
        {
            Console.Write(" ");
        }

        // Wartości w wierszu
        for (int j = 0; j <= i; j++)
        {
            Console.Write(value + " ");
            value = value * (i - j) / (j + 1);
        }
        Console.WriteLine();
    }
}

