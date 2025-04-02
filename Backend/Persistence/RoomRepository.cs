using Base.Persistence;
using Core.Contracts;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class RoomRepository : GenericRepository<Room>, IRoomRepository
{
    private readonly ApplicationDbContext _context;

    public RoomRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public IEnumerable<Room> FindAll()
    {
        return _context.Rooms
            .Include(r => r.Challenges) // Eager Loading der Challenges
            .ToList();
    }
    
    public Room FindById(int id)
    {
        return _context.Rooms
            .Include(r => r.Challenges)
            .FirstOrDefault(r => r.Id == id);
    }

    // Speichert einen Raum
    public Room SaveRoom(Room room)
    {
        _context.Rooms.Add(room);
        _context.SaveChanges();
        return room;
    }

    // Aktualisiert einen Raum
    public Room UpdateRoom(Room room)
    {
        _context.Rooms.Update(room);
        _context.SaveChanges();
        return room;
    }

    // Löscht einen Raum
    public void DeleteRoom(int id)
    {
        var room = FindById(id);
        if (room != null)
        {
            _context.Rooms.Remove(room);
            _context.SaveChanges();
        }
    }
}