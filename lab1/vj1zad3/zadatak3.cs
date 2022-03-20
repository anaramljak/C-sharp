using System;
using System.Collections.Generic;

namespace vj1zad3
{
    public enum Type { Stednja, Tekuci, Ziro };
    public struct BankAccount
    {
        public int aNumber { get; set; }
        public double amount { get; set; }
        public Type aType { get; set; }

    }
    class zadatak3
    {
        static void Main(string[] args)
        {
            BankAccount[] acc = new BankAccount[5];
            menu(acc);
        }

        static void menu(BankAccount [] acc)
        {
            int i = 0;
            while (true)
            {
                Console.WriteLine("Za unos novog racuna unesite 0, za ispis svih racuna unesite 1,  za prekid unesite 2 ");
                string a = Console.ReadLine().Trim();
                if (a == "0")
                {
                    Console.WriteLine("Unesite broj racuna");
                    inputAccNum(acc, i);
                    Console.WriteLine("Unesite iznos racuna");
                    inputAccAmount(acc, i);
                    Console.WriteLine("Unesite tip racuna:  Stednja,Tekuci ili Ziro");
                    inputAccTy(acc, i);
                    i++;
                }

                else if (a == "1")
                {
                    printAcc(acc);
                }

                else if (a == "2")
                {
                    break;
                }

            }
        }

        private static void inputAccNum(BankAccount [] acc, int i)
        {
                try
                {
                    acc[i].aNumber = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Pogresan unos");
                    Console.WriteLine("Ponovno unesite broj racuna");
                    inputAccNum(acc, i);
                }
            
        }
        private static void inputAccAmount(BankAccount [] acc, int i)
        { 
                try
                {
                    acc[i].amount = Convert.ToDouble(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Pogresan unos");
                    Console.WriteLine("Ponovno unesite iznos racuna");
                    inputAccAmount(acc, i);
                }
            
        }
        private static void inputAccTy(BankAccount [] acc, int i)
        {
            while (true)
            {
                string ty = Console.ReadLine().Trim();
                if (ty == "Stednja")
                {
                    acc[i].aType = Type.Stednja;
                    break;
                }
                else if (ty == "Tekuci")
                {
                    acc[i].aType = Type.Tekuci;
                    break;
                }
                else if (ty == "Ziro")
                {
                    acc[i].aType = Type.Ziro;
                    break;
                }
                else
                {
                    Console.WriteLine("Pogresan unos vrste racuna, unsite ponovno: ");

                }
            }
        }
        private static void printAcc(BankAccount[] acc)
        {
            foreach (BankAccount el in acc)
            {
                Console.Write("\nBroj racuna:  " + el.aNumber + "\nIznos racuna: " + el.amount + "\nVrsta racuna: " + el.aType);
            }
        }
    }
}
