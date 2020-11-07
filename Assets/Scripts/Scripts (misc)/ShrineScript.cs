using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShrineScript : MonoBehaviour
{
	public static List<ShrineScript> registeredShrines = new List<ShrineScript>();


	public static void CheckShrines(Vector3 pos)
	{
		for (int i = 0; i < registeredShrines.Count; i++)
		{
			if (registeredShrines[i] != null && registeredShrines[i].enabled == true)
			{
				CheckShrine(registeredShrines[i], pos);
			}
		}
	}


	//Non-static

	public bool loseAllAbilities = false;

	public bool gainCrush = false;
	public bool gainDoubleJump = false;
	public bool gainFlight = false;
	public bool gainQuintupleJump = false;
	public bool gainBazooka = false;
	public bool gainTeleportGun = false;
	public bool gainSword = false;
	public bool gainTripleJump = false;
	public bool gainExplosiveJump = false;
	public bool gainPunch = false;

	public GameObject shrineIcon;
	public GameObject pickedUpEffect;



	void Start()
	{
		registeredShrines.Add(this);
	}

	void Update()
	{
		if (xa.player)
		{
			CheckShrine(this, xa.player.transform.position);
		}
	}

	public static void CheckShrine(ShrineScript ss, Vector3 pos)
	{
		Transform t = ss.transform;
		float plBoxHeight = xa.playerBoxHeight;
		float plBoxWidth = xa.playerBoxWidth;

		if ((t.position.x + (t.localScale.x * 0.5f)) > (pos.x - (plBoxWidth * 0.5f)) &&
			(t.position.x - (t.localScale.x * 0.5f)) < (pos.x + (plBoxWidth * 0.5f)) &&
			(t.position.y + (t.localScale.y * 0.5f)) > (pos.y - (plBoxHeight * 0.5f)) &&
			(t.position.y - (t.localScale.y * 0.5f)) < (pos.y + (plBoxHeight * 0.5f)))
		{
			if (ss.loseAllAbilities)
			{
				xa.playerHasGroundPound = false;
				xa.playerHasDoubleJump = false;
				xa.playerHasJetpack = false;
				if (xa.playerScript != null)
				{
					SaveAbilitiesNodeScript.WipePotentials(false);
					SaveAbilitiesNodeScript.lostAllAbilities = true;
					xa.playerScript.SetAirJumps(0);
					xa.playerScript.SetMaxAirJumps(0);
					xa.playerScript.hasQuintupleJump = false;
					xa.playerScript.hasSword = false;
					xa.playerScript.hasTripleJump = false;
					xa.playerScript.hasExplosiveJump = false;
					xa.playerScript.hasPunch = false;
				}
			}
			if (ss.gainCrush) { xa.playerHasGroundPound = true; }
			if (ss.gainDoubleJump) { xa.playerHasDoubleJump = true; }
			if (ss.gainFlight) { xa.playerHasJetpack = true; }
			if (ss.gainQuintupleJump) { if (xa.playerScript != null) { xa.playerScript.hasQuintupleJump = true; xa.playerScript.SetAirJumps(4); } }
			if (ss.gainSword) { if (xa.playerScript != null) { xa.playerScript.hasSword = true; } }
			if (ss.gainTripleJump) { if (xa.playerScript != null) { xa.playerScript.hasTripleJump = true; xa.playerScript.SetAirJumps(2); } }
			if (ss.gainExplosiveJump) { if (xa.playerScript != null) { xa.playerScript.hasExplosiveJump = true; } }
			if (ss.gainPunch) { if (xa.playerScript != null) { xa.playerScript.hasPunch = true; } }


			Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.GetShrine);
			iTweenEvent.GetEvent(ss.shrineIcon, "scaleOut").Play();
			ss.pickedUpEffect.SetActive(true);
			ss.enabled = false;
		}

	}
}
