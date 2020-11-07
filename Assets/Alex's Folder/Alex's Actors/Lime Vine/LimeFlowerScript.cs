using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimeFlowerScript : MonoBehaviour {
	
	public int appearAtIndex = 0;
	public LimeVineControllerScript lv = null;
	
	void Awake() {
			transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
	}
	
	void Update () {
			if(lv.currentVinePointIndex  >= appearAtIndex){
				iTween.ScaleTo(gameObject, Vector3.one, 1f);
				this.enabled = false;
			}
	}
}
