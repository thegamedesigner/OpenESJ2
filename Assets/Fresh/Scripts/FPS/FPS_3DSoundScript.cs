using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_3DSoundScript : MonoBehaviour
{
	public AudioSource source;
	public AudioClip[] clips;
	public float volMulti = 1;

	float timeSet = 0;
	public float killAfterXSeconds = 5;
	void Start()
	{
		timeSet = Time.time;
		source.clip = clips[Random.Range(0,clips.Length)];
		source.volume = xa.soundVolume * xa.muteSound * xa.localMute * volMulti;
		source.Play();
	}

	void Update()
	{
		if (Time.time > (timeSet + killAfterXSeconds))
		{
			Destroy(this.gameObject);
		}
	}
}
