using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimeRainSoundScript : MonoBehaviour {

	//This script mutes the rain sound effect when the player goes above the clouds.
	void Update () {
		if(xa.player != null){
			GetComponent<AudioSource>().enabled = (xa.playerPos.y < 10);
		}
	}
}
