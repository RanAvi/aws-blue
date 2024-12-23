


using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using System.Text.Json;



var ctc=new CancellationTokenSource();
var sqsClient = new AmazonSQSClient(RegionEndpoint.ILCentral1);
//you can find the url by the name of the queue
var queueUrlResponse = await sqsClient.GetQueueUrlAsync("customers");

var reciveMessageRequest = new ReceiveMessageRequest
{
    QueueUrl = queueUrlResponse.QueueUrl,
    MaxNumberOfMessages = 10, // Receive up to 10 messages at once
    WaitTimeSeconds = 20,     // Enable long polling (20 seconds)
    VisibilityTimeout = 30, // Make the message invisible for 30 seconds after receiving
    AttributeNames= new List<string> { "All" },
    MessageAttributeNames= new List<string> { "All" }
    
};

while (!ctc.IsCancellationRequested)
{

  var response= await sqsClient.ReceiveMessageAsync(reciveMessageRequest,ctc.Token);
 
    if (response.Messages != null && response.Messages.Count > 0)
    {
        foreach (var message in response.Messages)
        {

            Console.WriteLine($"Message :[{message.MessageId}]");
            Console.WriteLine($"Message :[{message.Body}]");

            await DeleteMessageAsync(sqsClient, queueUrlResponse, message);

        }

    }

   await Task.Delay(3000,ctc.Token);
    
}

static async Task DeleteMessageAsync(AmazonSQSClient sqsClient, GetQueueUrlResponse queueUrlResponse, Message? message)
{
    await sqsClient.DeleteMessageAsync(queueUrlResponse.QueueUrl, message.ReceiptHandle);
}