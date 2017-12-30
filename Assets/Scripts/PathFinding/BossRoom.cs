using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : Room {

    public void SetupRoom(Vector2 roomPos, int width, int height, Direction direction) {
        
        roomWidth = width;
        roomHeight = height;

        xPos = (int)roomPos.x;
        yPos = (int)roomPos.y;
    }
}
