using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Core.Entities;

namespace Core.Entities;

public class GameState : EntityObject
{
    public int GameStateId { get; set; }
    public string? CurrentLobbyId { get; set; }
    
    public int CurrentRoomId { get; set; } // Foreign Key zu Room
    public Room CurrentRoom { get; set; }
    
    public int CurrentChallengeId { get; set; } // Foreign Key zu Challenge
    public Challenge CurrentChallenge { get; set; }
    //public List<Room> Rooms { get; set; } = new List<Room>();
    
    // All-args constructor
    public GameState(string currentLobbyId)
    {
        CurrentLobbyId = currentLobbyId;
    }

    public GameState()
    {
        
    }
}