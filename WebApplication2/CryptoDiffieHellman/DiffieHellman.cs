using System;
//console app
namespace CryptoDiffieHellman
{
    public static class DiffieHellman
    {
        
        public static ulong calc(ulong prime, ulong secret, ulong common)
        {
            if (secret == 0)
            {
                return 1;
            }
            else if (((ulong) (uint) secret) % 2 == 0)
            {
                ulong d = calc(prime, ((ulong) (uint) secret) / 2, common);
                ulong result = (((ulong) (uint) d) * ((ulong) (uint) d) % ((ulong) (uint) prime));
                result >>= 64;
                return result;
            }
            else
            {
                ulong result =
                    (((((ulong) (uint) common) % (((ulong) (uint) prime)) * calc(prime, secret - 1, common)) %
                      (((ulong) (uint) prime))));
                result >>= 64;
                return result;
            }
        }

        
    }
}