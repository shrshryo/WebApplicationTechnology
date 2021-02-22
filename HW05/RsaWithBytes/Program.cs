using System;
using System.Runtime;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Schema;

namespace RsaWithBytes
{
    class Program
    {
        public static void Main(string[] args)
        {
            var FirstPrimeKey = "";
            ulong FirstPrime = 0;
            ulong i = 0;
            int flagOne = 0;
            do
            {
                Console.Write("Please enter the first prime number (or X to cancel)");
                FirstPrimeKey = Console.ReadLine()?.ToLower().Trim();
                if (FirstPrimeKey != "x")
                {
                    if (int.TryParse(FirstPrimeKey, out var userValueInt)) ;
                    {
                        ulong uservalue = Convert.ToUInt64(userValueInt);
                        FirstPrime = uservalue;
                        for (i = 2; i <= FirstPrime / 2; i++)
                        {
                            if (FirstPrime % i == 0)
                            {
                                Console.WriteLine($"{FirstPrime} is not a prime number");
                                flagOne = 1;
                                break;
                            }
                        }

                        if (flagOne == 0)
                        {
                            Console.WriteLine($"First prime number is: {FirstPrime}");
                        }
                    }
                }

            } while (FirstPrimeKey != "x" && flagOne != 0);

            if (FirstPrimeKey == "x") return;
            
            var SecondPrimeKey = "";
            ulong SecondPrime = 0;
            ulong j = 0;
            int flagTwo = 0;
            do
            {
                Console.Write("Please enter the first prime number (or X to cancel)");
                SecondPrimeKey = Console.ReadLine()?.ToLower().Trim();
                if (SecondPrimeKey != "x")
                {
                    if (int.TryParse(SecondPrimeKey, out var userValueInt)) ;
                    {
                        ulong uservalue = Convert.ToUInt64(userValueInt);
                        SecondPrime = uservalue;
                        for (j = 2; j <= SecondPrime / 2; j++)
                        {
                            if (FirstPrime % j == 0)
                            {
                                Console.WriteLine($"{SecondPrime} is not a prime number");
                                flagTwo = 1;
                                break;
                            }
                        }

                        if (flagTwo == 0)
                        {
                            Console.WriteLine($"First prime number is: {SecondPrime}");
                        }
                    }
                }

            } while (SecondPrimeKey != "x" && flagTwo != 0);
            if (SecondPrimeKey == "x") return;

            ulong n = FirstPrime * SecondPrime;
            ulong m = (FirstPrime - 1) * (SecondPrime - 1);
            
            Console.WriteLine($"n = p * q is {n}");
            Console.WriteLine($"n = (p - 1) * (q - 1) is {m}");

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
            
            Console.WriteLine(($"Public key ({n}, {e})"));
            Console.WriteLine(($"Private key ({n}, {d})"));
            
            Console.Write("Message: ");
            var messageStr = Console.ReadLine();
            if (messageStr != null)
            {
                Console.WriteLine($"Length of text: {messageStr.Length}");
                var encrytpedBytes = RsaEncryptString(messageStr, e, n);
                
                var encryptedBytes = RsaEncryptString(messageStr, e, n);
                Console.WriteLine($"{encryptedBytes}\n");
                
                var decrypted = RsaDecryptString(encryptedBytes, d, n);
                Console.WriteLine($"{decrypted}");

            }
            else
            {
                Console.WriteLine("Message is null");
            }
            
            

        }

        static string RsaEncryptString(string input, ulong a, ulong b)
        {
            string result = "";
            var inputBytes = Encoding.UTF8.GetBytes(input);
            int i;
            for (i = 0; i < input.Length-1; i++)
            {
                result += RsaCalc(input[i], a, b) + " ";
            }
            result += RsaCalc(input[i], a, b);
            return result;
        }

        static string RsaDecryptString(string encryptInput, ulong c, ulong b)
        {
            var message = encryptInput.Split();
            var output = new byte[message.Length];
            for (int i = 0; i < message.Length; i++)
            {
                output[i] = (byte)RsaCalc(ulong.Parse(message[i]), c, b);
            }
        
            return Encoding.UTF8.GetString(output);
        }

        public static ulong RsaCalc(ulong input, ulong secret, ulong mod)
        {

            ulong B, D;
            B = input;
            
            B %= mod;
            D = 1;
            if ((secret & 1) == 1)
            {
                D = B;
            }
            while(secret > 1)
            {
                secret >>= 1;
                B = (B * B) % mod;
                if ((secret & 1) == 1)
                {
                    D = (D * B) % mod;
                }
            }
            return (ulong)D;

        }

        static ulong GCD(ulong a, ulong b)
        {
            if (a == 0) return b;
            return GCD(b % a, a);
        }
    }
}