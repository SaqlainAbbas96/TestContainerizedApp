using Microsoft.EntityFrameworkCore;
using TestContainerizedApp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// read connection string from env or appsettings
var conn = builder.Configuration.GetConnectionString("DefaultConnection")
?? Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
?? "Host=db;Database=usersdb;Username=appuser;Password=Test123.@Test";

builder.Services.AddDbContext<ApplicationDbContext>(opt =>
opt.UseNpgsql(conn));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/api/users/add", async (User user, ApplicationDbContext db) =>
{
    if (string.IsNullOrWhiteSpace(user.FullName))
        return Results.BadRequest("FullName is required");

    db.Users.Add(user);
    await db.SaveChangesAsync();
    return Results.Created($"/get", user);
});

app.MapGet("/api/users/get", async (ApplicationDbContext _db) =>
{
    var user = await _db.Users.ToListAsync();
    return user is not null ? Results.Ok(user) : Results.NotFound();
});

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
