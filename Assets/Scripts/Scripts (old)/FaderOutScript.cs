using System.Collections;
using UnityEngine;

public class FaderOutScript : MonoBehaviour
{
	float fadeSpd = 0;
	public GameObject failureMsg; //Optional
	bool fadeOut = false;
	string fadeOutToLevel = "";
	bool fastFade = false;
	public bool slowFade = false;
	public bool finalLevel = false;//will be white and fast for respawning, but long & black for changing to the outro
	AsyncOperation async;
	public bool preloadSystemActive = false;
	public bool triggerFailureMsg = false;

	void Start()
	{

		xa.glx = transform.position;
		// xa.glx.x = 100;
		transform.position = xa.glx;

		xa.faderOut = this.gameObject;
		xa.faderOutScript = this.gameObject.GetComponent<FaderOutScript>();

		if (za.forceChangeLevelOnStart)
		{
			fadeOutToLevel = za.forceChangeToThisLevel;
			za.forceChangeToThisLevel = "";
			za.forceChangeLevelOnStart = false;
			fadedOut();
		}
		else
		{
#if !UNITY_EDITOR
           preloadLevel();
#endif
		}
	}

	void fadedOut()
	{
		//xa.fadingOut = false;
		//xa.fadingIn = true;
		xa.frozenCamera = true;
		if (fastFade) { xa.fadeInFast = true; }
		xa.firstCheckpointTriggered = false;
		xa.cleanXa();
		string loadedLevel = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
		if (xa.localNodeScript)
		{
			if (fadeOutToLevel != loadedLevel)
			{
				//clean when going to a new level
				xa.cleanXaOncePerNewLevel();
				xa.checkpointedStarsThisLevel = 0;
				xa.hasCheckpointed = false;//THIS IS UNTESTED
				xa.deathCountThisLevel = 0;
				//za.relativeYForAwards = 0;//bit of a hack. This isn't resetting properly? It SHOULD reset on Award destroying, but this will stop it getting out of hand

				//Setup.GC_DebugLog("End of level - Forcing Garbage Collection!");
				System.GC.Collect();
			}
		}
		xa.music_Time = -999;

		if (preloadSystemActive)//Only for respawning
		{
			if (xa.fadingToSameLevel)
			{
				loadPreloaded();
			}
			else
			{
				//
				za.forceChangeLevelOnStart = true;
				za.forceChangeToThisLevel = fadeOutToLevel;
				loadPreloaded();
			}
		}
		else
		{
			//Debug.Log("Faded out. Now load level: " + fadeOutToLevel);
			UnityEngine.SceneManagement.SceneManager.LoadScene(fadeOutToLevel);
		}
	}

	void preloadLevel()
	{
		preloadSystemActive = true;
		StartCoroutine("load");
	}

	public void loadPreloaded()
	{
		async.allowSceneActivation = true;
	}

	IEnumerator load()
	{
		//Debug.LogWarning("ASYNC LOAD STARTED - " +
		//  "DO NOT EXIT PLAY MODE UNTIL SCENE LOADS... UNITY WILL CRASH");
		async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
		async.allowSceneActivation = false;
		yield return async;
	}

	void Update()
	{
		if (fadeOut)
		{
			//Check daddys love
			if (ThrowingBallScript.ReadOnly_DaddysLove == 0)
			{
				//Then slow fade
				slowFade = true;
				if (failureMsg != null)
				{
					failureMsg.SetActive(true);
				}
			}
			if(triggerFailureMsg)
			{
				if (failureMsg != null)
				{
					failureMsg.SetActive(true);
				}
			}

			fadeSpd += 0.2f * fa.deltaTime;
			xa.tempColor = this.gameObject.GetComponent<Renderer>().material.color;
			if (slowFade)
			{
				xa.tempColor.a += 0.5f * fa.deltaTime;
			}
			else
			{
				xa.tempColor.a += fadeSpd * fa.deltaTime;
				if (fastFade) { xa.tempColor.a += 6 * fa.deltaTime; }//12
			}
			xa.fadeOutProgress = xa.tempColor.a;
			//xa.fadingToOtherLevelWithMenuMusic = BardScript.checkIfGoingMenuToMenu(fadeOutToLevel, Application.loadedLevelName);

			// if done fading
			if (xa.tempColor.a >= 1)
			{
				fadedOut();
			}
			this.gameObject.GetComponent<Renderer>().material.color = xa.tempColor;
		}
	}

	public void fadeOutFunc(string lvl, bool fastFadeOrNot, string lastLvl)
	{

		fadeOutToLevel = lvl;
		fastFade = fastFadeOrNot;
		//xa.fadingToOtherLevelWithMenuMusic = BardScript.checkIfGoingMenuToMenu(fadeOutToLevel, UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
		fadeOut = true;
		if (lvl == lastLvl)
		{
			xa.fadingToSameLevel = true;
		}
		else
		{
			xa.fadingToSameLevel = false;
			GhostManager.OnLevelComplete();
		}
		xa.glx = transform.position;
		//xa.glx.x = 0;
		transform.position = xa.glx;
		xa.fadingOut = true;
		xa.fadingAtAll = true;

		if (fa.useBlackFaders)
		{
			GetComponent<Renderer>().material = xa.de.FaderMatBlack;
		}

		if (finalLevel && !xa.fadingToSameLevel)
		{
			if (lvl == "PostSatanStory")
			{
				slowFade = true;
				GetComponent<Renderer>().material.color = new Color(0, 0, 0, 0);
			}
		}

		//Debug.Log("Starting to fade out. Aiming for level: " + fadeOutToLevel);
	}
}
