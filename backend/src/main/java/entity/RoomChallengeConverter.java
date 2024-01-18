package entity;

import jakarta.persistence.*;

@Converter
public class RoomChallengeConverter implements AttributeConverter<RoomChallenge, String> {

    @Override
    public String convertToDatabaseColumn(RoomChallenge attribute) {
        if (attribute == null) {
            return null;
        }
        return attribute.getCurrentRoom() + ":" + attribute.getCurrentChallenge();
    }

    @Override
    public RoomChallenge convertToEntityAttribute(String dbData) {
        if (dbData == null) {
            return null;
        }
        String[] parts = dbData.split(":");
        return new RoomChallenge(Integer.parseInt(parts[0]), Integer.parseInt(parts[1]));
    }
}