using Base.Core.Contracts;
using Core.Entities;

namespace Core.Contracts;

public interface IRoomRepository : IGenericRepository<Room>
{
    public IEnumerable<Room> FindAll();

    public Room FindById(int id);

    // Speichert einen Raum
    public Room SaveRoom(Room room);

    // Aktualisiert einen Raum
    public Room UpdateRoom(Room room);

    // Löscht einen Raum
    public void DeleteRoom(int id);
}