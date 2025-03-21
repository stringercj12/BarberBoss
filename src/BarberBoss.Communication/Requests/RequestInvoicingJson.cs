using BarberBoss.Communication.Enums;

namespace BarberBoss.Communication.Requests
{
    public class RequestInvoicingJson
    {
        public string Title { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public PaymentType PaymentType { get; set; }
    }
}
