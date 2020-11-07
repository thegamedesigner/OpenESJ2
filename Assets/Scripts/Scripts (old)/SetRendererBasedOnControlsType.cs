using UnityEngine;
using System.Collections;

public class SetRendererBasedOnControlsType : MonoBehaviour
{
	public bool isKeyboard = false;
	public bool isXbox = false;

	public enum controlTypes { Keyboard, Xbox }
	public static controlTypes controlType = controlTypes.Keyboard;

	public static void checkControlsType()
	{
			if (Input.GetKey(KeyCode.Joystick1Button0) ||
				Input.GetKey(KeyCode.Joystick2Button0) ||
				Input.GetKey(KeyCode.Joystick3Button0) ||
				Input.GetKey(KeyCode.Joystick4Button0) ||
				Input.GetKey(KeyCode.Joystick1Button1) ||
				Input.GetKey(KeyCode.Joystick2Button1) ||
				Input.GetKey(KeyCode.Joystick3Button1) ||
				Input.GetKey(KeyCode.Joystick4Button1) ||
				Input.GetKey(KeyCode.Joystick1Button2) ||
				Input.GetKey(KeyCode.Joystick2Button2) ||
				Input.GetKey(KeyCode.Joystick3Button2) ||
				Input.GetKey(KeyCode.Joystick4Button2) ||
				Input.GetKey(KeyCode.Joystick1Button3) ||
				Input.GetKey(KeyCode.Joystick2Button3) ||
				Input.GetKey(KeyCode.Joystick3Button3) ||
				Input.GetKey(KeyCode.Joystick4Button3) ||
				Input.GetKey(KeyCode.Joystick1Button4) ||
				Input.GetKey(KeyCode.Joystick2Button4) ||
				Input.GetKey(KeyCode.Joystick3Button4) ||
				Input.GetKey(KeyCode.Joystick4Button4) ||
				Input.GetKey(KeyCode.Joystick1Button5) ||
				Input.GetKey(KeyCode.Joystick2Button5) ||
				Input.GetKey(KeyCode.Joystick3Button5) ||
				Input.GetKey(KeyCode.Joystick4Button5) ||
				Input.GetKey(KeyCode.Joystick1Button6) ||
				Input.GetKey(KeyCode.Joystick2Button6) ||
				Input.GetKey(KeyCode.Joystick3Button6) ||
				Input.GetKey(KeyCode.Joystick4Button6) ||
				Input.GetKey(KeyCode.Joystick1Button7) ||
				Input.GetKey(KeyCode.Joystick2Button7) ||
				Input.GetKey(KeyCode.Joystick3Button7) ||
				Input.GetKey(KeyCode.Joystick4Button7))
			{
				SetRendererBasedOnControlsType.controlType = controlTypes.Xbox;
			}



			if (Input.GetKey(KeyCode.Z) ||
				Input.GetKey(KeyCode.R) ||
				Input.GetKey(KeyCode.Q) ||
				Input.GetKey(KeyCode.X) ||
				Input.GetKey(KeyCode.Escape) ||
				Input.GetKey(KeyCode.LeftArrow) ||
				Input.GetKey(KeyCode.RightArrow))
			{
				SetRendererBasedOnControlsType.controlType = controlTypes.Keyboard;
			}
		updateControlStrings();
	}

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (isXbox)
		{
			if (SetRendererBasedOnControlsType.controlType == controlTypes.Keyboard)
			{
				GetComponent<Renderer>().enabled = false;
			}
			if (SetRendererBasedOnControlsType.controlType == controlTypes.Xbox)
			{
				GetComponent<Renderer>().enabled = true;
			}
		}
		if (isKeyboard)
		{
			if (SetRendererBasedOnControlsType.controlType == controlTypes.Keyboard)
			{
				GetComponent<Renderer>().enabled = true;
			}
			if (SetRendererBasedOnControlsType.controlType == controlTypes.Xbox)
			{
				GetComponent<Renderer>().enabled = false;
			}
		}
	}
	
	public static void updateControlStrings()
	{
		switch(controlType)
		{
		case controlTypes.Keyboard:
			xa.jumpButton = "Z";
			xa.poundButton = "X";
			break;
		case controlTypes.Xbox:
			xa.jumpButton = "A";
			xa.poundButton = "B";
			break;
		}
	}
}
