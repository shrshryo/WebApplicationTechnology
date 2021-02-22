namespace Domain
{
    public class RsaResults
    {
        public int Id { get; set; }
        public int Result { get; set; }
        
        public int RsaId { get; set; }
        public Rsa RSA { get; set; }
    }
}