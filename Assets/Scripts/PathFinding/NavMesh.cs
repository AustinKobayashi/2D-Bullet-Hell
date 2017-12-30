using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMesh : MonoBehaviour {

    public List<NavMeshComponent> navMeshComponents = new List<NavMeshComponent>();

    public void AddNavMeshComponent(NavMeshComponent navMeshComponent){ navMeshComponents.Add(navMeshComponent); }
}
