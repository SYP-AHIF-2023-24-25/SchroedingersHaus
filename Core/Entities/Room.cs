using System.ComponentModel.DataAnnotations.Schema;
using Base.Core.Entities;

namespace Core.Entities;

public class Room : EntityObject
{
    public int RoomId { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<Challenge>? Challenges { get; set; } = new List<Challenge>();
}