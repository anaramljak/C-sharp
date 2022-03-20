using System;

namespace vj1zad2
{
    class zadatak2
    {
        static void Main(string[] args)
        {
            
                try
                {
                    int a = 1;
                    long b = long.MaxValue;
                    Console.WriteLine(checked(a + b));
                }
                catch (OverflowException e)
                {
                    Console.WriteLine(e.Message);
                }
            
        }
    }
}
