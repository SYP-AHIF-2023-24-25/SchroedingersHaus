using System.Net.WebSockets;
using System.Text;
using Persistence;

namespace WebApi;

/*public class ChatSocket
{
    private readonly ChatService _chatService;

    public ChatSocket(ChatService chatService)
    {
        _chatService = chatService;
    }

    public async Task HandleWebSocketAsync(HttpContext context, string lobbyId, string username)
    {
        if (context.WebSockets.IsWebSocketRequest)
        {
            var webSocket = await context.WebSockets.AcceptWebSocketAsync();
            await _chatService.AddUserSessionAsync(lobbyId, username, webSocket);
            await ReceiveMessagesAsync(webSocket, lobbyId, username);
        }
    }

    private async Task ReceiveMessagesAsync(WebSocket webSocket, string lobbyId, string username)
    {
        var buffer = new byte[1024 * 4];

        while (webSocket.State == WebSocketState.Open)
        {
            var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            if (result.MessageType == WebSocketMessageType.Close)
            {
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
            }
            else
            {
                var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                await _chatService.OnMessageAsync(message, username, lobbyId);
            }
        }
    }
}*/

public class ChatSocket
{
    /*private readonly ChatService _chatService;

    public ChatSocket(ChatService chatService)
    {
        _chatService = chatService;
    }

    public async Task HandleWebSocketAsync(WebSocket webSocket, string lobbyId, string username)
    {
        // Füge den Benutzer zur Lobby hinzu
        _chatService.AddUserSession(lobbyId, username, webSocket);

        var buffer = new byte[1024 * 4]; // Buffer für empfangene Nachrichten
        WebSocketReceiveResult result;

        try
        {
            // Verarbeiten von Nachrichten
            while (webSocket.State == WebSocketState.Open)
            {
                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Text)
                {
                    var message = Encoding.UTF8.GetString(buffer, 0, result.Count);

                    // Verarbeite die eingehende Nachricht
                    await HandleIncomingMessage(message, username, lobbyId);
                }
                else if (result.MessageType == WebSocketMessageType.Close)
                {
                    // Wenn der Client die Verbindung schließt
                    _chatService.RemoveUser(lobbyId, username);
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by user", CancellationToken.None);
                }
            }
        }
        catch (Exception ex)
        {
            // Fehlerbehandlung
            Console.WriteLine($"Error handling WebSocket message: {ex.Message}");
            _chatService.RemoveUser(lobbyId, username);
            await webSocket.CloseAsync(WebSocketCloseStatus.InternalServerError, "Error occurred", CancellationToken.None);
        }


        Console.WriteLine($"WebSocket connection established for lobbyId: {lobbyId}, username: {username}");

        // Füge den Benutzer zur Lobby hinzu
        _chatService.AddUserSession(lobbyId, username, webSocket);

        var buffer = new byte[1024 * 4]; // Buffer für empfangene Nachrichten
        WebSocketReceiveResult result;

        try
        {
            // Verarbeiten von Nachrichten
            while (webSocket.State == WebSocketState.Open)
            {
                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                Console.WriteLine($"Message received: {Encoding.UTF8.GetString(buffer, 0, result.Count)}");

                if (result.MessageType == WebSocketMessageType.Text)
                {
                    var message = Encoding.UTF8.GetString(buffer, 0, result.Count);

                    // Verarbeite die eingehende Nachricht
                    await HandleIncomingMessage(message, username, lobbyId);
                }
                else if (result.MessageType == WebSocketMessageType.Close)
                {
                    // Wenn der Client die Verbindung schließt
                    _chatService.RemoveUser(lobbyId, username);
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by user", CancellationToken.None);
                    Console.WriteLine($"User {username} disconnected from lobby {lobbyId}");
                }
            }
        }
        catch (Exception ex)
        {
            // Fehlerbehandlung
            Console.WriteLine($"Error handling WebSocket message: {ex.Message}");
            _chatService.RemoveUser(lobbyId, username);
            await webSocket.CloseAsync(WebSocketCloseStatus.InternalServerError, "Error occurred", CancellationToken.None);
        }
    }

    // Verarbeiten eingehender Nachrichten
    private async Task HandleIncomingMessage(string message, string username, string lobbyId)
    {
        /*if (message.Equals("_ready_", StringComparison.OrdinalIgnoreCase))
        {
            await BroadcastMessageAsync($"User {username} joined", lobbyId);
        }
        else
        {
            var filteredMessage = _chatService.CheckMessage(message);
            if (filteredMessage == message)
            {
                await BroadcastMessageAsync($">> {username}: {message}", lobbyId);
            }
            else
            {
                await BroadcastMessageAsync($">> {username} used a bad word", lobbyId);
            }
        }

        Console.WriteLine($"Handling incoming message from {username} in lobby {lobbyId}: {message}");

        if (message.Equals("_ready_", StringComparison.OrdinalIgnoreCase))
        {
            await BroadcastMessageAsync($"User {username} joined", lobbyId);
        }
        else
        {
            var filteredMessage = _chatService.CheckMessage(message);
            if (filteredMessage == message)
            {
                await BroadcastMessageAsync($">> {username}: {message}", lobbyId);
            }
            else
            {
                await BroadcastMessageAsync($">> {username} used a bad word", lobbyId);
            }
        }
    }

    // Nachrichten an alle Benutzer der Lobby senden
    private async Task BroadcastMessageAsync(string message, string lobbyId)
    {
        /*var sessions = _chatService.GetAllSessionsFromLobby(lobbyId);

        if (sessions != null)
        {
            var messageBytes = Encoding.UTF8.GetBytes(message);

            foreach (var session in sessions.Values)
            {
                if (session.State == WebSocketState.Open)
                {
                    await session.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
        }
        Console.WriteLine($"Broadcasting message to lobby {lobbyId}: {message}");

        var sessions = _chatService.GetAllSessionsFromLobby(lobbyId);

        if (sessions != null)
        {
            var messageBytes = Encoding.UTF8.GetBytes(message);

            foreach (var session in sessions.Values)
            {
                if (session.State == WebSocketState.Open)
                {
                    await session.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);
                    Console.WriteLine("Message sent to session");
                }
                else
                {
                    Console.WriteLine("Session is not open");
                }
            }
        }
        else
        {
            Console.WriteLine("No sessions found for this lobby");
        }
    }*/
    
    /*private readonly ChatService _chatService;

    public ChatSocket(ChatService chatService)
    {
        _chatService = chatService;
    }

    public async Task HandleWebSocketAsync(HttpContext context, string lobbyId, string username)
    {
        if (context.WebSockets.IsWebSocketRequest)
        {
            var webSocket = await context.WebSockets.AcceptWebSocketAsync();
            _chatService.AddUserSession(lobbyId, username, webSocket);
            await ReceiveMessagesAsync(webSocket, lobbyId, username);
        }
    }

    private async Task ReceiveMessagesAsync(WebSocket webSocket, string lobbyId, string username)
    {
        var buffer = new byte[1024 * 4];

        while (webSocket.State == WebSocketState.Open)
        {
            var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            if (result.MessageType == WebSocketMessageType.Text)
            {
                var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                await HandleIncomingMessage(message, username, lobbyId);
            }
            else if (result.MessageType == WebSocketMessageType.Close)
            {
                _chatService.RemoveUser(lobbyId, username);
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
            }
        }
    }

    private async Task HandleIncomingMessage(string message, string username, string lobbyId)
    {
        var filteredMessage = _chatService.CheckMessage(message);
        await BroadcastMessageAsync($">> {username}: {filteredMessage}", lobbyId);
    }

    private async Task BroadcastMessageAsync(string message, string lobbyId)
    {
        var sessions = _chatService.GetAllSessionsFromLobby(lobbyId);

        if (sessions != null)
        {
            var messageBytes = Encoding.UTF8.GetBytes(message);
            foreach (var session in sessions.Values)
            {
                if (session.State == WebSocketState.Open)
                {
                    await session.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
        }
    }*/
}