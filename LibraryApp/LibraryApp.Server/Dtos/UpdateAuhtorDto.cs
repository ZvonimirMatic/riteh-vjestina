using FluentValidation;

namespace LibraryApp.Server.Dtos;

public record UpdateAuhtorDto(
    string Name)
{
    public class Validator : AbstractValidator<UpdateAuhtorDto>
    {
        public Validator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }
}
