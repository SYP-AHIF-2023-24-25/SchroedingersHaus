package helper;

import entity.GameState;
import entity.Lobby;
import entity.RoomChallenge;
import jakarta.annotation.PostConstruct;
import jakarta.enterprise.context.ApplicationScoped;
import jakarta.inject.Inject;
import repository.GameStateRepository;
import repository.LobbyRepository;

@ApplicationScoped
public class DatabaseSeeder {

    @Inject
    LobbyRepository lobbyRepository;

    @Inject
    GameStateRepository gameStateRepository;

    @PostConstruct
    void seedDatabase() {
        for (int i = 1; i <= 5; i++) {
            String lobbyId = "lobby" + i;

            // Lobby erstellen und speichern
            Lobby lobby = new Lobby(lobbyId);
            lobbyRepository.saveLobby(lobby);

            // GameState erstellen und speichern
            GameState gameState = new GameState();
            gameState.setLobbyId(lobbyId);

            // Zufällige RaumId (maximal 4)
            int randomRoomId = (int) (Math.random() * 4) + 1;

            // Zufällige Challenge (maximal 10)
            int randomChallenge = (int) (Math.random() * 10) + 1;

            gameState.setCurrentRoomChallenge(new RoomChallenge(randomChallenge, randomRoomId));

            gameStateRepository.saveGameState(gameState);
        }
    }
}