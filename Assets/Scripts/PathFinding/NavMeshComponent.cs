using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshComponent {

    Vector2[] corners;
    Vector2 center;
    Bounds bounds;
    List<NavMeshGateway> gateWays;

    public NavMeshComponent(Vector2 topLeft, Vector2 topRight, Vector2 bottomLeft, Vector2 bottomRight, List<NavMeshGateway> gateWays){

        corners = new Vector2[4];
        corners[0] = topLeft;
        corners[1] = topRight;
        corners[2] = bottomLeft;
        corners[3] = bottomRight;
        center = new Vector2((topRight.x - topLeft.x) / 2f, (topLeft.y - bottomLeft.y) / 2f);
        bounds = new Bounds(center, new Vector2(topRight.x - topLeft.x, topLeft.y - bottomLeft.y));
        this.gateWays = gateWays;
    }

    public Vector2[] GetCorners() { return corners; }

    public bool IsInsideNavMeshComponent(Vector2 point){ return bounds.Contains(point); }

    public Vector2 ClosestPointInsideNavMeshComponent(Vector2 point){ return bounds.ClosestPoint(point); }
}
