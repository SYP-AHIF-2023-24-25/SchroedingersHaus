import javax.inject.Inject;
import javax.ws.rs.*;
import javax.ws.rs.core.MediaType;
import javax.ws.rs.core.Response;
import java.time.LocalDateTime;
import java.util.stream.Stream;

@Produces(MediaType.APPLICATION_JSON)
@Consumes(MediaType.APPLICATION_JSON)

@Path("lobby")
public class LobbyResource {
    @Inject
    private ChatService chatService;

    //für unity und testing only, Unity erstellt eine Lobby
    @POST
    public Response createLobby() throws InterruptedException {
        var lobbyId = chatService.GenerateLobbyId();

        System.out.println(lobbyId);

        if(!chatService.AddLobby(lobbyId)){
            System.out.println("Erstellen fehlgeschlagen oder Lobby existiert schon");
            createLobby();
        }

        System.out.println("Neue Lobby erstellt");
        return Response.ok(new LoginResult(lobbyId, true)).build();
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
    public Stream<String> getAllLobbies(){
        return chatService.GetAllLobbyIds().stream();
    }

    record LoginResult(String lobby, boolean success){}

}

