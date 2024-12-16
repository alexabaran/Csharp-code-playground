//Zadania na zajęcia laboratoryjne: 
using System;
//task05();

//Zadanie 1  
//Obliczyć wartość wyrażenia: (a^2+b)/(a+b)^2
//dla zmiennych a i b typu float wczytywanych z klawiatury. Sprawdzić wykonalność obliczenia. 

static void task01()
{
    try
    {
        Console.WriteLine("Prosze podac wartosc a: ");
        float a = float.Parse(Console.ReadLine());

        Console.WriteLine("Prosze podac wartosc b: ");
        float b = float.Parse(Console.ReadLine());

        float licznik = (a * a + b);
        float mianownik = (a + b) * (a + b);
        if (mianownik == 0)
        {
            Console.WriteLine("Nie można dzielić przez 0");
        }
        else
        {
            float wyrazenie = licznik / mianownik;
            Console.WriteLine($"wartość wyrażenia: (a^2+b)/(a+b)^2 wynosi: {wyrazenie}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}


//Zadanie 2  
//Wykorzystując zmienne typu double obliczyć wartość wyrażenia wynoszącą: 
// a^2+b dla c > 0 
// a-b^2 dla c < 0 
//1/(a-b) dla c = 0 
//Sprawdzić wykonalność obliczenia. 

static void task02()
{
    try
    {
        Console.WriteLine("Prosze podac wartosc a: ");
        double a = double.Parse(Console.ReadLine());

        Console.WriteLine("Prosze podac wartosc b: ");
        double b = double.Parse(Console.ReadLine());

        Console.WriteLine("Prosze podac wartosc c: ");
        double c = double.Parse(Console.ReadLine());

        if (c > 0)
        {
            double wynik = a * a + b;
            Console.WriteLine($"Wynik a*a+b to: {wynik}");
        }
        else if (c < 0)
        {
            double wynik = a - b * b;
            Console.WriteLine($"Wynik a-b*b to: {wynik}");
        }
        else if (c == 0 && (a - b) == 0)
        {
            Console.WriteLine("Nie można dzielić przez 0");
        }
        else
        {
            double wynik = 1 / (a - b);
            Console.WriteLine($"Wynik 1/(a-b) to: {wynik}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}


//Zadanie 3 
//Napisać program obliczania największego wspólnego dzielnika dwóch dodatnich liczb całkowitych. Wykorzystać algorytm 
//Euklidesa nie używając operacji dzielenia. 

static void task03()
{
    try
    {
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

        static int NWD(int a,int b)
        {
            while (a != b)
            {
                if ( a > b )
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


//Zadanie 4 
//Napisać program obliczania sumy cyfr rozwinięcia dziesiętnego dla zadanej liczby naturalnej. 

static void task04()
{
    try
    {
        Console.WriteLine("Prosze podac liczbe naturalna: ");
        int a = int.Parse(Console.ReadLine());
        int sum = 0;
        if (a > 0)
        {
            while (a > 0)
            {
                int b = a % 10;
                sum += b;
                a = a / 10;
            }
            Console.WriteLine(sum);
        }
        else
        {
            Console.WriteLine("Błąd: Wprowadź poprawna liczbe naturalna.");
        }  
    }
    catch (FormatException)
    {
        Console.WriteLine("Błąd: Wprowadź poprawna liczbe naturalna.");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}


//Zadanie 5 
//Napisać program badania czy zadana liczba jest liczbą pierwszą. Zminimalizować liczbę operacji dzielenia. 

//Przykłady działania: 
//Podaj liczbe:35 
//Liczba 35 jest liczbą złożoną podzielną przez 5 

//Podaj liczbe:17 
//Liczba 17 jest liczbą pierwszą 
//Przykładowe liczby pierwsze do testów (może być konieczne użycie typu ulong): 

static void task05()
{
    try
    {
        Console.WriteLine("Prosze podac liczbe całkowitą większą od 0: ");
        ulong a = ulong.Parse(Console.ReadLine());

        if (a == 0)
        {
            Console.WriteLine("liczba musi być większa od 0");
            return;
        }
        if (a > 3)
        {
            for (ulong i = 2; i < a; i++)
            {
                ulong b = a % i;
                if (b == 0)
                {
                    Console.WriteLine($"Liczba {a} nie jest liczbą pierwszą i jest podzielna przez {i}");
                    break;
                }
                else
                {
                    Console.WriteLine($"Liczba {a} jest liczbą pierwszą");
                    break;
                }
            }
        }
        else
        {
            Console.WriteLine($"Liczba {a} jest liczbą pierwszą");
        }


    }
    catch (FormatException)
    {
        Console.WriteLine("Błąd: Wprowadź poprawna liczbe naturalna.");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

Console.WriteLine("Prosze podac liczbe całkowitą większą od 0: ");
ulong a = ulong.Parse(Console.ReadLine());
ulong divider;
var result = JestLiczbaPierwsza(a, out divider);
if (!result)
{
    Console.WriteLine($"Liczba {a} nie jest liczbą pierwszą i jest podzielna przez {divider}");
} else
{
    Console.WriteLine($"Liczba {a} jest liczba pierwsza");
}
/// <summary> This property always returns a value &lt; 1.</summary>
static bool JestLiczbaPierwsza(ulong number, out ulong divider) 
{
    divider = 0;
    if (number <= 3) return true;
    for (ulong i = 2; i < number; i++)
    {
        divider = i;
        ulong b = number % i;
        return b == 0 ? false : true;
    }
    return false;
}
//Zadanie 6 
//Napisać program drukujący choinkę składającą się z gwiazdek według zadanej wysokości. Choinka ma rozpoczynać się od pojedynczej gwiazdki i zwiększać szerokość o 2 gwiazdki z każdym wierszem. Choinka ma mieć pień o wysokości dwóch 
//znaków. Od początku wiersza figurę należy uzupełnić spacjami. 

//Przykłady działania: 
//Podaj wysokość choinki:1 
//* 
//| 
//| 

//Podaj wysokość choinki:5 
//    * 
//   *** 
//  ***** 
// ******* 
//********* 
//    |
//    |

static void task06()
{
    try
    {
        Console.WriteLine("Prosze podac wysokosc choinki: ");
        int a = int.Parse(Console.ReadLine());

        for (int i = 0; i < a; i++)
        {
            Console.WriteLine(new string(' ', (a - 1 - i)) + new string('*', i) + "*" + new string('*', i));
        }
        Console.WriteLine(new string(' ', (a - 1)) + "|");
        Console.WriteLine(new string(' ', (a - 1)) + "|");
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


//Zadanie 7
//Dla macierzy kwadratowej wprowadzanej z klawiatury wierszami zbadać, czy suma elementów powyżej głównej przekątnej 
//jest większa od sumy elementów poniżej tej przekątnej. 


//Zadanie 8 
//Napisać program sortujący wprowadzane z klawiatury liczby całkowite rosnąco. Program powinien zapytać użytkownika o 
//liczbę elementów do wprowadzenia <1,10>, zweryfikować wprowadzoną liczbę, wczytać liczby i wyprowadzić posortowane 
//elementy. Obsłużyć wyjątki. 



//Przykład działania: 
//Wprowadź liczbę elementów do posortowania <1 .. 10>: -2 
//Zła liczba, spróbuj ponownie ... 
//Wprowadź liczbę elementów do posortowania <1 .. 10>: 4 
//Wprowadź liczbę [1]=1 
//Wprowadź liczbę [2]=6 
//Wprowadź liczbę [3]=2 
//Wprowadź liczbę [4]=4 
//Wyprowadzanie posortowanych elementów: 
//Element [1]=1 
//Element [2]=2 
//Element [3]=4 
//Element [4]=6 



//Zadanie 9 
//Napisać program badania czy zadana liczba jest liczbą pierwszą nie wykorzystując operacji dzielenia (wykorzystać sito 
//Eratostenesa i tablicę elementów typu bool). Obsłużyć wyjątki. 
//Przykłady działania: 
//Podaj liczbe:35 
//Liczba 35 jest liczba złożoną podzielną przez 5 
//Podaj liczbe:17 
//Liczba 17 jest liczba pierwsza 



//Zadanie 10 
//Napisać program obliczania wartości wielomianu. Do przechowywania współczynników przy wyrazach wielomianu użyć 
//tablicy jednowymiarowej. Wykorzystać schemat Hornera. Obsłużyć wyjątki. 
//Przykład działania: 
//Podaj stopień wielomianu:3 
//Podaj wyraz wolny:5 
//Podaj współczynnik przy x^1:1 
//Podaj współczynnik przy x^2:0 
//Podaj współczynnik przy x^3:1 
//Podaj wartość x:3 
//Wartość wielomianu wynosi:35 





//Zadanie 11 
//Napisać program przedstawiający każdą liczbę parzystą x z podanego przedziału domkniętego jako sumę 2 liczb pierwszych. 
//Zastosować metodę sprawdzającą czy podana liczba jest pierwsza. Obsłużyć wyjątki. 





//Zadanie 12 
//Napisać program wypisujący wszystkie liczby pierwsze z przedziału od 1 do zadanej z klawiatury wartości. Obsłużyć wyjątki. 





//Zadanie 13 
//Liczba bliźniacze to liczby pierwsze, których różnica wynosi 2 np. {3,5}, {11,13}. Napisać program wypisujący wszystkie pry 
//liczb bliźniaczych z przedziału od 1 do zadanej z klawiatury wartości. Obsłużyć wyjątki. 





//Zadanie 14 
//Liczba doskonała to liczba równa sumie swoich podzielników mniejszych od niej samej, np. 1 + 2 + 4 + 7 + 14 = 28. Napisać 
//program wypisujący wszystkie liczby doskonałe z przedziału od 1 do zadanej z klawiatury wartości. Obsłużyć wyjątki. 





//Zadanie 15 
//Liczby zaprzyjaźnione to takie dwie liczby, z których każda jest równa sumie podzielników drugiej liczby mniejszych od tej 
//liczby.  Na przykład liczby 220 i 284 są zaprzyjaźnione, ponieważ: 220 dzieli się przez 1, 2, 4, 5, 10, 11, 20, 22, 44, 55 i 110, 
//których suma wynosi 284, a 284 dzieli się przez 1, 2, 71 i 142, których suma wynosi 220. Napisać program wypisujący 
//wszystkie liczby zaprzyjaźnione z przedziału od 1 do zadanej z klawiatury wartości. Obsłużyć wyjątki. 





//Zadanie 16 
//Napisać program obliczania wartości symbolu Newtona. Zastosować metodę obliczającą wartość n!. Obsłużyć wyjątki. 





//Zadanie 17 
//Napisać program obliczania dla każdej liczby z ciągu podanych liczb naturalnych ( String.Split()) sumy cyfr jej rozwinięcia
//dziesiętnego. Zastosować metodę obliczania sumy cyfr rozwinięcia dziesiętnego pojedynczej liczby. Obsłużyć wyjątki





//Zadanie 18 
//Napisać program znajdujący wszystkie liczby pierwsze, które można stworzyć z cyfr podanej liczby całkowitej. Np. z cyfr 
//liczby 1379 można stworzyć 31 liczb pierwszych. Uwaga: budując liczbę pierwszą każdą cyfrę można użyć tylko raz. Obsłużyć 
//wyjątki. 



//Zadanie 19 
// W pliku wejściowym znajduje się ciąg liczb całkowitych. Napisać program, który po wprowadzeniu nazwy pliku obliczy i 
//wydrukuje średnią arytmetyczną liczb zawartych w pliku. Obsłużyć wyjątki. 




//Zadanie 20 
//Napisać program odwracający kolejność łańcuchów tekstu podawanych z wejścia. Program ma zapamiętywać wprowadzane 
//dane wykorzystując stos implementowany jako listę. Po wprowadzeniu łańcucha pustego ma zostać wyświetlona 
//odpowiedź. Obsłużyć wyjątki. 
//Do przechowywania danych na liście należy wykorzystać strukturę: 
//class Element 
//{ 
//  string Klucz; 
//  public Element next; 
//  public Element(string Klucz) 
//  { 
//    this.Klucz = Klucz; 
//  } 
//} 


//Przykład działania: 
//pierwszy 
//drugi 
//trzeci 

//trzeci 
//drugi 
//pierwszy 
