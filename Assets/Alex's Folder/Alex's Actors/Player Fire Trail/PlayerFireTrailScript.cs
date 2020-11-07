using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireTrailScript : MonoBehaviour {

    ParticleSystem ps = null;
    
    void Awake() {
       ps = GetComponent<ParticleSystem>();
    }

	void Update () {
        if (xa.player) {
            Vector3 pPos = xa.player.transform.position;
            transform.position = new Vector3(pPos.x, pPos.y, pPos.z + 5);
            var emission = ps.emission;
            if(pPos.x > 825) {
                emission.enabled = false;
            } else {
                emission.enabled = true;
            }
        } else {
            transform.position = new Vector3(0, 0, -100);
        }
	}
}
