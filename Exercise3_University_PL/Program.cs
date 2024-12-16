//1.Napisz funkcje, która wyświetli na ekranie figurę o wymiarach a i b podanych przez użytkownika. Figura ma składać się  naprzemiennie z linii zbudowanych z dwóch znaków podanych przez użytkownika.

//Należy sprawdzać poprawność wprowadzonych liczb.
//Przykładowe działanie programu:
//Podaj wysokość a = 4
//Podaj szerokość b= 6
//Podaj pierwszy znak = &
//Podaj drugi znak = *

//&&&&&&
//******
//&&&&&&
//******

//2.Zdefiniuj funkcję, która będzie rysowała opisaną figurę przesuniętą o n spacji w prawo, gdzie n jest parametrem wejściowym funkcji

//3.Wykorzystaj tę funkcję do narysowania figury z punktu 1  i poniżej   figury przesuniętej o n=|a-b| spacji w prawo.


try
{
    //zadanie 1

    Console.WriteLine("*** Funkcja wyswietlajaca figure axb ***");

    Console.WriteLine("Prosze podac wysokosc figury: ");
    int a = int.Parse(Console.ReadLine());

    Console.WriteLine("Prosze podac szerokosc figury: ");
    int b = int.Parse(Console.ReadLine());

    Console.WriteLine("Prosze podac symbol 1: ");
    char s1 = char.Parse(Console.ReadLine());

    Console.WriteLine("Prosze podac symbol 2: ");
    char s2 = char.Parse(Console.ReadLine());

    if (a <= 0 || b <= 0)
    {
        Console.WriteLine("Obie liczby muszą być dodatnie całkowite i różne od 0");
        return;
    }

    RysujFigure(a, b, s1, s2);

    //zadanie 2

    Console.WriteLine("*** Funkcja wyswietlajaca figure axb przesunieta o n spacji***");
    Console.WriteLine("Prosze podac wartosc przesuniecia n: ");
    int n = int.Parse(Console.ReadLine());

    if (n <= 0)
    {
        Console.WriteLine("Wartosc n musi być dodatnia całkowita i różne od 0");
        return;
    }

    RysujFigure(a, b, s1, s2, n);

    //zadanie 3
    Console.WriteLine("*** Funkcja wyswietlajaca figure axb przesunieta o n = |a-b| spacji***");

    int c = 0;

    if (a >= b)
    {
        c = a - b;
    }
    else
    {
        c = (a - b) * (-1);
    }

    RysujFigure(a, b,s1,s2, c);
}
catch (FormatException)
{
    Console.WriteLine("Błąd: Niepoprawny format.");
}
catch (Exception)
{
    Console.WriteLine("Błąd: Wprowadź poprawna liczbe dodatnia całkowitą.");
}
//Funkcja rysujaca figure
static void RysujFigure(int a, int b, char s1, char s2, int n = 0)
{
    for (int i = 0; i < a; i++)
    {
        if ((i % 2) != 0)
        {
            Console.WriteLine(new string(' ', n) + new string(s1, b));
        }
        else
        {
            Console.WriteLine(new string(' ', n) + new string(s2, b));
        }
    }
}
