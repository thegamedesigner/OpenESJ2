using UnityEngine;
using System.Collections;

public class GainAbilityZoneScript : MonoBehaviour
{
	public bool gainGroundPound = false;
	public bool gainDoubleJump = false;
	public bool gainJetpack = false;
	public bool gainSuperJump = false;
	public bool gainAirSword = false;
	public bool gainQuintupleJump = false;
	public bool gainTripleJump = false;
    public bool gainExplosiveJump = false;
	public bool gainPunch = false;
	public bool loseAllAbilities = false;
	public bool onlyDoThisOnce = false;
	float plBoxHeight = 0;
	float plBoxWidth = 0;
	bool workedOnce = false;

	void Start()
	{

	}

	void Update()
	{
		if (!workedOnce)
		{
			if (xa.player)
			{
				plBoxHeight = xa.playerBoxHeight;
				plBoxWidth = xa.playerBoxWidth;

				if ((transform.position.x + (transform.localScale.x * 0.5f)) > (xa.player.transform.position.x - (plBoxWidth * 0.5f)) &&
					(transform.position.x - (transform.localScale.x * 0.5f)) < (xa.player.transform.position.x + (plBoxWidth * 0.5f)) &&
					(transform.position.y + (transform.localScale.y * 0.5f)) > (xa.player.transform.position.y - (plBoxHeight * 0.5f)) &&
					(transform.position.y - (transform.localScale.y * 0.5f)) < (xa.player.transform.position.y + (plBoxHeight * 0.5f)))
				{
                    if (loseAllAbilities)
                    {
                        xa.playerHasGroundPound = false;
                        xa.playerHasDoubleJump = false;
                        xa.playerHasJetpack = false;
                        xa.playerHasSuperJump = false;
                        if (xa.playerScript != null)
                        {
                            xa.playerScript.hasQuintupleJump = false;
                            xa.playerScript.hasSword = false;
                            xa.playerScript.hasTripleJump = false;
                            xa.playerScript.hasExplosiveJump = false;
                            xa.playerScript.hasPunch = false;
                        }
                    }
					if (gainGroundPound) { xa.playerHasGroundPound = true; }
					if (gainDoubleJump) { xa.playerHasDoubleJump = true; }
					if (gainJetpack) { xa.playerHasJetpack = true; }
					if (gainSuperJump) { xa.playerHasSuperJump = true; }
					if (gainQuintupleJump) { if (xa.playerScript != null) { xa.playerScript.hasQuintupleJump = true; } }
                    if (gainAirSword) { if (xa.playerScript != null) { xa.playerScript.hasSword = true; } }
					if (gainTripleJump) { if (xa.playerScript != null) { xa.playerScript.hasTripleJump = true; } }
					if (gainExplosiveJump) { if (xa.playerScript != null) { xa.playerScript.hasExplosiveJump = true;} }
					if (gainPunch) { if (xa.playerScript != null) { xa.playerScript.hasPunch = true;} }
					
					if (onlyDoThisOnce) { workedOnce = true; }
				}

			}
		}
	}
}
