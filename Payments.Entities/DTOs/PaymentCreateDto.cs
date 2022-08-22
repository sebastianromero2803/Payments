
namespace Payments.Entities.DTOs
{
    public class PaymentCreateDto
    {
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public BookingDto[] Details { get; set; }
        public int PaymentMethodId { get; set; }
    }
}
