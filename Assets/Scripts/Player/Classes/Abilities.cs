using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Abilities : NetworkBehaviour {

	public Ability firstAbility;
	public Ability secondAbility;
    public Ability thirdAbility;

	public Ability GetFirstAbility(){
		return firstAbility;
	}

	public Ability GetSecondAbility(){
		return secondAbility;
	}

    public Ability GetThirdAbility(){
        return thirdAbility;
    }
}
