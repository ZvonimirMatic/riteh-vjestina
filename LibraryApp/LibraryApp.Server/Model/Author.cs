using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Server.Model;

public class Author
{
    public int Id { get; set; }

    public required string FullName { get; set; }

    public ICollection<Book> Books { get; set; } = new List<Book>();

    public class Config : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {

        }
    }
}
