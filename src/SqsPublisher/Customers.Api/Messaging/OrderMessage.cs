namespace Customers.Api.Messaging
{
    public class OrderMessage : IMessage
    {
        public string MessageId { get; set; }
        public DateTime Timestamp { get; set; }
        public string OrderId { get; set; }
        public decimal Amount { get; set; }
    }
}


