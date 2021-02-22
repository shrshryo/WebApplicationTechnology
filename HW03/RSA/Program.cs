using System;
using System.Numerics;
using System.Runtime;
using System.Security.Cryptography;

namespace RSA
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var primeNumOne = "";
            ulong FirstPrime = 0;
            ulong mOne = 0;
            ulong iOne = 0;
            int flagOne = 0;
            do
            {
                Console.Write("Please enter a prime number (or X to cancel):");
                primeNumOne = Console.ReadLine()?.ToLower().Trim();
                if (primeNumOne != "x")
                {
                    if (int.TryParse(primeNumOne, out var userValueInt))
                    {
                        ulong userValue = Convert.ToUInt64(userValueInt);
                        FirstPrime = userValue;

                        mOne = FirstPrime / 2;
                        for (iOne = 2; iOne <= mOne; iOne++)
                        {
                            if (FirstPrime % iOne == 0)
                            {
                                Console.WriteLine($"{FirstPrime} is not a prime number!");
                                flagOne = 1;
                            }
                        }

                        if (flagOne == 0)
                        {
                            Console.WriteLine($"Prime number is: {FirstPrime}");
                        }
                    }
                }
            } while (primeNumOne != "x" && flagOne != 0);

            if (primeNumOne == "x") return;
            
            var primeNumTwo = "";
            ulong SecondPrime = 0;
            ulong mTwo = 0;
            ulong iTwo = 0;
            int flagTwo = 0;
            do
            {
                Console.Write("Please enter a prime number (or X to cancel):");
                primeNumTwo = Console.ReadLine()?.ToLower().Trim();
                if (primeNumTwo != "x")
                {
                    if (int.TryParse(primeNumTwo, out var userValueInt))
                    {
                        ulong userValue = Convert.ToUInt64(userValueInt);
                        SecondPrime = userValue;

                        mTwo = SecondPrime / 2;
                        for (iTwo = 2; iTwo <= mTwo; iTwo++)
                        {
                            if (SecondPrime % iTwo == 0)
                            {
                                Console.WriteLine($"{SecondPrime} is not a prime number!");
                                flagTwo = 1;
                            }
                        }

                        if (flagTwo == 0)
                        {
                            Console.WriteLine($"Prime number is: {SecondPrime}");
                        }
                    }
                }
            } while (primeNumTwo != "x" && flagTwo != 0);

            if (primeNumTwo == "x") return;
            
            Console.WriteLine($"First prime: {FirstPrime} Second prime: {SecondPrime}");

            ulong n = FirstPrime * SecondPrime;
            ulong m = (FirstPrime - 1) * (SecondPrime - 1);
            
            Console.WriteLine($"n = p * q is {n}");
            Console.WriteLine($"m = (p - 1) * (q - 1) is {m}");
            
            ulong e;
            for (e = 2; e < ulong.MaxValue; e++)
            {
                if (GCD(m, e) == 1) break;
            }

            ulong d = 0;
            for (ulong k = 2; k < ulong.MaxValue; k++)
            {
                if ((1 + k * m) % e == 0)
                {
                    d = (1 + k * m) / e;
                    break;
                }
            }

            Console.WriteLine(($"Public key ({n},{e})"));
            Console.WriteLine(($"Private key ({n},{d})"));
            
            Console.Write("Message: ");
            var messageStr = Console.ReadLine();
            ulong message = ulong.Parse(messageStr);
            
            var cipher = calc2(message, e, n);
            Console.Write($"Cipher text: {cipher}\n");

            var bruteForce = calc2(cipher, d, n);
            Console.Write($"Brute force result: {bruteForce}");

        }
        
        static ulong GCD(ulong a, ulong b)
        {
            if (a == 0) return b;
            return GCD(b % a, a);
        }

        static ulong largeCalc(ulong a, ulong b)
        {
            ulong result = ((ulong) (uint) a) * ((ulong) (uint) b);
            result >>= 64;
            ulong term1 = (a >> 64) * ((ulong) (uint) b);
            ulong term2 = (b >> 64) * ((ulong) (uint) a);
            result += (uint) term1;
            result += (uint) term2;
            result >>= 64;
            result += (term1 >> 64) + (term2 >> 64);
            result += (a >> 64) * (b >> 64);
            return result;
        }
        
        static int bits(ulong x){
            int y = 0;
            while(y > 0){
                x >>= 1;
                y++;
            }
            return y;
        }
        static ulong calc2(ulong a, ulong b, ulong c)
        {
            ulong x = a % c;
            int y = bits(b) - 2;
            while(y >= 0)
            {
                if (2 * Math.Log(x) > Math.Log(ulong.MaxValue))
                {
                    throw new System.ArgumentException("Overflow!");
                }
                x = (x * x) % c;
                if((b >> y & 1) == 1)
                {
                    if (Math.Log(x) + Math.Log(a) > Math.Log(ulong.MaxValue))
                    {
                        throw new System.ArgumentException("Overflow!");
                    } 
                    x = (x * a) % c;
                }
                y--;
            }
            return x;
        }
    }
}