namespace EfCoreDemo.Model;

public class Trainer
{
    public int Id {  get; set; }

    public required string Name { get; set; }

    public DateTime? Birthday { get; set; }

    public ICollection<Pokemon> Pokemons { get; set; } = new List<Pokemon>();
}
