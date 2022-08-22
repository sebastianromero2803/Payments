
namespace Payments.Entities.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public string PaymentToken { get; set; }
    }
}
