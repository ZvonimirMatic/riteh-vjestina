using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace EfCoreDemo.Model;

public class Pokemon
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public PokemonType Type { get; set; }

    public int TrainerId { get; set; }

    public Trainer? Trainer { get; set; }

    //public class Config : IEntityTypeConfiguration<Pokemon>
    //{
    //    public void Configure(EntityTypeBuilder<Pokemon> builder)
    //    {
    //        builder.Property(x => x.Type)
    //            .HasConversion<string>();

    //        builder.Property(x => x.Name)
    //            .HasMaxLength(50);
    //    }
    //}
}
