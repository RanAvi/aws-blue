namespace Customers.Api.Messaging
{
    public class TextMessage : IMessage
    {
        public string MessageId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Content { get; set; }
    }
}


