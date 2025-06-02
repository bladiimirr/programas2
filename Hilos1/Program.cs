using System;
using System.Diagnostics;
using System.Threading;
/*class Program
{
    static int sumaTotal = 0;
    static object lockObject = new object();
    static void CalcularPrimos(object rango)
    {
        (int inicio, int fin) = ((int, int))rango;
        int suma = 0;
        for (int i = inicio; i <= fin; i++)
        {
            if (EsPrimo(i))
            {
                suma += i;
            }
        }
        lock (lockObject)

        {
            sumaTotal += suma;
        }
    }
    static bool EsPrimo(int numero)
    {
        if (numero < 2) return false;
        for (int i = 2; i * i <= numero; i++)
        {
            if (numero % i == 0) return false;
        }
        return true;
    }
    static void Main()
    {
        Console.WriteLine("Ingrese el número límite:");
        int N = int.Parse(Console.ReadLine());
        int M = 4; // Número de hilos
        int rango = N / M;
        Thread[] hilos = new Thread[M];
        Stopwatch stopwatch = Stopwatch.StartNew();
        for (int i = 0; i < M; i++)
        {
            int inicio = i * rango + 1;
            int fin = (i == M - 1) ? N : inicio + rango - 1;
            hilos[i] = new Thread(CalcularPrimos);
            hilos[i].Start((inicio, fin));
        }
        foreach (var hilo in hilos)
        {
            hilo.Join();
        }
        stopwatch.Stop();
        Console.WriteLine($"Suma total de números primos hasta {N}: {sumaTotal}");
        Console.WriteLine($"Tiempo de ejecución: {stopwatch.ElapsedMilliseconds} ms");
    }
}*/


/*using System;
using System.Diagnostics;
using System.Threading;

class Program
{
    static int sumaTotal = 0;
    static object lockObject = new object();

    // Método secuencial (sin hilos)
    static void CalcularPrimosSecuencial(int inicio, int fin)
    {
        int suma = 0;
        for (int i = inicio; i <= fin; i++)
        {
            if (EsPrimo(i))
            {
                suma += i;
            }
        }
        sumaTotal += suma;
    }

    // Método concurrente (con hilos)
    static void CalcularPrimos(object rango)
    {
        (int inicio, int fin) = ((int, int))rango;
        int suma = 0;
        for (int i = inicio; i <= fin; i++)
        {
            if (EsPrimo(i))
            {
                suma += i;
            }
        }

        lock (lockObject)
        {
            sumaTotal += suma;
        }
    }

    // Método para verificar si un número es primo
    static bool EsPrimo(int numero)
    {
        if (numero < 2) return false;
        for (int i = 2; i * i <= numero; i++)
        {
            if (numero % i == 0) return false;
        }
        return true;
    }

    static void Main()
    {
        Console.WriteLine("Ingrese el número límite:");
        int N = int.Parse(Console.ReadLine());
        int M = 4; // Número de hilos
        int rango = N / M;

        // Ejecutar la versión secuencial y medir el tiempo
        sumaTotal = 0;
        Stopwatch stopwatch = Stopwatch.StartNew();

        // Calcular los primos de manera secuencial
        CalcularPrimosSecuencial(1, N);

        stopwatch.Stop();
        Console.WriteLine($"Suma total de números primos (secuencial) hasta {N}: {sumaTotal}");
        Console.WriteLine($"Tiempo de ejecución (secuencial): {stopwatch.ElapsedMilliseconds} ms");

        // Ahora ejecutamos la versión concurrente y medimos el tiempo
        sumaTotal = 0;
        Thread[] hilos = new Thread[M];
        stopwatch.Restart(); // Reinicia el cronómetro

        // Crear y lanzar los hilos
        for (int i = 0; i < M; i++)
        {
            int inicio = i * rango + 1;
            int fin = (i == M - 1) ? N : inicio + rango - 1;
            hilos[i] = new Thread(CalcularPrimos);
            hilos[i].Start((inicio, fin));
        }

        // Esperar a que todos los hilos terminen
        foreach (var hilo in hilos)
        {
            hilo.Join();
        }

        stopwatch.Stop();
        Console.WriteLine($"Suma total de números primos (concurrente) hasta {N}: {sumaTotal}");
        Console.WriteLine($"Tiempo de ejecución (concurrente): {stopwatch.ElapsedMilliseconds} ms");
    }
}*/
using System;
using System.Diagnostics;

class Program
{
    static int sumaTotal = 0;

    // Método secuencial para calcular la suma de números primos en un rango
    static void CalcularPrimosSecuencial(int inicio, int fin)
    {
        int suma = 0;
        for (int i = inicio; i <= fin; i++)
        {
            if (EsPrimo(i))
            {
                suma += i;
            }
        }
        sumaTotal += suma;
    }

    // Método para verificar si un número es primo
    static bool EsPrimo(int numero)
    {
        if (numero < 2) return false;
        for (int i = 2; i * i <= numero; i++)
        {
            if (numero % i == 0) return false;
        }
        return true;
    }

    // Método principal
    static void Main()
    {
        Console.WriteLine("Ingrese el número límite:");
        int N = int.Parse(Console.ReadLine());

        // Versión secuencial
        sumaTotal = 0;
        Stopwatch stopwatch = Stopwatch.StartNew();

        // Calcular los primos de manera secuencial
        CalcularPrimosSecuencial(1, N);

        // Pausa artificial para simular un proceso más largo y medir el tiempo
        Thread.Sleep(90); // Agrega una pausa de 1 segundo

        stopwatch.Stop();
        Console.WriteLine($"Suma total de números primos (secuencial) hasta {N}: {sumaTotal}");
        Console.WriteLine($"Tiempo de ejecución (secuencial): {stopwatch.ElapsedMilliseconds} ms");
    }

}