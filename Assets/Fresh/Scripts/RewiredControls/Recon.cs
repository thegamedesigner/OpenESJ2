using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class Recon : MonoBehaviour
{
	public static Recon self = null;
	public static bool useRecon = true;//Don't use previous control systems, use Rewired

	public static float deadzone = 0.4f;//the deadzone for axis on joysticks

	void Start()
	{
		self = this;
	}

	void Update()
	{	
		if (Input.GetKeyDown(KeyCode.Space)) { RawFuncs.WipePrint(); }
		/*
	
		//Platforming
		if (ReInput.players.GetPlayer(0).GetButtonDown("Jump")) { RawFuncs.Print("Jump"); }
		if (ReInput.players.GetPlayer(0).GetButtonDown("Ability1")) { RawFuncs.Print("Stomp/Airsword"); }
		if (ReInput.players.GetPlayer(0).GetButtonDown("Move Down")) { RawFuncs.Print("Drop off wall"); }
		if (ReInput.players.GetPlayer(0).GetButtonDown("Restart")) { RawFuncs.Print("Restart level"); }
		if (ReInput.players.GetPlayer(0).GetButtonDown("Respawn")) { RawFuncs.Print("Respawn from checkpoint"); }
		if (ReInput.players.GetPlayer(0).GetButtonDown("Move Left")) { RawFuncs.Print("Move Left"); }
		if (ReInput.players.GetPlayer(0).GetButtonDown("Move Right")) { RawFuncs.Print("Move Right"); }

		//Menus
		if (ReInput.players.GetPlayer(0).GetButtonDown("Menu Up")) { RawFuncs.Print("Menu Up"); }
		if (ReInput.players.GetPlayer(0).GetButtonDown("Menu Down")) { RawFuncs.Print("Menu Down"); }
		if (ReInput.players.GetPlayer(0).GetButtonDown("Menu Right")) { RawFuncs.Print("Menu Right"); }
		if (ReInput.players.GetPlayer(0).GetButtonDown("Menu Left")) { RawFuncs.Print("Menu Left"); }
		if (ReInput.players.GetPlayer(0).GetButtonDown("Menu Select")) { RawFuncs.Print("Menu Select"); }
		if (ReInput.players.GetPlayer(0).GetButtonDown("Menu Escape")) { RawFuncs.Print("Menu Escape"); }

		//FPS buttons
		if (ReInput.players.GetPlayer(0).GetButtonDown("FPS Fire")) { RawFuncs.Print("FPS Fire"); }
		if (ReInput.players.GetPlayer(0).GetButtonDown("FPS Cycle")) { RawFuncs.Print("FPS Cycle"); }

		//FPS axes
		float axis = 0;
		axis = ReInput.players.GetPlayer(0).GetAxis("FPS Forward");
		if (axis < -deadzone) { RawFuncs.Print("FPS Backwards"); }
		if (axis > deadzone) { RawFuncs.Print("FPS Forwards"); }

		axis = ReInput.players.GetPlayer(0).GetAxis("FPS Strafe");
		if (axis < -deadzone) { RawFuncs.Print("FPS Strafe Left"); }
		if (axis > deadzone) { RawFuncs.Print("FPS Strafe Right"); }

		axis = ReInput.players.GetPlayer(0).GetAxis("FPS Look Horizontal");
		if (axis < -deadzone) { RawFuncs.Print("FPS Look Left"); }
		if (axis > deadzone) { RawFuncs.Print("FPS Look Right"); }

		axis = ReInput.players.GetPlayer(0).GetAxis("FPS Look Vertical");
		if (axis < -deadzone) { RawFuncs.Print("FPS Look Down"); }
		if (axis > deadzone) { RawFuncs.Print("FPS Look Up"); }
		*/

	}

	public enum ControlState
	{
		Up,//GetKeyUp
		Down,//GetKeyDown
		Constant,//GetKey
		End
	}

	public static bool GetAxis(string name, bool positive)
	{

		float axis = 0;
		axis = ReInput.players.GetPlayer(0).GetAxis(name);
		if (positive)
		{
			if (axis > deadzone) { return true; }
		}
		else
		{
			if (axis < -deadzone) { return true; }
		}

		return false;
	}

	public static bool Translation(Controls.Type type, ControlState state, int playerNum)
	{
		//Debug.Log(type + ", " + state + ", " + Time.time);
		//playerNum is ignored, as it's defunct
		switch (state)
		{
			case ControlState.Constant:
				switch (type)
				{
					//Platforming
					case Controls.Type.Jump: return ReInput.players.GetPlayer(0).GetButton("Jump");
					case Controls.Type.Ability1: return ReInput.players.GetPlayer(0).GetButton("Ability1");
					case Controls.Type.MoveRight: return ReInput.players.GetPlayer(0).GetButton("Move Right");
					case Controls.Type.MoveLeft: return ReInput.players.GetPlayer(0).GetButton("Move Left");
					case Controls.Type.MoveDown: return ReInput.players.GetPlayer(0).GetButton("Move Down");
						
					case Controls.Type.FPSForward: return ReInput.players.GetPlayer(0).GetButton("FPS Forward");
					case Controls.Type.FPSBackward: return ReInput.players.GetPlayer(0).GetButton("FPS Backwards");
					case Controls.Type.FPSRight: return ReInput.players.GetPlayer(0).GetButton("FPS Right");
					case Controls.Type.FPSLeft: return ReInput.players.GetPlayer(0).GetButton("FPS Left");

					//case Controls.Type.FPSForward: return GetAxis("FPS Forward", true);
					//case Controls.Type.FPSBackward: return GetAxis("FPS Forward", false);
					//case Controls.Type.FPSRight: return GetAxis("FPS Strafe", true);
					//case Controls.Type.FPSLeft: return GetAxis("FPS Strafe", false);
					case Controls.Type.FPSLookUp: return GetAxis("FPS Look Vertical", true);
					case Controls.Type.FPSLookDown: return GetAxis("FPS Look Vertical", false);
					case Controls.Type.FPSLookRight: return GetAxis("FPS Look Horizontal", true);
					case Controls.Type.FPSLookLeft: return GetAxis("FPS Look Horizontal", false);
				}
				return false;
			case ControlState.Down:
				switch (type)
				{
					//Platforming
					case Controls.Type.Jump: return ReInput.players.GetPlayer(0).GetButtonDown("Jump");
					case Controls.Type.Ability1: return ReInput.players.GetPlayer(0).GetButtonDown("Ability1");
					case Controls.Type.MoveDown: return ReInput.players.GetPlayer(0).GetButtonDown("Move Down");
					case Controls.Type.MoveRight: return ReInput.players.GetPlayer(0).GetButtonDown("Move Right");
					case Controls.Type.MoveLeft: return ReInput.players.GetPlayer(0).GetButtonDown("Move Left");
					case Controls.Type.Respawn: return ReInput.players.GetPlayer(0).GetButtonDown("Respawn");
					case Controls.Type.Restart: return ReInput.players.GetPlayer(0).GetButtonDown("Restart");

					//menu
					case Controls.Type.MenuSelect: return ReInput.players.GetPlayer(0).GetButtonDown("Menu Select");
					case Controls.Type.OpenMenu: return ReInput.players.GetPlayer(0).GetButtonDown("Menu Escape");
					case Controls.Type.MenuUp: return ReInput.players.GetPlayer(0).GetButtonDown("Menu Up");
					case Controls.Type.MenuDown: return ReInput.players.GetPlayer(0).GetButtonDown("Menu Down");
					case Controls.Type.MenuRight: return ReInput.players.GetPlayer(0).GetButtonDown("Menu Right");
					case Controls.Type.MenuLeft: return ReInput.players.GetPlayer(0).GetButtonDown("Menu Left");

					//FPS
					case Controls.Type.FPSFire: return ReInput.players.GetPlayer(0).GetButtonDown("FPS Fire");
					case Controls.Type.FPSCycleWeapon: return ReInput.players.GetPlayer(0).GetButtonDown("FPS Cycle");


				}
				return false;
			case ControlState.Up:
				switch (type)
				{
					//Platforming
					case Controls.Type.Jump: return ReInput.players.GetPlayer(0).GetButtonUp("Jump");
					case Controls.Type.Ability1: return ReInput.players.GetPlayer(0).GetButtonUp("Ability1");
					case Controls.Type.MoveRight: return ReInput.players.GetPlayer(0).GetButtonUp("Move Right");
					case Controls.Type.MoveLeft: return ReInput.players.GetPlayer(0).GetButtonUp("Move Left");


				}
				return false;
		}


		return false;
	}

	
	public static bool AnyInputDown()
	{
		bool a = ReInput.players.GetPlayer(0).GetAnyButton();
		bool b = Input.anyKeyDown;
		if (a || b) { return true; }
		return false;
	}

	public static bool AnyInputDownOnce()//used in story spawners
	{
		return ReInput.players.GetPlayer(0).GetAnyButtonDown();
	}

}
