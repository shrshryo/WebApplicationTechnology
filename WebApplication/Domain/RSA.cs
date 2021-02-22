using System.Collections;
using System.Collections.Generic;

namespace Domain
{
    public class RSA
    {
        public int Id { get; set; }
        public int FirstPrimeNum { get; set; }
        public int SecondPrimeNum { get; set; }
        public int CipherText { get; set; }
        
        public ICollection<RSA_result> Result { get; set; }
    }
}