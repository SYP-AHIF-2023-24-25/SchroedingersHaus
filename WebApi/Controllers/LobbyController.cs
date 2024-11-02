using Persistence;

namespace GarageWebApi.Controllers;

using Core.Entities;
using Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

[ApiController]
[Route("api/v1/lobby")]
public class LobbyController : ControllerBase
{
    private readonly ChatService _chatService;
    private readonly ILobbyRepository _lobbyRepository;
    private readonly IGameStateRepository _gameStateRepository;
    private readonly IChallengeRepository _challengeRepository;
    private readonly IDiaryRepository _diaryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRoomRepository _roomRepository;

    public LobbyController(ChatService chatService, ILobbyRepository lobbyRepository, 
        IGameStateRepository gameStateRepository, IChallengeRepository challengeRepository, 
        IDiaryRepository diaryRepository, IRoomRepository roomRepository, IUnitOfWork unitOfWork)
    {
        _chatService = chatService;
        _lobbyRepository = lobbyRepository;
        _gameStateRepository = gameStateRepository;
        _challengeRepository = challengeRepository;
        _diaryRepository = diaryRepository;
        _roomRepository = roomRepository;
        _unitOfWork = unitOfWork;
    }

    // Erstellt eine neue Lobby (für Unity und Tests)
    [HttpPost]
    public async Task<ActionResult<LoginResultL>> CreateLobby()
    {
        var lobbyId = _chatService.GenerateLobbyId();
        
        if (!_chatService.AddLobby(lobbyId))
        {
            return BadRequest("Lobby konnte nicht erstellt werden oder existiert bereits.");
        }

        var lobby = new Lobby(lobbyId);

        try
        {
            _lobbyRepository.SaveLobby(lobby);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return StatusCode((int)HttpStatusCode.InternalServerError, "Lobby konnte nicht gespeichert werden.");
        }

        return Ok(new LoginResultL(lobbyId, true));
    }

    public record GameStatePostDto(int CurrentRoomId, int CurrentChallengeId);
    // Speichert einen GameState für eine Lobby
    [HttpPost("{lobbyId}/gameState")]
    public ActionResult SaveGameState(string lobbyId, [FromBody] GameStatePostDto gameStatePostDto)
    {
        if (_lobbyRepository.FindById(lobbyId) == null)
        {
            return NotFound("Lobby existiert nicht. Gamestate kann nicht gespeichert werden.");
        }
        
        var room = _roomRepository.FindById(gameStatePostDto.CurrentRoomId);
        var challenge = _challengeRepository.FindById(gameStatePostDto.CurrentChallengeId);
        
        if (room == null || challenge == null)
        {
            return NotFound("Raum oder Challenge existiert nicht. Gamestate kann nicht gespeichert werden.");
        }

        var newGameState = new GameState
        {
            CurrentLobbyId = lobbyId,
            CurrentRoom = room,
            CurrentChallenge = challenge
        };

        try
        {
            _gameStateRepository.SaveGameState(newGameState);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return StatusCode((int)HttpStatusCode.InternalServerError, "GameState konnte nicht gespeichert werden.");
        }

        return Ok(newGameState);
    }

    // Gibt einen gespeicherten GameState einer Lobby zurück
    [HttpGet("{lobbyId}/gameState")]
    public ActionResult<GameState> GetGameState(string lobbyId)
    {
        var gameState = _gameStateRepository.FindByLobbyId(lobbyId);
        if (gameState == null)
        {
            return NotFound("GameState nicht gefunden.");
        }
        return Ok(gameState);
    }
    
    // Gibt alle GameStates zurück
    [HttpGet("gameStates")]
    public ActionResult<IEnumerable<GameState>> GetAllGameStates()
    {
        var gameStates = _gameStateRepository.FindAll();
        return Ok(gameStates);
    }
    
    // Updated einen GameState
    [HttpPut("{lobbyId}/gameState")]
    public ActionResult UpdateGameState(string lobbyId, [FromBody] GameStatePostDto gameStatePostDto)
    {
        var gameState = _gameStateRepository.FindByLobbyId(lobbyId);
        if (gameState == null)
        {
            return NotFound("GameState nicht gefunden.");
        }
        
        var room = _roomRepository.FindById(gameStatePostDto.CurrentRoomId);
        var challenge = _challengeRepository.FindById(gameStatePostDto.CurrentChallengeId);

        if (room == null || challenge == null)
        {
            return NotFound("Raum oder Challenge existiert nicht.");
        }

        gameState.CurrentLobbyId = lobbyId;
        gameState.CurrentRoom = room;
        gameState.CurrentChallenge = challenge;
        
        try
        {
            //_gameStateRepository.SaveGameState(gameState);
            _unitOfWork.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return StatusCode((int)HttpStatusCode.InternalServerError, "GameState konnte nicht aktualisiert werden.");
        }

        return Ok();
    }
    
    // Überprüft, ob eine Lobby existiert (für Angular)
    [HttpGet("{lobbyId}")]
    public ActionResult<LoginResultL> RequestLobby(string lobbyId)
    {
        if (_lobbyRepository.FindById(lobbyId) != null)
        {
            Console.WriteLine("Lobby gefunden.");
            return Ok(new LoginResultL(lobbyId, true));
        }
        Console.WriteLine("Lobby nicht gefunden.");
        return Ok(new LoginResultL(lobbyId, false));
    }

    // Gibt alle Lobbys zurück
    [HttpGet("getAllLobbies")]
    public ActionResult<IList<Lobby>> GetAllLobbies()
    {
        var lobbies = _lobbyRepository.FindAll();
        return Ok(lobbies);
    }

    // Gibt eine Challenge anhand der ID zurück
    [HttpGet("challenge/{challengeId}")]
    public ActionResult<Challenge> GetChallenge(int challengeId)
    {
        var challenge = _challengeRepository.FindById(challengeId);
        if (challenge == null)
        {
            return NotFound("Challenge nicht gefunden.");
        }
        return Ok(challenge);
    }

    public record ChallengePostDto(string Name, string Description, string Hint, int RoomId);
    // Speichert eine neue Challenge
    [HttpPost("challenge")]
    public ActionResult SaveChallenge([FromBody] ChallengePostDto challengePostDto)
    {
        var room = _roomRepository.FindById(challengePostDto.RoomId);
        if (room == null)
        {
            return NotFound("Raum existiert nicht. Challenge kann nicht gespeichert werden.");
        }
        
        var newChallenge = new Challenge
        {
            Name = challengePostDto.Name,
            Description = challengePostDto.Description,
            Hint = challengePostDto.Hint,
            RoomId = challengePostDto.RoomId
        };

        try
        {
            _challengeRepository.SaveChallenge(newChallenge);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return StatusCode((int)HttpStatusCode.InternalServerError, "Challenge konnte nicht gespeichert werden.");
        }

        return Ok(newChallenge);
    }
    
    public record RoomPostDto(int RoomId, string Name);
    //Speichert einen Raum
    [HttpPost("room")]
    public ActionResult SaveRoom([FromBody] RoomPostDto roomPostDto)
    {
        var newRoom = new Room
        {
            Name = roomPostDto.Name,
            RoomId = roomPostDto.RoomId
        };
        
        try
        {
            _roomRepository.SaveRoom(newRoom);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return StatusCode((int)HttpStatusCode.InternalServerError, "Raum konnte nicht gespeichert werden.");
        }

        return Ok(newRoom);
    }

    // Speichert ein neues Tagebuch
    [HttpPost("diary")]
    public ActionResult SaveDiary([FromBody] Diary diary)
    {
        var newDiary = new Diary
        {
            ChallengeId = diary.ChallengeId,
            Chapter = diary.Chapter,
            Entry = diary.Entry
        };

        try
        {
            _diaryRepository.SaveDiary(newDiary);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return StatusCode((int)HttpStatusCode.InternalServerError, "Tagebuch konnte nicht gespeichert werden.");
        }

        return Ok();
    }

    // Gibt alle Tagebucheinträge zurück
    [HttpGet("diary")]
    public ActionResult<IEnumerable<Diary>> GetAllDiaries()
    {
        var diaries = _diaryRepository.FindAll();
        return Ok(diaries);
    }

    // Empfangt und speichert einen Screenshot
    [HttpPost("{lobbyId}/screenshot")]
    public ActionResult ReceiveScreenshot(string lobbyId, [FromBody] byte[] imageData)
    {
        var filename = Path.Combine("wwwroot", "assets", "img", "LiveView.png");
        try
        {
            System.IO.File.WriteAllBytes(filename, imageData);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return StatusCode((int)HttpStatusCode.InternalServerError, "Screenshot konnte nicht gespeichert werden.");
        }

        return Ok();
    }

    // Gibt den gespeicherten Screenshot zurück
    [HttpGet("{lobbyId}/screenshot")]
    public ActionResult GetScreenshot(string lobbyId)
    {
        var filename = Path.Combine("wwwroot", "assets", "img", "LiveView.png");
        if (!System.IO.File.Exists(filename))
        {
            return NotFound();
        }

        var imageData = System.IO.File.ReadAllBytes(filename);
        return File(imageData, "image/png");
    }

    // Überprüft, ob ein Host erreichbar ist (Ping)
    [HttpGet("ping")]
    public ActionResult<bool> Ping([FromQuery] string hostName)
    {
        try
        {
            var reachable = Dns.GetHostAddresses(hostName).Length > 0;
            return Ok(reachable);
        }
        catch
        {
            return BadRequest("Host konnte nicht erreicht werden.");
        }
    }
    
    // Gibt einen Hint zu einer Challenge zurück
    [HttpGet("hint/{challengeId}")]
    public ActionResult<string> GetHint(int challengeId)
    {
        var challenge = _challengeRepository.FindById(challengeId);
        if (challenge == null)
        {
            return NotFound("Challenge nicht gefunden.");
        }
        return Ok(challenge.Hint);
    }

    // Hilfsklasse für die Login-Ergebnisse
    public record LoginResultL(string Lobby, bool Success);
}