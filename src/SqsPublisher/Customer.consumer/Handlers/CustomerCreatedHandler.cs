using Customer.consumer.Messaging;
using MediatR;

namespace Customer.consumer.Handlers
{

    public class CustomerCreatedHandler : IRequestHandler<CustomerCreated>
    {
        private readonly ILogger<CustomerCreatedHandler> logger;

        public CustomerCreatedHandler(ILogger<CustomerCreatedHandler> logger)
        {
            this.logger=logger;
        }

        public Task<Unit> Handle(CustomerCreated request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"{request.Id}");
            return Unit.Task; 

        }
    }

    public class CustomerDeletedHandler : IRequestHandler<CustomerDeleted>
    {
        private readonly ILogger<CustomerDeletedHandler> logger;

        public CustomerDeletedHandler(ILogger<CustomerDeletedHandler> logger)
        {
            this.logger=logger;
        }

        public Task<Unit> Handle(CustomerDeleted request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"{request.Id}");
            return Unit.Task;
        }
    }


    public class CustomerUpdatedHandler : IRequestHandler<CustomerUpdated>
    {
        private readonly ILogger<CustomerUpdatedHandler> logger;

        public CustomerUpdatedHandler(ILogger<CustomerUpdatedHandler> logger)
        {
            this.logger=logger;
        }

        public Task<Unit> Handle(CustomerUpdated request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"{request.Email}");
            return Unit.Task;
        }
    }
}
