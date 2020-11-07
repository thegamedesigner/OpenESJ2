using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Structs;
using System.Text;

public class Controls : MonoBehaviour
{
	public static bool[] customControls = new bool[25];//if true, a control press on that action, goes to custom


	public static float deadzone = 0.4f;//the deadzone for axis on joysticks
	public static List<Control> controls = new List<Control>();
	public static List<Control> backupControls = new List<Control>();
	public static List<Control> tempControls = new List<Control>();//This will become the controls when APPLY is pressed. 
	public static StringBuilder sb = new StringBuilder();
	public static int[,] axes = new int[4, 10];
	public static int[,] axesOld = new int[4, 10];

	public static Platforms platform = Platforms.Windows;

	public enum Platforms
	{
		None,
		Windows,
		OSX,
		Linux,
		End
	}

	public enum Type //All the different types of in-game control types
	{
		None,
		MoveLeft,
		MoveRight,
		Jump,
		Ability1,
		Respawn,
		Restart,
		OpenMenu,//The button, ingame, that opens the menu
		MenuLeft,
		MenuRight,
		MenuUp,
		MenuDown,
		MenuSelect,
		FPSForward,
		FPSBackward,
		FPSLeft,
		FPSRight,
		FPSFire,
		FPSCycleWeapon,
		MoveDown,
		FPSLookLeft,
		FPSLookRight,
		FPSLookUp,
		FPSLookDown,
		End
	}

	public class Control
	{
		public Type type;
		public int player = 0;
		public int keyInt;
		public int joyNum;
		public int axisNum;
		public bool posAxis;
		public string label;

		public Control(Type t, int k, int jN, int jAx, bool pa, int player)
		{
			type = t;
			keyInt = k;
			joyNum = jN;
			axisNum = jAx;
			posAxis = pa;
			label = null;
		}
		public Control(Type t, int k, int jN, int jAx, bool pa, string l, int player)
		{
			type = t;
			keyInt = k;
			joyNum = jN;
			axisNum = jAx;
			posAxis = pa;
			label = l;
		}
	}

	public static bool GetInput(Type type, int playerNum)
	{
		if (Recon.useRecon)
		{
			if (!customControls[(int)type])
			{
				return Recon.Translation(type, Recon.ControlState.Constant, playerNum);
			}
		}

		//if (!useCustom) { return AutoControls.GetInput(type, playerNum, AutoControls.InputType.Current); }

		bool result = false;
		for (int i = 0; i < controls.Count; i++)
		{
			if (controls[i].type == type && controls[i].player == playerNum)
			{
				Control control = controls[i];
				//is it a key, or an axis?
				if (control.keyInt != -1)
				{
					if (Input.GetKey((KeyCode)control.keyInt)) { result = true; }
				}
				else
				{
					//handle axis
					if (control.posAxis)
					{
						if (axes[control.joyNum, control.axisNum] > deadzone) { result = true; }
					}
					else
					{
						if (axes[control.joyNum, control.axisNum] < -deadzone) { result = true; }
					}
				}
			}
		}

		return result;
	}

	public static bool GetInputUp(Type type, int playerNum)
	{
		if (Recon.useRecon)
		{
			if (!customControls[(int)type])
			{
				return Recon.Translation(type, Recon.ControlState.Up, playerNum);
			}
		}

		bool result = false;
		for (int i = 0; i < controls.Count; i++)
		{
			if (controls[i].type == type && controls[i].player == playerNum)
			{
				Control control = controls[i];
				//is it a key, or an axis?
				if (control.keyInt != -1)
				{
					if (Input.GetKeyUp((KeyCode)control.keyInt)) { result = true; }
				}
				else
				{
					//handle axis
					if (control.posAxis)
					{
						//if this axis used to be correct, AND it doesn't equal what it used to be.
						if (axesOld[control.joyNum, control.axisNum] > deadzone
							&&
							axesOld[control.joyNum, control.axisNum] != axes[control.joyNum, control.axisNum])
						{ result = true; }
					}
					else
					{
						if (axesOld[control.joyNum, control.axisNum] < -deadzone
							&&
							axesOld[control.joyNum, control.axisNum] != axes[control.joyNum, control.axisNum])
						{ result = true; }
					}
				}
			}
		}
		return result;
	}

	public static bool GetInputDown(Type type, int playerNum)
	{
		if (type == Type.OpenMenu)
		{
			if (Input.GetKeyDown(KeyCode.Escape)) { return true; }
		}
		if (fa.forceAltMenuControls)
		{
			if (type == Type.MenuSelect)
			{
				if (Input.GetKeyDown(KeyCode.M))
				{
					Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.Coin);
					return true;
				}
			}//hardcoded, remove at future date
		}

		if (Recon.useRecon)
		{
			if (!customControls[(int)type])
			{
				return Recon.Translation(type, Recon.ControlState.Down, playerNum);
			}
		}

		bool result = false;
		for (int i = 0; i < controls.Count; i++)
		{
			if (controls[i].type == type && controls[i].player == playerNum)
			{
				Control control = controls[i];
				//is it a key, or an axis?
				if (control.keyInt != -1)
				{
					if (Input.GetKeyDown((KeyCode)control.keyInt)) { result = true; }
				}
				else
				{
					//handle axis
					if (control.posAxis)
					{
						//if this axis is correct, AND it doesn't equal what it used to be.
						if (axes[control.joyNum, control.axisNum] > deadzone
							&&
							axesOld[control.joyNum, control.axisNum] != axes[control.joyNum, control.axisNum])
						{ result = true; }
					}
					else
					{
						if (axes[control.joyNum, control.axisNum] < -deadzone
							&&
							axesOld[control.joyNum, control.axisNum] != axes[control.joyNum, control.axisNum])
						{ result = true; }
					}
				}
			}
		}
		return result;
	}


	public static bool GetAnyKeyDownOnce()//used for story spawners
	{
		if (Input.anyKeyDown) {return true; }

		if (Recon.useRecon)
		{
			return Recon.AnyInputDownOnce();
		}

		return false;
	}

	public static bool GetAnyKeyDown()
	{
		if (Input.GetKey(KeyCode.Escape)) { return false; }//Escape doesn't count as an anykey //shouldnt it?
		if (Input.anyKeyDown) { return true; }

		//Mouse buttons arent bindable yet
		//if(Input.GetMouseButtonDown(0)) {return true; }
		//if(Input.GetMouseButtonDown(1)) {return true; }
		//if(Input.GetMouseButtonDown(2)) {return true; }

		//Detect joystick movement
		for (int i = 1; i < axes.GetLength(0); i++)
		{
			for (int a = 1; a < axes.GetLength(1); a++)
			{
				if (axes[i, a] != 0) { return true; }
			}
		}


		for (int i = 1; i <= 10; i++)
		{
			for (int a = 1; a <= 10; a++)
			{
				float r = Input.GetAxis("Joy" + i + " Axis " + a);

				if (r != 0) { return true; }

			}
		}

		if (Recon.useRecon)
		{
			return Recon.AnyInputDown();
		}


		return false;
	}

	public static Control GetInputIndexForType(Type type, int playerNum)
	{
		for (int i = 0; i < controls.Count; i++)
		{
			if (controls[i].type == type && controls[i].player == playerNum) { return controls[i]; }
		}
		return null;
	}

	public static void SetDefaultControls()
	{
		//Debug.Log("SET DEFAULT CONTROLS");
		SetDefaultControl(Type.MoveLeft);
		SetDefaultControl(Type.MoveRight);
		SetDefaultControl(Type.MoveDown);
		SetDefaultControl(Type.Jump);
		SetDefaultControl(Type.Ability1);
		SetDefaultControl(Type.Respawn);
		SetDefaultControl(Type.Restart);

		SetDefaultControl(Type.MenuLeft);
		SetDefaultControl(Type.MenuRight);
		SetDefaultControl(Type.MenuUp);
		SetDefaultControl(Type.MenuDown);
		SetDefaultControl(Type.MenuSelect);
		SetDefaultControl(Type.OpenMenu);

		SetDefaultControl(Type.FPSForward);
		SetDefaultControl(Type.FPSBackward);
		SetDefaultControl(Type.FPSLeft);
		SetDefaultControl(Type.FPSRight);
		SetDefaultControl(Type.FPSFire);
		SetDefaultControl(Type.FPSCycleWeapon);
		SetDefaultControl(Type.FPSLookLeft);
		SetDefaultControl(Type.FPSLookRight);
		SetDefaultControl(Type.FPSLookUp);
		SetDefaultControl(Type.FPSLookDown);

	}

	public static Control SetDefaultControl(Type type)
	{
		switch (type)
		{
			case Type.MoveLeft: return SetKey(Type.MoveLeft, KeyCode.LeftArrow, 0);
			case Type.MoveRight: return SetKey(Type.MoveRight, KeyCode.RightArrow, 0);
			case Type.MoveDown: return SetKey(Type.MoveDown, KeyCode.DownArrow, 0);
			case Type.Jump: return SetKey(Type.Jump, KeyCode.Z, 0);
			case Type.Ability1: return SetKey(Type.Ability1, KeyCode.X, 0);
			case Type.Respawn: return SetKey(Type.Respawn, KeyCode.R, 0);
			case Type.Restart: return SetKey(Type.Restart, KeyCode.Q, 0);

			case Type.MenuLeft: return SetKey(Type.MenuLeft, KeyCode.LeftArrow, 0);
			case Type.MenuRight: return SetKey(Type.MenuRight, KeyCode.RightArrow, 0);
			case Type.MenuUp: return SetKey(Type.MenuUp, KeyCode.UpArrow, 0);
			case Type.MenuDown: return SetKey(Type.MenuDown, KeyCode.DownArrow, 0);
			case Type.MenuSelect: return SetKey(Type.MenuSelect, KeyCode.Return, 0);
			case Type.OpenMenu: return SetKey(Type.OpenMenu, KeyCode.Escape, 0);

			case Type.FPSForward: return SetKey(Type.FPSForward, KeyCode.W, 0);
			case Type.FPSBackward: return SetKey(Type.FPSBackward, KeyCode.S, 0);
			case Type.FPSLeft: return SetKey(Type.FPSLeft, KeyCode.A, 0);
			case Type.FPSRight: return SetKey(Type.FPSRight, KeyCode.D, 0);
			case Type.FPSFire: return SetKey(Type.FPSFire, KeyCode.Space, 0);
			case Type.FPSCycleWeapon: return SetKey(Type.FPSCycleWeapon, KeyCode.Q, 0);
			case Type.FPSLookLeft: return SetKey(Type.FPSLookLeft, -1, 1, 1, true, 0);
			case Type.FPSLookRight: return SetKey(Type.FPSLookRight, -1, 1, 1, false, 0);
			case Type.FPSLookUp: return SetKey(Type.FPSLookUp, -1, 1, 2, true, 0);
			case Type.FPSLookDown: return SetKey(Type.FPSLookDown, -1, 1, 2, false, 0);

		}

		return null;
	}

	public static void ResetControls()
	{
		//delete all controls!
		SetDefaultControls();
	}

	public static Control SetKey(Type type, Control control, int player) { return SetKey(type, control.keyInt, control.joyNum, control.axisNum, control.posAxis, player); }
	public static Control SetKey(Type type, KeyCode keycode, int player) { return SetKey(type, (int)keycode, -1, -1, true, player); }
	public static Control SetKey(Type type, int keyInt, int joyNum, int axisNum, bool posAxis, int player)
	{
		Control c = new Control(type, keyInt, joyNum, axisNum, posAxis, player);
		bool found = false;
		for (int i = 0; i < controls.Count; i++)
		{
			if (controls[i].type == type)
			{
				controls[i] = c;
			}
		}
		if (!found)
		{
			controls.Add(c);
		}

		return c;
	}

	public static Control SetTempKey(Type type, int keyInt, int joyNum, int axisNum, bool posAxis, int player)
	{
		Control c = new Control(type, keyInt, joyNum, axisNum, posAxis, player);
		bool found = false;
		for (int i = 0; i < tempControls.Count; i++)
		{
			if (tempControls[i].type == type)
			{
				tempControls[i] = c;
			}
		}
		if (!found)
		{
			tempControls.Add(c);
		}

		return c;
	}

	public static void SetTempControlsFromControls()
	{
		tempControls = new List<Control>();
		for (int i = 0; i < controls.Count; i++)
		{
			Control c = new Control(controls[i].type, controls[i].keyInt, controls[i].joyNum, controls[i].axisNum, controls[i].posAxis, controls[i].player);
			tempControls.Add(c);
		}
	}

	public static void SetControlsFromTempControls(int plNum)
	{/*
		controls = new List<Control>();
		for (int i = 0; i < tempControls.Count; i++)
		{
			Control c = new Control(tempControls[i].type, tempControls[i].keyInt, tempControls[i].joyNum, tempControls[i].axisNum, tempControls[i].posAxis, tempControls[i].player);
			controls.Add(c);
		}*/




		List<Control> newControls = new List<Control>();

		//If desired, save some of the old controls
		if (plNum != -1)//if it doesn, then don't save anything from the fire
		{
			for (int i = 0; i < controls.Count; i++)
			{
				if (controls[i].player != plNum)
				{
					Control c = new Control(controls[i].type, controls[i].keyInt, controls[i].joyNum, controls[i].axisNum, controls[i].posAxis, controls[i].player);
					newControls.Add(c);
				}
			}
		}

		for (int i = 0; i < tempControls.Count; i++)
		{
			if (plNum == -1 || tempControls[i].player == plNum)
			{
				Control c = new Control(tempControls[i].type, tempControls[i].keyInt, tempControls[i].joyNum, tempControls[i].axisNum, tempControls[i].posAxis, tempControls[i].player);
				newControls.Add(c);
			}
		}

		//Now copy them over
		controls = new List<Control>();
		for (int i = 0; i < newControls.Count; i++)
		{
			Control c = new Control(newControls[i].type, newControls[i].keyInt, newControls[i].joyNum, newControls[i].axisNum, newControls[i].posAxis, newControls[i].player);
			controls.Add(c);
		}


	}

	public static void SetBackupControlsFromControls()
	{
		backupControls = new List<Control>();
		for (int i = 0; i < controls.Count; i++)
		{
			Control c = new Control(controls[i].type, controls[i].keyInt, controls[i].joyNum, controls[i].axisNum, controls[i].posAxis, controls[i].player);
			backupControls.Add(c);
		}
	}

	public static void SetControlsFromBackupControls(int plNum)//-1 wipes all 4 players and everything
	{
		List<Control> newControls = new List<Control>();

		//If desired, save some of the old controls
		if (plNum != -1)//if it doesn, then don't save anything from the fire
		{
			for (int i = 0; i < controls.Count; i++)
			{
				if (controls[i].player != plNum)
				{
					Control c = new Control(controls[i].type, controls[i].keyInt, controls[i].joyNum, controls[i].axisNum, controls[i].posAxis, controls[i].player);
					newControls.Add(c);
				}
			}
		}

		for (int i = 0; i < backupControls.Count; i++)
		{
			if (plNum == -1 || backupControls[i].player == plNum)
			{
				Control c = new Control(backupControls[i].type, backupControls[i].keyInt, backupControls[i].joyNum, backupControls[i].axisNum, backupControls[i].posAxis, backupControls[i].player);
				newControls.Add(c);
			}
		}

		//Now copy them over
		controls = new List<Control>();
		for (int i = 0; i < newControls.Count; i++)
		{
			Control c = new Control(newControls[i].type, newControls[i].keyInt, newControls[i].joyNum, newControls[i].axisNum, newControls[i].posAxis, newControls[i].player);
			controls.Add(c);
		}
	}


	public static bool ReturnAxis(int num, int axis, bool posAxis)
	{
		float value = Input.GetAxis("Joy" + num + " Axis " + axis);
		if (posAxis && value > 0.5f)
		{
			return true;
		}
		else if (!posAxis && value < -0.5f)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public static bool settingKey = false;
	public static string keyString = null;
	public static int keyInt = -1;
	public static int playerInt = 0;
	public static Type returnType = Type.None;

	public static void StartSettingKey(Type type)
	{
		settingKey = true;
		keyString = null;
		keyInt = -1;
		returnType = type;
	}

	public static string GetLabelForControl(Control control)
	{

		string result = "";
		if (control.keyInt > 0)
		{
			result = "" + (KeyCode)control.keyInt;
		}
		else
		{
			result = "Controller" + control.joyNum + " Axis" + control.axisNum + " Dir" + control.posAxis;
		}
		return result;
	}

	public static string GetLabelForControlType(Type type)
	{
		Control control = null;

		for (int i = 0; i < controls.Count; i++)
		{
			if (controls[i].type == type) { control = controls[i]; break; }
		}
		if (control == null) { return "CONTROL NOT FOUND"; }

		string result = "";


		if (DetectController.controller == DetectController.KnownControllers.Keyboard)
		{
			if (control.keyInt > 0)
			{
				result = "" + (KeyCode)control.keyInt;
			}
			else //Nothing else has worked, just use custom joystick settings
			{
				result = "Controller" + control.joyNum + " Axis" + control.axisNum + " Dir" + control.posAxis;
			}
		}
		else if (DetectController.controller == DetectController.KnownControllers.Xbox360)
		{
			if (control.keyInt > 0)
			{
				if (platform == Platforms.Windows)
				{
					if (control.keyInt == 0) { return "A"; }
					if (control.keyInt == 1) { return "B"; }
					if (control.keyInt == 2) { return "X"; }
					if (control.keyInt == 3) { return "Y"; }
				}
				if (platform == Platforms.OSX)
				{
					if (control.keyInt == 16) { return "A"; }
					if (control.keyInt == 17) { return "B"; }
					if (control.keyInt == 18) { return "X"; }
					if (control.keyInt == 19) { return "Y"; }
				}
				if (platform == Platforms.Linux)
				{
					if (control.keyInt == 0) { return "A"; }
					if (control.keyInt == 1) { return "B"; }
					if (control.keyInt == 2) { return "X"; }
					if (control.keyInt == 3) { return "Y"; }
				}
			}
			else
			{
				//It's an axis
				result = "Controller" + control.joyNum + " Axis" + control.axisNum + " Dir" + control.posAxis;
			}
		}




		return result;
	}

	public static KeyCode FetchKey()
	{
		int maxkey = (int)KeyCode.Joystick4Button19;
		for (int i = 0; i < maxkey; ++i)
		{
			if (Input.GetKeyDown((KeyCode)i))
			{
				return (KeyCode)i;
			}
		}
		return KeyCode.None;
	}

	public static void HandleDetectKeys()//Called every frame from Main
	{
		if (!settingKey) { return; }

		if (Input.anyKeyDown)
		{
			//is a key
			KeyCode key = FetchKey();

			if (IsBindable(key))//check for unbindable keys
			{
				Control c = new Control(Type.None, (int)key, -1, -1, true, playerInt);
				settingKey = false;
				ReturnSettingKey(c);
			}
		}
		else
		{

			//Detect joystick movement
			float value = 0;
			bool breakOut = false;
			int joyNum = 0;
			int axisNum = 0;
			bool posAxis = true;//default to true
			for (int i = 1; i <= 10; i++)
			{
				for (int a = 1; a <= 10; a++)
				{
					value = Input.GetAxis("Joy" + i + " Axis " + a);
					joyNum = i;
					axisNum = a;
					if (value < 0) { posAxis = false; }
					if (value > 0) { posAxis = true; }
					if (value != 0) { breakOut = true; break; }

				}
				if (breakOut) { break; }
			}

			if (value != 0)
			{
				//Debug.Log("Controller Input Detected: Joy" + joyNum + " Axis " + axisNum + ": Value: " + value);

				Control c = new Control(Type.None, -1, joyNum, axisNum, posAxis, playerInt);
				settingKey = false;
				ReturnSettingKey(c);

			}

		}
	}

	public static void ReturnSettingKey(Control control)
	{
		control.type = returnType;
		control.player = 0;//only one type of player
		string label = GetLabelForControl(control);
		Debug.Log("Binding key: " + label + " to " + returnType + ". JoyNum: " + control.joyNum + ", JoyAxis: " + control.axisNum);
		control.label = label;

		SetTempKey(control.type, control.keyInt, control.joyNum, control.axisNum, control.posAxis, control.player);

		returnType = Type.None;
	}

	public static void DetectControllerAxises()
	{
		float temp = 0;
		for (int j = 1; j < axes.GetLength(0); j++)//check for 4 joysticks
		{
			for (int a = 1; a < axes.GetLength(1); a++)
			{
				sb.Length = 0;
				sb.Append("Joy");
				sb.Append(j);
				sb.Append(" Axis ");
				sb.Append(a);
				temp = Input.GetAxis(sb.ToString());
				axesOld[j, a] = axes[j, a];

				//sanitize & apply
				if (temp > deadzone) { axes[j, a] = 1; }
				else if (temp < -deadzone) { axes[j, a] = -1; }
				else { axes[j, a] = 0; }

			}
		}
	}
	/*
	public static void DetectControllerAxises()
	{
	float value = 0;
	bool breakOut = false;
	int joyNum = 0;
	int axisNum = 0;
	for (int i = 1; i <= 10; i++)
	{
		for (int a = 1; a <= 10; a++)
		{
			value = Input.GetAxis("Joy" + i + " Axis " + a);
			joyNum = i;
			axisNum = a;
			if (value != 0) { breakOut = true; break; }

		}
		if (breakOut) { break; }
	}

	if (value != 0)
	{
		//Debug.Log("Controller Input Detected: Joy" + joyNum + " Axis " + axisNum + ": Value: " + value);

	}
	else
	{
		//Debug.Log("Value: 0");
	}
	}
	*/

	static bool IsBindable(KeyCode key)//returns false for keys that I don't want to let them bind to custom controls.
	{
		if (key == KeyCode.Escape) { return false; }
		return true;
	}


	public static bool EscapeDown()//returns false for keys that I don't want to let them bind to custom controls.
	{
		if (Input.GetKeyDown(KeyCode.Escape)) { return true; }

		return false;
	}


	public static bool EscapeUp()//returns false for keys that I don't want to let them bind to custom controls.
	{
		if (Input.GetKeyUp(KeyCode.Escape)) { return true; }

		return false;
	}


	public static Platforms GetPlatform()
	{
#if UNITY_STANDALONE_WIN
		return Platforms.Windows;
#endif
#if UNITY_STANDALONE_OSX
		return Platforms.OSX;
#endif
#if UNITY_STANDALONE_LINUX
		return Platforms.Linux;
#endif
	}



	public static void DetectCurrentPlatform()//Called on start
	{
		platform = GetPlatform();
	}


	public static string GetLabelForJumpKey()
	{
		//if (!useCustom)
		//{
		//	AutoControls.GetLabelForJumpKey();
		//}
		//else
		//{
		for (int i = 0; i < controls.Count; i++)
		{
			if (controls[i].type == Type.Jump)
			{
				if (controls[i].keyInt > 0)
				{
					return "" + (KeyCode)(controls[i].keyInt);
				}
				else
				{
					return "Joy" + controls[i].joyNum + " Axis " + controls[i].axisNum;
				}
			}
		}
		//}
		return "Z";//just return the best guess, if it's gotten this far
	}

	public static string GetLabelForAbility1Key()
	{
		//if (!useCustom)
		//{
		//	AutoControls.GetLabelForAbility1Key();
		//}
		//else
		//{
		for (int i = 0; i < controls.Count; i++)
		{
			if (controls[i].type == Type.Ability1)
			{
				if (controls[i].keyInt > 0)
				{
					return "" + (KeyCode)(controls[i].keyInt);
				}
				else
				{
					return "Joy" + controls[i].joyNum + " Axis " + controls[i].axisNum;
				}
			}
		}
		//}
		return "X";//just return the best guess, if it's gotten this far
	}

	public static string GetLabelForMoveDownKey()
	{
		//if (!useCustom)
		//{
		//	AutoControls.GetLabelForMoveDownKey();
		//}
		//else
		//{
		for (int i = 0; i < controls.Count; i++)
		{
			if (controls[i].type == Type.Ability1)
			{
				if (controls[i].keyInt > 0)
				{
					return "" + (KeyCode)(controls[i].keyInt);
				}
				else
				{
					return "Joy" + controls[i].joyNum + " Axis " + controls[i].axisNum;
				}
			}
		}
		//}
		return "Down Arrow";//just return the best guess, if it's gotten this far
	}
}
