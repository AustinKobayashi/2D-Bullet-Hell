using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FireShieldController : NetworkBehaviour {

	PlayerWizardStatsTest stats;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetStats(PlayerWizardStatsTest stats){
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
		Destroy (this.gameObject);
		yield return null;
	}
}
