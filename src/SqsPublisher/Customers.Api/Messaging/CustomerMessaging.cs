namespace Customers.Api.Messaging;

public class CustomerCreated:IMessage
{
    public Guid Id { get; init; }
    public string Email { get; init; }
    public string FullName { get; init; }
    public DateTime DateOfBirth { get; init; }
    public string GithubUserName { get; init; }
    public string MessageId { get; set; }
    public DateTime Timestamp { get; set; }
}
public class CustomerUpdated:IMessage
{
    public Guid Id { get; init; }
    public string Email { get; init; }
    public string FullName { get; init; }
    public DateTime DateOfBirth { get; init; }
    public string GithubUserName { get; init; }
    public string MessageId { get; set; }
    public DateTime Timestamp { get; set; }
}
public class CustomerDeleted:IMessage
{
    public Guid Id { get; init; }
    public string MessageId { get; set; }
    public DateTime Timestamp { get; set; }
}










