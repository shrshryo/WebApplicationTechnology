using System;

namespace Domain
{
    class Rsa
    {
        public int Id { get; set; }
        public int FirstPrimeNum { get; set; }
        public int SecondPrimeNum { get; set; }
        
        public string MessageString { get; set; }
        
        public string CipherTextString { get; set; }
    }
}