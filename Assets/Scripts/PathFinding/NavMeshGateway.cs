using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshGateway {

    NavMeshComponent navMeshComponent1;
    NavMeshComponent navMeshComponent2;

    public NavMeshGateway(NavMeshComponent navMeshComponent1, NavMeshComponent navMeshComponent2){
        this.navMeshComponent1 = navMeshComponent1;
        this.navMeshComponent2 = navMeshComponent2;
    }

    public NavMeshComponent GetNavMeshComponent1() { return navMeshComponent1; }
    public NavMeshComponent GetNavMeshComponent2() { return navMeshComponent2; }
}
