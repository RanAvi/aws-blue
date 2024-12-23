



using Amazon;
using Amazon.SQS;
using Customer.consumer.Background;
using MediatR;



var builder = WebApplication.CreateBuilder(args);


//services
builder.Services.AddSingleton<IAmazonSQS>(serviceProvider => {
    var config = new AmazonSQSConfig { RegionEndpoint = RegionEndpoint.ILCentral1 };
    return new AmazonSQSClient(config);
});

builder.Services.AddHostedService<QueueConsumerService>();
builder.Services.AddMediatR(typeof(Program));
 




var app = builder.Build();



app.Run();
