using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RSA = System.Security.Cryptography.RSA;

namespace WebApp01.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<RSA> RSA { get; set; }
        public DbSet<RSA_result> RsaResults { get; set; }
        public DbSet<Diffie_hellman> DiffieHellmans { get; set; }
        public DbSet<Diffie_hellman_result> DiffieHellmansResults { get; set; }
        
    }
}