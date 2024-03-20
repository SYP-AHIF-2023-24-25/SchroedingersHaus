import jakarta.enterprise.context.ApplicationScoped;
import jakarta.inject.Inject;
import jakarta.ws.rs.*;
import jakarta.ws.rs.core.MediaType;
import jakarta.ws.rs.core.Response;

import java.util.stream.Stream;
@ApplicationScoped

@Produces(MediaType.APPLICATION_JSON)
@Consumes(MediaType.APPLICATION_JSON)
@Path("user")
public class UserResource {

    @Inject
    ChatService chatService;


    //für unity user todo?


    //für angular
    @POST
    public Response requestUser(LoginRequest request) throws InterruptedException {

        var userName = request.userName;
        var lobbyId = request.lobbyId;
        System.out.println(lobbyId);
        System.out.println(userName);
        Thread.sleep(1500);


        if (chatService.GetAllUsersFromLobby(lobbyId).contains(userName)){
            return Response.ok(new LoginResult(userName, lobbyId,false)).build();
        }

        //nutzernamen belegen bis zum anlegen der Session
        chatService.AddUserName(lobbyId,userName);
        System.out.println("Username ist frei");
        return Response.ok(new LoginResult(userName, lobbyId, true)).build();

    }

    //zum testen
    @GET
    @Path("/{lobbyId}")
    public Stream<String> getUsersInLobby(@PathParam("lobbyId") String lobbyId) throws InterruptedException {
        return chatService.GetAllUsersFromLobby(lobbyId).stream();
    }

    record LoginResult(String userName, String lobbyId, boolean success){}
    record LoginRequest(String userName, String lobbyId){}
}