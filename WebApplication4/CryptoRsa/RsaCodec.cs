using System;
using System.Text;

namespace CryptoRsa
{
    public static class RsaCodec
    {
        public static string calc(ulong FirstPrime, ulong SecondPrime, string Message)
        {
            ulong n = FirstPrime * SecondPrime;
            ulong m = (FirstPrime - 1) * (SecondPrime - 1);

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
            return RsaEncrypt(Message, e, n);
        }

        public static string RsaEncrypt(string input, ulong a, ulong b)
        {
            string result = "";
            var inputBytes = Encoding.UTF8.GetBytes(input);
            int i;
            for (i = 0; i < input.Length-1; i++)
            {
                result += RsaCalc(input[i], a, b) + " ";
            }
            result += result += RsaCalc(input[i], a, b);
            return result;
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