using Base.Persistence;
using Core.Contracts;
using Core.Entities;

namespace Persistence;

public class DiaryRepository : GenericRepository<Diary>, IDiaryRepository
{
    private readonly ApplicationDbContext _context;

    public DiaryRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    // Speichert einen Diary-Eintrag
    public Diary SaveDiary(Diary diary)
    {
        _context.Diaries.Add(diary);
        _context.SaveChanges();
        return diary;
    }

    // Findet einen Diary-Eintrag anhand der ID
    public Diary FindById(int id)
    {
        return _context.Diaries.Find(id)!;
    }

    // Gibt eine Liste aller Diary-Einträge zurück
    public List<Diary> FindAll()
    {
        return _context.Diaries.ToList();
    }
}