namespace EfCoreDemo.Dtos;

public record CreatePokemonDto(
    string Name,
    string? Description,
    string Type,
    int TrainerId);
