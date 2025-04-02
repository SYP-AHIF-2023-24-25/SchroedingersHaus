using Persistence;

namespace GarageWebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("api/v1/user")]
[Produces("application/json")]
[Consumes("application/json")]
public class UserController : ControllerBase
{
    private readonly ChatService _chatService;

    public UserController(ChatService chatService)
    {
        _chatService = chatService;
    }

    // Für Angular: Benutzer anfordern
    [HttpPost]
    public async Task<ActionResult<LoginResult>> RequestUser([FromBody] LoginRequest request)
    {
        var userName = request.UserName;
        var lobbyId = request.LobbyId;
        
        // Simulierte Verzögerung
        await Task.Delay(1500);

        // Überprüfen, ob der Benutzer bereits in der Lobby ist
        if (_chatService.GetAllUsersFromLobby(lobbyId).Contains(userName))
        {
            return Ok(new LoginResult(userName, lobbyId, false));
        }

        // Benutzername bis zur Erstellung der Sitzung reservieren
        _chatService.AddUserSession(lobbyId, userName, null);
        return Ok(new LoginResult(userName, lobbyId, true));
    }

    // Test-Methode: Gibt alle Benutzer in einer Lobby zurück
    [HttpGet("{lobbyId}")]
    public ActionResult<IEnumerable<string>> GetUsersInLobby(string lobbyId)
    {
        var users = _chatService.GetAllUsersFromLobby(lobbyId);
        if (users == null || !users.Any())
        {
            return NotFound("Keine Benutzer in der Lobby gefunden.");
        }

        return Ok(users);
    }

    // Hilfsklassen für Login-Resultate und Requests
    public record LoginResult(string UserName, string LobbyId, bool Success);
    public record LoginRequest(string UserName, string LobbyId);
}