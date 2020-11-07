using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HurtZoneScript : MonoBehaviour
{
	public static List<HZ> HZs = null;

	public class HZ
	{
		public Vector2 pos;//x,y
		public Vector2 size;//width,height
		public GameObject self;
		public bool dontHurtAirsword;//the airsword detects if it's in a hurtzone, and flagging this makes it not kill the player
	}
	HZ myHZ = null;

	public GameObject gameobjectToDieOnImpact = null;
	public float dist = 0;
	public float forcedPlBoxHeight = 0;
	public float forcedPlBoxWidth = 0;
	public bool hurtInRadius = false;
	public bool killInvinciblePlayer = false;
	public bool hurtInBox = false;
	public bool forcePlayerBox = false;
	public bool dieOnImpact = false;
	public bool dontHurtAirsword = false;
	public bool dontHurtIfFalling = false;
	float plBoxHeight = 0;
	float plBoxWidth = 0;
	bool setBoxStats = false;

	public static void CleanDeadHurtZones()
	{
		if (HZs == null) { return; }
		for (int i = 0; i < HZs.Count; i++)
		{
			if (HZs[i] == null || HZs[i].self == null)
			{
				HZs.RemoveAt(i);
				break;
			}
		}
	}

	//Used by airsword, when it detects it's in a hurt zone.
	public static void StaticHurtFunc()
	{
		if (xa.player)
		{
			HealthScript script = null;
			script = xa.player.GetComponent<HealthScript>();
			if (script)
			{
				if (script.invincibleTimer <= 0)
				{
					script.health = 0;
					script.setPosWhenKilled = true;
					script.posWhenKilled = xa.player.transform.position;
				}
			}
		}
	}

	void hurtFunc()
	{
		if (xa.player)
		{
			if (xa.playerScript != null)
			{
				if (dontHurtAirsword && xa.playerScript.state == NovaPlayerScript.State.SwordState)
				{
					return;//dont kill the player, if they're airswording, and donthurtairsword is true
				}
				if (dontHurtIfFalling && xa.playerScript.vel.y < 0)
				{
					return;//dont kill the player, they're not falling
				}
			}
			HealthScript script = null;
			script = xa.player.GetComponent<HealthScript>();
			if (script)
			{
				if (script.invincibleTimer <= 0 || killInvinciblePlayer)
				{
					script.health = 0;
					script.setPosWhenKilled = true;
					script.posWhenKilled = xa.player.transform.position;
				}
			}
			if (dieOnImpact)
			{
				if (gameobjectToDieOnImpact)
				{
					script = gameobjectToDieOnImpact.GetComponent<HealthScript>();
				}

				if (script)
				{
					if (script.invincibleTimer <= 0 || killInvinciblePlayer)
					{
						script.health = 0;
					}

				}
			}
		}
	}

	void Start()
	{
		if (HZs == null) { HZs = new List<HZ>(); }
		HZ hz = new HZ();
		hz.pos = transform.position;
		hz.size = transform.localScale;
		hz.self = this.gameObject;
		hz.dontHurtAirsword = dontHurtAirsword;
		myHZ = hz;
		HZs.Add(hz);
	}

	void Update()
	{
		if (myHZ != null)
		{
			myHZ.pos = transform.position;
			myHZ.size = transform.localScale;
		}
		if (xa.player)
		{
			if (!setBoxStats)
			{
				setBoxStats = true;
				if (forcePlayerBox)
				{
					plBoxHeight = forcedPlBoxHeight;
					plBoxWidth = forcedPlBoxWidth;
				}
				else
				{
					plBoxHeight = xa.playerBoxHeight;
					plBoxWidth = xa.playerBoxWidth;
				}
			}
			if (hurtInBox)
			{
				if ((transform.position.x + (transform.localScale.x * 0.5f)) > (xa.player.transform.position.x - (plBoxWidth * 0.5f)) &&
					(transform.position.x - (transform.localScale.x * 0.5f)) < (xa.player.transform.position.x + (plBoxWidth * 0.5f)) &&
					(transform.position.y + (transform.localScale.y * 0.5f)) > (xa.player.transform.position.y - (plBoxHeight * 0.5f)) &&
					(transform.position.y - (transform.localScale.y * 0.5f)) < (xa.player.transform.position.y + (plBoxHeight * 0.5f)))
				{
					hurtFunc();
				}
			}
			else if (hurtInRadius)
			{
				xa.glx = transform.position;
				xa.glx.z = xa.player.transform.position.z;
				if (Vector3.Distance(xa.glx, xa.player.transform.position) < dist)
				{
					hurtFunc();
				}
			}
		}
	}
}
