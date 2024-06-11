package entity;

import jakarta.persistence.*;
import jakarta.websocket.Session;
import lombok.*;

import jakarta.enterprise.context.ApplicationScoped;
import java.util.HashSet;
import java.util.Map;
import java.util.Set;
import java.util.concurrent.ConcurrentHashMap;

@Getter
@Setter
@Builder
@ApplicationScoped
@NoArgsConstructor
@AllArgsConstructor

@Entity
@Table(name = "lobbies")
public class Lobby {
    @Id
    @Column(name = "lobby_id")
    private String lobbyId;

    @Transient
    private Map<String, Session> sessions = new ConcurrentHashMap<>();

    @ElementCollection
    @CollectionTable(name = "lobby_users", joinColumns = @JoinColumn(name = "lobby_id"))
    @Column(name = "user_name")
    private Set<String> users = new HashSet<>();


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

    public Set<String> getUserNames(){
        return users;
    }
}