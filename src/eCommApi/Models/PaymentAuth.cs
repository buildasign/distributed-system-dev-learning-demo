namespace EcommApi.Models
{
    public class PaymentAuth
    {
        public bool Authorized { get; set; }
        public string AuthCode { get; set; } 
    }
}