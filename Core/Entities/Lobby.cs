using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Core.Entities;

namespace Core.Entities;

public class Lobby : EntityObject
{
    public string LobbyId { get; set; }

    [NotMapped]
    public ConcurrentDictionary<string, System.Net.WebSockets.WebSocket> Sessions { get; set; } = new ConcurrentDictionary<string, System.Net.WebSockets.WebSocket>();

    [Column("user_name")]
    public List<string> Users { get; set; } = new List<string>();

    // Default constructor
    public Lobby() { }

    // Constructor with lobbyId
    public Lobby(string lobbyId)
    {
        LobbyId = lobbyId;
    }

    public void AddSession(string userName, System.Net.WebSockets.WebSocket session)
    {
        Sessions.TryAdd(userName, session);
    }
    

    public void AddUserName(string userName)
    {
        Users.Add(userName);
    }

    public void RemoveSession(string userName)
    {
        Sessions.TryRemove(userName, out _);
        Users.Remove(userName);
    }

    public List<string> GetUserNames()
    {
        return Users;
    }
    
    public ConcurrentDictionary<string, System.Net.WebSockets.WebSocket> GetSessions()
    {
        return Sessions;
    }
}