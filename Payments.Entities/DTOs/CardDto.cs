
namespace Payments.Entities.DTOs
{
    public class CardDto
    {
        public string Number { get; set; }
        public int ExpMonth { get; set; }
        public int ExpYear { get; set; }
        public string Cvv { get; set; }
    }
}
