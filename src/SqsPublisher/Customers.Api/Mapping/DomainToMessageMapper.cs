

using Customers.Api.Domain;
using Customers.Api.Messaging;


namespace Customers.Api.Mapping
{
    public static class DomainToMessageMapper
    {
        public static CustomerCreated ToCustomerCreatedMessage(this Customer customer)
        {
            return new CustomerCreated
            {
                Id = customer.Id,
                Email = customer.Email,
                GithubUserName=customer.GitHubUsername,
                FullName = customer.FullName,
                DateOfBirth = customer.DateOfBirth,
                MessageId=customer.Id.ToString(),
                Timestamp=DateTime.Now,
            };
        }

        public static CustomerUpdated ToCustomerUpdateMessage(this Customer customer)
        {
            return new CustomerUpdated
            {
                Id = customer.Id,
                Email = customer.Email,
                GithubUserName=customer.GitHubUsername,
                FullName = customer.FullName,
                DateOfBirth = customer.DateOfBirth,
                MessageId=customer.Id.ToString(),
                Timestamp=DateTime.Now,
            };
        }


  
    }
}
