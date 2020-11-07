using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveAbilitiesNodeScript : MonoBehaviour
{
	/*

		This is a local solution, which does nothing, if this script isn't placed on a gameobject, in the level.

		When it does exist, it saves all abilities the player gets, and all keys collected. This allows
		for some levels to exist, and have checkpoints, which otherwise could not. 

		It also allows for check points that could be gotten out of order, so that abilities don't just forceable
		set on spawn.

	*/


	public static SaveAbilitiesNodeScript self;

	public static string levelName;

	public static bool lostAllAbilities = false;

	public static bool potential_explosiveJump;
	public static bool potential_doubleJump;
	public static bool potential_tripleJump;
	public static bool potential_quintupleJump;
	public static bool potential_stomp;
	public static bool potential_airsword;
	public static bool potential_punch;
	public static bool potential_redDoorOpened;
	public static bool potential_pinkDoorOpened;
	public static bool potential_orangeDoorOpened;
	public static bool potential_purpleDoorOpened;

	public static bool explosiveJump;
	public static bool doubleJump;
	public static bool tripleJump;
	public static bool quintupleJump;
	public static bool stomp;
	public static bool airsword;
	public static bool punch;

	public static bool redDoorOpened;
	public static bool pinkDoorOpened;
	public static bool orangeDoorOpened;
	public static bool purpleDoorOpened;

	public GameObject redDoorGO;
	public GameObject pinkDoorGO;
	public GameObject orangeDoorGO;
	public GameObject purpleDoorGO;

	public static int SavedDaddysLove = 5;

	void Awake()
	{
		lostAllAbilities = false;
		self = this;

		if (levelName != SceneManager.GetActiveScene().name)
		{
			levelName = SceneManager.GetActiveScene().name;

			WipeSavedAbilities();
		}
	}

	void Start()
	{

	}

	public static void WipeSavedAbilities()//called when the level changes
	{
		WipePotentials(true);
		WipeJustSavedAbilities(true);
	}

	public static void WipeJustSavedAbilities(bool wipeDoors)
	{
		explosiveJump = false;
		doubleJump = false;
		tripleJump = false;
		quintupleJump = false;
		stomp = false;
		airsword = false;
		punch = false;
		if (wipeDoors)
		{
			redDoorOpened = false;
			pinkDoorOpened = false;
			orangeDoorOpened = false;
			purpleDoorOpened = false;
		}
	}

	public static void WipePotentials(bool wipeDoors)
	{
		potential_explosiveJump = false;
		potential_doubleJump = false;
		potential_tripleJump = false;
		potential_quintupleJump = false;
		potential_stomp = false;
		potential_airsword = false;
		potential_punch = false;
		if (wipeDoors)
		{
			potential_redDoorOpened = false;
			potential_pinkDoorOpened = false;
			potential_orangeDoorOpened = false;
			potential_purpleDoorOpened = false;
		}
	}

	public static void HitCheckpoint()
	{
		if (lostAllAbilities)
		{
			WipeJustSavedAbilities(false);
			lostAllAbilities = false;
		}
		if (potential_explosiveJump) { explosiveJump = true; }
		if (potential_doubleJump) { doubleJump = true; }
		if (potential_tripleJump) { tripleJump = true; }
		if (potential_quintupleJump) { quintupleJump = true; }
		if (potential_stomp) { stomp = true; }
		if (potential_airsword) { airsword = true; }
		if (potential_punch) { punch = true; }
		if (potential_redDoorOpened) { redDoorOpened = true; }
		if (potential_pinkDoorOpened) { pinkDoorOpened = true; }
		if (potential_orangeDoorOpened) { orangeDoorOpened = true; }
		if (potential_purpleDoorOpened) { purpleDoorOpened = true; }

		if (ThrowingBallScript.ReadOnly_DaddysLove != -1)
		{
			SavedDaddysLove = ThrowingBallScript.ReadOnly_DaddysLove;
		}

		WipePotentials(true);
	}

	public static void GetSavedAbilities()
	{
		//Debug.Log("GOT SAVED ABILITIES, OPENED DOORS. Red door: " + redDoorOpened + ", red door potential: " + potential_redDoorOpened + ", " + Time.time);
		if (levelName != SceneManager.GetActiveScene().name)
		{
			//new level
			levelName = SceneManager.GetActiveScene().name;
			SavedDaddysLove = 5;
			ThrowingBallScript.External_SetToXDaddysLove(5, "Reset because new level");
			WipeSavedAbilities();

		}
		else
		{
			ThrowingBallScript.External_SetToXDaddysLove(SavedDaddysLove, "Set To Saved");
		}
		if (xa.playerScript != null)
		{
			if (!xa.playerHasDoubleJump && doubleJump) { xa.playerHasDoubleJump = true; }
			if (!xa.playerScript.hasTripleJump && tripleJump) { xa.playerScript.hasTripleJump = true; }
			if (!xa.playerScript.hasQuintupleJump && quintupleJump) { xa.playerScript.hasQuintupleJump = true; }
			if (!xa.playerScript.hasExplosiveJump && explosiveJump) { xa.playerScript.hasExplosiveJump = true; }
			if (!xa.playerScript.hasSword && airsword) { xa.playerScript.hasSword = true; }
			if (!xa.playerScript.hasPunch && punch) { xa.playerScript.hasPunch = true; }
			if (!xa.playerHasGroundPound && stomp) { xa.playerHasGroundPound = true; }
		}
		if (self != null)
		{
			if (redDoorOpened) { iTweenEvent.GetEvent(self.redDoorGO, "remoteTween1").Play(); }
			if (pinkDoorOpened) { iTweenEvent.GetEvent(self.pinkDoorGO, "remoteTween1").Play(); }
			if (orangeDoorOpened) { iTweenEvent.GetEvent(self.orangeDoorGO, "remoteTween1").Play(); }
			if (purpleDoorOpened) { iTweenEvent.GetEvent(self.purpleDoorGO, "remoteTween1").Play(); }
		}
	}

	void Update()
	{
		if (xa.playerScript != null)
		{
			if (xa.playerScript.hasExplosiveJump && !explosiveJump)
			{
				potential_explosiveJump = true;
			}
			if (xa.playerHasDoubleJump && !doubleJump)
			{
				potential_doubleJump = true;
			}
			if (xa.playerScript.hasTripleJump && !tripleJump)
			{
				potential_tripleJump = true;
			}
			if (xa.playerScript.hasQuintupleJump && !quintupleJump)
			{
				potential_quintupleJump = true;
			}
			if (xa.playerHasGroundPound && !stomp)
			{
				potential_stomp = true;
			}
			if (xa.playerScript.hasSword && !airsword)
			{
				potential_airsword = true;
			}
			if (xa.playerScript.hasPunch && !punch)
			{
				potential_punch = true;
			}

		}
	}
}
