using UnityEngine;
using System.Collections;

public class TriggerNessieScript : MonoBehaviour {
	
	GameObject nessie;
	iTweenEvent nessieTween;
	
	float beganShaking = 0;
	
	// Use this for initialization
	void Start () {
		//Debug.Log ("I started up!");
	
		nessie = GameObject.Find("Bronto");
		nessieTween = nessie.GetComponent<iTweenEvent>();
		nessieTween.Play();
	
		beganShaking = Time.time;
	
	}
	
	// Update is called once per frame
	void Update()
	{
		RandomShakeAbility shake = Camera.main.transform.parent.gameObject.GetComponent<RandomShakeAbility>();
		shake.BeginShake(1);
	
		if (Time.time - beganShaking > 2)
			Destroy(this);
	}
}
