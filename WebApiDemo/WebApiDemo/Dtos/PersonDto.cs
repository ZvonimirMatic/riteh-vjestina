using WebApiDemo.Model;

namespace WebApiDemo.Dtos;

public class PersonDto
{
    public required string Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required DateTime Birthday { get; set; }

    public static PersonDto FromModel(Person person)
    {
        return new PersonDto
        {
            Id = person.Id,
            FirstName = person.FirstName,
            LastName = person.LastName,
            Birthday = person.Birthday,
        };
    }
}
