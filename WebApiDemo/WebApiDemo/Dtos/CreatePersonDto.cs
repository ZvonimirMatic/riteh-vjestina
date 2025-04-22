using System.ComponentModel.DataAnnotations;

namespace WebApiDemo.Dtos;

public record CreatePersonDto
{
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public DateTime Birthday { get; set; }
}
