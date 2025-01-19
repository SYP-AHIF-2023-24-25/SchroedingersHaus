// See https://aka.ms/new-console-template for more information
using ImportConsoleApp;
using Persistence;

Console.WriteLine("Garage - Import");
await ImportDataAsync();
await ReadDataAsync();
Console.ReadKey();

static async Task ImportDataAsync()
{
    await using var uow = new UnitOfWork();
    Console.WriteLine("Datenbank migrieren");
    await uow.DeleteDatabaseAsync();
    await uow.CreateDatabaseAsync();

    Console.WriteLine("Daten importieren");

    //var data = await ImportController.ImportDataAsync();

    /*Console.WriteLine($"- {data.ParkingSpots.Count} Parkplätze erzeugt");
    Console.WriteLine($"- {data.Bookings.Count} Buchungen erzeugt");
    Console.WriteLine($"- {data.Cars.Count} cars erzeugt\n");*/

    Console.WriteLine("Daten speichern");

    // TODO: Save data to database
    //throw new NotImplementedException("sava data to database not yet implemented");

}

static async Task ReadDataAsync()
{
    await using var uow = new UnitOfWork();

    /*var carsCount = await uow.Cars.CountAsync();
    var spotsCount = await uow.ParkingSpots.CountAsync();
    var bookingsCount = await uow.Bookings.CountAsync();
    var countOpen = await uow.ParkingSpots.CountAsync(s => s.CarId == null);*/

    /*Console.WriteLine($"- {carsCount} Cars, {spotsCount} Spots, {bookingsCount} Buchungen wurden aus DB gelesen.");
    Console.WriteLine($"- Davon sind {countOpen} Parkplätze frei");*/
}