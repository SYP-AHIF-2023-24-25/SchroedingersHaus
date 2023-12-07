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

    @Column(name = "current_room")
    private String currentRoom;

    @Column(name = "current_challenge")
    private String currentChallenge;

    @ElementCollection
    @CollectionTable(name = "solved_challenges", joinColumns = @JoinColumn(name = "gamestate_id"))
    @MapKeyColumn(name = "challenge_name")
    @Column(name = "is_solved")
    private Map<String, Boolean> solvedChallenges;

    // Beispiel-Methode zum Speichern des Gamestates
    public void saveGameState(EntityManager entityManager) {
        EntityTransaction transaction = null;

        try {
            transaction = entityManager.getTransaction();
            transaction.begin();

            entityManager.persist(this); // Das aktuelle GameStateEntity-Objekt wird gespeichert

            transaction.commit();
        } catch (Exception e) {
            if (transaction != null && transaction.isActive()) {
                transaction.rollback();
            }
            e.printStackTrace(); // In der Praxis sollten Sie eine geeignete Logging-Lösung verwenden
        }
    }
}