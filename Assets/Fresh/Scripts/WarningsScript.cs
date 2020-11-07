using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningsScript : MonoBehaviour
{
	public GameObject warning;
	public GameObject seizures;
	public GameObject motionSickness;
	public GameObject gameContains;
	public GameObject swearing;
	public GameObject murder;
	public GameObject orgasms;
	public GameObject farts;
	public GameObject blasphemy;
	public GameObject tentacles1;
	public GameObject tentacles2;
	public GameObject swearing1;
	public GameObject swearing2;
	public GameObject satanObj;
	public GameObject satanArms;
	public GameObject satanHead;
	public GameObject satanBody;
	public ParticleSystem bloodPS;

	float shake1 = 1;
	float orgasm = 2.9f;
	float fart = 3.4f;
	float satan = 3.9f;
	float blood = 2.6f;
	float swearingTime = 2.1f;
	float allowClick = 1f;

	void Start()
	{
		iTween.MoveBy(satanArms, iTween.Hash("y", 0.1f, "time", 1, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));
		iTween.MoveBy(satanHead, iTween.Hash("x", -0.15f, "time", 1, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));
		iTween.MoveBy(satanBody, iTween.Hash("y", -0.3f, "time", 0.15f, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));


		warning.transform.SetY(-20);
		seizures.transform.SetY(-20);
		motionSickness.transform.SetY(-20);
		gameContains.transform.SetY(-20);
		swearing.transform.SetY(-20);
		murder.transform.SetY(-20);
		orgasms.transform.SetY(-20);
		farts.transform.SetY(-20);
		blasphemy.transform.SetY(-20);
		iTween.MoveTo(warning, iTween.Hash("delay", 0.3f, "x", 0, "y", 8, "time", 0.2f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(seizures, iTween.Hash("delay", 0.6f, "x", 0, "y", 5, "time", 0.2f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(motionSickness, iTween.Hash("delay", 0.9f, "x", 0, "y", 3, "time", 0.2f, "easetype", iTween.EaseType.easeOutSine));

		iTween.MoveTo(gameContains, iTween.Hash("delay", 1.5f, "x", 0, "y", 0, "time", 0.2f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(swearing, iTween.Hash("delay", 1.9f, "x", 0, "y", -1.4, "time", 0.2f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(murder, iTween.Hash("delay", 2.3f, "x", 0, "y", -2.8, "time", 0.2f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(orgasms, iTween.Hash("delay", 2.8f, "x", 0, "y", -4.2, "time", 0.2f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(farts, iTween.Hash("delay", 3.3f, "x", 0, "y", -5.6, "time", 0.2f, "easetype", iTween.EaseType.easeOutSine));
		iTween.MoveTo(blasphemy, iTween.Hash("delay", 3.8f, "x", 0, "y", -7, "time", 0.2f, "easetype", iTween.EaseType.easeOutSine));


	}

	void Update()
	{
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			xa.re.cleanLoadLevel(Restart.RestartFrom.RESTART_FROM_MENU, "ESJ2Title");
		}
		if (allowClick != -1 && Time.timeSinceLevelLoad > allowClick)
		{
			allowClick = -1;
		}
		if (allowClick == -1)
		{

			if (Controls.GetAnyKeyDown() || Input.GetMouseButtonDown(0))
			{
				xa.re.cleanLoadLevel(Restart.RestartFrom.RESTART_FROM_MENU, "StartMenu");
			}
		}
		if (shake1 != -1 && Time.timeSinceLevelLoad > shake1)
		{
			shake1 = -1;
			//ScreenShakeCamera.Screenshake(1, 0.15f, ScreenShakeCamera.ScreenshakeMethod.Basic);
		}
		if (swearingTime != -1 && Time.timeSinceLevelLoad > swearingTime)
		{
			swearingTime = -1;
			iTween.MoveTo(swearing1, iTween.Hash("x", 0, "y", 0, "time", 0.2f, "easetype", iTween.EaseType.easeOutSine));
			iTween.MoveTo(swearing2, iTween.Hash("x", 0, "y", 0, "time", 0.2f, "easetype", iTween.EaseType.easeOutSine));

		}

		if (blood != -1 && Time.timeSinceLevelLoad > blood)
		{
			blood = -1;
			bloodPS.Play();
		}

		if (fart != -1 && Time.timeSinceLevelLoad > fart)
		{
			fart = -1;
			Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.Fart);
			iTween.MoveTo(tentacles1, iTween.Hash("x", 0, "y", 0, "time", 0.2f, "easetype", iTween.EaseType.easeOutSine));
			iTween.MoveTo(tentacles2, iTween.Hash("x", 0, "y", 0, "time", 0.2f, "easetype", iTween.EaseType.easeOutSine));

			//ScreenShakeCamera.Screenshake(1, 0.15f, ScreenShakeCamera.ScreenshakeMethod.Basic);
		}
		if (orgasm != -1 && Time.timeSinceLevelLoad > orgasm)
		{
			orgasm = -1;
			Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.Checkpoint);
		}
		if (satan != -1 && Time.timeSinceLevelLoad > satan)
		{
			satan = -1;
			Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.MegaSatan_Scream);
			iTween.MoveTo(satanObj, iTween.Hash("x", 0, "y", 0, "time", 0.2f, "easetype", iTween.EaseType.easeOutSine));

		}
	}
}
