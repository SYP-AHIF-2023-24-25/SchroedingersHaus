using Base.Core.Contracts;
using Core.Entities;

namespace Core.Contracts;

public interface ILobbyRepository : IGenericRepository<Lobby>
{
    // Gibt alle Lobbys zurück
    public IEnumerable<Lobby> FindAll();

    // Findet eine Lobby anhand der ID
    public Lobby FindById(string id);

    // Speichert eine Lobby
    public Lobby SaveLobby(Lobby lobby);
}