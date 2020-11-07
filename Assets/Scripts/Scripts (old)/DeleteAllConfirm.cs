using UnityEngine;
using System.Collections;

public class DeleteAllConfirm : MonoBehaviour
{
    /*
	Vector3 mousePosOld = Vector3.zero;
	public GameObject selectBox = null;
	int result = 0;
	bool poppedUp = false;
	bool triggered = false;
	bool yesSelected = false;

	void Awake()
	{
		moveDown();
		ResetPopup();
	}

	void LateUpdate()
	{
		//Popup
		if(xa.deleteAllConfirm && poppedUp == false)
		{
			//Debug.Log ("VideoSettingsConfirm: Popping up!");
			yesSelected = false;
			xa.glx = selectBox.transform.localPosition;
			xa.glx.x = 2;
			selectBox.transform.localPosition = xa.glx;
			triggered = false;
			moveUp();
		}
	
		//Controls
		else if(xa.deleteAllConfirm && poppedUp == true)
		{
			//Mouse
			bool usingMouse = false;
			if (Vector3.Distance(mousePosOld, Input.mousePosition) > 0.1f && mousePosOld != Vector3.zero)
			//if (Vector3.Distance(mousePosOld, AspectUtility.mousePosition) > 0.1f && mousePosOld != Vector3.zero)
			{
				usingMouse = true;
			}
			mousePosOld = Input.mousePosition;
	
			if(usingMouse)
			{
				Ray ray = new Ray();
				ray = Camera.main.camera.ScreenPointToRay(Input.mousePosition);
				xa.glx = ray.GetPoint(30);
	
				if(xa.glx.x <= 17.5f)
				{
					moveToYes();
				}
				else
					moveToNo();
			}

			if(yesSelected && Input.GetMouseButtonDown(0))
			{
				deleteAllProgress();
			}
			else if(!yesSelected && Input.GetMouseButtonDown(0))
			{
				moveDown();
			}
	
			//Manual Controls to Choose
			if (//Input.GetKeyDown(KeyCode.Joystick1Button7) ||
				//Input.GetKeyDown(KeyCode.Joystick2Button7) ||
				//Input.GetKeyDown(KeyCode.Joystick3Button7) ||
				//Input.GetKeyDown(KeyCode.Joystick4Button7) ||
				//Input.GetKeyDown(KeyCode.X) ||
				CustomControls.GetKeyDown(CustomControls.Action.STOMP) ||
				Input.GetKeyDown(KeyCode.Escape))
			{
				triggered = true;
			}
			if (triggered) { moveDown(); }
	
			if (yesSelected)
			{
				triggered = false;
				if (Input.GetKeyDown(KeyCode.Return)) { triggered = true; }
				//if (Input.GetKeyDown(KeyCode.Joystick1Button0) ||
				//	Input.GetKeyDown(KeyCode.Joystick2Button0) ||
				//	Input.GetKeyDown(KeyCode.Joystick3Button0) ||
				//   	Input.GetKeyDown(KeyCode.Joystick4Button0) ||
				//	Input.GetKeyDown(KeyCode.Z))
				if (CustomControls.GetKeyDown(CustomControls.Action.JUMP))
				{
					triggered = true;
				}
				if (triggered) {
					deleteAllProgress();
				}
			}
			else
			{
				triggered = false;
				if (Input.GetKeyDown(KeyCode.Return)) { triggered = true; }
				//if (Input.GetKeyDown(KeyCode.Joystick1Button0) ||
				//	Input.GetKeyDown(KeyCode.Joystick2Button0) ||
				//	Input.GetKeyDown(KeyCode.Joystick3Button0) ||
				//	Input.GetKeyDown(KeyCode.Joystick4Button0) ||
				//	Input.GetKeyDown(KeyCode.Z))
				if (CustomControls.GetKeyDown(CustomControls.Action.JUMP))
				{
					triggered = true;
	
				}
				if (triggered) {
					moveDown();
					}
			}
	
			//check move to yes?
			result = 0;
			//if (Input.GetKeyDown(KeyCode.LeftArrow))
			if (CustomControls.GetKey(CustomControls.Action.LEFT))
			{ result = -1; }
			//if (Input.GetKeyDown(KeyCode.RightArrow))
			if (CustomControls.GetKey(CustomControls.Action.RIGHT))
			{ result = 1; }

			if (result == -1) { moveToYes(); }
			if (result == 1) { moveToNo(); }
		}
	}
	
	void deleteAllProgress()
	{
		// Warning! Selecting 'Yes' will delete all of your progress, controls, achievements, and settings!
		//Debug.LogWarning("Deleteing all PlayerPrefs");
		PlayerPrefs.DeleteAll();
		PlayerPrefs.Save();

		xa.savedProgress    = 0;
		xa.maxPortalStep    = 0;
		xa.pgMode           = false;
		xa.showTimer        = false;
		xa.fullscreen       = true;
		xa.vSync            = 0;
		xa.muteMusic        = 1;
		xa.muteSound        = 1;
		xa.musicVolume      = 1.0f;
		xa.soundVolume      = 0.8f;
		xa.lastPortalStep   = 0;
		xa.lastSpeedRunTime = 0;

		for (int i = 0; i < xa.speedRunTimes.Length; ++i)
		{
			xa.speedRunTimes[i] = 0;
			xa.playerStars[i]   = 0;
			xa.playerDeaths[i]  = -1;
		}
		for (int i = 0; i < xa.achievementsUnlocked.Length; ++i)
		{
			xa.achievementsUnlocked[i] = false;
		}
		CustomControls.resetBindings();
        //Commented during GC_ cleanup//xa.saveFile.SaveData();
		Setup.callFadeOutFunc("level0", true, Application.loadedLevelName);
	}
	
	void ResetPopup()
	{
		//Reset
		xa.deleteAllConfirm = false;
		poppedUp = false;
	}

	void moveToYes()
	{
		yesSelected = true;
		xa.glx = selectBox.transform.localPosition;
		xa.glx.x = -2;
		selectBox.transform.localPosition = xa.glx;
	}

	void moveToNo()
	{
		yesSelected = false;
		xa.glx = selectBox.transform.localPosition;
		xa.glx.x = 2;
		selectBox.transform.localPosition = xa.glx;
	}

	void moveUp()
	{
		poppedUp = true;
		xa.glx = transform.localPosition;
		xa.glx.y = 100;
		transform.localPosition = xa.glx;

		if (xa.faderOut) { xa.tempColor = Color.white; xa.tempColor.a = 0; xa.faderOut.renderer.material.color = xa.tempColor; }
		if (xa.faderIn) { xa.tempColor = Color.white; xa.tempColor.a = 0; xa.faderIn.renderer.material.color = xa.tempColor; }
	}

	void moveDown()
	{
		poppedUp = false;
		xa.glx = transform.localPosition;
		xa.glx.y = 80;
		transform.localPosition = xa.glx;
		ResetPopup();
	}*/
}
