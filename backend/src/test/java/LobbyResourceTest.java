import entity.GameState;
import entity.Lobby;
import entity.RoomChallenge;
import io.quarkus.test.junit.QuarkusTest;
import jakarta.inject.Inject;
import jakarta.transaction.Transactional;
import jakarta.ws.rs.core.Response;
import org.junit.jupiter.api.Test;
import repository.GameStateRepository;
import repository.HintRepository;
import repository.LobbyRepository;

import static io.smallrye.common.constraint.Assert.assertNotNull;
import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertTrue;

@QuarkusTest
class LobbyResourceTest {
    @Inject
    LobbyResource lobbyResource;

    @Inject
    HintRepository hintRepository;

    @Inject
    GameStateRepository gameStateRepository;

    @Inject
    LobbyRepository lobbyRepository;

    @Transactional
    @Test
    void testCreateLobby_Success() {
        try {
            Response response = lobbyResource.createLobby();
            assertEquals(200, response.getStatus());
            assertTrue(response.getEntity() instanceof LobbyResource.LoginResult);
            LobbyResource.LoginResult loginResult = (LobbyResource.LoginResult) response.getEntity();
            assertTrue(loginResult.success());
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
    }

    /*@Transactional
    @Test
    void testAddGameState_Succeed() {
        try {
            Lobby lobby = lobbyResource.getAllLobbies().stream().findFirst().get();
            GameState gameState = new GameState();
            gameState.setLobbyId(lobby.getLobbyId());
            gameState.setCurrentRoomChallenge(new RoomChallenge(1, 1));
            Response response = lobbyResource.saveGameState(gameState.getLobbyId(), gameState.getCurrentRoomChallenge().getCurrentRoom(), gameState.getCurrentRoomChallenge().getCurrentChallenge());
            assertEquals(200, response.getStatus());
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
    }*/

    /*@Transactional
    @Test
    void testAddHint_Succeed() {
        Hint hint = new Hint();
        hint.setHintText("Zerschlage mit dem Schwert die Ketten an der Tür");
        hint.setRoomChallenge(new RoomChallenge(4, 5));
        
        hintRepository.saveHint(hint);

        Hint hintFromDB = hintRepository.findById(hint.getId());

        assertNotNull(hintFromDB);
        assertEquals("Zerschlage mit dem Schwert die Ketten an der Tür", hintFromDB.getHintText());
        assertEquals(5, hintFromDB.getRoomChallenge().getCurrentChallenge());
        assertEquals(4, hintFromDB.getRoomChallenge().getCurrentRoom());
    }*/

    @Test
    @Transactional
    public void testSaveGameStateToExistingLobby() {
        // Erstelle eine Lobby
        //Lobby lobby = lobbyRepository.findById("HQuB42");
        Lobby lobby = new Lobby("VsIr42");
        //lobbyRepository.saveLobby(lobby);

        // Erstelle einen Beispiel-GameState
        GameState gameState = new GameState();
        gameState.setLobbyId(lobby.getLobbyId());
        RoomChallenge roomChallenge = new RoomChallenge(3, 2);
        gameState.setCurrentRoomChallenge(roomChallenge);

        // Speichere den GameState
        gameStateRepository.saveGameState(gameState);

        // Überprüfe, ob der GameState in der Datenbank gespeichert wurde
        GameState savedGameState = gameStateRepository.findById(lobby.getLobbyId());
        assertNotNull(savedGameState);
        assertEquals(3, savedGameState.getCurrentRoomChallenge().getCurrentRoom());
        assertEquals(2, savedGameState.getCurrentRoomChallenge().getCurrentChallenge());

        // Überprüfe, ob der GameState zur richtigen Lobby gehört
        assertEquals(lobby.getLobbyId(), savedGameState.getLobbyId());
    }
}
