using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryApp.Server.Model;

public class Book
{
    public int Id { get; set; }

    public required string Title { get; set; }

    public int AuthorId { get; set; }

    public Author? Author { get; set; }

    public class Config : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {

        }
    }
}
