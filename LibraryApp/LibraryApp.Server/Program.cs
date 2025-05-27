using FluentValidation;
using LibraryApp.Server.Dtos;
using LibraryApp.Server.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LibraryContext>(
    options => options.UseNpgsql(
        builder.Configuration.GetConnectionString("Default")));

builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<LibraryContext>();

builder.Services.AddValidatorsFromAssemblyContaining<CreateAuthorDto>();

builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder
            .WithOrigins("https://localhost:61417")
            .AllowCredentials()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.MapIdentityApi<IdentityUser>();

#region Author

app.MapGet("/api/author", async (LibraryContext context, CancellationToken cancellationToken) =>
{
    var authors = await context
        .Set<Author>()
        .Select(x => new AuthorDto(x.Id, x.FullName))
        .ToListAsync(cancellationToken);

    return Results.Ok(authors);
})
    .WithOpenApi()
    .WithName("GetAllAuthors")
    .Produces<IEnumerable<AuthorDto>>()
    .RequireAuthorization();

app.MapGet("/api/author/{id}", async (int id, LibraryContext context, CancellationToken cancellationToken) =>
{
    var author = await context
        .Set<Author>()
        .Where(x => x.Id == id)
        .Select(x => new AuthorDto(x.Id, x.FullName))
        .FirstOrDefaultAsync(cancellationToken);

    if (author is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(author);
})
    .WithOpenApi()
    .WithName("GetAuthorById")
    .Produces<AuthorDto>()
    .Produces(404)
    .RequireAuthorization();

app.MapPost("/api/author", async (CreateAuthorDto dto, CreateAuthorDto.Validator validator, LibraryContext context, CancellationToken cancellationToken) =>
{
    var validationResult = await validator.ValidateAsync(dto, cancellationToken);
    if (!validationResult.IsValid)
    {
        return Results.ValidationProblem(validationResult.ToDictionary());
    }

    var author = new Author
    {
        FullName = dto.Name
    };

    context.Add(author);

    await context.SaveChangesAsync(cancellationToken);

    return Results.Ok(new AuthorDto(author.Id, author.FullName));
})
    .WithOpenApi()
    .WithName("CreateAuthor")
    .Produces<AuthorDto>()
    .ProducesValidationProblem()
    .RequireAuthorization();

app.MapPut("/api/author/{id}", async (int id, UpdateAuhtorDto dto, UpdateAuhtorDto.Validator validator, LibraryContext context, CancellationToken cancellationToken) =>
{
    var validationResult = await validator.ValidateAsync(dto, cancellationToken);
    if (!validationResult.IsValid)
    {
        return Results.ValidationProblem(validationResult.ToDictionary());
    }

    var author = await context
        .Set<Author>()
        .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    if (author is null)
    {
        return Results.NotFound();
    }

    author.FullName = dto.Name;

    await context.SaveChangesAsync(cancellationToken);

    return Results.Ok(new AuthorDto(author.Id, author.FullName));
})
    .WithOpenApi()
    .WithName("UpdateAuthor")
    .Produces<AuthorDto>()
    .ProducesValidationProblem()
    .Produces(404)
    .RequireAuthorization();

app.MapDelete("/api/author/{id}", async (int id, LibraryContext context, CancellationToken cancellationToken) =>
{
    var author = await context
        .Set<Author>()
        .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    if (author is null)
    {
        return Results.NotFound();
    }

    context.Remove(author);

    await context.SaveChangesAsync(cancellationToken);

    return Results.Ok();
})
    .WithOpenApi()
    .WithName("DeleteAuthor")
    .Produces(200)
    .Produces(404)
    .RequireAuthorization();

#endregion

#region Books

app.MapGet("/api/books", async (LibraryContext context, CancellationToken cancellationToken) =>
{
    var books = await context
        .Set<Book>()
        .Select(x => new BookDto(x.Id, x.Title, x.AuthorId))
        .ToListAsync(cancellationToken);

    return Results.Ok(books);
})
    .WithOpenApi()
    .WithName("GetAllBooks")
    .Produces<IEnumerable<BookDto>>()
    .RequireAuthorization();

app.MapGet("/api/book/{id}", async (int id, LibraryContext context, CancellationToken cancellationToken) =>
{
    var book = await context
        .Set<Book>()
        .Where(x => x.Id == id)
        .Select(x => new BookDto(x.Id, x.Title, x.AuthorId))
        .FirstOrDefaultAsync(cancellationToken);

    if (book is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(book);
})
    .WithOpenApi()
    .WithName("GetBookById")
    .Produces<BookDto>()
    .Produces(404)
    .RequireAuthorization();

app.MapPost("/api/book", async (CreateBookDto dto, CreateBookDto.Validator validator, LibraryContext context, CancellationToken cancellationToken) =>
{
    var validationResult = await validator.ValidateAsync(dto, cancellationToken);
    if (!validationResult.IsValid)
    {
        return Results.ValidationProblem(validationResult.ToDictionary());
    }

    var book = new Book
    {
        Title = dto.Title,
        AuthorId = dto.AuthorId,
    };

    context.Add(book);

    await context.SaveChangesAsync(cancellationToken);

    return Results.Ok(new BookDto(book.Id, book.Title, book.AuthorId));
})
    .WithOpenApi()
    .WithName("CreateBook")
    .Produces<BookDto>()
    .ProducesValidationProblem()
    .RequireAuthorization();

app.MapPut("/api/book/{id}", async (int id, UpdateBookDto dto, UpdateBookDto.Validator validator, LibraryContext context, CancellationToken cancellationToken) =>
{
    var validationResult = await validator.ValidateAsync(dto, cancellationToken);
    if (!validationResult.IsValid)
    {
        return Results.ValidationProblem(validationResult.ToDictionary());
    }

    var book = await context
        .Set<Book>()
        .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    if (book is null)
    {
        return Results.NotFound();
    }

    book.Title = dto.Title;
    book.AuthorId = dto.AuthorId;

    await context.SaveChangesAsync(cancellationToken);

    return Results.Ok(new BookDto(book.Id, book.Title, book.AuthorId));
})
    .WithOpenApi()
    .WithName("UpdateBook")
    .Produces<BookDto>()
    .ProducesValidationProblem()
    .Produces(404)
    .RequireAuthorization();

app.MapDelete("/api/book/{id}", async (int id, LibraryContext context, CancellationToken cancellationToken) =>
{
    var book = await context
        .Set<Book>()
        .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    if (book is null)
    {
        return Results.NotFound();
    }

    context.Remove(book);

    await context.SaveChangesAsync(cancellationToken);

    return Results.Ok();
})
    .WithOpenApi()
    .WithName("DeleteBook")
    .Produces(200)
    .Produces(404)
    .RequireAuthorization();

#endregion

app.MapFallbackToFile("/index.html");

app.Run();
