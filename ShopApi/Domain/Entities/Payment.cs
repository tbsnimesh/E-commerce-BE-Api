namespace ShopApi.Domain.Entities
{
    public class Payment
    {

        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Method { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";
        public string? TransactionId { get; set; }
        public DateTime? PaidAt { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

    }
}
