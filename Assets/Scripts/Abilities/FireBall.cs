using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FireBall : Ability{

	float cooldown;

	public FireBall(){
		cooldown = 0.1f;	
	}

}
