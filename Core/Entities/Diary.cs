using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Core.Entities;

namespace Core.Entities;

public class Diary : EntityObject
{
    public int DiaryId { get; set; }
    public int ChallengeId { get; set; }
    public string? Chapter { get; set; }
    public string? Entry { get; set; }
    public string? Date { get; set; }

    // Default constructor
    public Diary() { }

    // All-args constructor
    public Diary(int diaryId, int challengeId, string chapter, string entry, string date)
    {
        DiaryId = diaryId;
        ChallengeId = challengeId;
        Chapter = chapter;
        Entry = entry;
        Date = date;
    }
}