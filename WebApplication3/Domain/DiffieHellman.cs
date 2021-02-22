using System;
using Microsoft.AspNetCore.Identity;

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
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}