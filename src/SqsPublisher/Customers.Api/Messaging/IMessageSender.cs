
namespace Customers.Api.Messaging
{
    public interface IMessageSender
    {
        Task SendOrderMessage();
        Task SendTextMessage();
    }
}