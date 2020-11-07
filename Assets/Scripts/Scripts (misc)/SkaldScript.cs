using UnityEngine;
using UnityEngine.SceneManagement;

public class SkaldScript : MonoBehaviour
{
	/*
     * - I want the Skald to play Menu music on all levels flagged as Menulevels, without interupting it on level load.
     * 
     * - I want the Skald to fade out the old music and fade in the new music for each Non-Menu level.
     * 
     * - I want the Skald to use Music (always looped) and SuperSmooth checkboxes in localNode
     * 
     */
	 
	public AudioClip menuMusicClip;
	public AudioClip altMenuMusicClip;
	public AudioSource audio1;
	public AudioSource audio2;

	public enum State { None, StartMenuMusic, MenuMusic, StartNormalMusic, NormalMusic, StartSuperSmoothMusic, SuperSmoothMusic, StartRobotMusic, RobotMusic, SnapOff, FrEdMode }
	public enum FrEdModeState
	{
		None,
		Waiting,
		Starting,
		Playing,
		End
	}
	public State state = State.None;
	public FrEdModeState frEdState = FrEdModeState.None;
	int oldLevel = -1;
	bool changedLevel = false;
	float fadeOutSpeed = 1;
	float fadeInSpeed = 1;
	int superSmoothStage = 0;
	float volume = 0;
	float desiredVolume1 = 0;
	float desiredVolume2 = 0;
	float[] spectrum = new float[128];
	bool paused = false;
	float previousVolumeAudio1 = 0;
	float previousVolumeAudio2 = 0;
	float previousTimeAudio1 = 0;
	float previousTimeAudio2 = 0;
	bool usePreviousTime = false;

	public void pauseSkald()
	{
		previousVolumeAudio1 = desiredVolume1;
		previousVolumeAudio2 = desiredVolume2;
		previousTimeAudio1 = audio1.time;
		previousTimeAudio2 = audio2.time;
		audio1.Pause();
		audio2.Pause();
		paused = true;
	}

	public void unpauseSkald()
	{
		audio1.Play();
		audio2.Play();
		audio1.time = previousTimeAudio1;
		audio2.time = previousTimeAudio2;
		desiredVolume1 = previousVolumeAudio1;
		desiredVolume2 = previousVolumeAudio2;
		paused = false;
	}

	public bool isPaused()
	{
		return paused;
	}

	public void applyMusicalChanges()
	{
		//Setup.GC_DebugLog("Tried to apply musical changes...");
		changedLevel = true;
		state = State.None;
		previousTimeAudio1 = audio1.time;
		previousTimeAudio2 = audio2.time;
		usePreviousTime = true;
	}

	void HandleMute()
	{
		audio1.volume = desiredVolume1 * xa.muteMusic * xa.localMute;
		audio2.volume = desiredVolume2 * xa.muteMusic * xa.localMute;
	}

	public void FrEd_PlayClip(AudioClip clip)//Written for FrEd editor, 
	{
		state = State.FrEdMode;
		frEdState = FrEdModeState.Starting;
		audio1.clip = clip;
		audio2.clip = null;
	}


	void Start()
	{
		if (!za.skald)
		{
			DontDestroyOnLoad(this.gameObject);
			za.skald = this.gameObject;
			za.skaldScript = this;
		}
		else
		{
			Destroy(this.gameObject);
			return;
		}

	}

	void Update()
	{

		if (paused)
		{
			//Play nothing
			if ((desiredVolume1 > 0 && audio1.isPlaying) || (desiredVolume2 > 0 && audio2.isPlaying))
			{
				//Fade the music out
				desiredVolume1 -= fadeOutSpeed * fa.deltaTime;
				desiredVolume2 -= fadeOutSpeed * fa.deltaTime;
				if (desiredVolume1 < 0) { desiredVolume1 = 0; }
				if (desiredVolume2 < 0) { desiredVolume2 = 0; }
			}
			else
			{
				desiredVolume1 = 0;
				desiredVolume2 = 0;
				audio1.Stop();
				audio2.Stop();
			}
		}
		else
		{

			volume = xa.musicVolume * xa.muteMusic * za.artificalMusicVolumeCap * xa.localMute;
		//	Debug.Log("Vol: " + volume);
			if (oldLevel != UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex)
			{
				oldLevel = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
				changedLevel = true;
			}

			//get freq

			if (audio1.isPlaying)
			{
				audio1.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
				xa.beat_Freq = spectrum[1];
				xa.music_Spectrum = spectrum;
				xa.music_Time = audio1.time;
			}
			else if (audio2.isPlaying)
			{
				audio2.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
				xa.beat_Freq = spectrum[1];
				xa.music_Spectrum = spectrum;
				xa.music_Time = audio2.time;
			}


			//States
			decideState();
			//Debug.Log("SKALD MUSIC STATE: " + state);
			if (state == State.None)
			{
				//Setup.GC_DebugLog("Is STATE: NONE");
				//Play nothing
				if ((desiredVolume1 > 0 && audio1.isPlaying) || (desiredVolume2 > 0 && audio2.isPlaying))
				{
					//Fade the music out
					desiredVolume1 -= fadeOutSpeed * fa.deltaTime;
					desiredVolume2 -= fadeOutSpeed * fa.deltaTime;
					if (desiredVolume1 < 0) { desiredVolume1 = 0; }
					if (desiredVolume2 < 0) { desiredVolume2 = 0; }
				}
				else
				{
					desiredVolume1 = 0;
					desiredVolume2 = 0;
					audio1.Stop();
					audio2.Stop();
				}
			}
			else if (state == State.FrEdMode)
			{
				switch (frEdState)
				{
					case FrEdModeState.None:
						audio1.clip = null;
						audio2.clip = null;
						if (audio1.isPlaying) { audio1.Stop(); }
						if (audio2.isPlaying) { audio2.Stop(); }
						break;
					case FrEdModeState.Starting:
						desiredVolume1 = 0;
						desiredVolume2 = 0;
						if (audio2.isPlaying) { audio2.Stop(); }
						audio1.time = 0;
						audio1.Play();
						audio1.loop = true;
						frEdState = FrEdModeState.Playing;
						break;
					case FrEdModeState.Playing:

						//Playing Normal Music...
						if (desiredVolume1 < volume)
						{
							//Fade the music in
							desiredVolume1 += fadeInSpeed * Time.deltaTime;//Don't use fa.deltaTime, so it can fade while paused.

							//If I've overshot...
							if (desiredVolume1 > volume)
							{
								desiredVolume1 = volume;
							}
						}
						if (desiredVolume1 > volume)//lower it, as needed
						{
							//Fade the music in
							desiredVolume1 -= fadeInSpeed * Time.deltaTime;//Don't use fa.deltaTime, so it can fade while paused.

							//If I've overshot...
							if (desiredVolume1 < volume)
							{
								desiredVolume1 = volume;
							}
						}
						break;

				}

			}
			else if (state == State.StartMenuMusic)
			{
				//Start playing Menu Music
				if ((desiredVolume1 > 0 && audio1.isPlaying) || (desiredVolume2 > 0 && audio2.isPlaying))
				{
					//Fade the music out
					desiredVolume1 -= fadeOutSpeed * fa.deltaTime;
					desiredVolume2 -= fadeOutSpeed * fa.deltaTime;
					if (desiredVolume1 < 0) { desiredVolume1 = 0; }
					if (desiredVolume2 < 0) { desiredVolume2 = 0; }
				}
				else
				{
					previousTimeAudio1 = 0;
					previousTimeAudio2 = 0;
					desiredVolume1 = 0;
					desiredVolume2 = 0;
					if (audio2.isPlaying) { audio2.Stop(); }
					if(fa.coinsCollected >= fa.coinsCollectedGoal)
					{
						audio1.clip = altMenuMusicClip;
					}
					else
					{
						audio1.clip = menuMusicClip;
					}
					audio1.time = 0;
					audio1.Play();
					audio1.loop = true;
					state = State.MenuMusic;
				}
			}
			else if (state == State.MenuMusic)
			{
				//Debug.Log("HERE" + desiredVolume1 + ", " + Time.time);
				//Adjust volume to desired level...
				if (desiredVolume1 < volume)
				{
					//Fade the music in
					desiredVolume1 += fadeInSpeed * fa.deltaTime;

					//If I've overshot...
					if (desiredVolume1 > volume)
					{
						desiredVolume1 = volume;
					}
				}
				if (desiredVolume1 > volume)
				{
					//Fade the music in
					desiredVolume1 -= fadeOutSpeed * fa.deltaTime;

					//If I've overshot...
					if (desiredVolume1 < volume)
					{
						desiredVolume1 = volume;
					}
				}
			}
			else if (state == State.StartNormalMusic)
			{
				if ((desiredVolume1 > 0 && audio1.isPlaying) || (desiredVolume2 > 0 && audio2.isPlaying))
				{
					//Fade the music out
					desiredVolume1 -= fadeOutSpeed * fa.deltaTime;
					desiredVolume2 -= fadeOutSpeed * fa.deltaTime;
					if (desiredVolume1 < 0) { desiredVolume1 = 0; }
					if (desiredVolume2 < 0) { desiredVolume2 = 0; }
				}
				else
				{
					previousTimeAudio1 = 0;
					previousTimeAudio2 = 0;
					desiredVolume1 = 0;
					desiredVolume2 = 0;
					if (audio2.isPlaying) { audio2.Stop(); }
					audio1.clip = xa.localNodeScript.music;
					audio1.time = 0;
					audio1.Play();
					audio1.loop = true;
					state = State.NormalMusic;


					if (xa.localNodeScript.usePlayPoint)
					{
						audio1.time = xa.localNodeScript.playPoint;
						//Debug.Log("HERE IN MUSIC");
					}
				}
			}
			else if (state == State.NormalMusic)
			{
				//Playing Normal Music...
				if (desiredVolume1 < volume)
				{
					//Fade the music in
					desiredVolume1 += fadeInSpeed * Time.deltaTime;//Don't use fa.deltaTime, so it can fade while paused.

					//If I've overshot...
					if (desiredVolume1 > volume)
					{
						desiredVolume1 = volume;
					}
				}
				if (desiredVolume1 > volume)//lower it, as needed
				{
					//Fade the music in
					desiredVolume1 -= fadeInSpeed * Time.deltaTime;//Don't use fa.deltaTime, so it can fade while paused.

					//If I've overshot...
					if (desiredVolume1 < volume)
					{
						desiredVolume1 = volume;
					}
				}
			}
			else if (state == State.StartSuperSmoothMusic)
			{
				if ((desiredVolume1 > 0 && audio1.isPlaying) || (desiredVolume2 > 0 && audio2.isPlaying))
				{
					//Fade the music out
					desiredVolume1 -= fadeOutSpeed * fa.deltaTime;
					desiredVolume2 -= fadeOutSpeed * fa.deltaTime;
					if (desiredVolume1 < 0) { desiredVolume1 = 0; }
					if (desiredVolume2 < 0) { desiredVolume2 = 0; }
				}
				else
				{
					if (xa.localNodeScript)
					{
						previousTimeAudio1 = 0;
						previousTimeAudio2 = 0;
						desiredVolume1 = 0;
						desiredVolume2 = 0;
						if (audio2.isPlaying) { audio2.Stop(); }
						superSmoothStage = 0;
						audio1.clip = xa.localNodeScript.SuperSmoothSytem_introTrack;
						audio1.time = 0;
						audio1.Play();
						audio1.loop = true;
						state = State.SuperSmoothMusic;
					}
				}
			}
			else if (state == State.SuperSmoothMusic)
			{
				//Playing Super Smooth Music...

				if (superSmoothStage == 0)
				{
					//In first stage.
					if (desiredVolume1 < volume)
					{
						//Fade the music in
						desiredVolume1 += fadeInSpeed * fa.deltaTime;

						//If I've overshot...
						if (desiredVolume1 > volume)
						{
							desiredVolume1 = volume;
						}
					}

					if (audio1.time >= (audio1.clip.length - 3))
					{
						superSmoothStage = 1;
					}

				}
				else if (superSmoothStage == 1)
				{
					audio2.clip = xa.localNodeScript.SuperSmoothSytem_loopTrack;
					audio2.loop = true;
					audio2.Play();

					float result = audio1.time - (audio1.clip.length - 3);
					if (result < 0) { result = 0; }
					audio2.time = result;
					superSmoothStage = 2;
				}
				else if (superSmoothStage == 2)
				{
					if (xa.localNodeScript)
					{
						if (audio1.time >= ((audio1.clip.length - 3) + 2f))
						{
							superSmoothStage = 3;
							desiredVolume2 = desiredVolume1;
							desiredVolume1 = 0;
							// audio1.Stop();
						}
					}
				}
				else if (superSmoothStage == 3)
				{
					if (desiredVolume2 < volume)
					{
						//Fade the music in
						desiredVolume2 += fadeInSpeed * fa.deltaTime;

						//If I've overshot...
						if (desiredVolume2 > volume)
						{
							desiredVolume2 = volume;
						}
					}
				}



			}

			if (state == State.SnapOff)
			{
				desiredVolume1 = 0;
				desiredVolume2 = 0;
				previousTimeAudio1 = 0;
				previousTimeAudio2 = 0;
				audio1.Stop();
				audio2.Stop();
				state = State.None;
			}
		}
		HandleMute();

	}

	public void forceState(State inState)
	{
		state = inState;
	}

	void decideState()//Called EVERY frame!
	{
		//if in editor level
		//if (SceneManager.GetActiveScene().buildIndex == 37)
		//{
		//	state = State.FrEdMode;
//
//
		//}
		//else
		//{
			//Have I switched levels?
			if (changedLevel == true)//Gets here once per changing levels
			{
				//Am I on a menu music level & Am Not already playing Menu Music?
				if (isMenuLevel())
				{
					//Menu level, so if I'm not playing menu music, start.
					if (state != State.StartMenuMusic && state != State.MenuMusic)
					{
						state = State.StartMenuMusic;
					}
				}
				else
				{

					if (xa.localNodeScript)
					{
						if (xa.localNodeScript.useSuperSmoothTransferIntoLooping)
						{
							//I am on a SuperSmooth level.
							//I've just changed to a different level, so I should override everything start playing new music.
							if (state != State.StartSuperSmoothMusic && state != State.SuperSmoothMusic)
							{
								state = State.StartSuperSmoothMusic;
							}
						}
						else if (xa.localNodeScript.music)//Is there normal music?
						{
							//I'm in a normal music level.
							//I've just changed to a different level, so I should override everything start playing new music.
							state = State.StartNormalMusic;
						}
						else
						{
							//Must be a none level
							state = State.None;
						}
					}
				}

				if (usePreviousTime)//so far, only called in applyChanges.
				{
					usePreviousTime = false;
					audio1.time = previousTimeAudio1;
					audio2.time = previousTimeAudio2;
				}

				changedLevel = false;
			//}
		}
	}

	bool isMenuLevel()
	{
		if (xa.fresh_localNode != null)
		{
			if (xa.fresh_localNode.isMenuLevel) { return true; }
		}

		// if (Application.loadedLevelName == "SetControls") { return true; }
		return false;
	}
}
