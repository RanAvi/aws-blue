
using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using SqsPublisher.Contract;
using System.Text.Json;

var sqsClient = new AmazonSQSClient(RegionEndpoint.ILCentral1);

var customer = new CustomerCreated
{
    Id=Guid.NewGuid(),
    Email="Ravitaln@gmail.com",
    FullName="Ran Avital",
    DateOfBirth=new DateTime(1993, 1, 1),
    GithubUserName="Ranai"

};

//you can find the url by the name of the queue
var queueUrlResponse = await sqsClient.GetQueueUrlAsync("customers");

var sendMessageRequest = new SendMessageRequest
{
    QueueUrl=queueUrlResponse.QueueUrl,
    MessageBody=JsonSerializer.Serialize(customer),
    MessageAttributes=new Dictionary<string, MessageAttributeValue>
    {
        {
            "MessageType", new MessageAttributeValue{ 
                DataType="String",
                StringValue=nameof(CustomerCreated)}
        }

    }

};

var response= await sqsClient.SendMessageAsync(sendMessageRequest);

Console.WriteLine();


