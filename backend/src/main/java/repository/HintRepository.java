package repository;

import entity.Hint;
import jakarta.enterprise.context.ApplicationScoped;
import jakarta.enterprise.inject.Model;
import jakarta.inject.Inject;
import jakarta.persistence.EntityManager;
import jakarta.transaction.Transactional;

import java.util.Collection;


@ApplicationScoped
@Model
public class HintRepository {
    
    @Inject
    EntityManager entityManager;

    @Transactional
    public Collection<Hint> getAllHints() {
        return entityManager.createQuery("SELECT h FROM Hint h", Hint.class).getResultList();
    }

    /*@Transactional
    public Hint getHintByRoomAndChallenge(int room, int challenge) {
        return entityManager.createQuery("SELECT h FROM Hint h WHERE h.room = :room AND h.challenge = :challenge", Hint.class)
                .setParameter("room", room)
                .setParameter("challenge", challenge)
                .getSingleResult();
    }*/



    @Transactional
    public Hint findById(final Long id) {
        return entityManager.find(Hint.class, id);
    }

    @Transactional
    public void saveHint(Hint hint) {
        entityManager.persist(hint);
    }
}
