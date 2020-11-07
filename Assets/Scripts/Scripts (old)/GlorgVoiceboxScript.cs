using UnityEngine;

public class GlorgVoiceboxScript : MonoBehaviour
{
	public AudioClip[] rawrSounds;
	bool mouthClosing = true;
	
	int result = 0;
	// Use this for initialization
	void Start ()
	{
		//if(rawrSounds.Length == 0)
			//Debug.LogError("GLORG VOICEBOX: No sound effects assigned to the Glorg!");
	
		GetComponent<AudioSource>().clip = null;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	public void rawr()
	{
		//Debug.LogWarning("Glorg Rawr called!");
	
		mouthClosing = !mouthClosing;
	
		if(GetComponent<AudioSource>().isPlaying || mouthClosing)
			return;
	
		GetComponent<AudioSource>().Stop();
		result = 101 * xa.fakeRandom;
		xa.fakeRandom += 1;
	
		result = Mathf.Abs(result);
		while (result >= rawrSounds.Length)
		{
			result -= rawrSounds.Length;
		}
	
		GetComponent<AudioSource>().clip = rawrSounds[result];
		GetComponent<AudioSource>().volume = xa.soundVolume * xa.muteSound;
		//Debug.LogWarning("Playing Glorg Rawr! Time: " + Time.time.ToString());
		GetComponent<AudioSource>().PlayDelayed(0.6f);
		//audio.Play();
	}
}
