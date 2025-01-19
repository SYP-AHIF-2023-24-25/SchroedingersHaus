using System.ComponentModel.DataAnnotations;
using Base.Core.Entities;

namespace Core.Entities;

public class Challenge : EntityObject
{
    public int SequenceNumber { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Hint { get; set; } = string.Empty;
    
    public int RoomId { get; set; } // Foreign Key zu Room
}