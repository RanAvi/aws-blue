using Amazon.SQS;
using Amazon.SQS.Model;
using Customers.Api.Domain;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Customers.Api.Messaging
{
    public interface ISqsMessenger
    {
        Task<SendMessageResponse> SendMessageAsync<T>(T message)
            where T : IMessage; // This adds a constraint ensuring that T must implement IMessage
    }

    public class SqsMessenger : ISqsMessenger
    {
        private readonly IAmazonSQS _sqsClient;
        private string _queueUrl;
        private readonly  IOptions<QueueSettings> _queueOptions;

        public SqsMessenger(IAmazonSQS sqsClient, IOptions<QueueSettings> queueOptions)
        {
            _sqsClient = sqsClient;
            _queueOptions=queueOptions;
        }

        public async Task<SendMessageResponse> SendMessageAsync<T>(T message) where T : IMessage
        {
            _queueUrl =  await GetQueueurlAsync();
  
            // Convert the message to a string (or serialize to JSON if needed)
            var messageBody = JsonSerializer.Serialize(message);

            var request = new SendMessageRequest
            {
                QueueUrl = _queueUrl,
                MessageBody = messageBody,
                MessageAttributes=new Dictionary<string, MessageAttributeValue>
            {
                {
                    "MessageType", new MessageAttributeValue{
                        DataType="String",
                        StringValue=typeof(T).Name}
                }

              }

            };

            // Send the message using the SQS client
            return await _sqsClient.SendMessageAsync(request);
        }

        private async Task<string> GetQueueurlAsync()
        {
            if (_queueUrl is not null) return _queueUrl;

            var queueUrl= await _sqsClient.GetQueueUrlAsync("customers");
            return queueUrl.QueueUrl;
        }

     
    }






}


