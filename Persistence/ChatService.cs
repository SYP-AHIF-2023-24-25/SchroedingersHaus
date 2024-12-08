using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using Core.Entities;
using Core.Helper;

namespace Persistence;

/*public class ChatService
{
    private readonly ProfanityFilter _filter;
    private readonly LobbyRepository _lobbyRepository;
    private readonly HashSet<Lobby> _lobbies = new();
    private readonly HashSet<string> _lobbyIds = new();
    private readonly RandomStringGenerate _generateString = new();

    public ChatService(ProfanityFilter filter, LobbyRepository lobbyRepository)
    {
        _filter = filter;
        _lobbyRepository = lobbyRepository;
    }

    public ConcurrentDictionary<string, System.Net.WebSockets.WebSocket> GetAllSessionsFromLobby(string lobbyId)
    {
        return _lobbies
            .FirstOrDefault(lobby => lobby.LobbyId.Equals(lobbyId))?.GetSessions() ?? new ConcurrentDictionary<string, System.Net.WebSockets.WebSocket>();
    }

    public void RemoveLobby(string lobbyId)
    {
        _lobbyIds.Remove(lobbyId);
        _lobbies.RemoveWhere(lobby => lobby.LobbyId == lobbyId);
    }

    public void ReloadFilter()
    {
        // _filter.ReloadFilter();
    }

    public string CheckMessage(string input)
    {
        return input; // _filter.FilterText(input);
    }

    public bool AddLobby(string lobbyId)
    {
        if (!_lobbyRepository.FindAll().Any(l => l.LobbyId == lobbyId))
        {
            var lobby = new Lobby(lobbyId);
            _lobbyIds.Add(lobbyId);
            _lobbies.Add(lobby);
            return true;
        }
        return false;
    }

    public HashSet<string> GetAllUsersFromLobby(string lobbyId)
    {
        var lobby = _lobbies.FirstOrDefault(l => l.LobbyId == lobbyId);
        return lobby?.GetUserNames() ?? new HashSet<string>();
    }

    public HashSet<string> GetAllLobbyIds()
    {
        return _lobbyIds;
    }

    // Fügt einen User samt Session hinzu
    public void AddUserSession(string lobbyId, string userName, WebSocket session)
    {
        var lobby = _lobbies.FirstOrDefault(l => l.LobbyId == lobbyId);
        lobby?.AddSession(userName, session);
    }

    public void RemoveUser(string lobbyId, string userName)
    {
        var lobby = _lobbies.FirstOrDefault(l => l.LobbyId == lobbyId);
        lobby?.RemoveSession(userName);
    }

    // Belegt den Benutzernamen
    public void AddUserName(string lobbyId, string userName)
    {
        var lobby = _lobbies.FirstOrDefault(l => l.LobbyId == lobbyId);
        lobby?.AddUserName(userName);
    }

    public string GenerateLobbyId()
    {
        return _generateString.GenerateRandomString(6);
    }
    
    //
    
    private readonly ConcurrentDictionary<string, ConcurrentDictionary<string, WebSocket>> _lobbies = new();

    public async Task AddUserSessionAsync(string lobbyId, string username, WebSocket session)
    {
        var lobby = _lobbies.GetOrAdd(lobbyId, _ => new ConcurrentDictionary<string, WebSocket>());
        lobby.TryAdd(username, session);
        await BroadcastAsync($"{username} joined the lobby", lobbyId);
    }

    public async Task OnMessageAsync(string message, string username, string lobbyId)
    {
        if (message.Equals("_ready_", StringComparison.OrdinalIgnoreCase))
        {
            await BroadcastAsync($"{username} joined", lobbyId);
        }
        else
        {
            await BroadcastAsync($">> {username}: {message}", lobbyId);
        }
    }

    private async Task BroadcastAsync(string message, string lobbyId)
    {
        if (_lobbies.TryGetValue(lobbyId, out var lobby))
        {
            var buffer = Encoding.UTF8.GetBytes(message);
            var segment = new ArraySegment<byte>(buffer);

            foreach (var session in lobby.Values)
            {
                if (session.State == WebSocketState.Open)
                {
                    await session.SendAsync(segment, WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
        }
    }
}*/

/*public class ChatService
{
    private readonly LobbyRepository _lobbyRepository;

    private RandomStringGenerate _generateString = new RandomStringGenerate();
    
    // Speicher für Lobbys und deren Sessions
    private readonly ConcurrentDictionary<string, Lobby> _lobbies = new ConcurrentDictionary<string, Lobby>();

    public ChatService(LobbyRepository lobbyRepository)
    {
        _lobbyRepository = lobbyRepository;

        // Lade die Lobbys beim Start
        //LoadInitialLobbies();
    }

    // Läd alle Lobbys aus der Datenbank
    private void LoadInitialLobbies()
    {
        var lobbies = _lobbyRepository.FindAll();
        foreach (var lobby in lobbies)
        {
            _lobbies.TryAdd(lobby.LobbyId, lobby);
        }
    }

    // Holt alle Sessions einer bestimmten Lobby
    public ConcurrentDictionary<string, WebSocket> GetAllSessionsFromLobby(string lobbyId)
    {
        if (_lobbies.TryGetValue(lobbyId, out var lobby))
        {
            return lobby.Sessions;
        }
        return null;
    }

    // Fügt eine neue Lobby hinzu
    public bool AddLobby(string lobbyId)
    {
        if (!_lobbies.ContainsKey(lobbyId))
        {
            var lobby = new Lobby(lobbyId);
            _lobbies.TryAdd(lobbyId, lobby);
            _lobbyRepository.SaveLobby(lobby);
            return true;
        }
        return false;
    }

    

    // Entfernt einen Benutzer aus der Lobby
    public void RemoveUser(string lobbyId, string userName)
    {
        if (_lobbies.TryGetValue(lobbyId, out var lobby))
        {
            lobby.RemoveSession(userName);
        }
    }

    // Überprüft eine Nachricht auf anstößige Wörter (Placeholder)
    public string CheckMessage(string message)
    {
        // Hier kannst du später einen Profanity-Filter integrieren
        return message;
    }
    // Gibt alle Lobby-IDs zurück
    public IEnumerable<string> GetAllLobbyIds()
    {
        return _lobbies.Keys;
    }

    public void AddUserSession(string lobbyId, string userName, WebSocket session)
    {
        if (_lobbies.TryGetValue(lobbyId, out var lobby))
        {
            lobby.AddSession(userName, session);
        }
    }

    // Gibt alle Benutzer einer Lobby zurück
    public List<string> GetAllUsersFromLobby(string lobbyId)
    {
        if (_lobbies.TryGetValue(lobbyId, out var lobby))
        {
            return lobby.Users;
        }
        return new List<string>();
    }

    
    
    public string GenerateLobbyId()
    {
        return _generateString.GenerateRandomString(6);
    }
    
}*/



public class ChatService
{
    private readonly LobbyRepository _lobbyRepository;

    private RandomStringGenerate _generateString = new RandomStringGenerate();
    
    // Speicher für Lobbys und deren Sessions
    private readonly ConcurrentDictionary<string, Lobby> _lobbies = new ConcurrentDictionary<string, Lobby>();

    public ChatService(LobbyRepository lobbyRepository)
    {
        _lobbyRepository = lobbyRepository;

        // Lade die Lobbys beim Start
        //LoadInitialLobbies();
    }

    // Läd alle Lobbys aus der Datenbank
    /*private void LoadInitialLobbies()
    {
        var lobbies = _lobbyRepository.FindAll();
        foreach (var lobby in lobbies)
        {
            _lobbies.TryAdd(lobby.LobbyId, lobby);
        }
    }

    // Holt alle Sessions einer bestimmten Lobby
    public ConcurrentDictionary<string, WebSocket> GetAllSessionsFromLobby(string lobbyId)
    {
        if (_lobbies.TryGetValue(lobbyId, out var lobby))
        {
            return lobby.Sessions;
        }
        return null;
    }

    // Fügt eine neue Lobby hinzu
    public bool AddLobby(string lobbyId)
    {
        if (!_lobbies.ContainsKey(lobbyId))
        {
            var lobby = new Lobby(lobbyId);
            _lobbies.TryAdd(lobbyId, lobby);
            _lobbyRepository.SaveLobby(lobby);
            return true;
        }
        return false;
    }

    

    // Entfernt einen Benutzer aus der Lobby
    public void RemoveUser(string lobbyId, string userName)
    {
        if (_lobbies.TryGetValue(lobbyId, out var lobby))
        {
            lobby.RemoveSession(userName);
        }
    }

    // Überprüft eine Nachricht auf anstößige Wörter (Placeholder)
    public string CheckMessage(string message)
    {
        // Hier kannst du später einen Profanity-Filter integrieren
        return message;
    }
    // Gibt alle Lobby-IDs zurück
    public IEnumerable<string> GetAllLobbyIds()
    {
        return _lobbies.Keys;
    }*/

    public void AddUserSession(string lobbyId, string userName, WebSocket session)
    {
        if (_lobbies.TryGetValue(lobbyId, out var lobby))
        {
            lobby.AddSession(userName, session);
        }
    }

    // Gibt alle Benutzer einer Lobby zurück
    public List<string> GetAllUsersFromLobby(string lobbyId)
    {
        if (_lobbies.TryGetValue(lobbyId, out var lobby))
        {
            return lobby.Users;
        }
        return new List<string>();
    }

    
    
    public string GenerateLobbyId()
    {
        return _generateString.GenerateRandomString(6);
    }

    public bool AddLobby(string lobbyId)
    {
        if (!_lobbies.ContainsKey(lobbyId))
        {
            var lobby = new Lobby(lobbyId);
            _lobbies.TryAdd(lobbyId, lobby);
            _lobbyRepository.SaveLobby(lobby);
            return true;
        }
        return false;
    }
}