using Microsoft.AspNetCore.SignalR;
using Persistence;

public class ChatHub : Hub
{
    /*public async Task SendMessage(string user, string message, string lobbyId)
    {
        // Sendet die Nachricht an alle in der spezifischen Lobby
        await Clients.Group(lobbyId).SendAsync("ReceiveMessage", user, message);
    }

    public override async Task OnConnectedAsync()
    {
        var lobbyId = Context.GetHttpContext()?.Request.Query["lobbyId"].ToString();
        if (lobbyId != null)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, lobbyId);
        }
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var lobbyId = Context.GetHttpContext()?.Request.Query["lobbyId"].ToString();
        if (lobbyId != null)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, lobbyId);
        }
        await base.OnDisconnectedAsync(exception);
    }*/
    
    /*private readonly ChatService _chatService;

    public ChatHub(ChatService chatService)
    {
        _chatService = chatService;
    }

    public async Task JoinLobby(string lobbyId, string userName)
    {
        if (!_chatService.LobbyExists(lobbyId))
        {
            _chatService.CreateLobby(lobbyId);
        }

        var lobby = _chatService.GetLobby(lobbyId);
        lobby.AddUser(userName, Context.ConnectionId);

        await Groups.AddToGroupAsync(Context.ConnectionId, lobbyId);
        await Clients.Group(lobbyId).SendAsync("ReceiveMessage", "System", $"{userName} joined the lobby.");
    }

    public async Task SendMessage(string lobbyId, string userName, string message)
    {
        var lobby = _chatService.GetLobby(lobbyId);

        if (lobby != null && lobby.GetUserNames().Contains(userName))
        {
            await Clients.Group(lobbyId).SendAsync("ReceiveMessage", userName, message);
        }
    }

    public override async Task OnDisconnectedAsync(System.Exception exception)
    {
        // Logic for handling user disconnect and removing from lobby
        await base.OnDisconnectedAsync(exception);
    }*/
    
    public async Task SendMessage(string message)
    {
        await Clients.Group(Context.GetHttpContext().Request.Query["lobbyId"]).SendAsync("ReceiveMessage", message);
    }

    public override async Task OnConnectedAsync()
    {
        var lobbyId = Context.GetHttpContext().Request.Query["lobbyId"];
        await Groups.AddToGroupAsync(Context.ConnectionId, lobbyId);
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        var lobbyId = Context.GetHttpContext().Request.Query["lobbyId"];
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, lobbyId);
        await base.OnDisconnectedAsync(exception);
    }
}