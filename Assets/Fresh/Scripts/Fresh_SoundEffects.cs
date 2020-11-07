using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Structs;

public class Fresh_SoundEffects : MonoBehaviour
{
	public AudioSource[] audioSources;
	public Sound[] sounds;

	[System.Serializable]
	public class Sound
	{
		public string label;
		public AudioClip clip;
		public Type type;
	}

	public static Fresh_SoundEffects self;

	public enum Type
	{
		None,
		SwordSlice,
		Pound,
		RockImpact,
		Checkpoint,
		Jump,
		DoubleJump,
		Land,
		BounceCoin,
		BouncePad,
		GetShrine,
		Coin,
		PG_Checkpoint,
		StickyWall,
		Butt,
		Door,
		DoubleJumpExplo,
		UziFiring,
		ShotgunFiring,
		ShotgunEmpty,
		RocketLauncherEmpty,
		GetShotgunAmmo,
		ElfSound,
		GrossSplat,
		MegaSatan_Explo,
		MegaSatan_Scream,
		MegaSatan_Bounce,
		Fart,
		Highfive,
		BaseballBat,
		Achivo,
		FPSMS_Intro,
		FPSMS_Death,
		FPSMS_Scream1,
		FPSMS_Scream2,
		Multifart,
		End
	}

	void Awake()
	{
		self = this;
	}

	public static void PlaySound(Type type) { PlaySound(type, 1); }
	public static void PlaySound(Type type, float volMod)
	{
		if (self == null) { return; }

		if (type == Type.Checkpoint)
		{
			if (fa.muteCheckpointSounds)
			{
				return;
			}
			else if (xa.pgMode)
			{
				return;
				//type = Type.PG_Checkpoint;//pg checkpoints suck
			}
		}


		//Choose source
		AudioSource source = null;
		source = self.audioSources[0];
		for (int i = 0; i < self.audioSources.Length; i++)
		{
			if (!self.audioSources[i].isPlaying)
			{
				source = self.audioSources[i];
			}
		}

		source.Stop();

		//Find clip
		List<int> list = new List<int>();
		int ran = 0;
		for (int i = 0; i < self.sounds.Length; i++)
		{
			if (self.sounds[i].type == type)
			{
				list.Add(i);
			}
		}
		ran = Random.Range(0, list.Count);
		if (type == Type.Checkpoint)
		{
			int t = xa.fakeRandom;
			if (xa.fakeRandom > 1000) { xa.fakeRandom -= 1000; }
			while (t > (list.Count - 1))
			{
				t -= list.Count;
			}
			if (t < 0) { t = 0; }
			if (t > (list.Count - 1)) { t = (list.Count - 1); }
			ran = t;
			//Debug.Log("FakeRandom: " + xa.fakeRandom + ", Ran: " + ran + ", ListCount: " + list.Count);

		}

		if (type == Type.Coin)
		{
			Debug.Log("Coin types: " + list.Count);
		}

		source.volume = xa.soundVolume * xa.muteSound * xa.localMute * volMod;
		source.clip = self.sounds[list[ran]].clip;
		source.Play();
	}
}
