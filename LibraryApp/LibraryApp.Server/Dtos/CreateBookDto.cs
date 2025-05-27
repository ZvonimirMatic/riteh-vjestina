using FluentValidation;
using LibraryApp.Server.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Server.Dtos;

public record CreateBookDto(
    string Title,
    int AuthorId)
{
    public class Validator : AbstractValidator<CreateBookDto>
    {
        public Validator(LibraryContext context)
        {
            RuleFor(x => x.Title)
                .NotEmpty();

            RuleFor(x => x.AuthorId)
                .MustAsync(async (auhtorId, cancellationToken) =>
                {
                    return await context
                        .Set<Author>()
                        .AnyAsync(x => x.Id == auhtorId, cancellationToken);
                })
                .WithMessage("Author does not exist.");
        }
    }
}
