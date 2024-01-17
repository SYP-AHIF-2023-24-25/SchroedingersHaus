package entity;

import javax.websocket.Session;
import java.util.*;
import java.util.concurrent.ConcurrentHashMap;

public class Lobby {
    String lobbyId;

    //Map von Username + Session
    private final Map<String, Session> sessions = new ConcurrentHashMap<>();

    //Set von den Usernames (für Abfragen)
    private final Set<String> users = new HashSet<>();

    //aktuellstes Rätse
    private String state = "0";

    public Lobby(String lobbyId) {
        this.lobbyId = lobbyId;
    }

    public void AddSession(String userName, Session session){
        sessions.put(userName,session);
    }

    public void AddUserName(String userName){
        users.add(userName);
    }

    public void RemoveSession(String userName){
        sessions.remove(userName);
        users.remove(userName);
    }

    public boolean setState(String newState){
        //todo: abfrage ob state größer ist als alter state
        this.state = newState;
        return true;
    }

    public String getState() {
        return state;
    }

    public Set<String> getUserNames(){
        return users;
    }

    public String getLobbyId() {
        return lobbyId;
    }

    public Map<String, Session> getSessions() {
        return sessions;
    }
}
