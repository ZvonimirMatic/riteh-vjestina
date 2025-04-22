namespace WebApiDemo.Model;

public class Person
{
    public string Id { get; private set; } = Guid.NewGuid().ToString();

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required DateTime Birthday { get; set; }
}
