import entity.Challenge;
import entity.Diary;
import entity.GameState;
import entity.Lobby;
import jakarta.enterprise.context.ApplicationScoped;
import jakarta.inject.Inject;
import jakarta.persistence.EntityManager;
import jakarta.transaction.Transactional;
import jakarta.ws.rs.*;
import jakarta.ws.rs.core.MediaType;
import jakarta.ws.rs.core.Response;
import repository.ChallengeRepository;
import repository.DiaryRepository;
import repository.GameStateRepository;
import repository.LobbyRepository;

import java.io.IOException;
import java.net.InetAddress;
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

    @Inject
    ChallengeRepository challengeRepository;

    @Inject
    DiaryRepository diaryRepository;

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
            lobbyRepository.saveLobby(lobby);
        } catch (Exception e) {
            e.printStackTrace();
        }
        //saveGameState(lobbyId, 1, 1);

        System.out.println("Neue Lobby erstellt");
        return Response.ok(new LoginResult(lobbyId, true)).build();
    }

    @POST
    @Path("/{lobbyId}/gameState")
    @Consumes(MediaType.APPLICATION_JSON)
    @Transactional
    public Response saveGameState(@PathParam("lobbyId") String lobbyId, GameState gameState) throws InterruptedException {

        if(!lobbyRepository.findAll().stream().findAny().equals(lobbyId)){
            System.out.println("Übergebene LobbyId existiert nicht. Kein Gamestate speichern möglich");
        }
        var newGameState = new GameState();
        newGameState.setCurrentLobbyId(lobbyId);
        newGameState.setCurrentRoomId(gameState.getCurrentRoomId());
        newGameState.setCurrentChallengeId(gameState.getCurrentChallengeId());

        try {
            // Persistiere die Lobby in der Datenbank
            gameStateRepository.saveGameState(newGameState);
        } catch (Exception e) {
            e.printStackTrace();

        }
        System.out.println("GameState für Lobby " + lobbyId + " gespeichert");
        return Response.ok(newGameState).build();
    }

    @GET
    @Path("/{lobbyId}/gameState")
    @Transactional
    public Response getGameState(@PathParam("lobbyId") String lobbyId) throws InterruptedException {
        var gameState = gameStateRepository.findById(lobbyId);
        if (gameState == null) {
            return Response.status(Response.Status.NOT_FOUND).build();
        }
        return Response.ok(gameState).build();
    }

    /*@GET
    @Path("/reloadProfanityFilter")
    public Response reloadFilter() throws InterruptedException {
        chatService.ReloadFilter();
        System.out.println("Filter Reloaded");
        return Response.ok().build();
    }*/


    //fragt nur ab ob die lobby eh existiert
    //für angular
    @GET
    @Path("/{lobbyId}")
    public Response requestLobby(@PathParam("lobbyId") String lobbyId) throws InterruptedException {
        System.out.println(lobbyId);
        /*
        if (chatService.GetAllLobbyIds().contains(lobbyId)){
            System.out.println("Lobby existiert");
            return Response.ok(new LoginResult(lobbyId, true)).build();
        }
        System.out.println("Lobby existiert nicht");*/
        if (lobbyRepository.findById(lobbyId) != null) {
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

    @GET
    @Path("/challenge/{challengeId}/")
    @Transactional
    public Response getChallenge(@PathParam("challengeId") String challengeId) throws InterruptedException {
        var challenge = challengeRepository.findById(challengeId);
        if (challenge == null) {
            return Response.status(Response.Status.NOT_FOUND).build();
        }
        return Response.ok(challenge).build();
    }

    @POST
    @Path("/challenge")
    @Consumes(MediaType.APPLICATION_JSON)
    @Transactional
    public Response saveChallenge(Challenge challenge) {
        Challenge newChallenge = new Challenge();
        newChallenge.setName(challenge.getName());
        newChallenge.setDescription(challenge.getDescription());
        newChallenge.setHint(challenge.getHint());

        try {
            challengeRepository.saveChallenge(newChallenge);
        } catch (Exception e) {
            e.printStackTrace();
        }
        System.out.println(newChallenge.getName() + " challenge created");
        return Response.ok(newChallenge).build();
    }

    @POST
    @Path("/diary")
    @Consumes(MediaType.APPLICATION_JSON)
    @Transactional
    public Response saveDiary(Diary diary) {

        Diary newDiary = new Diary();
        newDiary.setChallengeId(diary.getChallengeId());
        newDiary.setChapter(diary.getChapter());
        newDiary.setEntry(diary.getEntry());

        try {
            diaryRepository.saveDiary(newDiary);
        } catch (Exception e) {
            e.printStackTrace();
        }

        System.out.println("Diary Entry" + newDiary.getChapter() + " created");
        return Response.ok().build();
    }

    @GET
    @Path("/ping")
    @Transactional
    public Response ping (@QueryParam("hostName") String hostName) {
        Boolean reachable = false;
        try {
            reachable = InetAddress.getByName(hostName).isReachable(1000);
        } catch (IOException e) {
            e.printStackTrace();
            return Response.status(Response.Status.BAD_REQUEST).build();
        }
        return Response.ok(reachable).build();
    }


    @GET
    @Path("/diary")
    @Transactional
    public Collection<Diary> getAllDiaries(){
        return diaryRepository.findAll();
    }


    record LoginResult(String lobby, boolean success){}

}

