using EfCoreDemo;
using EfCoreDemo.Dtos;
using EfCoreDemo.Model;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PokemonDbContext>(
    options => options.UseNpgsql(
        builder.Configuration.GetConnectionString("Default"))
        .EnableSensitiveDataLogging(true));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/pokemon", async (PokemonDbContext context, string? type) =>
{
    //IQueryable<Pokemon> pokemonsQuery = context
    //    .Pokemons;
    //    //.Include(x => x.Trainer);

    //if (type is not null)
    //{
    //    var pokemonType = Enum.Parse<PokemonType>(type);

    //    pokemonsQuery = pokemonsQuery
    //        .Where(x => x.Type == pokemonType);
    //}

    //pokemonsQuery = pokemonsQuery
    //    .OrderBy(x => x.Name);

    //var pokemons = await pokemonsQuery
    //    .ToListAsync();

    //var pokemonDtos = pokemons
    //    .Select(x => new PokemonDto(
    //        x.Id,
    //        x.Name,
    //        x.Description,
    //        x.Type.ToString(),
    //        x.TrainerId));

    IQueryable<Pokemon> pokemonsQuery = context
        .Pokemons;

    if (type is not null)
    {
        var pokemonType = Enum.Parse<PokemonType>(type);

        pokemonsQuery = pokemonsQuery
            .Where(x => x.Type == pokemonType);
    }

    pokemonsQuery = pokemonsQuery
        .OrderBy(x => x.Name);

    var pokemonDtos = await pokemonsQuery
        .Select(x => new PokemonDto(
            x.Id,
            x.Name,
            x.Description,
            x.Type.ToString(),
            x.TrainerId))
        .ToListAsync();

    return Results.Ok(pokemonDtos);
})
    .WithName("GetAllPokemons")
    .WithOpenApi();

app.MapGet("/pokemon/{id}", async (PokemonDbContext context, int id) =>
{
    var pokemon = await context
        .Pokemons
        //.Include(x => x.Trainer)
        .FirstOrDefaultAsync(x => x.Id == id);

    if (pokemon is null)
    {
        return Results.NotFound(id);
    }

    return Results.Ok(new PokemonDto(
        pokemon.Id,
        pokemon.Name,
        pokemon.Description,
        pokemon.Type.ToString(),
        pokemon.TrainerId));
})
    .WithName("GetPokemonById")
    .WithOpenApi();

app.MapPost("/pokemon", async (PokemonDbContext context, CreatePokemonDto dto) =>
{
    var newPokemon = new Pokemon
    {
        Name = dto.Name,
        Description = dto.Description,
        Type = Enum.Parse<PokemonType>(dto.Type),
        TrainerId = dto.TrainerId,
    };

    context.Add(newPokemon);

    await context.SaveChangesAsync();

    return Results.Ok(new PokemonDto(
        newPokemon.Id,
        newPokemon.Name,
        newPokemon.Description,
        newPokemon.Type.ToString(),
        newPokemon.TrainerId));
})
    .WithName("AddPokemon")
    .WithOpenApi();

app.MapPut("/pokemon/{id}", async (PokemonDbContext context, CreatePokemonDto dto, int id) =>
{
    var existingPokemon = await context
        .Pokemons
        .FindAsync(id);

    if (existingPokemon is null)
    {
        return Results.NotFound(id);
    }

    existingPokemon.Name = dto.Name;
    existingPokemon.Description = dto.Description;
    existingPokemon.Type = Enum.Parse<PokemonType>(dto.Type);
    existingPokemon.TrainerId = dto.TrainerId;

    await context.SaveChangesAsync();

    return Results.Ok(new PokemonDto(
        existingPokemon.Id,
        existingPokemon.Name,
        existingPokemon.Description,
        existingPokemon.Type.ToString(),
        existingPokemon.TrainerId));
})
    .WithName("EditPokemon")
    .WithOpenApi();

app.MapDelete("/pokemon/{id}", async (PokemonDbContext context, int id) =>
{
    var existingPokemon = await context
        .Pokemons
        .FindAsync(id);

    if (existingPokemon is null)
    {
        return Results.NotFound(id);
    }

    context.Remove(existingPokemon);

    await context.SaveChangesAsync();

    return Results.Ok();
})
    .WithName("RemovePokemon")
    .WithOpenApi();

app.Run();
