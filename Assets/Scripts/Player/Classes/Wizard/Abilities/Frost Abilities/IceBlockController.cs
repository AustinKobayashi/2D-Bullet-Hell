using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//Dont think we need this
public class IceBlockController : NetworkBehaviour {

    /*
    [SyncVar]
    public NetworkInstanceId parentNetId;

    public override void OnStartClient(){
        // When we are spawned on the client,
        // find the parent object using its ID,
        // and set it to be our transform's parent.
        GameObject parentObject = ClientScene.FindLocalObject(parentNetId);
        transform.SetParent(parentObject.transform);
    }
    */
}
