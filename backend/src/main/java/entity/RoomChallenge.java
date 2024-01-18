package entity;

import jakarta.enterprise.context.ApplicationScoped;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Getter
@Setter
@ApplicationScoped
@NoArgsConstructor
@AllArgsConstructor


public class RoomChallenge {
    private int currentRoom;
    private int currentChallenge;
}
