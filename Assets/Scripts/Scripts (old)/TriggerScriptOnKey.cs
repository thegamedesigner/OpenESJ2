using UnityEngine;
using System.Collections;

public class TriggerScriptOnKey : MonoBehaviour
{
	public string label = "";
	public Behaviour[] scriptsToActivate;
	public bool disableBehaviours = false;
	public bool disableSelfOnTriggering = false;
	public bool triggerOnKey = false;
	public KeyCode triggerKey = KeyCode.Mouse0;
	public bool triggerOnXboxA = false;
	public bool triggerOnXboxB = false;
	public bool triggerOnXboxY = false;
	public bool triggerOnXboxX = false;
	public bool triggerOnXboxBack = false;
	public bool triggerOnXboxStart = false;
	public bool onlyTriggerOnce = false;
	public bool showDebugMsg = false;
	public string debugMsg = "Triggered!";
	bool triggered = false;

	void Update()
	{
		triggered = false;
		//Debug.LogError("TriggerScriptOnKey " + triggerKey.ToString());
		if (triggerOnKey) {
			if (Input.GetKeyDown(triggerKey)) {
				triggered = true;
			}
		}

		if (triggerOnXboxA)
		{
			if (Input.GetKeyDown(KeyCode.Joystick1Button0) ||
			   Input.GetKeyDown(KeyCode.Joystick2Button0) ||
			   Input.GetKeyDown(KeyCode.Joystick3Button0) ||
			   Input.GetKeyDown(KeyCode.Joystick4Button0)) { triggered = true; }
		}
		if (triggerOnXboxB)
		{
			if (Input.GetKeyDown(KeyCode.Joystick1Button1) ||
			Input.GetKeyDown(KeyCode.Joystick2Button1) ||
			Input.GetKeyDown(KeyCode.Joystick3Button1) ||
			Input.GetKeyDown(KeyCode.Joystick4Button1)) { triggered = true; }
		}
		if (triggerOnXboxX)
		{
			if (Input.GetKeyDown(KeyCode.Joystick1Button2) ||
			Input.GetKeyDown(KeyCode.Joystick2Button2) ||
			Input.GetKeyDown(KeyCode.Joystick3Button2) ||
			Input.GetKeyDown(KeyCode.Joystick4Button2)) { triggered = true; }
		}
		if (triggerOnXboxY)
		{
			if (Input.GetKeyDown(KeyCode.Joystick1Button3) ||
			   Input.GetKeyDown(KeyCode.Joystick2Button3) ||
			   Input.GetKeyDown(KeyCode.Joystick3Button3) ||
			   Input.GetKeyDown(KeyCode.Joystick4Button3)) { triggered = true; }
		}
		if (triggerOnXboxBack)
		{
			if (Input.GetKeyDown(KeyCode.Joystick1Button6) ||
			   Input.GetKeyDown(KeyCode.Joystick2Button6) ||
			   Input.GetKeyDown(KeyCode.Joystick3Button6) ||
			   Input.GetKeyDown(KeyCode.Joystick4Button6)) { triggered = true; }
		}
		if (triggerOnXboxStart)
		{
			if (Input.GetKeyDown(KeyCode.Joystick1Button7) ||
			   Input.GetKeyDown(KeyCode.Joystick2Button7) ||
			   Input.GetKeyDown(KeyCode.Joystick3Button7) ||
			   Input.GetKeyDown(KeyCode.Joystick4Button7)) { triggered = true; }
		}

		if (triggered)
		{
			foreach (Behaviour co in scriptsToActivate)
			{
				if (disableBehaviours) {
					co.enabled = false;
				} else {
					co.enabled = true;
				}
			}
			if (!onlyTriggerOnce) {
				triggered = false;
			}
			if (disableSelfOnTriggering) {
				this.enabled = false;
			}
		}
	}
}
