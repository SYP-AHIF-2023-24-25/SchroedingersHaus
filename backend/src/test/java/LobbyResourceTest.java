import entity.GameState;
import entity.Lobby;
import io.quarkus.test.junit.QuarkusTest;
import jakarta.inject.Inject;
import jakarta.ws.rs.core.Response;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertTrue;

@QuarkusTest
class LobbyResourceTest {
    @Inject
    LobbyResource lobbyResource;

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

    @Test
    void testAddGameState_Succeed() {
        try {
            Lobby lobby = lobbyResource.getAllLobbies().stream().findFirst().get();
            GameState gameState = new GameState();
            gameState.setLobbyId(lobby.getLobbyId());
            gameState.setCurrentRoom("1");
            gameState.setCurrentChallenge("1");
            Response response = lobbyResource.saveGameState(gameState.getLobbyId(), gameState.getCurrentRoom(), gameState.getCurrentChallenge());
            assertEquals(200, response.getStatus());
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
    }
}
