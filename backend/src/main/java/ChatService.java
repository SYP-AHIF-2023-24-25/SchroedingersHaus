import entity.Lobby;
import helper.ProfanityFilter;
import helper.RandomStringGenerate;

import javax.enterprise.context.ApplicationScoped;
import javax.websocket.Session;
import java.util.*;
import java.util.stream.Collectors;

@ApplicationScoped
public class ChatService {

    private ProfanityFilter filter = new ProfanityFilter();

    //Set von den allen Lobbies
    private Set<Lobby> lobbies = new HashSet<>();

    //Set von den LobbyIds
    private Set<String> lobbyIds = new HashSet<>();

    private RandomStringGenerate generateString = new RandomStringGenerate();


    public Map<String, Session> GetAllSessionsFromLobby(String lobbyId){
        return lobbies.stream().filter(lobby -> lobby.getLobbyId().equals(lobbyId))
                .findFirst().orElse(null).getSessions();
    }

    public void RemoveLobby(String lobbyId){
        lobbyIds.remove(lobbyId);
        lobbies.removeIf(lobby -> lobby.getLobbyId().equals(lobbyId));
    }

    public void ReloadFilter(){
        filter.reloadFilter();
    }

    public String CheckMessage(String input){
        return filter.filterText(input);
    }

    public boolean AddLobby(String lobbyId){
        if(!GetAllLobbyIds().contains(lobbyId)){
            Lobby lobby = new Lobby(lobbyId);
            lobbyIds.add(lobbyId);
            lobbies.add(lobby);
            return true;
        }
        return false;
    }
    public Set<String> GetAllUsersFromLobby(String lobbyId){
      return lobbies.stream().filter(lobby -> lobby.getLobbyId().equals(lobbyId))
              .toList().get(0).getUserNames();
    }q

    public Set<String> GetAllLobbyIds() {
        return lobbyIds;
    }

    //fügt einen User samt Session hinzu
    public void AddUserSession(String lobbyId, String userName, Session session) {
        lobbies.stream().filter(lobby -> lobby.getLobbyId().equals(lobbyId))
                .toList().get(0).AddSession(userName, session);
    }

    public void RemoveUser(String lobbyId ,String userName) {
        lobbies.stream().filter(lobby -> lobby.getLobbyId().equals(lobbyId))
                .toList().get(0).RemoveSession(userName);
    }

    //belegt den username
    public void AddUserName(String lobbyId, String userName) {
        this.lobbies.stream().filter(lobby -> lobby.getLobbyId().equals(lobbyId))
                .toList().get(0).AddUserName(userName);
    }

    public String GenerateLobbyId() {
        return this.generateString.GenerateRandomString(6);
    }
}