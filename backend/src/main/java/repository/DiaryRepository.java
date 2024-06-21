package repository;

import entity.Diary;
import jakarta.enterprise.context.ApplicationScoped;
import jakarta.inject.Inject;
import jakarta.persistence.EntityManager;
import jakarta.persistence.TypedQuery;
import jakarta.transaction.Transactional;

import java.util.List;

@ApplicationScoped
public class DiaryRepository {
    @Inject
    EntityManager entityManager;

    @Transactional
    public Diary saveDiary(Diary diary) {
        entityManager.persist(diary);
        return diary;
    }

    @Transactional
    public Diary findById(final int id) {
        return entityManager.find(Diary.class, id);
    }

    @Transactional
    public List<Diary> findAll() {
        TypedQuery<Diary> query = entityManager.createQuery("SELECT d FROM Diary d", Diary.class);
        return query.getResultList();
    }

}
