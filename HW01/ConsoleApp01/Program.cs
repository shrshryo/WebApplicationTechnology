using System;
using System.Text;

namespace ConsoleApp01
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("HW01 Ryo Shiraishi 195557IVSB");

            var userInput = "";
            do
            {
                Console.WriteLine();
                Console.WriteLine("1) Cesar cipher");
                Console.WriteLine("2) Vigenere cipher");
                Console.WriteLine("3) Cesar decrypt");
                Console.WriteLine("4) Vigenere decrypt");
                Console.WriteLine("5) Vigenere decrypt");
                Console.WriteLine("X) Exit");
                Console.Write(">");

                userInput = Console.ReadLine()?.ToLower();

                switch (userInput)
                {
                    case "1":
                        Cesar();
                        break;
                    case "2":
                        Vigenere();
                        break;
                    case "3":
                        CesarDecrypt();
                        break;
                    case "4":
                        VigenereDecrypt();
                        break;
                    case "5":
                        Rsa();
                        break;
                    case "x":
                        Console.WriteLine("closing down...");
                        break;
                    default:
                        Console.WriteLine($"Don't have this '{userInput}' as an option!");
                        break;
                }
            } while (userInput != "x");
        }


        static void Cesar()
        {
            Console.WriteLine("Cesar Cipher");

            // byte per character
            // 0-255
            // 0-127 - latin
            // 128-255 - change what you want
            // ABCD - A 189, B - 195, C 196, D 202
            // unicode 
            // AÄÖÜLA❌
            
            var userInput = "";
            var key = 0;
            do
            {
                Console.Write("Please enter your shift amount (or X to cancel):");
                userInput = Console.ReadLine()?.ToLower().Trim();
                if (userInput != "x")
                {
                    if (int.TryParse(userInput, out var userValue))
                    {
                        key = userValue % 255;
                        if (key == 0)
                        {
                            Console.WriteLine("multiples of 255 is no cipher, this would not do anything!");
                        }
                        else
                        {
                           Console.WriteLine($"Cesar key is: {key}");
                        }
                    }
                }

            } while (key == 0 && userInput != "x");

            if (userInput == "x") return;
            
 
            Console.Write("Please enter your plaintext:");
            var plainText = Console.ReadLine();
            if (plainText != null)
            {
                Console.WriteLine($"length of text: {plainText.Length}");
                
                //ShowEncoding(plainText, Encoding.Default);
                
                var encryptedBytes = CesarEncryptString(plainText, (byte) key, Encoding.Default);
                
                Console.Write("Encrypted bytes: ");
                foreach (var encryptedByte in encryptedBytes)
                {
                    Console.Write(encryptedByte + " ");
                }

                Console.WriteLine("base64: " + System.Convert.ToBase64String(encryptedBytes));
                
                /*
                ShowEncoding(plainText, Encoding.UTF7);
                ShowEncoding(plainText, Encoding.UTF8);
                ShowEncoding(plainText, Encoding.UTF32);
                ShowEncoding(plainText, Encoding.Unicode);
                ShowEncoding(plainText, Encoding.ASCII);
                ShowEncoding(plainText, Encoding.Default); // most likely UTF-8
                */


            }
            else
            {
                Console.WriteLine("Plaintext is null!");
            }


        }

        static byte[] CesarEncryptString(string input, byte shiftAmount, Encoding encoding)
        {
            var inputBytes = encoding.GetBytes(input);
            return CesarEncrypt(inputBytes, shiftAmount);
        }

        static byte[] CesarEncrypt(byte[] input, byte shiftAmount)
        {
            var result = new byte[input.Length];

            
            if (shiftAmount == 0)
            {
                // no shifting needed, just create deep copy
                for (var i = 0; i < input.Length; i++)
                {
                    result[i] = input[i];
                }
            }
            else
            {
                for (int i = 0; i < input.Length; i++)
                {
                    var newCharValue = (input[i] + shiftAmount);
                    if (newCharValue > byte.MaxValue)
                    {
                        newCharValue = newCharValue - byte.MaxValue;
                    }

                    result[i] = (byte)newCharValue; // drop the first 3 bytes of int, just use the last one
                }
            }

            
            return result;
        }

        static void CesarDecrypt()
        {
            Console.WriteLine("Cesar Cipher Decryption");
            
            var userInput = "";
            var key = 0;
            do
            {
                Console.Write("Please enter your shifted amount (or X to cancel):");
                userInput = Console.ReadLine()?.ToLower().Trim();
                if (userInput != "x")
                {
                    if (int.TryParse(userInput, out var userValue))
                    {
                        key = userValue % 255;
                        if (key == 0)
                        {
                            Console.WriteLine("multiples of 255 is no cipher, this would not do anything!");
                        }
                        else
                        {
                            Console.WriteLine($"Cesar key is: {key}");
                        }
                    }
                }

            } while (key == 0 && userInput != "x");

            if (userInput == "x") return;
            
            Console.Write("Please enter your encrypted text:");
            var encryptedText = Console.ReadLine();
            if (encryptedText != null)
            {
                Console.WriteLine($"length of text: {encryptedText.Length}");

                var encryptedDefText = Base64Decode(encryptedText);

                ShowEncoding(encryptedDefText, Encoding.Default);

                var decryptedBytes = CesarDecryptString(encryptedDefText, (byte) key, Encoding.Default);
                
                string result = System.Text.Encoding.UTF8.GetString(decryptedBytes);
                
                Console.WriteLine("Decrypted text : " + result);
            }
            
            else
            {
                Console.WriteLine("Plaintext is null!");
            }
            
        }
        
        // static string Base64Decode(string base64EncodedData) {
        //     var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
        //     return System.Text.Encoding.Default.GetString(base64EncodedBytes);
        // }
        
        static byte[] CesarDecryptString(string input, byte shiftAmount, Encoding encoding)
        {
            var inputBytes = encoding.GetBytes(input);
            return CesarDecrypt(inputBytes, shiftAmount);
        }

        static byte[] CesarDecrypt(byte[] input, byte shiftAmount)
        {
            var result = new byte[input.Length];


            if (shiftAmount == 0)
            {
                // no shifting needed, just create deep copy
                for (var i = 0; i < input.Length; i++)
                {
                    result[i] = input[i];
                }
            }
            else
            {
                for (int i = 0; i < input.Length; i++)
                {
                    var newCharValue = (input[i] - shiftAmount);
                    if (newCharValue > byte.MaxValue)
                    {
                        newCharValue = newCharValue + byte.MaxValue;
                    }

                    result[i] = (byte) newCharValue; // drop the first 3 bytes of int, just use the last one
                }
            }

            return result;
            
        }

        // static void DecryptCeasar()
        // {
        //     
        //     var userInput = "";
        //     var key = 0;
        //     do
        //     {
        //         Console.Write("Please enter your shift amount (or X to cancel):");
        //         userInput = Console.ReadLine()?.ToLower().Trim();
        //         if (userInput != "x")
        //         {
        //             if (int.TryParse(userInput, out var userValue))
        //             {
        //                 key = userValue % 255;
        //                 if (key == 0)
        //                 {
        //                     Console.WriteLine("multiples of 255 is no cipher, this would not do anything!");
        //                 }
        //                 else
        //                 {
        //                     Console.WriteLine($"Cesar key is: {key}");
        //                 }
        //             }
        //         }
        //
        //     } while (key == 0 && userInput != "x");
        //
        //     if (userInput == "x") return;
        //     
        //     Console.Write("Please enter your encoded text:");
        //     var encodedText = Console.ReadLine();
        //     if (encodedText != null)
        //     {
        //         Console.WriteLine($"length of text: {encodedText.Length}");
        //         
        //         //var encodedBytes = Convert.FromBase64String(encodedText);
        //
        //         //string plainText = Encoding.UTF8.GetString(encodedBytes);
        //         
        //         //var encryptedBytes = CesarEncryptString(plainText, (byte) key, Encoding.Default);
        //
        //         var encodedByptes = DecodeString(encodedText, (byte) key);
        //         
        //         Console.Write("Encrypted bytes: ");
        //         foreach (var encryptedByte in encryptedBytes)
        //         {
        //             Console.Write(encryptedByte + " ");
        //         }
        //
        //         Console.WriteLine("base64: " + System.Convert.ToBase64String(encryptedBytes));
        //
        //     }
        //     else
        //     {
        //         Console.WriteLine("Encoded text is null!");
        //     }
        //     
        // }
        

        static void Vigenere()
        {
            Console.WriteLine("Vigenere");

            var userKey = "";
            var key = "";
            do
            {
                Console.Write("Please enter your key (or X to cancel):");
                userKey = Console.ReadLine()?.ToUpper().Trim();
                if (userKey != "X")
                {
                    key = userKey;
                    if (key != null)
                    {
                        Console.WriteLine($"Vigenere key is: {key}");
                    }
                    else
                    {
                        Console.WriteLine("cannot be empty");
                    }
                }

            } while (key == null && userKey != "X");

            if (userKey == "X") return;
            
 
            Console.Write("Please enter your plaintext:");
            var plainText = Console.ReadLine()?.ToUpper();
            if (plainText != null)
            {
                Console.WriteLine($"length of text: {plainText.Length}");
                
                ShowEncoding(plainText, Encoding.Default);
                
                var encryptedBytes = VigenereEncryptString(plainText, userKey, Encoding.Default);
                
                Console.WriteLine("base64: " + System.Convert.ToBase64String(encryptedBytes));
                
            }
            else
            {
                Console.WriteLine("Plaintext is null!");
            }

        }
        
        
        static byte[] VigenereEncryptString(string input, string useKey, Encoding encoding)
        {
            var inputBytes = encoding.GetBytes(input);
            return encryptByteVigenere(inputBytes, useKey);
        }
        
        static byte[] encryptByteVigenere(byte[] plaintext, string key)
        {
            byte[] result= new byte[plaintext.Length];
            int IndexOfKey = 0;
            int LengthOfKey = key.Length;
            for (int i = 0; i < plaintext.Length; i++)
            {
                IndexOfKey = IndexOfKey % LengthOfKey;
                int shift = (int)key[IndexOfKey] - 65;
                result[i] = (byte)(((int)plaintext[i] + shift) % 255);
                IndexOfKey++;
            }
            return result;
        }

        static void VigenereDecrypt()
        {
            Console.WriteLine("Vigenere Decrypt");
            
            var userKey = "";
            var key = "";
            do
            {
                Console.Write("Please enter your key (or X to cancel):");
                userKey = Console.ReadLine()?.ToUpper().Trim();
                if (userKey != "X")
                {
                    key = userKey;
                    if (key != null)
                    {
                        Console.WriteLine($"Vigenere key is: {key}");
                    }
                    else
                    {
                        Console.WriteLine("cannot be empty");
                    }
                }

            } while (key == null && userKey != "X");

            if (userKey == "X") return;
            
            Console.Write("Please enter your encrypted text:");
            var encryptedText = Console.ReadLine();
            if (encryptedText != null)
            {
                Console.WriteLine($"length of text: {encryptedText.Length}");

                var encryptedDefText = Base64Decode(encryptedText);

                ShowEncoding(encryptedDefText, Encoding.Default);

                var decryptedBytes = VigenereDecryptString(encryptedDefText, key, Encoding.Default);
                
                string result = System.Text.Encoding.UTF8.GetString(decryptedBytes);


                Console.WriteLine("Decrypted text : " + result);
            }
            
            else
            {
                Console.WriteLine("Plaintext is null!");
            }
            
        }
        
        static string Base64Decode(string base64EncodedData) {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.Default.GetString(base64EncodedBytes);
        }
        
        static byte[] VigenereDecryptString(string input, string useKey, Encoding encoding)
        {
            var inputBytes = encoding.GetBytes(input);
            return decryptByteVigenere(inputBytes, useKey);
        }
        
        static byte[] decryptByteVigenere(byte[] encryptedText, string key)
        {
            byte[] result = new byte[encryptedText.Length];
            int IndexOfKey = 0;
            int LengthOfKey = key.Length;
            for (int i = 0; i < encryptedText.Length; i++)
            {
                IndexOfKey = IndexOfKey % LengthOfKey;
                int shift = (int)key[IndexOfKey] - 65;
                result[i]= (byte)(((int)encryptedText[i] + 256 - shift) % 256);
                IndexOfKey++;
            }
            return result;
        }

        static void Rsa()
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
                //ShowEncoding(messageStr, Encoding.Default);
                var encryptedBytes = RsaEncryptString(messageStr, (byte) e, (byte) n, Encoding.Default);
                
                
                
                string converted = Encoding.UTF8.GetString(encryptedBytes, 0, encryptedBytes.Length);
                
                Console.WriteLine($"{converted}");
                
            }
            else
            {
                Console.WriteLine("Message is null");
            }
        }
        
        static byte[] RsaEncryptString(string input, byte a, byte b, Encoding encoding)
        {
            var inputBytes = encoding.GetBytes(input);
            return RsaEncrypt(inputBytes, a, b);
        }

        static byte[] RsaEncrypt(byte[] input, byte secret, byte modulus)
        {
            var result = new byte[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                var newCharValue = input[i] % modulus;
                int c = 1;
                for (int j = 1; j < secret; j++)
                {
                    newCharValue = (newCharValue * c % modulus);
                }

                result[i] = (byte) newCharValue;
            }

            return result;
        }

        static ulong GCD(ulong a, ulong b)
        {
            if (a == 0) return b;
            return GCD(b % a, a);
        }
        
        static void ShowEncoding(string text, Encoding encoding)
        {
            Console.WriteLine(encoding.EncodingName);
            
            Console.Write("Preamble ");
            foreach (var preambleByte in encoding.Preamble)
            {
                Console.Write(preambleByte + " ");
            }
            Console.WriteLine();
            for (int i = 0; i < text.Length; i++)
            {
                Console.Write($"{text[i]} "); 
                foreach (var byteValue in encoding.GetBytes(text.Substring(i,1)))
                {
                    Console.Write(byteValue + " ");
                }
            }

            Console.WriteLine();
        }

    }
}
