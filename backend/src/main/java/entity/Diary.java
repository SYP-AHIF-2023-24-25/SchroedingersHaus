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
@Table(name = "diary")
public class Diary {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "diary_id")
    private int id;

    @Column(name = "challenge_id")
    private int challengeId;

    @Column(name = "chapter")
    private String chapter;

    @Column(name = "entry")
    private String entry;
}
