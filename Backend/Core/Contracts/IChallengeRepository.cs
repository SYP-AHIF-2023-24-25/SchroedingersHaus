using Base.Core.Contracts;
using Core.Entities;

namespace Core.Contracts;

public interface IChallengeRepository : IGenericRepository<Challenge>
{
    // Speichert einen Challenge-Eintrag
    public Challenge SaveChallenge(Challenge challenge);

    // Findet einen Challenge-Eintrag anhand der ID
    public Challenge FindById(int id);

    // Gibt eine Liste aller Challenge-Einträge zurück
    public List<Challenge> FindAll();
    List<Challenge> FindByRoomId(int roomId);
    Challenge UpdateChallenge(Challenge challenge);
    void DeleteChallenge(int id);
}