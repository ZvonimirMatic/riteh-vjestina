using Microsoft.EntityFrameworkCore;

namespace EfCoreDemo.Model;

public class PokemonDbContext : DbContext
{
    public DbSet<Pokemon> Pokemons { get; set; }

    public DbSet<Trainer> Trainers { get; set; }

    public PokemonDbContext(DbContextOptions<PokemonDbContext> options) 
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        modelBuilder.Entity<Pokemon>(entityBuilder =>
        {
            entityBuilder.Property(x => x.Type)
                .HasConversion<string>();

            entityBuilder.Property(x => x.Name)
                .HasMaxLength(50);
        });
    }
}
