using FluentValidation;
using WebApiDemo.Dtos;

namespace WebApiDemo.Validators;

public class CreatePersonDtoValidator : AbstractValidator<CreatePersonDto>
{
    public CreatePersonDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("First name should not be empty and can have max 50 chars.");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Birthday)
            .LessThanOrEqualTo(DateTime.Now.AddYears(-18));
    }
}
