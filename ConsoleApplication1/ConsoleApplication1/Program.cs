using System;
using System.Text;

namespace ConsoleApp01
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Practice 01 Ryo Shiraishi 195557IVSB");

            var userInput = "";
            do
            {
                Console.WriteLine();
                Console.WriteLine("1) Vigenere cipher");
                Console.WriteLine("2) Vigenere decrypt");
                Console.WriteLine("X) Exit");
                Console.Write(">");

                userInput = Console.ReadLine()?.ToLower();

                switch (userInput)
                {
                    case "1":
                        Vigenere();
                        break;
                    case "2":
                        VigenereDecrypt();
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
                var plainTextBin = StringToBinary(plainText);
                Console.WriteLine($"Your plaintext in binary format: {plainTextBin}");

                var encryptedBytes = VigenereEncryptString(plainText, userKey, Encoding.Default);

                Console.WriteLine("base64: " + System.Convert.ToBase64String(encryptedBytes));

                string encryptedStr = System.Text.Encoding.UTF8.GetString(encryptedBytes);

                var encryptedBin = StringToBinary(encryptedStr);
                Console.WriteLine($"Your encrypted text in binary format is: {encryptedBin}");




            }
            else
            {
                Console.WriteLine("Plaintext is null!");
            }

        }

        static string StringToBinary(string data)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in data.ToCharArray())
            {
                sb.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
            }

            return sb.ToString();
        }


        static byte[] VigenereEncryptString(string input, string useKey, Encoding encoding)
        {
            var inputBytes = encoding.GetBytes(input);
            return encryptByteVigenere(inputBytes, useKey);
        }

        static byte[] encryptByteVigenere(byte[] plaintext, string key)
        {
            byte[] result = new byte[plaintext.Length];
            int IndexOfKey = 0;
            int LengthOfKey = key.Length;
            for (int i = 0; i < plaintext.Length; i++)
            {
                IndexOfKey = IndexOfKey % LengthOfKey;
                int shift = (int) key[IndexOfKey] - 65;
                result[i] = (byte) (((int) plaintext[i] + shift) % 255);
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
                Console.Write("Please enter your key used to encrypt the message (or X to cancel):");
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

            Console.Write("Please enter your encrypted text which is the base64:");
            var encryptedText = Console.ReadLine();
            if (encryptedText != null)
            {
                Console.WriteLine($"length of text: {encryptedText.Length}");

                var encryptedDefText = Base64Decode(encryptedText);

                // ShowEncoding(encryptedDefText, Encoding.Default);

                var decryptedBytes = VigenereDecryptString(encryptedDefText, key, Encoding.Default);

                string result = System.Text.Encoding.UTF8.GetString(decryptedBytes);
                string resultBin = StringToBinary(result);


                Console.WriteLine("Decrypted text : " + result);
                Console.WriteLine("Decrypted text in binary format : " + resultBin);
            }

            else
            {
                Console.WriteLine("Plaintext is null!");
            }

        }

        static string Base64Decode(string base64EncodedData)
        {
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
                int shift = (int) key[IndexOfKey] - 65;
                result[i] = (byte) (((int) encryptedText[i] + 256 - shift) % 256);
                IndexOfKey++;
            }

            return result;
        }
    }
    
}
