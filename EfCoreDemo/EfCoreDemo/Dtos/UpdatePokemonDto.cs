namespace EfCoreDemo.Dtos;

public record UpdatePokemonDto(
    string Name,
    string? Description,
    string Type,
    int TrainerId);
