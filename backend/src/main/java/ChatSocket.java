import jakarta.enterprise.inject.Model;
import jakarta.inject.Inject;
import jakarta.websocket.*;
import jakarta.websocket.server.PathParam;
import jakarta.websocket.server.ServerEndpoint;

import jakarta.enterprise.context.ApplicationScoped;


@ApplicationScoped
@Model
@ServerEndpoint("/chat/{lobbyId}/{username}")
public final class ChatSocket {

    @Inject
    ChatService chatService;


    @OnOpen
    public void onOpen(Session session,
                       @PathParam("username") String username,
                       @PathParam("lobbyId") String lobbyId) {
        chatService.AddUserSession(lobbyId,username,session);
    }

    @OnClose
    public void onClose(Session session,
                        @PathParam("username") String username,
                        @PathParam("lobbyId") String lobbyId) {

        chatService.RemoveUser(lobbyId,username);

        broadcast("User " + username + " left", lobbyId);

        //lobby kann nur vom Unity client geschlossen werden -> deshalb kein Timeout
    }

    @OnError
    public void onError(Session session,
                        @PathParam("username") String username,
                        @PathParam("lobbyId") String lobbyId, Throwable throwable) {

        this.chatService.RemoveUser(lobbyId,username);
        broadcast("User " + username + " left on error: " + throwable, lobbyId);
    }

    @OnMessage
    public void onMessage(String message,
                          @PathParam("username") String username,
                          @PathParam("lobbyId") String lobbyId) {

        /*
         * JSONObject jo = new JSONObject();
         * jo.put("type", "chat");
         * jo.put("text", message); either
         * jo.put("state", null); or
         *
         * jo.put("type", "puzzle");
         * jo.put("text", null); either
         * jo.put("state", message); or
         *
         * if(jo.getString("type") == "chat") {
         * }
         *
         * */
        if (message.equalsIgnoreCase("_ready_")) {
            broadcast("User " + username + " joined", lobbyId);
        }
        else {
            if(chatService.CheckMessage(message) == message){
                broadcast(">> " + username + ": " + message, lobbyId);
            }
            else {
                broadcast(">> " + username + " used a bad word", lobbyId);
            }
        }
    }

    //versendet Nachrichten an alle User der selben Lobby
    private void broadcast(String message, String lobbyId) {
        chatService.GetAllSessionsFromLobby(lobbyId).values().forEach(session -> {
            session.getAsyncRemote().sendObject(message, result -> {
                if (result.getException() != null) {
                    System.out.println("Unable to send message: " + result.getException());
                }
            });
        });
    }
}