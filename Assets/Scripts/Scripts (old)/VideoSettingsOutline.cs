using UnityEngine;
using System.Collections;

public class VideoSettingsOutline : MonoBehaviour
{/*

	float blinkDelayOn = 3f;
	float blinkDelayOff = 0.1f;
	float timeSave = 0;
	bool blinkStatus = false;
	bool usingMouse = true;
	Vector3 mousePosOld = Vector3.zero;
	int currentNum = 0;
	float scrollDelay = 0.2f;
	float scrollTimeSave = 0;
	bool triggered = false;
	int rememberedCol = 1;
	//float joystickDeadzone = 0.5f;
	
	void Start()
	{
		//find zero
		xa.tempobj = findNum(0);
		xa.glx = transform.position;
		xa.glx.x = xa.tempobj.transform.position.x;
		xa.glx.y = xa.tempobj.transform.position.y;
		transform.position = xa.glx;

		renderer.enabled = true;
	}

	// Update is called once per frame
	void Update()
	{
		if(xa.videoSettingsConfirmExit || xa.videoSettingsRevertAfterApply)
			return;
	
		if(CustomControls.GetKeyDown(CustomControls.Action.STOMP))
		{
			if(xa.lastWorldMenu != "none")
				Setup.callFadeOutFunc(xa.lastWorldMenu, true, Application.loadedLevelName);
			else
				Setup.callFadeOutFunc("levelSelect1C", true, Application.loadedLevelName);
		}
		//updateBlink();
		if (scrollTimeSave <= fa.time && scrollTimeSave != 0)
		{
			scrollTimeSave = 0;
		}
		//if (Input.GetKeyDown(KeyCode.UpArrow) ||
		//	Input.GetKeyDown(KeyCode.DownArrow) ||
		//	Input.GetKeyDown(KeyCode.LeftArrow) ||
		//	Input.GetKeyDown(KeyCode.RightArrow))
		if (CustomControls.GetKeyDown(CustomControls.Action.UP) ||
			CustomControls.GetKeyDown(CustomControls.Action.DOWN) ||
			CustomControls.GetKeyDown(CustomControls.Action.LEFT) ||
			CustomControls.GetKeyDown(CustomControls.Action.RIGHT))
		{
			scrollTimeSave = 0;
		}

		//check if using mouse
		usingMouse = false;
		if (Vector3.Distance(mousePosOld, Input.mousePosition) > 0.1f && mousePosOld != Vector3.zero)
		//if (Vector3.Distance(mousePosOld, AspectUtility.mousePosition) > 0.1f && mousePosOld != Vector3.zero)
		{
			usingMouse = true;
		}
		mousePosOld = Input.mousePosition;
		//mousePosOld = AspectUtility.mousePosition;

		if (usingMouse)
		{
			xa.tempobj = findNearestNumToMouse();
			xa.glx = transform.position;
			xa.glx.x = xa.tempobj.transform.position.x;
			xa.glx.y = xa.tempobj.transform.position.y;
			transform.position = xa.glx;
		}
		else
		{
			if (scrollTimeSave == 0) { checkNonMouseInput(); }

			checkForLessThanZero();

			xa.tempobj = null;
			xa.tempobj = findNum(currentNum);
			if (xa.tempobj)
			{
				xa.glx = transform.position;
				xa.glx.x = xa.tempobj.transform.position.x;
				xa.glx.y = xa.tempobj.transform.position.y;
				transform.position = xa.glx;
			}
			else
			{
				currentNum = 0;
				xa.tempobj = null;
				xa.tempobj = findNum(currentNum);
				xa.glx = transform.position;
				xa.glx.x = xa.tempobj.transform.position.x;
				xa.glx.y = xa.tempobj.transform.position.y;
				transform.position = xa.glx;
			}

			checkFakeClickedOn();
		}
		//update using the mouse
		xa.levelSelectCurrentNumber = currentNum;
		//if (Application.loadedLevelName == "levelSelect2C") { xa.levelSelectCurrentNumber += 15; }
		//if (Application.loadedLevelName == "levelSelect3C") { xa.levelSelectCurrentNumber += 30; }
	}

	void checkFakeClickedOn()
	{
		triggered = false;
		if (Input.GetKeyDown(KeyCode.Return)) { triggered = true; }
		//if (Input.GetKeyDown(KeyCode.Joystick1Button0) ||
		//   Input.GetKeyDown(KeyCode.Joystick2Button0) ||
		//   Input.GetKeyDown(KeyCode.Joystick3Button0) ||
		//   Input.GetKeyDown(KeyCode.Joystick4Button0)) { triggered = true; }
		//  if( Input.GetKeyDown(KeyCode.Z))
		if (CustomControls.GetKeyDown(CustomControls.Action.JUMP))
		{
			triggered = true;
		}
		if (triggered)
		{
			xa.tempobj = findNum(currentNum);
			xa.tempobj.SendMessage("clickedFunc");
		}
	}

	void checkNonMouseInput()
	{
		bool up1 = false;
		bool down1 = false;
		bool left1 = false;
		bool right1 = false;

		//if (Input.GetKey(KeyCode.UpArrow))
		if (CustomControls.GetKey(CustomControls.Action.UP))
		{ up1 = true; }
		//if (Input.GetKey(KeyCode.DownArrow))
		if (CustomControls.GetKey(CustomControls.Action.DOWN))
		{ down1 = true; }
		//if (Input.GetKey(KeyCode.LeftArrow))
		if (CustomControls.GetKey(CustomControls.Action.LEFT))
		{ left1 = true; }
		//if (Input.GetKey(KeyCode.RightArrow))
		if (CustomControls.GetKey(CustomControls.Action.RIGHT))
		{ right1 = true; }

		if (left1 || right1 || up1 || down1) { scrollTimeSave = fa.time + scrollDelay; }
	
		if (left1)
		{
			if(currentNum == 0)
				currentNum = 8;
			else
				currentNum--;
		}
		if (right1)
		{
			if(currentNum == 8)
				currentNum = 0;
			else
				currentNum++;
		}
	
		if (up1)
		{
			if(currentNum == 0)
			{
				currentNum = rememberedCol + 6;
			}
			else if(currentNum == 1 || currentNum == 2)
			{
				rememberedCol = currentNum;
				currentNum = 0;
			}
			else
			{
				currentNum -= 2;
			}
		}
		if (down1)
		{
			if(currentNum == 0)
			{
				currentNum = rememberedCol;
			}
			else if(currentNum == 7 || currentNum == 8)
			{
				rememberedCol = currentNum - 6;
				currentNum = 0;
			}
			else
			{
				currentNum += 2;
			}
		}
	}

	void checkForLessThanZero()
	{
		if (currentNum < 0)
		{
			int maxNum = 0;
			NavMenuScript script;
			GameObject[] gos;
			gos = GameObject.FindGameObjectsWithTag("metaNode");

			foreach (GameObject go in gos)
			{
				script = null;
				script = go.GetComponent<NavMenuScript>();
				if (script)
				{
					if (script.number > maxNum)
					{
						maxNum = script.number;
					}
				}
			}
			currentNum = maxNum;
		}
	}


	void updateBlink()
	{
		if (blinkStatus)
		{
			if (timeSave <= fa.time)
			{
				timeSave = fa.time + blinkDelayOff;
				renderer.enabled = false;
				blinkStatus = !blinkStatus;
			}
		}
		else
		{
			if (timeSave <= fa.time)
			{
				timeSave = fa.time + blinkDelayOn;
				renderer.enabled = true;
				blinkStatus = !blinkStatus;
			}
		}
	}

	GameObject findNum(int num)
	{
		NavMenuScript script;
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("metaNode");

		foreach (GameObject go in gos)
		{
			script = null;
			script = go.GetComponent<NavMenuScript>();
			if (script)
			{
				if (script.number == num)
				{
					return (go);
				}
			}
		}
		return (null);

	}
	GameObject findNearestNumToMouse()
	{

		Ray ray = new Ray();
		ray = Camera.main.camera.ScreenPointToRay(Input.mousePosition);
		//ray = Camera.main.camera.ScreenPointToRay(AspectUtility.mousePosition);
		xa.glx = ray.GetPoint(30);
		xa.tempobj = null;
		NavMenuScript script;
		float dist = 9999;
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("metaNode");

		foreach (GameObject go in gos)
		{
			xa.glx2 = go.transform.position;
			xa.glx2.z = xa.glx.z;
			if (Vector3.Distance(xa.glx, xa.glx2) < dist)
			{
				dist = Vector3.Distance(xa.glx, xa.glx2);
				script = null;
				script = go.GetComponent<NavMenuScript>();
				if (script)
				{
					currentNum = script.number;
				}
				xa.tempobj = go;
			}
		}
		return (xa.tempobj);

	}*/
}
