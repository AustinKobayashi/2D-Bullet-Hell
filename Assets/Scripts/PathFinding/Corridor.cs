using UnityEngine;
using System;
using Random = UnityEngine.Random;

// Enum to specify the direction is heading.
public enum Direction {
    North, East, South, West,
}

[Serializable]
public class Corridor : ScriptableObject {
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
            Debug.DrawLine(new Vector3(this.xPos + 1, yPos - 1), new Vector3(xPos + 3, yPos - 1), Color.green, 10000f); //bottom line
            Debug.DrawLine(new Vector3(this.xPos + 1, yPos + length), new Vector3(xPos + 3, yPos + length), Color.green, 10000f); //top line
            Debug.DrawLine(new Vector3(this.xPos + 1, yPos - 1), new Vector3(xPos + 1, yPos + length), Color.green, 10000f); //left line
            Debug.DrawLine(new Vector3(xPos + 3, yPos - 1), new Vector3(xPos + 3, yPos + length), Color.green, 10000f); //right line
        } else {
            Debug.DrawLine(new Vector3(this.xPos - 1, yPos + 1), new Vector3(xPos + length, yPos + 1), Color.green, 10000f); //bottom line
            Debug.DrawLine(new Vector3(this.xPos - 1, yPos + 3), new Vector3(xPos + length, yPos + 3), Color.green, 10000f); //top line
            Debug.DrawLine(new Vector3(this.xPos - 1, yPos + 1), new Vector3(xPos - 1, yPos + 3), Color.green, 10000f); //left line
            Debug.DrawLine(new Vector3(xPos + length, yPos + 1), new Vector3(xPos + length, yPos + 3), Color.green, 10000f); //right line
        }
    }

    public bool PointIsInCorridor(Vector2 point) {

        if (direction == Direction.North || direction == Direction.South)
            return point.x >= xPos && point.x < xPos + 5 && point.y >= yPos && point.y < yPos + corridorLength;
        
        return point.x >= xPos && point.x < xPos + corridorLength && point.y >= yPos && point.y < yPos + 5;
    }

    public void BuildCorridor(GameObject dungeonParent, GameObject[] floorTiles, GameObject[] wallTiles){
        
        if (direction == Direction.North || direction == Direction.South){
            for (int x = xPos; x < xPos + 5; x++){
                for (int y = yPos; y < yPos + corridorLength; y++){
                    if(x == xPos || x == xPos + 4){
                        GameObject wall = Instantiate(wallTiles[Random.Range(0, wallTiles.Length)], new Vector2(x, y), Quaternion.identity) as GameObject;
                        wall.transform.parent = dungeonParent.transform;
                    } else {
                        GameObject tile = Instantiate(floorTiles[Random.Range(0, floorTiles.Length)], new Vector2(x, y), Quaternion.identity) as GameObject;
                        tile.transform.parent = dungeonParent.transform;
                    }
                }
            }
        } else {
            for (int x = xPos; x < xPos + corridorLength; x++){
                for (int y = yPos; y < yPos + 5; y++){
                    if (y == yPos || y == yPos + 4){
                        GameObject wall = Instantiate(wallTiles[Random.Range(0, wallTiles.Length)], new Vector2(x, y), Quaternion.identity) as GameObject;
                        wall.transform.parent = dungeonParent.transform;
                    }
                    else{
                        GameObject tile = Instantiate(floorTiles[Random.Range(0, floorTiles.Length)], new Vector2(x, y), Quaternion.identity) as GameObject;
                        tile.transform.parent = dungeonParent.transform;
                    }
                }
            }
        }
    }
}