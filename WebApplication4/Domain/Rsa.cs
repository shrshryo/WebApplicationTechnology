using System;
using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class Rsa
    {
        public int Id { get; set; }
        public int FirstPrimeNum { get; set; }
        public int SecondPrimeNum { get; set; }
        public string MessageString { get; set; }
        public string CipherTextString { get; set; }
        public string UserId { get; set; }
        
        public IdentityUser User { get; set; }
    }
}