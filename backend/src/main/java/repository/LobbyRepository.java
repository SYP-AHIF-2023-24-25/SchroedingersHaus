package repository;

import entity.Lobby;
import jakarta.enterprise.context.ApplicationScoped;
import jakarta.enterprise.inject.Model;
import jakarta.inject.Inject;
import jakarta.persistence.EntityManager;
import jakarta.transaction.Transactional;

import java.util.Collection;

@ApplicationScoped
@Model
public class LobbyRepository {
    @Inject
    EntityManager entityManager;

    @Transactional
    public Collection<Lobby> findAll() {
        return entityManager.createQuery("select lobby from Lobby lobby", Lobby.class).getResultList();
    }

    @Transactional
    public Lobby findById(final String id) {
        return entityManager.find(Lobby.class, id);
    }
}
