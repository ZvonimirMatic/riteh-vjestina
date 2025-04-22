namespace WebApiDemo.Dtos;

public record UpdatePersonDto
{
    public required string FirstName { get; init; }

    public required string LastName { get; set; }

    public DateTime Birthday { get; set; }
}

//public record UpdatePersonDto(
//    string FirstName,
//    string LastName,
//    DateTime BirthDay);
