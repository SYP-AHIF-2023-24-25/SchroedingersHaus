using Base.Persistence;
using Core.Contracts;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class ChallengeRepository : GenericRepository<Challenge>, IChallengeRepository
{
    private readonly ApplicationDbContext _context;

    public ChallengeRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    // Speichert einen Challenge-Eintrag
    public Challenge SaveChallenge(Challenge challenge)
    {
        _context.Challenges.Add(challenge);
        _context.SaveChanges();
        return challenge;
    }

    // Findet einen Challenge-Eintrag anhand der ID
    public Challenge FindById(int id)
    {
        return _context.Challenges.Find(id)!;
    }

    // Gibt eine Liste aller Challenge-Einträge zurück
    public List<Challenge> FindAll()
    {
        return _context.Challenges.ToList();
    }
    
    // Gibt alle Challenges in einem bestimmten Raum zurück
    public List<Challenge> FindByRoomId(int roomId)
    {
        return _context.Challenges
            .Where(c => c.RoomId == roomId)
            .ToList();
    }

    // Aktualisiert einen Challenge-Eintrag
    public Challenge UpdateChallenge(Challenge challenge)
    {
        _context.Challenges.Update(challenge);
        _context.SaveChanges();
        return challenge;
    }

    // Löscht einen Challenge-Eintrag
    public void DeleteChallenge(int id)
    {
        var challenge = FindById(id);
        if (challenge != null)
        {
            _context.Challenges.Remove(challenge);
            _context.SaveChanges();
        }
    }
}