using UnityEngine;
using System.Collections;

public class VideoSettingsRevert : MonoBehaviour
{/*
	Vector3 mousePosOld = Vector3.zero;
	public GameObject selectBox = null;
	public TextMesh revertDialog = null;
	public Transform yesPosition = null;
	public Transform noPosition = null;
	int result = 0;
	bool poppedUp = false;
	bool triggered = false;
	bool yesSelected = false;
	float revertTimer = 0;

	void Awake()
	{
		moveDown();
		ResetPopup();
	}

	void LateUpdate()
	{
		//Popup
		if(xa.videoSettingsRevertAfterApply && poppedUp == false)
		{
			yesSelected = false;
			xa.glx = selectBox.transform.localPosition;
			xa.glx.x = 2;
			selectBox.transform.localPosition = xa.glx;
			triggered = false;
			moveUp();
			revertTimer = 15f;
		}
	
		//Controls
		else if(xa.videoSettingsRevertAfterApply && poppedUp == true)
		{
			updateTimer();
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
//				xa.glx.z = yesPosition.position.z;
//				xa.glx.x *= -1;
//				xa.glx.y += 70;
				if(xa.glx.y <= 10.5f && xa.glx.y >= 8f)
				{
					if(xa.glx.x >= 15f && xa.glx.x <= 17f)
					{
						moveToYes();
					}
					else if(xa.glx.x >= 19f && xa.glx.x <= 21f)
					{
						moveToNo();
					}
					Setup.GC_DebugLog(xa.glx);
				}
			}

			if(yesSelected && Input.GetMouseButtonDown(0))
			{
				xa.videoSettingsRevertAfterApply = false;
				xa.oldVSync = xa.vSync;
				xa.oldFullscreen = xa.fullscreen;
				xa.oldResolutionIndex = xa.currentResolutionIndex;
				moveDown();
			}
			else if(!yesSelected && Input.GetMouseButtonDown(0))
			{
				revertSettings();
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
					xa.videoSettingsRevertAfterApply = false;
					xa.oldVSync = xa.vSync;
					xa.oldFullscreen = xa.fullscreen;
					xa.oldResolutionIndex = xa.currentResolutionIndex;
					moveDown();
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
					revertSettings();
	
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
	
	void updateTimer()
	{
		revertTimer -= fa.deltaTime;
	
		if(revertTimer <= 0) // Timer is done
		{
			revertSettings();
		}
	
		revertDialog.text = "Settings will revert back in " + Mathf.CeilToInt(revertTimer).ToString() + " seconds.";
	}
	
	void revertSettings()
	{
		xa.fullscreen = xa.oldFullscreen;
		xa.currentResolutionIndex = xa.oldResolutionIndex;
		xa.vSync = xa.oldVSync;
	
		QualitySettings.vSyncCount = xa.vSync;
		//PlayerPrefs.SetInt("vSync", xa.vSync);
		Screen.SetResolution(xa.supportedResolutions[xa.currentResolutionIndex].width, xa.supportedResolutions[xa.currentResolutionIndex].height, xa.fullscreen);
		xa.videoSettingsRevertAfterApply = false;
		Setup.callFadeOutFunc(Application.loadedLevelName, true, Application.loadedLevelName);
	}
	
	void ResetPopup()
	{
		//Reset
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
