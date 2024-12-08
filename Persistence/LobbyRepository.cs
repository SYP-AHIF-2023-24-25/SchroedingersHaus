using Base.Persistence;
using Core.Contracts;
using Core.Entities;

namespace Persistence;

public class LobbyRepository : GenericRepository<Lobby>, ILobbyRepository
{
    private readonly ApplicationDbContext _context;

    public LobbyRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    // Gibt alle Lobbys zurück
    public IEnumerable<Lobby> FindAll()
    {
        return _context.Lobbies.ToList();
    }

    // Findet eine Lobby anhand der ID
    public Lobby FindById(string id)
    {
        return _context.Lobbies.FirstOrDefault(l => l.LobbyId == id)!;
    }

    // Speichert eine Lobby
    public Lobby SaveLobby(Lobby lobby)
    {
        _context.Lobbies.Add(lobby);
        _context.SaveChanges();
        return lobby;
    }
}