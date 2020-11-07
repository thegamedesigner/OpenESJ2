using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Structs;

public class AutoControls : MonoBehaviour
{
	/*
		Auto-control options.
			- A choice of layouts? Hmm, I like that. 
		
		Player 1 is keyboard & first controller, second is second controller, etc.


	*/
	public static float controllerCheckDelay = 3;
	public static float controllerCheckTimeSet = -99;
	public static ControllerMapping desiredControllerMapping = ControllerMapping.None;
	public static ControllerMapping currentControllerMapping = ControllerMapping.None;

	public enum InputType
	{
		Current,
		Down,
		Up
	}

	public enum MouseButtons
	{
		None = -1,
		LeftButton = 0,
		RightButton = 1,
		MiddleButton = 2,
		End
	}

	public enum ControllerMapping
	{
		None,
		Xbox360,
		XboxOne,
		End
	}

	public static void UpdateAutoControls()//Called in main.cs every frame
	{
		if (Time.time > (controllerCheckDelay + controllerCheckTimeSet))
		{
			controllerCheckTimeSet = Time.time;

			string[] strs = Input.GetJoystickNames();
			for (int i = 0; i < strs.Length; i++)
			{
				if (currentControllerMapping != ControllerMapping.Xbox360 && strs[i].Contains("360"))
				{
					Debug.Log("Switching to Auto-Xbox360");
					currentControllerMapping = ControllerMapping.Xbox360;
				}
			}
		}

	}

	public static bool GetInput(Controls.Type type, int playerNum, InputType inputType)
	{
		int result = 0;
		//Handle controllers axis first. 
		if (currentControllerMapping == ControllerMapping.Xbox360)
		{
			if (Controls.platform == Controls.Platforms.Windows)
			{
				switch (type)
				{
					//Platformer
					case Controls.Type.Jump: if (GetGenericInput(KeyCode.Joystick1Button0, inputType)) { result++; } break;
					case Controls.Type.MoveLeft: if (GetMultiInput(new Int3(1, 1, 0), new Int3(1, 6, 0), inputType)) { result++; } break;
					case Controls.Type.MoveRight: if (GetMultiInput(new Int3(1, 1, 1), new Int3(1, 6, 1), inputType)) { result++; } break;
					case Controls.Type.MoveDown: if (GetGenericInput(KeyCode.Joystick1Button2, inputType)) { result++; } break;
					case Controls.Type.Ability1: if (GetGenericInput(KeyCode.Joystick1Button1, inputType)) { result++; } break;
					case Controls.Type.Respawn: if (GetGenericInput(KeyCode.Joystick1Button3, inputType)) { result++; } break;
					case Controls.Type.Restart: if (GetGenericInput(KeyCode.Joystick1Button6, inputType)) { result++; } break;

					//FPS
					case Controls.Type.FPSForward: if (GetMultiInput(new Int3(1, 2, 0), new Int3(1, 7, 1), inputType)) { result++; } break;
					case Controls.Type.FPSBackward: if (GetMultiInput(new Int3(1, 2, 1), new Int3(1, 7, 0), inputType)) { result++; } break;
					case Controls.Type.FPSLeft: if (GetMultiInput(new Int3(1, 1, 0), new Int3(1, 6, 0), inputType)) { result++; } break;
					case Controls.Type.FPSRight: if (GetMultiInput(new Int3(1, 1, 1), new Int3(1, 6, 1), inputType)) { result++; } break;
					case Controls.Type.FPSFire: if (GetMultiInput(KeyCode.Joystick1Button0, new Int3(1, 3, 0), inputType)) { result++; } break;
					case Controls.Type.FPSCycleWeapon: if (GetGenericInput(KeyCode.Joystick1Button1, inputType)) { result++; } break;
					case Controls.Type.FPSLookLeft: if (GetGenericInput(1, 4, false, inputType)) { result++; } break;
					case Controls.Type.FPSLookRight: if (GetGenericInput(1, 4, true, inputType)) { result++; } break;
					case Controls.Type.FPSLookUp: if (GetGenericInput(1, 5, false, inputType)) { result++; } break;
					case Controls.Type.FPSLookDown: if (GetGenericInput(1, 5, true, inputType)) { result++; } break;

					//Menu
					case Controls.Type.MenuLeft: if (GetMultiInput(new Int3(1, 1, 0), new Int3(1, 6, 0), inputType)) { result++; } break;
					case Controls.Type.MenuRight: if (GetMultiInput(new Int3(1, 1, 1), new Int3(1, 6, 1), inputType)) { result++; } break;
					case Controls.Type.MenuUp: if (GetMultiInput(new Int3(1, 2, 0), new Int3(1, 7, 1), inputType)) { result++; } break;
					case Controls.Type.MenuDown: if (GetMultiInput(new Int3(1, 2, 1), new Int3(1, 7, 0), inputType)) { result++; } break;
					case Controls.Type.MenuSelect: if (GetGenericInput(KeyCode.Joystick1Button0, inputType)) { result++; } break;
					case Controls.Type.OpenMenu: if (GetGenericInput(KeyCode.Joystick1Button7, inputType)) { result++; } break;
				}
			}
			if (Controls.platform == Controls.Platforms.OSX)
			{
				switch (type)
				{
					//Platformer
					case Controls.Type.Jump: if (GetGenericInput(KeyCode.Joystick1Button16, inputType)) { result++; } break;
					case Controls.Type.MoveLeft: if (GetMultiInput(KeyCode.Joystick1Button7, new Int3(1, 1, 0), inputType)) { result++; } break;
					case Controls.Type.MoveRight: if (GetMultiInput(KeyCode.Joystick1Button8, new Int3(1, 1, 1), inputType)) { result++; } break;
					case Controls.Type.MoveDown: if (GetMultiInput(KeyCode.Joystick1Button6, KeyCode.Joystick1Button18, inputType)) { result++; } break;
					case Controls.Type.Ability1: if (GetGenericInput(KeyCode.Joystick1Button17, inputType)) { result++; } break;
					case Controls.Type.Respawn: if (GetGenericInput(KeyCode.Joystick1Button19, inputType)) { result++; } break;
					case Controls.Type.Restart: if (GetGenericInput(KeyCode.Joystick1Button10, inputType)) { result++; } break;

					//FPS
					case Controls.Type.FPSForward: if (GetMultiInput(new Int3(1, 2, 0), new Int3(1, 7, 1), inputType)) { result++; } break;
					case Controls.Type.FPSBackward: if (GetMultiInput(new Int3(1, 2, 1), new Int3(1, 7, 0), inputType)) { result++; } break;
					case Controls.Type.FPSLeft: if (GetMultiInput(new Int3(1, 1, 0), new Int3(1, 6, 0), inputType)) { result++; } break;
					case Controls.Type.FPSRight: if (GetMultiInput(new Int3(1, 1, 1), new Int3(1, 6, 1), inputType)) { result++; } break;
					case Controls.Type.FPSFire: if (GetGenericInput(KeyCode.Joystick1Button16, inputType)) { result++; } break;
					case Controls.Type.FPSCycleWeapon: if (GetGenericInput(KeyCode.Joystick1Button17, inputType)) { result++; } break;
					case Controls.Type.FPSLookLeft: if (GetGenericInput(1, 3, true, inputType)) { result++; } break;
					case Controls.Type.FPSLookRight: if (GetGenericInput(1, 3, false, inputType)) { result++; } break;
					case Controls.Type.FPSLookUp: if (GetGenericInput(1, 4, false, inputType)) { result++; } break;
					case Controls.Type.FPSLookDown: if (GetGenericInput(1, 4, false, inputType)) { result++; } break;

					//Menu
					case Controls.Type.MenuLeft: if (GetMultiInput(KeyCode.Joystick1Button7, new Int3(1, 1, 0), inputType)) { result++; } break;
					case Controls.Type.MenuRight: if (GetMultiInput(KeyCode.Joystick1Button8, new Int3(1, 1, 1), inputType)) { result++; } break;
					case Controls.Type.MenuUp: if (GetMultiInput(KeyCode.Joystick1Button5, new Int3(1, 2, 0), inputType)) { result++; } break;
					case Controls.Type.MenuDown: if (GetMultiInput(KeyCode.Joystick1Button6, new Int3(1, 2, 1), inputType)) { result++; } break;
					case Controls.Type.MenuSelect: if (GetGenericInput(KeyCode.Joystick1Button16, inputType)) { result++; } break;
					case Controls.Type.OpenMenu: if (GetGenericInput(KeyCode.Joystick1Button9, inputType)) { result++; } break;
				}
			}
			if (Controls.platform == Controls.Platforms.Linux)
			{
				switch (type)
				{
					//Platformer
					case Controls.Type.Jump: if (GetGenericInput(KeyCode.Joystick1Button0, inputType)) { result++; } break;
					case Controls.Type.MoveLeft: if (GetMultiInput(new Int3(1, 1, 0), new Int3(1, 7, 0), inputType)) { result++; } break;
					case Controls.Type.MoveRight: if (GetMultiInput(new Int3(1, 1, 1), new Int3(1, 7, 1), inputType)) { result++; } break;
					case Controls.Type.MoveDown: if (GetGenericInput(KeyCode.Joystick1Button2, inputType)) { result++; } break;
					case Controls.Type.Ability1: if (GetGenericInput(KeyCode.Joystick1Button1, inputType)) { result++; } break;
					case Controls.Type.Respawn: if (GetGenericInput(KeyCode.Joystick1Button3, inputType)) { result++; } break;
					case Controls.Type.Restart: if (GetGenericInput(KeyCode.Joystick1Button6, inputType)) { result++; } break;

					//FPS
					case Controls.Type.FPSForward: if (GetMultiInput(new Int3(1, 2, 0), new Int3(1, 8, 1), inputType)) { result++; } break;
					case Controls.Type.FPSBackward: if (GetMultiInput(new Int3(1, 2, 1), new Int3(1, 8, 0), inputType)) { result++; } break;
					case Controls.Type.FPSLeft: if (GetMultiInput(new Int3(1, 1, 0), new Int3(1, 7, 0), inputType)) { result++; } break;
					case Controls.Type.FPSRight: if (GetMultiInput(new Int3(1, 1, 1), new Int3(1, 7, 1), inputType)) { result++; } break;
					case Controls.Type.FPSFire: if (GetGenericInput(KeyCode.Joystick1Button0, inputType)) { result++; } break;
					case Controls.Type.FPSCycleWeapon: if (GetGenericInput(KeyCode.Joystick1Button1, inputType)) { result++; } break;
					case Controls.Type.FPSLookLeft: if (GetGenericInput(1, 4, true, inputType)) { result++; } break;
					case Controls.Type.FPSLookRight: if (GetGenericInput(1, 4, false, inputType)) { result++; } break;
					case Controls.Type.FPSLookUp: if (GetGenericInput(1, 5, false, inputType)) { result++; } break;
					case Controls.Type.FPSLookDown: if (GetGenericInput(1, 5, false, inputType)) { result++; } break;

					//Menu
					case Controls.Type.MenuLeft: if (GetMultiInput(new Int3(1, 1, 0), new Int3(1, 7, 0), inputType)) { result++; } break;
					case Controls.Type.MenuRight: if (GetMultiInput(new Int3(1, 1, 1), new Int3(1, 7, 1), inputType)) { result++; } break;
					case Controls.Type.MenuUp: if (GetMultiInput(new Int3(1, 2, 0), new Int3(1, 8, 1), inputType)) { result++; } break;
					case Controls.Type.MenuDown: if (GetMultiInput(new Int3(1, 2, 1), new Int3(1, 8, 0), inputType)) { result++; } break;
					case Controls.Type.MenuSelect: if (GetGenericInput(KeyCode.Joystick1Button0, inputType)) { result++; } break;
					case Controls.Type.OpenMenu: if (GetGenericInput(KeyCode.Joystick1Button7, inputType)) { result++; } break;
				}
			}
		}

		//Keyboards/Mouse Buttons second
		switch (type)
		{
			//Platformer
			case Controls.Type.Jump: if (GetGenericInput(KeyCode.Z, inputType)) { result++; } break;
			case Controls.Type.MoveLeft: if (GetGenericInput(KeyCode.LeftArrow, inputType)) { result++; } break;
			case Controls.Type.MoveRight: if (GetGenericInput(KeyCode.RightArrow, inputType)) { result++; } break;
			case Controls.Type.MoveDown: if (GetGenericInput(KeyCode.DownArrow, inputType)) { result++; } break;
			case Controls.Type.Ability1: if (GetGenericInput(KeyCode.X, inputType)) { result++; } break;
			case Controls.Type.Respawn: if (GetGenericInput(KeyCode.R, inputType)) { result++; } break;
			case Controls.Type.Restart: if (GetGenericInput(KeyCode.Q, inputType)) { result++; } break;

			//FPS
			case Controls.Type.FPSForward: if (GetGenericInput(KeyCode.W, inputType)) { result++; } break;
			case Controls.Type.FPSBackward: if (GetGenericInput(KeyCode.S, inputType)) { result++; } break;
			case Controls.Type.FPSLeft: if (GetGenericInput(KeyCode.A, inputType)) { result++; } break;
			case Controls.Type.FPSRight: if (GetGenericInput(KeyCode.D, inputType)) { result++; } break;
			case Controls.Type.FPSFire: if (GetMultiInput(KeyCode.Space, MouseButtons.LeftButton, inputType)) { result++; } break;
			case Controls.Type.FPSCycleWeapon: if (GetGenericInput(KeyCode.E, inputType)) { result++; } break;
			case Controls.Type.FPSLookLeft: if (GetGenericInput(KeyCode.LeftArrow, inputType)) { result++; } break;
			case Controls.Type.FPSLookRight: if (GetGenericInput(KeyCode.RightArrow, inputType)) { result++; } break;
			case Controls.Type.FPSLookUp: if (GetGenericInput(KeyCode.UpArrow, inputType)) { result++; } break;
			case Controls.Type.FPSLookDown: if (GetGenericInput(KeyCode.DownArrow, inputType)) { result++; } break;

			//Menu
			case Controls.Type.MenuLeft: if (GetMultiInput(KeyCode.A, KeyCode.LeftArrow, inputType)) { result++; } break;
			case Controls.Type.MenuRight: if (GetMultiInput(KeyCode.D, KeyCode.RightArrow, inputType)) { result++; } break;
			case Controls.Type.MenuUp: if (GetMultiInput(KeyCode.W, KeyCode.UpArrow, inputType)) { result++; } break;
			case Controls.Type.MenuDown: if (GetMultiInput(KeyCode.S, KeyCode.DownArrow, inputType)) { result++; } break;
			case Controls.Type.MenuSelect: if (GetMultiInput(KeyCode.Return, KeyCode.KeypadEnter, inputType)) { result++; } break;
			case Controls.Type.OpenMenu: if (GetGenericInput(KeyCode.Escape, inputType)) { result++; } break;

		}

		if (result > 0) { return true; } else { return false; }
	}

	public static bool GetKeycode(KeyCode keyCode, InputType inputType)
	{
		if (inputType == InputType.Current) { return Input.GetKey(keyCode); }
		else if (inputType == InputType.Down) { return Input.GetKeyDown(keyCode); }
		else { return Input.GetKeyUp(keyCode); }
	}

	public static bool GetMouseButton(MouseButtons mouseButton, InputType inputType)
	{
		if (inputType == InputType.Current) { return Input.GetMouseButton((int)mouseButton); }
		else if (inputType == InputType.Down) { return Input.GetMouseButtonDown((int)mouseButton); }
		else { return Input.GetMouseButtonUp((int)mouseButton); }
	}

	public static Int3 i3Null = new Int3(-1, -1, -1);

	public static bool GetMultiInput(Int3 joyA, Int3 joyB, InputType inputType)
	{ return GetMultiInput(joyA, joyB, i3Null, KeyCode.None, KeyCode.None, KeyCode.None, MouseButtons.None, inputType); }
	public static bool GetMultiInput(KeyCode keyCode1, Int3 joyA, InputType inputType)
	{ return GetMultiInput(joyA, i3Null, i3Null, keyCode1, KeyCode.None, KeyCode.None, MouseButtons.None, inputType); }
	public static bool GetMultiInput(KeyCode keyCode1, KeyCode keyCode2, Int3 joyA, InputType inputType)
	{ return GetMultiInput(joyA, i3Null, i3Null, keyCode1, keyCode2, KeyCode.None, MouseButtons.None, inputType); }
	public static bool GetMultiInput(KeyCode keyCode1, KeyCode keyCode2, InputType inputType)
	{ return GetMultiInput(i3Null, i3Null, i3Null, keyCode1, keyCode2, KeyCode.None, MouseButtons.None, inputType); }
	public static bool GetMultiInput(KeyCode keyCode1, KeyCode keyCode2, KeyCode keyCode3, InputType inputType)
	{ return GetMultiInput(i3Null, i3Null, i3Null, keyCode1, keyCode2, keyCode3, MouseButtons.None, inputType); }
	public static bool GetMultiInput(KeyCode keyCode1, MouseButtons mouseButton, InputType inputType)
	{ return GetMultiInput(i3Null, i3Null, i3Null, keyCode1, KeyCode.None, KeyCode.None, mouseButton, inputType); }
	public static bool GetMultiInput(Int3 controllerA, Int3 controllerB, Int3 controllerC, KeyCode keyCode1, KeyCode keyCode2, KeyCode keyCode3, MouseButtons mouseButton, InputType inputType)
	{
		if (GetGenericInput(controllerA.x, controllerA.y, (controllerA.z == 1), inputType) ||
			GetGenericInput(controllerB.x, controllerB.y, (controllerB.z == 1), inputType) ||
			GetGenericInput(controllerC.x, controllerC.y, (controllerC.z == 1), inputType) ||
			GetGenericInput(keyCode1, inputType) ||
			GetGenericInput(keyCode2, inputType) ||
			GetGenericInput(keyCode3, inputType) ||
			GetGenericInput(mouseButton, inputType))
		{
			return true;
		}
		return false;
	}

	public static bool GetGenericInput(KeyCode keyCode, InputType inputType) { return GetGenericInput(-1, -1, false, keyCode, MouseButtons.None, inputType); }
	public static bool GetGenericInput(int joyNum, int axisNum, bool axisDir, InputType inputType) { return GetGenericInput(joyNum, axisNum, axisDir, KeyCode.None, MouseButtons.None, inputType); }
	public static bool GetGenericInput(MouseButtons mouseButton, InputType inputType) { return GetGenericInput(-1, -1, false, KeyCode.None, mouseButton, inputType); }
	public static bool GetGenericInput(int joyNum, int axisNum, bool axisDir, KeyCode keyCode, MouseButtons mouseButton, InputType inputType)
	{

		if (axisNum != -1)
		{
			if (inputType == InputType.Current)
			{
				if (axisDir)//just slightly self documenting. 
				{
					if (Controls.axes[joyNum, axisNum] > Controls.deadzone) { return true; }
				}
				else
				{
					if (Controls.axes[joyNum, axisNum] < -Controls.deadzone) { return true; }
				}
				return false;
			}
			else if (inputType == InputType.Down)
			{
				//handle axis
				if (axisDir)
				{
					//if this axis is correct, AND it doesn't equal what it used to be.
					if (Controls.axes[joyNum, axisNum] > Controls.deadzone
						&&
						Controls.axesOld[joyNum, axisNum] != Controls.axes[joyNum, axisNum])
					{ return true; }
				}
				else
				{
					if (Controls.axes[joyNum, axisNum] < -Controls.deadzone
						&&
						Controls.axesOld[joyNum, axisNum] != Controls.axes[joyNum, axisNum])
					{ return true; }
				}
				return false;
			}
			else if (inputType == InputType.Up)
			{
				if (axisDir)
				{
					//if this axis used to be correct, AND it doesn't equal what it used to be.
					if (Controls.axesOld[joyNum, axisNum] > Controls.deadzone
						&&
						Controls.axesOld[joyNum, axisNum] != Controls.axes[joyNum, axisNum])
					{ return true; }
				}
				else
				{
					if (Controls.axesOld[joyNum, axisNum] < -Controls.deadzone
						&&
						Controls.axesOld[joyNum, axisNum] != Controls.axes[joyNum, axisNum])
					{ return true; }
				}
				return false;
			}


		}

		if (mouseButton != MouseButtons.None)
		{
			return GetMouseButton(mouseButton, inputType);
		}
		if (keyCode != KeyCode.None)
		{
			return GetKeycode(keyCode, inputType);
		}
		return false;
	}


	


	public static string GetLabelForJumpKey()//Called in controls.cs
	{
		if (currentControllerMapping == ControllerMapping.Xbox360)
		{
			return "A";
		}
		else
		{
			return "Z";
		}
	}
	

	public static string GetLabelForAbility1Key()//Called in controls.cs
	{
		if (currentControllerMapping == ControllerMapping.Xbox360)
		{
			return "B";
		}
		else
		{
			return "X";
		}
	}

	public static string GetLabelForMoveDownKey()//Called in controls.cs
	{
		if (currentControllerMapping == ControllerMapping.Xbox360)
		{
			return "X";
		}
		else
		{
			return "Down Arrow";
		}
	}

}
