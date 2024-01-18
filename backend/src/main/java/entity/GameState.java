package entity;
import jakarta.persistence.*;
import lombok.*;

import jakarta.enterprise.context.ApplicationScoped;
import java.util.Map;

@Getter
@Setter
@ApplicationScoped
@NoArgsConstructor
@AllArgsConstructor

@Entity
@Table(name = "lobbies")
public class GameState extends Lobby{

    /*@Column(name = "current_room")
    private String currentRoom;

    @Column(name = "current_challenge")
    private String currentChallenge;*/

    @Column(name = "current_room_challenge", columnDefinition = "VARCHAR(255)")
    @Convert(converter = RoomChallengeConverter.class)
    private RoomChallenge currentRoomChallenge;

    @ElementCollection
    @CollectionTable(name = "solved_challenges", joinColumns = @JoinColumn(name = "gamestate_id"))
    @MapKeyColumn(name = "challenge_name")
    @Column(name = "is_solved")
    private Map<String, Boolean> solvedChallenges;
}