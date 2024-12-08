using Base.Core.Contracts;
using Core.Entities;

namespace Core.Contracts;

public interface IGameStateRepository : IGenericRepository<GameState>
{
    // Findet alle GameStates
    public IEnumerable<GameState> FindAll();
    
    // Findet einen GameState-Eintrag anhand der ID (String)
    public GameState FindById(string id);

    // Findet einen GameState-Eintrag passend zu einer LobbyId
    public GameState FindByLobbyId(string lobbyId);
    
    // Speichert einen GameState-Eintrag
    public GameState SaveGameState(GameState gameState);
}