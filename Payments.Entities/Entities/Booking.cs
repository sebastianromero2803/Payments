
namespace Payments.Entities.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public int PaymentId { get; set; }
        public string ContainerId { get; set; }
        public int Fee { get; set; }
    }
}
