using UnityEngine;
using System;

// Enum to specify the direction is heading.
public enum Direction {
    North, East, South, West,
}

[Serializable]
public class Corridor {
    public int xPos;         // The x coordinate for the start of the corridor.
    public int yPos;         // The y coordinate for the start of the corridor.
    public int corridorLength;            // How many units long the corridor is.
    public Direction direction;   // Which direction the corridor is heading from it's room.

    public void SetUpCorridor(int xPos, int yPos, int length, Direction direction) {

        this.xPos = xPos;
        this.yPos = yPos;
        corridorLength = length;
        this.direction = direction;

        if (direction == Direction.North || direction == Direction.South) {
            Debug.DrawLine(new Vector3(this.xPos + 1, yPos), new Vector3(xPos + 3, yPos), Color.green, 10000f);
            Debug.DrawLine(new Vector3(this.xPos + 1, yPos + length), new Vector3(xPos + 3, yPos + length), Color.green, 10000f);
            Debug.DrawLine(new Vector3(this.xPos + 1, yPos), new Vector3(xPos + 1, yPos + length), Color.green, 10000f);
            Debug.DrawLine(new Vector3(xPos + 3, yPos), new Vector3(xPos + 3, yPos + length), Color.green, 10000f);
        } else {
            Debug.DrawLine(new Vector3(this.xPos, yPos + 1), new Vector3(xPos + length, yPos + 1), Color.green, 10000f);
            Debug.DrawLine(new Vector3(this.xPos, yPos + 3), new Vector3(xPos + length, yPos + 3), Color.green, 10000f);
            Debug.DrawLine(new Vector3(this.xPos, yPos + 1), new Vector3(xPos, yPos + 3), Color.green, 10000f);
            Debug.DrawLine(new Vector3(xPos + length, yPos + 1), new Vector3(xPos + length, yPos + 3), Color.green, 10000f);
        }
    }

    public bool PointIsInCorridor(Vector2 point) {

        if (direction == Direction.North || direction == Direction.South)
            return point.x >= xPos && point.x < xPos + 5 && point.y >= yPos && point.y < yPos + corridorLength;
        
        return point.x >= xPos && point.x < xPos + corridorLength && point.y >= yPos && point.y < yPos + 5;
    }
}