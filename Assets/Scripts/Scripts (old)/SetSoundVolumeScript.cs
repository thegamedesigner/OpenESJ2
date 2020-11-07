using UnityEngine;
using System.Collections;

public class SetSoundVolumeScript : MonoBehaviour
{
	public float overrideVolume = 0;
	public float offSetVolume   = 0;

	void Awake()
	{
		AudioSource sound;
		sound = this.gameObject.GetComponent<AudioSource>();
	
		if(overrideVolume != 0)
		{
			sound.volume = overrideVolume * xa.muteSound;
		}
		else
		{
            sound.volume = (xa.soundVolume + offSetVolume) * xa.muteSound * xa.localMute;
		}
	}
}
