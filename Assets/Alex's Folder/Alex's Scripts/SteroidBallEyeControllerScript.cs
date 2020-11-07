using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteroidBallEyeControllerScript : MonoBehaviour {

    List<GameObject> eyes = new List<GameObject>();

    readonly float blinkHoldTime = 0.1f;
    float counter = 0f;
    
    bool eyesClosed = false;

	void Start () {
        counter = 1f;
        foreach (Transform t in gameObject.transform) {
            eyes.Add(t.gameObject);
        }
	}
	
	void Update () {
	    if(counter > 0) {
            counter -= fa.deltaTime;
        } else {
            eyesClosed = !eyesClosed;
            if (eyesClosed) {
                counter = blinkHoldTime;
            } else {
                counter = Random.Range(0.5f, 5f);
            }
        }
        for(int i = 0; i < eyes.Count; i++) {
            eyes[i].SetActive(!eyesClosed);
        }
	}
}
