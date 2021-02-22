namespace Domain
{
    public class DiffieHellmanResults
    {
        public int Id { get; set; }
        public int ResultValue { get; set; }
        
        public int DiffieHellmanId { get; set; }
        public DiffieHellman Diffie_Hellman { get; set; }
    }
}