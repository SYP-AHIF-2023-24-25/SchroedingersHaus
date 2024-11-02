namespace Core.Contracts;

using Base.Core.Contracts;

public interface IUnitOfWork : IBaseUnitOfWork
{
    IChallengeRepository Challenges { get; }
    IGameStateRepository GameStates { get; }
    ILobbyRepository Lobbies { get; }
    IDiaryRepository Diaries { get; }
    
    IRoomRepository Rooms { get; }
}