using System.Collections.Generic;

namespace Domain
{
    public class Rsa
    {
        public int Id { get; set; }
        public int FirstPrimeNum { get; set; }
        public int SecondPrimeNum { get; set; }
        public int CipherText { get; set; }

        public ICollection<DiffieHellmanResults> Result { get; set; }
    }
}