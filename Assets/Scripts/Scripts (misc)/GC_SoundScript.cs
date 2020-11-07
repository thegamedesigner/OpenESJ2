using UnityEngine;
using System.Collections;

public class GC_SoundScript : MonoBehaviour
{
	public AudioClip[] stickyWalls = new AudioClip[0];
	public AudioClip[] missiles = new AudioClip[0];
	public AudioClip[] checkpoints = new AudioClip[0];
	public AudioClip[] checkpointsPG = new AudioClip[0];
	public AudioClip[] neonFlickers = new AudioClip[0];
	public AudioClip[] stars = new AudioClip[0];
	public AudioClip[] bounceCoin = new AudioClip[0];
	public AudioClip[] bouncePad = new AudioClip[0];
	public AudioClip[] jumpSounds = new AudioClip[0];
	public AudioClip[] doubleJumpSounds = new AudioClip[0];
	public AudioClip[] landSounds = new AudioClip[0];
	public AudioClip[] stompSounds = new AudioClip[0];
	//REMEMBER TO TURN OFF '3D SOUND' IN THE IMPORT SETTINGS

	void Awake()
	{

	}

	void Update()
	{

	}

	public enum Sounds { None, StickyWall, Missile, Checkpoint, Neon, NeonSingle, Star, BounceCoin, BouncePad, Jump, DoubleJump, Land, Stomp }


	public void playSound(Sounds sound)
	{
		switch (sound)
		{
			case Sounds.Checkpoint:
				if (!xa.pgMode) { Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.Checkpoint); }
				else { }
				break;
			case Sounds.Jump:
				Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.Jump);break;
			case Sounds.DoubleJump:
				Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.Jump);break;
			case Sounds.Land:
				Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.Land);break;
			case Sounds.BounceCoin:
				Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.BounceCoin);break;
			case Sounds.BouncePad:
				Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.BouncePad);break;
			case Sounds.Stomp:
				Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.Pound);break;

		}
		return;



		//BELOW IS DEFUNCT, BUT ALSO HASNT BEEN TRANSFERED TO THE NEW SYSTEM YET.
		//REMEMBER TO TURN OFF '3D SOUND' IN THE IMPORT SETTINGS
		if (sound == Sounds.StickyWall)
		{
			AudioSource.PlayClipAtPoint(stickyWalls[Random.Range(0, stickyWalls.Length)], Vector3.zero, fa.hardwiredVolume); //xa.soundVolume * xa.muteSound * xa.localMute);

		}
		if (sound == Sounds.Missile)
		{
			AudioSource.PlayClipAtPoint(missiles[Random.Range(0, missiles.Length)], Vector3.zero, fa.hardwiredVolume); //xa.soundVolume * xa.muteSound * xa.localMute);
		}
		if (sound == Sounds.Neon)
		{
			AudioSource.PlayClipAtPoint(neonFlickers[Random.Range(0, neonFlickers.Length)], Vector3.zero, fa.hardwiredVolume); //xa.soundVolume * xa.muteSound * xa.localMute);
		}
		if (sound == Sounds.NeonSingle)
		{
			// AudioSource.PlayClipAtPoint(neonFlickers[0], Vector3.zero);
			AudioSource.PlayClipAtPoint(neonFlickers[Random.Range(0, 2)], Vector3.zero, fa.hardwiredVolume); //xa.soundVolume * xa.muteSound * xa.localMute);
		}
		if (sound == Sounds.Star)
		{
			AudioSource.PlayClipAtPoint(stars[Random.Range(0, stars.Length)], Vector3.zero, fa.hardwiredVolume); //xa.soundVolume * xa.muteSound * xa.localMute);
		}
	}
}
