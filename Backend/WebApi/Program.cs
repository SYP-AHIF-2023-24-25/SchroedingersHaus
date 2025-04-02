using Core.Contracts;
using Microsoft.AspNetCore.WebSockets;
using Microsoft.EntityFrameworkCore;
using Persistence;
using WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Füge den ChatService als Singleton hinzu
builder.Services.AddScoped<IChallengeRepository, ChallengeRepository>();
builder.Services.AddScoped<IDiaryRepository, DiaryRepository>();
builder.Services.AddScoped<IGameStateRepository, GameStateRepository>();
builder.Services.AddScoped<ILobbyRepository, LobbyRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<RoomRepository>();
builder.Services.AddScoped<ChatService>();
builder.Services.AddScoped<DiaryRepository>();
builder.Services.AddScoped<GameStateRepository>();
builder.Services.AddScoped<LobbyRepository>();
//builder.Services.AddSingleton<ChatService>();
builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowAllOrigins");
app.UseWebSockets(); // WebSocket-Middleware aktivieren

app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("/chatHub");

var scope = app.Services.CreateScope();
var unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();
var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

try
{
    app.Logger.LogInformation("Ensure database is created...");

    // Datenbank migrieren, falls notwendig
    dbContext.Database.Migrate(); // Dies stellt sicher, dass alle Migrationen angewendet werden

    // Wenn du initiale Daten importieren möchtest, kannst du hier den Import ausführen
    // Beispiel:
    // if (!dbContext.Lobbies.Any())
    // {
    //     dbContext.Lobbies.Add(new Lobby { Name = "Lobby 1", Description = "Test Lobby" });
    //     dbContext.SaveChanges();
    // }
}
catch (Exception e)
{
    app.Logger.LogError("Fehler beim Erstellen oder Migrieren der Datenbank: " + e.Message);
}

app.Run();
