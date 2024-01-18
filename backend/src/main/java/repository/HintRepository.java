package repository;

import entity.Hint;
import jakarta.enterprise.context.ApplicationScoped;
import jakarta.enterprise.inject.Model;
import jakarta.inject.Inject;
import jakarta.persistence.EntityManager;
import jakarta.transaction.Transactional;


@ApplicationScoped
@Model
public class HintRepository {
    
    @Inject
    EntityManager entityManager;

    @Transactional
    public Hint findById(final Long id) {
        return entityManager.find(Hint.class, id);
    }

    @Transactional
    public void saveHint(Hint hint) {
        entityManager.persist(hint);
    }
}
