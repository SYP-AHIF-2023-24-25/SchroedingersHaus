using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Persistence;

public class ChatHub : Hub
{
    private readonly ApplicationDbContext _dbContext;

    private static readonly ConcurrentDictionary<string, string> UserLobbyMap = new();
    
    public ChatHub(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task JoinLobby(string lobbyId, string userName)
    {
        var lobbyExists = await _dbContext.Lobbies.AnyAsync(l => l.LobbyId == lobbyId);

        if (!lobbyExists)
        {
            throw new HubException("Lobby existiert nicht.");
        }

        string connectionId = Context.ConnectionId;
        UserLobbyMap[connectionId] = lobbyId;
        
        await Groups.AddToGroupAsync(connectionId, lobbyId);
        await Clients.Group(lobbyId).SendAsync("UserJoined", userName);
    }

    public async Task SendMessage(string lobbyId, string user, string message)
    {
        await Clients.Group(lobbyId).SendAsync("ReceiveMessage", user, message);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        string connectionId = Context.ConnectionId;
        if (UserLobbyMap.TryRemove(connectionId, out string? lobbyId))
        {
            await Groups.RemoveFromGroupAsync(connectionId, lobbyId);
        }

        await base.OnDisconnectedAsync(exception);
    }
}