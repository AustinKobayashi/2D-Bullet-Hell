using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FireShieldController : NetworkBehaviour {

    AbstractPlayerStats stats;


    public void SetStats(AbstractPlayerStats stats){
		this.stats = stats;
	}


	[Server]
	public void AddDefence(int amount, int duration){
		StartCoroutine (Shield (amount, duration));
	}


	IEnumerator Shield(int amount, int duration){
		stats.CmdIncreaseDefence (amount);
		yield return new WaitForSeconds (duration);
		stats.CmdDecreaseDefence (amount);
        NetworkServer.Destroy(gameObject);
		Destroy (gameObject);
		yield return null;
	}
}
