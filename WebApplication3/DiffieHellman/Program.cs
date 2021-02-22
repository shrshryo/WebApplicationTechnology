using System;
using CryptoDiffieHellman;

namespace DiffieHellman
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            var primeKey = "";
            ulong commonPrimeNum = 0;
            ulong m = 0;
            ulong i = 0;
            int flag = 0;
            do
            {
                Console.Write("Please enter a prime number (or X to cancel):");
                primeKey = Console.ReadLine()?.ToLower().Trim();
                if (primeKey != "x")
                {
                    if (int.TryParse(primeKey, out var userValueInt))
                    {
                        ulong userValue = Convert.ToUInt64(userValueInt);
                        commonPrimeNum = userValue;

                        m = commonPrimeNum / 2;
                        for (i = 2; i <= m; i++)
                        {
                            if (commonPrimeNum % i == 0)
                            {
                                Console.WriteLine($"{commonPrimeNum} is not a prime number!");
                                flag = 1;
                                //break;
                            }
                        }

                        if (flag == 0)
                        {
                            Console.WriteLine($"Prime number is: {commonPrimeNum}");
                        }
                    }
                }
            } while (primeKey != "x" && flag != 0);

            if (primeKey == "x") return;

            var key = "";
            ulong commonNum = 0;
            do
            {
                Console.Write("Please enter common number (or X to cancel):");
                key = Console.ReadLine()?.ToLower().Trim();
                if (key != "x")
                {
                    if (int.TryParse(key, out var userValueKeyInt))
                    {
                        ulong userValue = Convert.ToUInt64(userValueKeyInt);
                        commonNum = userValue;
                        if (commonNum == 0)
                        {
                            Console.WriteLine("Will not do anything!");
                        }
                        else
                        {
                            //ulong ok = checkPrimitive(commonNum); 
                            Console.WriteLine($"Common number is: {commonNum}");
                        }
                    }
                }
            } while (commonNum == 0 && key != "x");

            if (key == "x") return;

            var userInputOne = "";
            ulong secretKeyOne = 0;
            Console.Write("Please enter the secret key for first person");
            userInputOne = Console.ReadLine();
            if (int.TryParse(userInputOne, out var userValueOneInt))
            {
                ulong userValue = Convert.ToUInt64(userValueOneInt);
                secretKeyOne = userValue;
                if (secretKeyOne == 0)
                {
                    Console.WriteLine("Will not do anything!");
                }
                else
                {
                    Console.WriteLine($"First secret key is: {secretKeyOne}");
                }
            }

            var userInputTwo = "";
            ulong secretKeyTwo = 0;
            Console.Write("Please enter the secret key for second person");
            userInputTwo = Console.ReadLine();
            if (int.TryParse(userInputTwo, out var userValueTwoInt))
            {
                ulong userValue = Convert.ToUInt64(userValueTwoInt);
                secretKeyTwo = userValue;
                if (secretKeyTwo == 0)
                {
                    Console.WriteLine("Will not do anything!");
                }
                else
                {
                    Console.WriteLine($"Second secret key is: {secretKeyTwo}");
                }
            }
        }
    }
}