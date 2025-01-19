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

// Füge WebSocket-Support hinzu
/*builder.Services.AddWebSockets(options =>
{
    options.KeepAliveInterval = TimeSpan.FromSeconds(120);
});*/

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
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowAllOrigins");
app.UseWebSockets(); // WebSocket-Middleware aktivieren


/*app.Use(async (context, next) =>
{
    if (context.WebSockets.IsWebSocketRequest)
    {
        var webSocket = await context.WebSockets.AcceptWebSocketAsync();
        var lobbyId = context.Request.Query["lobbyId"];
        var username = context.Request.Query["username"];
        
        // Hier kannst du die ChatSocket-Logik aufrufen
        var chatSocket = new ChatSocket(app.Services.GetRequiredService<ChatService>());
        await chatSocket.HandleWebSocketAsync(webSocket, lobbyId, username);
    }
    else
    {
        await next();
    }
});*/

app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("/chat");

app.Run();
