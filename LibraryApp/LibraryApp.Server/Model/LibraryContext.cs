using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Server.Model;

public class LibraryContext : IdentityDbContext
{
    public LibraryContext(DbContextOptions<LibraryContext> options)
        : base(options) 
    { 
    
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
