package repository;

import entity.GameState;
import jakarta.enterprise.context.ApplicationScoped;
import jakarta.enterprise.inject.Model;
import jakarta.inject.Inject;
import jakarta.persistence.EntityManager;
import jakarta.transaction.Transactional;

@ApplicationScoped
@Model
public class GameStateRepository {
    @Inject
    EntityManager entityManager;

    @Transactional
    public GameState findById(final String id) {
        return entityManager.find(GameState.class, id);
    }

    @Transactional
    public GameState saveGameState(GameState gameState) {
        entityManager.persist(gameState);
        return gameState;
    }
}