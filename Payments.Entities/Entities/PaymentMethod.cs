
namespace Payments.Entities.Entities
{
    public class PaymentMethod
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Token { get; set; }
        public string LastDigits { get; set; }
        
    }
}
