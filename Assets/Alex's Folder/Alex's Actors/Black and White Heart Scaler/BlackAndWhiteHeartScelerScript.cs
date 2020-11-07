using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackAndWhiteHeartScelerScript : MonoBehaviour {

    public GameObject whiteHeart, blackHeart;
    float heartCounter = 0f;
    readonly float heartTime = 1.3f;
    bool blackHeartSpawn = false;

	void Update () {
        if(heartCounter > 0) {
            heartCounter -= fa.deltaTime;
        } else {
            if (blackHeartSpawn) {
                GameObject obj = Instantiate(blackHeart, transform.position, Quaternion.identity);
				obj.transform.parent = gameObject.transform;
            } else {
                GameObject obj = Instantiate(whiteHeart, transform.position, Quaternion.identity);
				obj.transform.parent = gameObject.transform;
            }
            blackHeartSpawn = !blackHeartSpawn;
            heartCounter = heartTime;
        }
	}
}
