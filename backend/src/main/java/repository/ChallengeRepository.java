package repository;

import entity.Challenge;
import jakarta.enterprise.context.ApplicationScoped;
import jakarta.inject.Inject;
import jakarta.persistence.*;
import jakarta.transaction.*;

import java.util.List;

@ApplicationScoped
public class ChallengeRepository {

    @Inject
    EntityManager entityManager;

    @Transactional
    public Challenge saveChallenge(Challenge challenge) {
        entityManager.persist(challenge);
        return challenge;
    }

    @Transactional
    public Challenge findById(final String id) {
        return entityManager.find(Challenge.class, id);
    }

    @Transactional
    public List<Challenge> findAll() {
        TypedQuery<Challenge> query = entityManager.createQuery("SELECT c FROM Challenge c", Challenge.class);
        return query.getResultList();
    }

}
