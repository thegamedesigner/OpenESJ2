using UnityEngine;
using System.Collections;

public class SetRandomSoundToAudioSource : MonoBehaviour
{
	public AudioClip[] clips;
	public AudioClip[] pgClips;
	public bool ignorePGMode = false;

	int result = 0;

	void Start()
	{
		result = 101 * xa.fakeRandom;
		xa.fakeRandom += 1;
	
		GetComponent<AudioSource>().Stop();
		if(xa.pgMode && !ignorePGMode)
		{
			while (result >= pgClips.Length)
			{
				result -= pgClips.Length;
			}
			result = Mathf.Abs((int)(result));
	
	
			if (result < 0) { result = 0; }
			if (result > pgClips.Length - 1) { result = pgClips.Length - 1; }
			GetComponent<AudioSource>().clip = pgClips[result];
		}
		else
		{
			while (result >= clips.Length)
			{
				result -= clips.Length;
			}
			result = Mathf.Abs((int)(result));
	
	
			if (result < 0) { result = 0; }
			if (result > clips.Length - 1) { result = clips.Length - 1; }
			GetComponent<AudioSource>().clip = clips[result];
		}
		////Debug.Log (name + ": Assigning clip " + result.ToString() + " to audiosource");
		GetComponent<AudioSource>().volume = xa.soundVolume * xa.muteSound;
		GetComponent<AudioSource>().Play();
		this.enabled = false;
	}
}
