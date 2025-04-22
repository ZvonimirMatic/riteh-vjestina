namespace EfCoreDemo.Dtos;

public record PokemonDto(
    int Id,
    string Name,
    string? Description,
    string Type,
    int TrainerId);
