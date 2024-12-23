
using Amazon.SQS;
using Amazon.SQS.Model;
using Customer.consumer.Messaging;
using MediatR;
using System.Text.Json;


namespace Customer.consumer.Background;

public class QueueConsumerService : BackgroundService
{
    private readonly IAmazonSQS _sqlClient;
    private const string queueName = "customers";
    private readonly IMediator _mediator;
    private readonly ILogger<QueueConsumerService> _logger;

    public QueueConsumerService(IAmazonSQS sqlClient, IMediator mediator, ILogger<QueueConsumerService>logger)
    {
        _sqlClient=sqlClient;
        _mediator=mediator;
        _logger=logger;
    }
    protected override async Task ExecuteAsync(CancellationToken ctc)
    {
        var queueUrlResponse = await _sqlClient.GetQueueUrlAsync("customers");

        var reciveMessageRequest = new ReceiveMessageRequest
        {
            QueueUrl = queueUrlResponse.QueueUrl,
            MaxNumberOfMessages = 1, // Receive up to 10 messages at once
            WaitTimeSeconds = 20,     // Enable long polling (20 seconds)
            VisibilityTimeout = 30, // Make the message invisible for 30 seconds after receiving
            AttributeNames= new List<string> { "All" },
            MessageAttributeNames= new List<string> { "All" }

        };

        while (!ctc.IsCancellationRequested)
        {

            var response = await _sqlClient.ReceiveMessageAsync(reciveMessageRequest, ctc);

            if (response.Messages != null && response.Messages.Count > 0)
            {
                foreach (var message in response.Messages)
                {
                    var messageType = message.MessageAttributes["MessageType"].StringValue;

                    var type=Type.GetType($"Customer.consumer.Messaging.{messageType}");
                    if(messageType == null)
                    {
                        _logger.LogWarning("Uknown message type:{MessageType}", messageType);
                        continue;
                    }
                    var typedMessage =(ISqsMessage)JsonSerializer.Deserialize(message.Body,type)!;

                    try
                    {
                        await _mediator.Send(typedMessage,ctc);

                    }
                    catch(Exception ex) 
                    {
                        _logger.LogError(ex,"Message failed during processing");
                        continue;

                    }





                    //switch (messageType)
                    //{
                    //    case nameof(CustomerCreated):
                    //        var createdCustomer=JsonSerializer.Deserialize<CustomerCreated>(message.Body);

                    //        break;
                    //     case nameof(CustomerUpdated):
                    //        break;
                    //    case nameof(CustomerDeleted): 
                    //        break;
                    //        default:
                    //     throw new Exception($"MessageType is unkown");

                    //}

                    await _sqlClient.DeleteMessageAsync(queueUrlResponse.QueueUrl, message.ReceiptHandle);
                }

            }

            await Task.Delay(1000, ctc);

        }


    }


}
