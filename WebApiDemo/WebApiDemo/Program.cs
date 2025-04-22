using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Dtos;
using WebApiDemo.Repositories;
using WebApiDemo.Repositories.Implementations;
using WebApiDemo.Services;
using WebApiDemo.Services.Implementations;
using WebApiDemo.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IPeopleRepository, PeopleRepository>();
builder.Services.AddScoped<IPeopleService, PeopleService>();

builder.Services.AddValidatorsFromAssemblyContaining<CreatePersonDtoValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/people", (IPeopleService peopleService) =>
{
    var people = peopleService.GetPeople();
    return Results.Ok(people);
})
    .WithName("GetAllPeople")
    .Produces<IEnumerable<PersonDto>>()
    .WithSummary("Gets all people.")
    .WithOpenApi();

app.MapGet("/people/{id}", (IPeopleService peopleService, string id) =>
{
    var person = peopleService.GetPersonById(id);
    if (person is null)
    {
        return Results.NotFound(id);
    }

    return Results.Ok(person);
})
    .WithName("GetPerson")
    .Produces<PersonDto>()
    .Produces(404)
    .WithOpenApi();

app.MapPost("/people", (IPeopleService peopleService, IValidator<CreatePersonDto> validator, CreatePersonDto request) =>
{
    var validationResult = validator.Validate(request);
    if (!validationResult.IsValid)
    {
        return Results.ValidationProblem(validationResult.ToDictionary());
    }

    var createdPerson = peopleService.CreatePerson(request);
    return Results.Created($"/people/{createdPerson.Id}", createdPerson);
})
    .WithName("CreatePerson")
    .Produces<PersonDto>(201)
    .ProducesValidationProblem()
    .WithOpenApi();

app.MapPut("/people/{id}", (IPeopleService peopleService, string id, UpdatePersonDto request) =>
{
    var updatedPerson = peopleService.UpdatePerson(id, request);
    if (updatedPerson is null)
    {
        return Results.NotFound(id);
    }

    return Results.Ok(updatedPerson);
})
    .WithName("UpdatePerson")
    .Produces<PersonDto>()
    .Produces(404)
    .WithOpenApi();

app.MapDelete("/people/{id}", (IPeopleService peopleService, string id) =>
{
    var person = peopleService.GetPersonById(id);
    if (person is null)
    {
        return Results.NotFound(id);
    }

    peopleService.DeletePerson(id);
    return Results.Ok();
})
    .WithName("DeletePerson")
    .Produces(200)
    .Produces(404)
    .WithOpenApi();

app.Run();
