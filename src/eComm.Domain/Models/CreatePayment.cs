namespace eComm.Domain.Models
{
    public class CreatePayment
    {
        public string CardNumber { get; set; }
        public string ExpDate { get; set; }
        public string Cvv { get; set; }
    }
}