namespace eComm.Domain.Models
{
    public class CreatePayment
    {
        public string CartNumber { get; set; }
        public decimal Amount { get; set; } 
    }
}