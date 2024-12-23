namespace Customers.Api.Messaging
{
    public interface IMessage
    {
        // You can add any common properties or methods that all messages should have.
        // For example, a common "MessageId" or "Timestamp" might be useful across messages.
        string MessageId { get; set; }
        DateTime Timestamp { get; set; }
    }
}
