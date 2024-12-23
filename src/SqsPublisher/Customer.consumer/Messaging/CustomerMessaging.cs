namespace Customer.consumer.Messaging;

public class CustomerCreated :ISqsMessage
{
public Guid Id { get; init; }
public string Email { get; init; }
public string FullName { get; init; }
public DateTime DateOfBirth { get; init; }
public string GithubUserName { get; init; }
public string MessageId { get; set; }
public DateTime Timestamp { get; set; }
}
public class CustomerUpdated : ISqsMessage
{
public Guid Id { get; init; }
public string Email { get; init; }
public string FullName { get; init; }
public DateTime DateOfBirth { get; init; }
public string GithubUserName { get; init; }
public string MessageId { get; set; }
public DateTime Timestamp { get; set; }
}
public class CustomerDeleted : ISqsMessage
{
public Guid Id { get; init; }
public string MessageId { get; set; }
public DateTime Timestamp { get; set; }
}

