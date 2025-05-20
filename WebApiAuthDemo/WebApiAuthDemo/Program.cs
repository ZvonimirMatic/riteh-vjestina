using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApiAuthDemo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(
    o => o.UseNpgsql(
        builder.Configuration.GetConnectionString("Default")));

builder.Services.AddAuthorization(o =>
{
    o.AddPolicy("Admin", policy =>
    {
        policy.RequireRole("Admin");
    });
});

builder.Services.AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<AppDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapIdentityApi<User>();

app.MapPost("/register-custom", async (UserManager<User> userManager, RegisterDto dto) =>
{
    await userManager.CreateAsync(new User
    {
        Email = dto.Email,
        UserName = dto.Email,
        Birthday = dto.Birthday,
    }, dto.Password);
})
.WithName("RegisterCustom")
.WithOpenApi();

app.MapGet("/hello-auth", () =>
{
    return "SUCCESS";
})
.WithName("HelloAuth")
.WithOpenApi()
.RequireAuthorization();

app.MapGet("/hello-no-auth", () =>
{
    return "SUCCESS";
})
.WithName("HelloNoAuth")
.WithOpenApi();

app.MapGet("/hello-admin", () =>
{
    return "SUCCESS";
})
.WithName("HelloAdmin")
.WithOpenApi()
.RequireAuthorization("Admin");

app.Run();
