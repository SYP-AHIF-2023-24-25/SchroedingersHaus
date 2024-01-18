package entity;

import jakarta.persistence.*;
import lombok.*;

import jakarta.enterprise.context.ApplicationScoped;

@Getter
@Setter
@ApplicationScoped
@NoArgsConstructor
@AllArgsConstructor

@Entity
@Table(name = "hints")
public class Hint {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    /*@Column(name = "room_id")
    private int roomId;

    @Column(name = "challenge_number")
    private int challengeNumber;*/

    @Column(name = "room_challenge")
    @Convert(converter = RoomChallengeConverter.class)
    private RoomChallenge roomChallenge;

    @Column(name = "hint_text")
    private String hintText;
}
