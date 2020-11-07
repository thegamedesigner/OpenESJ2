using UnityEngine;
using System.Collections;

public class RandomShakeAbility : MonoBehaviour {

	float shakeAmount = 10;
	
	void Start () {
	
	}
	
	public void BeginShake(float amount) {
		shakeAmount = amount;
	}
	
	// Update is called once per frame
	void Update () {
	
		transform.position = new Vector3(Random.Range(-shakeAmount, shakeAmount), Random.Range(-shakeAmount, shakeAmount), 30);
	
		shakeAmount *= 0.9f;
	}
}
