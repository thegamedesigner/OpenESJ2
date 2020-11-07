using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Custom : MonoBehaviour
{
	public static bool CustomTranslation(Controls.Type type, Recon.ControlState state, int playerNum)
	{
		switch (state)
{
			case Recon.ControlState.Constant:
				switch (type)
				{
					//Platforming
					//case Controls.Type.Jump: return ReInput.players.GetPlayer(0).GetButton("Jump");
					//case Controls.Type.Ability1: return ReInput.players.GetPlayer(0).GetButton("Ability1");
					//case Controls.Type.MoveRight: return ReInput.players.GetPlayer(0).GetButton("Move Right");
					//case Controls.Type.MoveLeft: return ReInput.players.GetPlayer(0).GetButton("Move Left");
				}
				return false;
			case Recon.ControlState.Down:
				switch (type)
				{
					//case Controls.Type.MenuSelect: return ReInput.players.GetPlayer(0).GetButtonDown("Menu Select");

					//Platforming
					//case Controls.Type.Jump: return ReInput.players.GetPlayer(0).GetButtonDown("Jump");
					//case Controls.Type.Ability1: return ReInput.players.GetPlayer(0).GetButtonDown("Ability1");
					//case Controls.Type.MoveDown: return ReInput.players.GetPlayer(0).GetButtonDown("Move Down");
					//case Controls.Type.MoveRight: return ReInput.players.GetPlayer(0).GetButtonDown("Move Right");
					//case Controls.Type.MoveLeft: return ReInput.players.GetPlayer(0).GetButtonDown("Move Left");
					//case Controls.Type.Respawn: return ReInput.players.GetPlayer(0).GetButtonDown("Respawn");
					//case Controls.Type.Restart: return ReInput.players.GetPlayer(0).GetButtonDown("Restart");

					//menu
					//case Controls.Type.MenuSelect: return ReInput.players.GetPlayer(0).GetButtonDown("Menu Select");
					//case Controls.Type.OpenMenu: return ReInput.players.GetPlayer(0).GetButtonDown("Menu Escape");
					//case Controls.Type.MenuUp: return ReInput.players.GetPlayer(0).GetButtonDown("Menu Up");
					//case Controls.Type.MenuDown: return ReInput.players.GetPlayer(0).GetButtonDown("Menu Down");
					//case Controls.Type.MenuRight: return ReInput.players.GetPlayer(0).GetButtonDown("Menu Right");
					//case Controls.Type.MenuLeft: return ReInput.players.GetPlayer(0).GetButtonDown("Menu Left");


				}
				return false;
			case Recon.ControlState.Up:
				switch (type)
				{
					//Platforming
					//case Controls.Type.Jump: return ReInput.players.GetPlayer(0).GetButtonUp("Jump");
					//case Controls.Type.Ability1: return ReInput.players.GetPlayer(0).GetButtonUp("Ability1");
					//case Controls.Type.MoveRight: return ReInput.players.GetPlayer(0).GetButtonUp("Move Right");
					//case Controls.Type.MoveLeft: return ReInput.players.GetPlayer(0).GetButtonUp("Move Left");


				}
				return false;
		}
		return false;
	}



}
