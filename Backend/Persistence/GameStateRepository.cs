using Base.Persistence;
using Core.Contracts;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class GameStateRepository : GenericRepository<GameState>, IGameStateRepository
{
    private readonly ApplicationDbContext _context;

    public GameStateRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    
    // Findet alle GameStates
    public IEnumerable<GameState> FindAll()
    {
        return _context.GameStates
            .ToList();
    }

    // Findet einen GameState-Eintrag anhand der ID (String)
    public GameState FindById(string id)
    {
        return _context.GameStates
            .Include(gs => gs.CurrentRoom)
            .Include(gs => gs.CurrentChallenge)
            .FirstOrDefault(g => g.GameStateId.ToString() == id)!;
    }
    
    // Findet einen GameState-Eintrag passend zu einer LobbyId
    public GameState FindByLobbyId(string lobbyId)
    {
        return _context.GameStates
            .Include(gs => gs.CurrentRoom)
            .Include(gs => gs.CurrentChallenge)
            .FirstOrDefault(gs => gs.CurrentLobbyId == lobbyId);
    }

    // Speichert einen GameState-Eintrag
    public GameState SaveGameState(GameState gameState)
    {
        _context.GameStates.Update(gameState);
        _context.SaveChanges();
        return gameState;
    }
}