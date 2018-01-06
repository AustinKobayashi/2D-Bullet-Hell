using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshComponent {

    Vector2[] corners;
    Vector2 center;
    Bounds bounds;
    List<NavMeshGateway> gateWays = new List<NavMeshGateway>();

    public NavMeshComponent(Vector2 topLeft, Vector2 topRight, Vector2 bottomLeft, Vector2 bottomRight){

        corners = new Vector2[4];
        corners[0] = topLeft;
        corners[1] = topRight;
        corners[2] = bottomLeft;
        corners[3] = bottomRight;
        center = new Vector2((topRight.x - topLeft.x) / 2f, (topLeft.y - bottomLeft.y) / 2f);
        bounds = new Bounds(center, new Vector2(topRight.x - topLeft.x, topLeft.y - bottomLeft.y));

        Debug.DrawLine(bottomLeft, bottomRight, Color.red, 10000f);
        Debug.DrawLine(topLeft, topRight, Color.red, 10000f);
        Debug.DrawLine(bottomLeft, topLeft, Color.red, 10000f);
        Debug.DrawLine(bottomRight, topRight, Color.red, 10000f);
    }


    public Vector2 RandomPointInsideNavMeshComponent(){
        return new Vector2(Random.Range(corners[2].x, corners[3].x), Random.Range(corners[2].y, corners[0].y));    
    }

    public void AddNavMeshGateWay(NavMeshGateway gateWay){ gateWays.Add(gateWay); }

    public Vector2[] GetCorners() { return corners; }

    public bool IsInsideNavMeshComponent(Vector2 point){ return bounds.Contains(point); }

    public Vector2 ClosestPointInsideNavMeshComponent(Vector2 point){ return bounds.ClosestPoint(point); }
}
