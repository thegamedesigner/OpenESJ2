using UnityEngine;
using System.Collections;

public class DestroySoundWhenFinishedScript : MonoBehaviour
{
	AudioSource sound;
	bool hasPlayed = false;
	void Start()
	{
		sound = this.gameObject.GetComponent<AudioSource>();
	}

	void Update()
	{
		if (sound.isPlaying) { hasPlayed = true; }
		if (!sound.isPlaying && hasPlayed) { Destroy(this.gameObject); }
	}
}
