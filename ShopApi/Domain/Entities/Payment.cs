 using ShopApi.Domain.Enums;
namespace ShopApi.Domain.Entities
{
    public class Payment
    {

        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string Method { get; set; } = string.Empty;
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
        public string? TransactionId { get; set; }
        public DateTime? PaidAt { get; set; }

        public Guid OrderId { get; set; }
        public Order Order { get; set; } = null!;

    }
}
