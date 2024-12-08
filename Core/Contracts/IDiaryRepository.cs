using Base.Core.Contracts;
using Core.Entities;

namespace Core.Contracts;

public interface IDiaryRepository : IGenericRepository<Diary>
{
    // Speichert einen Diary-Eintrag
    public Diary SaveDiary(Diary diary);
    
    // Findet einen Diary-Eintrag anhand der ID
    public Diary FindById(int id);

    // Gibt eine Liste aller Diary-Einträge zurück
    public List<Diary> FindAll();
}