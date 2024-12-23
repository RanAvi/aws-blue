namespace Customers.Api.Messaging
{
    public class MessageSender : IMessageSender
    {
        private readonly ISqsMessenger _sqsMessenger;

        public MessageSender(ISqsMessenger sqsMessenger)
        {
            _sqsMessenger = sqsMessenger;
        }

        public async Task SendTextMessage()
        {
            var textMessage = new TextMessage
            {
                MessageId = Guid.NewGuid().ToString(),
                Timestamp = DateTime.UtcNow,
                Content = "Hello, this is a text message."
            };

            var response = await _sqsMessenger.SendMessageAsync(textMessage);
            Console.WriteLine($"Message sent with ID: {response.MessageId}");
        }

        public async Task SendOrderMessage()
        {
            var orderMessage = new OrderMessage
            {
                MessageId = Guid.NewGuid().ToString(),
                Timestamp = DateTime.UtcNow,
                OrderId = "12345",
                Amount = 250.75m
            };

            var response = await _sqsMessenger.SendMessageAsync(orderMessage);
            Console.WriteLine($"Order message sent with ID: {response.MessageId}");
        }
    }






}


