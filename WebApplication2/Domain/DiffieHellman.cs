using System;
using System.Collections.Generic;

namespace Domain
{
    public class DiffieHellman
    {
        public int Id { get; set; }
        public int PrimeNum { get; set; }
        public int CommonNum { get; set; }
        public int SecretOne { get; set; }
        public int SecretTwo { get; set; }
        public int CipherText { get; set; }

        public ICollection<DiffieHellmanResults> Result { get; set; }
    }
}