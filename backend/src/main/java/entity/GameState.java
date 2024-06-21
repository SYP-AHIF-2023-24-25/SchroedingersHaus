package entity;

import jakarta.enterprise.context.ApplicationScoped;
import jakarta.persistence.*;
import lombok.*;

@Getter
@Setter
@ApplicationScoped
@NoArgsConstructor
@AllArgsConstructor

@Entity
@Table(name = "gameState")
public class GameState {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "gamestate_id")
    private int gameStateId;

    @Column(name = "current_lobby_id")
    private String currentLobbyId;

    @Column(name = "current_room_id")
    private int currentRoomId;

    @Column(name = "current_challenge_id")
    private int currentChallengeId;
}