using Payments.Entities.Entities;

namespace Payments.Entities.DTOs
{
    public class PaymentShowDto : Payment
    {
        public Booking[] Details { get; set; }
    }
}
