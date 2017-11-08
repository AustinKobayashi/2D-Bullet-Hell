using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Networking;

public class CameraNetScript : NetworkBehaviour {
	public Camera cam;

	// Use this for initialization
	void Start () {
		if (!isLocalPlayer) {
			cam.enabled = false;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
