using UnityEngine;
using System.Collections;

public class CallbackExample : MonoBehaviour {

	void PlaySound(string volume) {
		//Setup.GC_DebugLog("called back");
		GetComponent<AudioSource>().volume = float.Parse(volume);
		GetComponent<AudioSource>().Play();
	}
	
	void ChangeToYellow() {
		GetComponent<Renderer>().material.color = Color.yellow;	
	}
}