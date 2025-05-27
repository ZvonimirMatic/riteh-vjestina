using FluentValidation;

namespace LibraryApp.Server.Dtos;

public record CreateAuthorDto(
    string Name)
{
    public class Validator : AbstractValidator<CreateAuthorDto>
    {
        public Validator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }
}
