using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodOrangeSkyStripeScript : MonoBehaviour {
    
    public enum ScreenSide { Top, Bottom }
    public ScreenSide side;

    bool triggered = false;
    float yAddition = 0f;
    float trigDist = 0f;

	void Awake () {
	    transform.localScale = new Vector3(1f, 0f, 1f);
        Vector3 startP = transform.position;
        yAddition = startP.y;
        if (side == ScreenSide.Bottom) {
            trigDist = -5;
        } else {
            trigDist = -20;           
        }
	}
	
	void Update () {
	    Vector3 pos = transform.position;
        pos = new Vector3(transform.position.x, Camera.main.transform.position.y + yAddition, transform.position.z);   
        transform.position = pos;

        if (xa.player && !triggered) {
            if(xa.playerPos.x > pos.x + trigDist) { 
                iTween.ScaleTo(gameObject, iTween.Hash(
                    "x", 1f, "y", 1f, "z", 1f,
                    "time", 2f,
                    "easetype", iTween.EaseType.linear,
                    "looptype", iTween.LoopType.none
                ));
                triggered = true;
            }
        }
	}
}
