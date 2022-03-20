using System;

namespace vj1z1
{
    class zadatak1
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("unsite dva cjelobroja broja");
                int a = Convert.ToInt32(Console.ReadLine());
                int b = Convert.ToInt32(Console.ReadLine());

                double result = (double)a / b;
                Console.WriteLine("rezultat u currency formatu " + result.ToString("C"));
                Console.WriteLine("rezultat u integer formatu " + (int)result);
                Console.WriteLine("rezultat u scientific formatu " + result.ToString("E"));
                Console.WriteLine("rezultat u fixed-point formatu " + result.ToString("F"));
                Console.WriteLine("rezultat u general format " + result.ToString("G"));
                Console.WriteLine("rezultat u number formatu " + result.ToString("N"));
                Console.WriteLine("rezultat u hexadecimal formatu " + ((int)result).ToString("X"));
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
