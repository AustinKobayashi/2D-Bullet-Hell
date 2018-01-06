using UnityEngine;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;

[Serializable]
public class Room : ScriptableObject {
    
    public int xPos;                      // The x coordinate of the lower left tile of the room.
    public int yPos;                      // The y coordinate of the lower left tile of the room.
    public int roomWidth;                     // How many tiles wide the room is.
    public int roomHeight;                    // How many tiles high the room is.
    public Direction enteringCorridor;    // The direction of the corridor that is entering this room.
    List<Corridor> corridors = new List<Corridor>();
    NavMeshComponent navMeshComponent;
    List<KeyValuePair<Corridor, Vector2>> gateWayPoints = new List<KeyValuePair<Corridor, Vector2>>();


    public void SetupRoom(IntRange widthRange, IntRange heightRange) {

        roomWidth = widthRange.Random;
        roomHeight = heightRange.Random;

        xPos = 0 - (int)(roomWidth / 2f);
        yPos = 0 - (int)(roomHeight / 2f);
    }



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
    }



    public void BuildRoom(GameObject dungeonParent, GameObject[] floorTiles, GameObject[] wallTiles){

        /*
        Debug.DrawLine(new Vector3(xPos + 1, yPos + 1), new Vector3(xPos + roomWidth - 2, yPos + 1), Color.red, 10000f);
        Debug.DrawLine(new Vector3(xPos + 1, yPos + roomHeight - 2), new Vector3(xPos + roomWidth - 2, yPos + roomHeight - 2), Color.red, 10000f);
        Debug.DrawLine(new Vector3(xPos + 1, yPos + 1), new Vector3(xPos + 1, yPos + roomHeight - 2), Color.red, 10000f);
        Debug.DrawLine(new Vector3(xPos + roomWidth - 2, yPos + 1), new Vector3(xPos + roomWidth - 2, yPos + roomHeight - 2), Color.red, 10000f);
        */

        for (int x = xPos; x < xPos + roomWidth; x++){
            for (int y = yPos; y < yPos + roomHeight; y++){
                if (x == xPos || x == xPos + roomWidth - 1 || y == yPos || y == yPos + roomHeight - 1){
                    PlaceWallTile(wallTiles, dungeonParent, x, y);
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



    public void BuildRoomNavMesh(NavMesh navMesh){
        navMeshComponent = new NavMeshComponent(new Vector2(xPos + 1, yPos + roomHeight - 2), new Vector2(xPos + roomWidth - 2, yPos + roomHeight - 2),
                                                                 new Vector2(xPos + 1, yPos + 1), new Vector2(xPos + roomWidth - 2, yPos + 1));
        navMesh.AddNavMeshComponent(navMeshComponent);
    }



    public void BuildRoomNavMeshGateWays(){
        
        foreach(Corridor corridor in corridors){
            NavMeshGateway navMeshGateWay = new NavMeshGateway(navMeshComponent, corridor.GetNavMeshComponent());
            navMeshComponent.AddNavMeshGateWay(navMeshGateWay);
            corridor.GetNavMeshComponent().AddNavMeshGateWay(navMeshGateWay);

        }

        GameObject point = GameObject.Find("knob");

        foreach(KeyValuePair<Corridor, Vector2> kvp in gateWayPoints){
            Instantiate(point, kvp.Value, Quaternion.identity);
        }
    }



    public void AddCorridor(Corridor corridor){ 
        corridors.Add(corridor);

        if(corridor.yPos >= yPos + roomHeight - 2 && corridor.xPos >= xPos && corridor.xPos <= xPos + roomWidth)
            gateWayPoints.Add(new KeyValuePair<Corridor, Vector2>(corridor, new Vector2(corridor.xPos + 2, yPos + roomHeight - 2.5f)));
        
        if (corridor.yPos <= yPos - 2 && corridor.xPos >= xPos && corridor.xPos <= xPos + roomWidth)
            gateWayPoints.Add(new KeyValuePair<Corridor, Vector2>(corridor, new Vector2(corridor.xPos + 2, yPos + 1.5f)));
        
        if(corridor.xPos >= xPos + roomWidth - 2 && corridor.yPos >= yPos && corridor.yPos <= yPos + roomHeight)
            gateWayPoints.Add(new KeyValuePair<Corridor, Vector2>(corridor, new Vector2(xPos + roomWidth - 2.5f, corridor.yPos + 2)));
        
        if(corridor.xPos <= xPos - 2 && corridor.yPos >= yPos && corridor.yPos <= yPos + roomHeight)
            gateWayPoints.Add(new KeyValuePair<Corridor, Vector2>(corridor, new Vector2(xPos + 1.5f, corridor.yPos + 2)));   
    }



    public bool PointIsInRoom(Vector2 point){ return point.x >= xPos && point.x < xPos + roomWidth && point.y >= yPos && point.y < yPos + roomHeight; }



    public NavMeshComponent GetNavMeshComponent() { return navMeshComponent; }
}