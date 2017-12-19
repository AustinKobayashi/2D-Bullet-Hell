using UnityEngine;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;

[Serializable]
public class Room : MonoBehaviour {
    
    public int xPos;                      // The x coordinate of the lower left tile of the room.
    public int yPos;                      // The y coordinate of the lower left tile of the room.
    public int roomWidth;                     // How many tiles wide the room is.
    public int roomHeight;                    // How many tiles high the room is.
    public Direction enteringCorridor;    // The direction of the corridor that is entering this room.
    List<Corridor> corridors = new List<Corridor>();

    // This is used for the first room.  It does not have a Corridor parameter since there are no corridors yet.
    public void SetupRoom(IntRange widthRange, IntRange heightRange) {

        // Set a random width and height.
        roomWidth = widthRange.Random;
        roomHeight = heightRange.Random;

        // Set the x and y coordinates so the room is roughly in the middle of the board.
        xPos = 0 - (int)(roomWidth / 2f);
        yPos = 0 - (int)(roomHeight / 2f);

        Debug.DrawLine(new Vector3(xPos + 1, yPos + 1), new Vector3(xPos + roomWidth - 2, yPos + 1), Color.red, 10000f);
        Debug.DrawLine(new Vector3(xPos + 1, yPos + roomHeight - 2), new Vector3(xPos + roomWidth - 2, yPos + roomHeight - 2), Color.red, 10000f);
        Debug.DrawLine(new Vector3(xPos + 1, yPos + 1), new Vector3(xPos + 1, yPos + roomHeight - 2), Color.red, 10000f);
        Debug.DrawLine(new Vector3(xPos + roomWidth - 2, yPos + 1), new Vector3(xPos + roomWidth - 2, yPos + roomHeight - 2), Color.red, 10000f);
    }


    // This is an overload of the SetupRoom function and has a corridor parameter that represents the corridor entering the room.
    public void SetupRoom(Vector2 roomPos, IntRange widthRange, IntRange heightRange, int maxWidth, int maxHeight, Direction direction) {

        roomWidth = widthRange.Random;
        roomHeight = heightRange.Random;

        if (roomWidth > maxWidth)
            roomWidth = maxWidth;
        if (roomHeight > maxHeight)
            roomHeight = maxHeight;
        
        if (direction == Direction.West)
            roomWidth = maxWidth;
        if (direction == Direction.South)
            roomHeight = maxHeight;
        
        xPos = (int)roomPos.x;
        yPos = (int)roomPos.y;

        Debug.DrawLine(new Vector3(xPos + 1, yPos + 1), new Vector3(xPos + roomWidth - 2, yPos + 1), Color.red, 10000f);
        Debug.DrawLine(new Vector3(xPos + 1, yPos + roomHeight - 2), new Vector3(xPos + roomWidth - 2, yPos + roomHeight - 2), Color.red, 10000f);
        Debug.DrawLine(new Vector3(xPos + 1, yPos + 1), new Vector3(xPos + 1, yPos + roomHeight - 2), Color.red, 10000f);
        Debug.DrawLine(new Vector3(xPos + roomWidth - 2, yPos + 1), new Vector3(xPos + roomWidth - 2, yPos + roomHeight - 2), Color.red, 10000f);
    }


    public void BuildRoom(GameObject dungeonParent, GameObject[] floorTiles, GameObject[] wallTiles){
        for (int x = xPos; x < xPos + roomWidth; x++){
            for (int y = yPos; y < yPos + roomHeight; y++){
                if (x == xPos || x == xPos + roomWidth - 1 || y == yPos || y == yPos + roomHeight - 1){
                    PlaceWallTile(wallTiles, dungeonParent,x, y);
                }
                else{
                    GameObject tile = Instantiate(floorTiles[Random.Range(0, floorTiles.Length)], new Vector2(x, y), Quaternion.identity) as GameObject;
                    tile.transform.parent = dungeonParent.transform;
                }
            }
        }    
    }



    void PlaceWallTile(GameObject[] wallTiles, GameObject dungeonParent, int x, int y){

        foreach (Corridor corridor in corridors)
            if (corridor.PointIsInCorridor(new Vector2(x, y)))
                return;

        GameObject wall = Instantiate(wallTiles[Random.Range(0, wallTiles.Length)], new Vector2(x, y), Quaternion.identity) as GameObject;
        wall.transform.parent = dungeonParent.transform;
    }


    public void AddCorridor(Corridor corridor){ corridors.Add(corridor); }

    public bool PointIsInRoom(Vector2 point){ return point.x >= xPos && point.x < xPos + roomWidth && point.y >= yPos && point.y < yPos + roomHeight; }
}