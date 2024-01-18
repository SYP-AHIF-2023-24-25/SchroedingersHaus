import entity.GameState;
import entity.Lobby;
import entity.RoomChallenge;
import jakarta.enterprise.context.ApplicationScoped;
import jakarta.inject.Inject;
import jakarta.persistence.EntityManager;
import jakarta.transaction.Transactional;
import jakarta.ws.rs.*;
import jakarta.ws.rs.core.MediaType;
import jakarta.ws.rs.core.Response;
import repository.GameStateRepository;
import repository.LobbyRepository;

import java.util.Collection;


@Consumes(MediaType.APPLICATION_JSON)

@Path("lobby")
@ApplicationScoped
public class LobbyResource {
    @Inject
    ChatService chatService;

    @Inject
    EntityManager entityManager;

    @Inject
    GameStateRepository gameStateRepository;

    @Inject
    LobbyRepository lobbyRepository;

    //für unity und testing only, Unity erstellt eine Lobby
    @POST
    @Transactional
    public Response createLobby() throws InterruptedException {
        var lobbyId = chatService.GenerateLobbyId();

        System.out.println(lobbyId);

        if(!chatService.AddLobby(lobbyId)){
            System.out.println("Erstellen fehlgeschlagen oder Lobby existiert schon");
            createLobby();
        }

        // Erstelle eine neue Lobby-Entität
        Lobby lobby = new Lobby(lobbyId);

        try {
            // Persistiere die Lobby in der Datenbank
            entityManager.persist(lobby);
        } catch (Exception e) {
            e.printStackTrace();
        }

        System.out.println("Neue Lobby erstellt");
        return Response.ok(new LoginResult(lobbyId, true)).build();
    }

    @POST
    @Transactional
    public Response saveGameState(String lobbieId, int currentRoom, int currenChallenge) throws InterruptedException {

        if(!chatService.GetAllLobbyIds().stream().findAny().equals(lobbieId)){
            System.out.println("Übergebene LobbyId existiert nicht. Kein Gamestate speichern möglich");
        }
        GameState gameState = new GameState();
        Lobby lobby = new Lobby();

        try {
            // Persistiere die Lobby in der Datenbank
            gameState = entityManager.find(GameState.class, lobbieId);
            RoomChallenge roomChallenge = new RoomChallenge(currentRoom, currenChallenge);
            gameState.setCurrentRoomChallenge(roomChallenge);

            entityManager.persist(gameState);
        } catch (Exception e) {
            e.printStackTrace();
        }

        System.out.println("GameState für Lobby " + lobbieId + " gespeichert");
        return Response.ok(gameState).build();
    }


    @GET
    @Path("/reloadProfanityFilter")
    public Response reloadFilter() throws InterruptedException {
        chatService.ReloadFilter();
        System.out.println("Filter Reloaded");
        return Response.ok().build();
    }


    //fragt nur ab ob die lobby eh existiert
    //für angular
    @GET
    @Path("/{lobbyId}")
    public Response requestLobby(@PathParam("lobbyId") String lobbyId) throws InterruptedException {
        System.out.println(lobbyId);
        if (chatService.GetAllLobbyIds().contains(lobbyId)){
            System.out.println("Lobby existiert");
            return Response.ok(new LoginResult(lobbyId, true)).build();
        }
        System.out.println("Lobby existiert nicht");
        return Response.ok(new LoginResult(lobbyId, false)).build();
    }

    //fürs testen
    @GET
    @Path("getAllLobbies")
    public Collection<Lobby> getAllLobbies(){
        return lobbyRepository.findAll();
    }

    record LoginResult(String lobby, boolean success){}

}

