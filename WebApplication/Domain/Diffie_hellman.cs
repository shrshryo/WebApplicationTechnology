using System;
using System.Collections;
using System.Collections.Generic;

namespace Domain
{
    public class Diffie_hellman
    {
        public int Id { get; set; }
        public int PrimeNum { get; set; }
        public int CommonNum { get; set; }
        public int SecretOne { get; set; }
        public int SecretTwo { get; set; }

        public ICollection<Diffie_hellman_result> Result { get; set; }
    }
}