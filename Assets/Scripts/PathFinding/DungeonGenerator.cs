using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

//todo add start portal, treasure room spanwer.
public class DungeonGenerator : MonoBehaviour {

    public IntRange numRooms;
    public IntRange roomWidth;
    public IntRange roomHeight;
    public int bossRoomWidth;
    public int bossRoomHeight;
    public IntRange corridorLength;
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public List<Room> rooms = new List<Room>();
    public List<Corridor> corridors = new List<Corridor>();
    GameObject dungeonParent;
    Vector2 nullVector = new Vector2(-1000, -1000);
    bool spawnedBossRoom;



    void Start(){
        dungeonParent = new GameObject("Dungeon Parent");
        PlaceStartRoom();
        PlaceRoom(rooms[0], 0);

        if(!spawnedBossRoom){
            for (int i = rooms.Count - 1; i > 0; i++){
                PlaceBossRoom(rooms[i]);
                if (spawnedBossRoom)
                    break;
            }
            if (!spawnedBossRoom) {
                rooms.Clear();
                corridors.Clear();
                Start();
            }
        }

        BuildRooms();
        BuildCorridors();
    }



    void PlaceStartRoom(){

        Room startRoom = ScriptableObject.CreateInstance<Room>();
        startRoom.SetupRoom(roomWidth, roomHeight);
        rooms.Add(startRoom);
    }



    void PlaceRoom(Room connectedRoom, int recursionDepth){

        if (recursionDepth > numRooms.Random && spawnedBossRoom){
            return;                
        } 
        if (recursionDepth > numRooms.m_Max && !spawnedBossRoom){
            PlaceBossRoom(connectedRoom);
            return;
        }
        
        List<Direction> directions = GetRandomValidCorridorDirections(connectedRoom, Random.Range(1, 5));
        if (directions.Count == 0)
            return;

        Debug.Break();

        foreach (Direction direction in directions){
            Vector2 roomPos = FindValidRoomPlacement(connectedRoom, direction);
            Vector2 maxRoomDimensions = FindMaxRoomSize(roomPos);

            if (roomPos == nullVector || maxRoomDimensions == nullVector)
                break;

            Room room = ScriptableObject.CreateInstance<Room>();
            room.SetupRoom(roomPos, roomWidth, roomHeight, (int)maxRoomDimensions.x, (int)maxRoomDimensions.y, direction);
            rooms.Add(room);

            Corridor corridor = PlaceCorridor(connectedRoom, room, direction);

            if (corridor != null) {
                room.AddCorridor(corridor);
                connectedRoom.AddCorridor(corridor);
                PlaceRoom(room, recursionDepth + 1);
            }
        }
    }



    void PlaceBossRoom(Room connectedRoom){
        List<Direction> directions = GetRandomValidCorridorDirections(connectedRoom, Random.Range(1, 5));
        if (directions.Count == 0)
            return;

        foreach (Direction direction in directions){
            Vector2 roomPos = FindValidRoomPlacement(connectedRoom, direction, true);

            if (roomPos == nullVector)
                break;

            BossRoom bossRoom = ScriptableObject.CreateInstance<BossRoom>();
            bossRoom.SetupRoom(roomPos, bossRoomWidth, bossRoomHeight, direction);
            rooms.Add(bossRoom);

            Corridor corridor = PlaceCorridor(connectedRoom, bossRoom, direction);

            if (corridor != null){
                spawnedBossRoom = true;
                bossRoom.AddCorridor(corridor);
                connectedRoom.AddCorridor(corridor);
                return;
            }
        }
    }



    Vector2 FindValidRoomPlacement(Room room, Direction direction, bool bossRoom = false){

        switch(direction){
            case Direction.North:
                for (int x = room.xPos; x < room.xPos + room.roomWidth - 5; x++){
                    for (int y = room.yPos + room.roomHeight + corridorLength.m_Max - 1; y >= room.yPos + room.roomHeight; y--){
                        if(bossRoom){
                            if(RoomCanBePlaced(x, y, bossRoomWidth, bossRoomHeight))
                                return new Vector2(x, y);

                        } else if (RoomCanBePlaced(x, y, roomWidth.m_Min, roomHeight.m_Min))
                            return new Vector2(x, y);
                    }
                }
                break;
            case Direction.East:
                for (int x = room.xPos + room.roomWidth + corridorLength.m_Max - 1; x >= room.xPos + room.roomWidth; x--){
                    for (int y = room.yPos; y < room.yPos + room.roomHeight - 5; y++){
                        if(bossRoom){
                            if(RoomCanBePlaced(x, y, bossRoomWidth, bossRoomHeight))
                                return new Vector2(x, y);

                        } else if (RoomCanBePlaced(x, y, roomWidth.m_Min, roomHeight.m_Min))
                            return new Vector2(x, y);
                    }
                }
                break;
            case Direction.South:
                for (int x = room.xPos; x < room.xPos + room.roomWidth - 5; x++){
                    for (int y = room.yPos - corridorLength.m_Max - roomHeight.m_Max + 1; y <= room.yPos - roomHeight.m_Min; y++){
                        if(bossRoom){
                            if(RoomCanBePlaced(x, y, bossRoomWidth, bossRoomHeight))
                                return new Vector2(x, y);

                        } else if (RoomCanBePlaced(x, y, roomWidth.m_Min, roomHeight.m_Min))
                            return new Vector2(x, y);
                    }
                }
                break;
            case Direction.West:
                for (int x = room.xPos - corridorLength.m_Max - roomWidth.m_Max + 1; x <= room.xPos - roomWidth.m_Min; x++){
                    for (int y = room.yPos; y < room.yPos + room.roomHeight - 5; y++){
                        if(bossRoom){
                            if(RoomCanBePlaced(x, y, bossRoomWidth, bossRoomHeight))
                                return new Vector2(x, y);

                        } else if (RoomCanBePlaced(x, y, roomWidth.m_Min, roomHeight.m_Min))
                            return new Vector2(x, y);
                    }
                }
                break;
        }

        return nullVector;
    }



    Vector2 FindMaxRoomSize(Vector2 roomPos){

        if (roomPos == nullVector)
            return nullVector;
        
        for (int width = roomWidth.m_Max; width >= roomWidth.m_Min; width--){
            for (int height = roomHeight.m_Max; height >= roomHeight.m_Min; height--){
                if (RoomCanBePlaced((int)roomPos.x, (int)roomPos.y, width, height))
                    return new Vector2(width, height);
            }
        }

        return nullVector;
    }



    Corridor PlaceCorridor(Room startRoom, Room endRoom, Direction direction){

        Corridor corridor = ScriptableObject.CreateInstance<Corridor>();
        Vector2 pos = FindValidCorridorPlacement(startRoom, endRoom, direction);

        if(pos == nullVector){
            rooms.Remove(endRoom);
            return null;
        }

        switch(direction){
            case Direction.North:
                corridor.SetUpCorridor((int)pos.x, (int)pos.y, endRoom.yPos - (startRoom.yPos + startRoom.roomHeight - 2), direction);
                corridors.Add(corridor);
                break;
            case Direction.East:
                corridor.SetUpCorridor((int)pos.x, (int)pos.y, endRoom.xPos - (startRoom.xPos + startRoom.roomWidth - 2), direction);
                corridors.Add(corridor);
                break;
            case Direction.South:
                corridor.SetUpCorridor((int)pos.x, (int)pos.y, startRoom.yPos - (endRoom.yPos + endRoom.roomHeight - 2), direction);
                corridors.Add(corridor);
                break;
            case Direction.West:
                corridor.SetUpCorridor((int)pos.x, (int)pos.y, startRoom.xPos - (endRoom.xPos + endRoom.roomWidth - 2), direction);
                corridors.Add(corridor);
                break;
        }

        return corridor;
    }



    Vector2 FindValidCorridorPlacement(Room startRoom, Room endRoom, Direction direction){

        List<Vector2> positions = new List<Vector2>();

        switch (direction){
            case Direction.North:
                for (int x = endRoom.xPos; x < Mathf.Min(startRoom.xPos + startRoom.roomWidth - 4, endRoom.xPos + endRoom.roomWidth - 4); x++)
                    if (CorridorCanBePlaced(startRoom, x, startRoom.yPos + startRoom.roomHeight - 1, 5, endRoom.yPos - (startRoom.yPos + startRoom.roomHeight)))
                        positions.Add(new Vector2(x, startRoom.yPos + startRoom.roomHeight - 1)); //changed startRoom.yPos + startRoom.roomHeight - 1 to get the debug.drawlines to touch
                return positions.Count == 0 ? nullVector : positions[Random.Range(0, positions.Count)];
            case Direction.East:
                for (int y = endRoom.yPos; y < Mathf.Min(startRoom.yPos + startRoom.roomHeight - 4, endRoom.yPos + endRoom.roomHeight - 4); y++)
                    if (CorridorCanBePlaced(startRoom, startRoom.xPos + startRoom.roomWidth, y, endRoom.xPos - (startRoom.xPos + startRoom.roomWidth), 5))
                        positions.Add(new Vector2(startRoom.xPos + startRoom.roomWidth - 1, y));
                return positions.Count == 0 ? nullVector : positions[Random.Range(0, positions.Count)];
            case Direction.South:
                for (int x = endRoom.xPos; x < Mathf.Min(startRoom.xPos + startRoom.roomWidth - 4, endRoom.xPos + endRoom.roomWidth - 4); x++)
                    if (CorridorCanBePlaced(startRoom, x, endRoom.yPos + endRoom.roomHeight, 5, startRoom.yPos - (endRoom.yPos + endRoom.roomHeight)))
                        positions.Add(new Vector2(x, endRoom.yPos + endRoom.roomHeight - 1));
                return positions.Count == 0 ? nullVector : positions[Random.Range(0, positions.Count)];
            case Direction.West:
                for (int y = endRoom.yPos; y < Mathf.Min(startRoom.yPos + startRoom.roomHeight - 4, endRoom.yPos + endRoom.roomHeight - 4); y++)
                    if (CorridorCanBePlaced(startRoom, endRoom.xPos + endRoom.roomWidth, y, startRoom.xPos - (endRoom.xPos + endRoom.roomWidth), 5))
                        positions.Add(new Vector2(endRoom.xPos + endRoom.roomWidth - 1, y));
            return positions.Count == 0 ? nullVector : positions[Random.Range(0, positions.Count)];
        }

        return nullVector;
    }



    bool RoomCanBePlaced(int xPos, int yPos, int width, int height){

        foreach (Room room in rooms){
            foreach(Corridor corridor in corridors){
                for (int x = xPos; x < xPos + width; x++){
                    for (int y = yPos; y < yPos + height; y++){
                        if (room.PointIsInRoom(new Vector2(x, y)) || corridor.PointIsInCorridor(new Vector2(x, y)))
                            return false;
                    }
                }
            }
        }
        return true;
    }



    bool CorridorCanBePlaced(Room connectedRoom, int xPos, int yPos, int width, int height){


        foreach (Room room in rooms){
            foreach (Corridor corridor in corridors){
                for (int x = xPos; x < xPos + width; x++){
                    for (int y = yPos; y < yPos + height; y++){
                        if ((room.PointIsInRoom(new Vector2(x, y)) && connectedRoom != room) || corridor.PointIsInCorridor(new Vector2(x, y)))
                            return false;
                    }
                }
            }
        }
        return true;
        
    }



    public List<Direction> GetRandomValidCorridorDirections(Room room, int numCorridors){

        List<Direction> directions = new List<Direction>();

        for (int i = 0; i < 4; i++){
            if (FindValidRoomPlacement(room, (Direction)i) != nullVector)
                directions.Add((Direction)i);
        }

        while (directions.Count > numCorridors){
            directions.RemoveAt(Random.Range(0, directions.Count));
        }

        return directions;
    }



    void BuildRooms(){
        foreach (Room room in rooms){
            room.BuildRoom(dungeonParent, floorTiles, wallTiles);
        }
    }



    void BuildCorridors(){
        
        foreach(Corridor corridor in corridors){
            corridor.BuildCorridor(dungeonParent, floorTiles, wallTiles);
        }
    }
}