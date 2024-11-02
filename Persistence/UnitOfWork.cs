using Core.Contracts;
using Core.Entities;

namespace Persistence;

using Base.Persistence;

public class UnitOfWork : BaseUnitOfWork, IUnitOfWork
{
        private readonly ApplicationDbContext? _dbContext;
        public UnitOfWork() : this(new ApplicationDbContext())
        { }
        public UnitOfWork(ApplicationDbContext dBContext) : base(dBContext) 
        {
            _dbContext = dBContext;
            Challenges = new ChallengeRepository(_dbContext);
            GameStates = new GameStateRepository(_dbContext);
            Lobbies = new LobbyRepository(_dbContext);
            Diaries = new DiaryRepository(_dbContext);
            Rooms = new RoomRepository(_dbContext);
        }

        public IRoomRepository Rooms { get; }
        public IChallengeRepository Challenges { get; }
        public IGameStateRepository GameStates { get; }
        public ILobbyRepository Lobbies { get; }
        public IDiaryRepository Diaries { get; }

}