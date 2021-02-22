using System;

namespace CryptoDiffieHellman
{
    public static class DiffieHellmanConsole
    {
        public static ulong calc(ulong key, ulong secret, ulong prime)
        {
            if (secret == 0)
            {
                return 1;
            }
            else if (((ulong) (uint) secret) % 2 == 0)
            {
                ulong d = calc(key, ((ulong) (uint) secret) / 2, prime);
                ulong result = (((ulong) (uint) d) * ((ulong) (uint) d) % ((ulong) (uint) prime));
                result >>= 64;
                return result;
            }
            else
            {
                ulong result =
                    (((((ulong) (uint) key) % (((ulong) (uint) prime)) * calc(key, secret - 1, prime)) %
                      (((ulong) (uint) prime))));
                result >>= 64;
                return result;
            }
        }
    }
}