namespace Domain
{
    public class Rsa
    {
        public int Id { get; set; }
        public int FirstPrimeNum { get; set; }
        public int SecondPrimeNum { get; set; }
        
        public int Message { get; set; }
        public int CipherText { get; set; }
        
        public string MessageString { get; set; }
        
        public string CipherTextString { get; set; }
    }
}