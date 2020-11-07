using UnityEngine;

public class MerpsPlayerAniScript : MonoBehaviour
{
	public float aniSpd = 0;
	public float aniStandSpd = 0;
	public float aniDeadSpd = 0;
	public float aniTimePerFrame = 0;
	public float totalWalkFrames = 0;
	public float totalJumpFrames = 0;
	public float totalFallFrames = 0;
	public float totalStandFrames = 0;
	public float totalDeadFrames = 0;
	public float totalWallFrames = 0;

	public GameObject puppet;

	public Material sheet;


	float aniProgress = 0;
	int aniStage = 0;
	bool flipped = false;
	bool demandStumble = false;
	enum aniTypes { None, Stand, Walk, Jump, Fall, Pound, Dead, Dead2, Wall }
	aniTypes currentAniType = aniTypes.Stand;
	aniTypes oldAniType = aniTypes.None;
	bool oldMoving = false;
	bool oldOnGround = false;
	bool oldDead = false;
	bool playedFirstDead = false;
	int tempOffset = 0;
	int tempOffset2 = 0;

	//local, and not updated freq! That's ok!

	//float plSize = 0.9f;
	//float plHalf = 0.45f;
	//float plHeight = 1.3f;//1.5
	//float plHeightHalf = 0.65f;





	void Start()
	{

	}

	void Update()
	{
		if (fa.time > 0.05f)
		{
			chooseAnimation();
			animateFrames();
		}
		za.playerFlipped = flipped;
	}

	void animateFrames()
	{
		if (xa.playerDead)
		{
			setTexture((int)(aniStage + 3), 2);
			// if (currentAniType == aniTypes.Dead) { setTexture((int)(aniStage + 2), 2); }
			//else { setTexture((int)(aniStage + 4), 2); }
		}
		else
		{
			if (demandStumble)
			{
				if (za.hasGun)
				{
					if (za.gunDir == 0)//forward
					{ setTexture(0, 4); }
					if (za.gunDir == 2)//up
					{ setTexture(0, 5); }
					if (za.gunDir == 3)//down
					{ setTexture(0, 6); }
				}
				else
				{
					setTexture(0, 1);
				}
				//setTexture(aniStage, 1);
				// Setup.GC_DebugLog("stumbled " + aniStage);
			}
			else
			{
				if (currentAniType == aniTypes.Pound)
				{
					setTexture(0, 2);
				}
				else if (currentAniType == aniTypes.Wall)
				{
					setTexture(0, 3);
				}
				else if (xa.playerOnGround)
				{
					if (currentAniType == aniTypes.Stand)
					{
						if (za.hasGun)
						{
							if (za.gunDir == 0)//forward
							{ setTexture(1, 4); }
							if (za.gunDir == 2)//up
							{ setTexture(1, 5); }
							if (za.gunDir == 3)//down
							{ setTexture(1, 6); }
						}
						else
						{
							setTexture(0, 0);
						}
					}
					if (currentAniType == aniTypes.Walk)
					{
						if (za.hasGun)
						{
							if (za.gunDir == 0)//forward
							{ setTexture((int)(aniStage + 2), 4); }

							if (za.gunDir == 2)//up
							{ setTexture((int)(aniStage + 2), 5); }
							if (za.gunDir == 3)//down
							{ setTexture((int)(aniStage + 2), 6); }

						}
						else
						{
							setTexture((int)(aniStage + 2), 0);
						}
					}
				}
				else
				{
					//jump stuff
					tempOffset = 0;
					tempOffset2 = 0;
					if (xa.playerVel.y < -0.1f) { tempOffset = 13; tempOffset2 = 6; }
					else if (xa.playerVel.y > 0.1f) { tempOffset = 9; tempOffset2 = 2; }
					else { tempOffset = 11; tempOffset2 = 4; }

					if (za.hasGun)
					{
						if (za.gunDir == 0)//forward
						{
							if (za.forceAimGun)
							{
								setTexture(aniStage + tempOffset, 4);
							}
							else
							{
								setTexture(aniStage + tempOffset, 3);
							}
						}
						if (za.gunDir == 2)//up
						{
							setTexture(aniStage + tempOffset, 5); // forceAimGun true/false, looks silly otherwise
						}
						if (za.gunDir == 3)//down
						{
							if (za.forceAimGun)
							{
								setTexture(aniStage + tempOffset, 6);
							}
							else
							{
								setTexture(aniStage + tempOffset, 3);
							} 
						}
					}
					else
					{
						setTexture(aniStage + tempOffset2, 1);
					}
				}
			}
		}
	}

	void setTexture(int v1, int v2)
	{
		float multi = 0.25f;
		float x1 = 0;
		float y1 = 0;
		float x2 = 0;
		float y2 = 0;

		x1 = 0.125f * multi;
		y1 = 0.125f * multi;
		x2 = (0.125f * multi) * v1;
		y2 = 1 - (((0.125f * multi) * v2) + (0.125f * multi));

		if ((!flipped && xa.forcePlayerDirection == 0) || xa.forcePlayerDirection == 1)
		{
			puppet.GetComponent<Renderer>().material.mainTextureScale = new Vector2(x1, y1);
			puppet.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(x2, y2);
		}
		else if ((flipped && xa.forcePlayerDirection == 0) || xa.forcePlayerDirection == -1)
		{
			puppet.GetComponent<Renderer>().material.mainTextureScale = new Vector2(-x1, y1);
			puppet.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(x2 + (0.125f * multi), y2);
		}
	}

	void chooseAnimation()
	{
		//update flipped bool
		if ((xa.playerDir == -1 && !xa.playerMoveRight) || xa.playerMoveLeft) { flipped = true; }
		if ((xa.playerDir == 1 && !xa.playerMoveLeft) || xa.playerMoveRight) { flipped = false; }
		// if (xa.playerVel.x < -0.1f) { flipped = true; }
		//if (xa.playerVel.x > 0.1f) { flipped = false; }

		currentAniType = aniTypes.Stand;
		if ((xa.inZoneIce <= 0 && xa.playerMoving) || (xa.inZoneIce > 0 && (xa.playerMoveLeft || xa.playerMoveRight))) { currentAniType = aniTypes.Walk; }

		//Sticky floor
		if (!xa.playerOnGround) { currentAniType = aniTypes.Fall; }
		if (xa.playerJumped) { currentAniType = aniTypes.Jump; }
		if (xa.playerStuck)
		{
			//if (xa.playerVel.y <= xa.playerWallStickingMaxFallingVel && xa.playerPoundTimer <= 0)
			//{
			//	if (xa.playerVel.x > -0.04f && xa.playerVel.x < 0.04f)
			//	{
					currentAniType = aniTypes.Wall;
			//	}
			//}
		}
		if (oldMoving != xa.playerMoving)
		{
			oldMoving = xa.playerMoving;
			if (xa.playerMoving == false && !demandStumble)
			{
				demandStumble = true;
				aniStage = 0;
				aniProgress = 0;
			}
		}

		if (oldOnGround != xa.playerOnGround)
		{
			oldOnGround = xa.playerOnGround;
			if (xa.playerOnGround == true && !demandStumble && xa.playerPoundTimer == 0)
			{
				demandStumble = true;
				aniStage = 0;
				aniProgress = 0;
			}
		}

		if (oldDead != xa.playerDead)
		{
			oldDead = xa.playerDead;
			if (xa.playerDead == true)
			{
				aniStage = 0;
				aniProgress = 0;
			}
		}

		if (xa.playerMoving && demandStumble)
		{
			demandStumble = false;
			aniStage = 0;
			aniProgress = 0;
		}
		if (!xa.playerOnGround && demandStumble)
		{
			demandStumble = false;
			aniStage = 0;
			aniProgress = 0;
		}
		if (xa.playerJumpedFresh)
		{
			xa.playerJumpedFresh = false;
			if (xa.playerPoundTimer > 0) { xa.playerPoundTimer = 0; }
		}
		if (xa.playerMoving && xa.playerPoundTimer > 0) { xa.playerPoundTimer = 0; }
		if (xa.playerPoundTimer > 0) { currentAniType = aniTypes.Pound; }
		if (xa.playerDead) { currentAniType = aniTypes.Dead; }
		if (xa.playerDead && playedFirstDead) { currentAniType = aniTypes.Dead2; }


		//restart the animation cycle if it's just changed
		if (oldAniType != currentAniType)
		{
			oldAniType = currentAniType;
			aniStage = 0;
			aniProgress = 0;
		}

		if (currentAniType == aniTypes.Stand) { aniProgress += aniStandSpd * fa.deltaTime; }
		else if (currentAniType == aniTypes.Dead) { aniProgress += aniDeadSpd * fa.deltaTime; }
		else { aniProgress += aniSpd * fa.deltaTime; }
		if (aniProgress > aniTimePerFrame)
		{
			aniProgress = 0;
			aniStage++;
			if (aniStage >= totalWalkFrames && currentAniType == aniTypes.Walk) { aniStage = 0; }
			if (aniStage >= totalJumpFrames && currentAniType == aniTypes.Jump) { aniStage = 0; xa.playerJumped = false; }
			if (aniStage >= totalFallFrames && currentAniType == aniTypes.Fall) { aniStage = 0; }
			if (aniStage >= totalStandFrames && currentAniType == aniTypes.Stand) { aniStage = 0; }
			if (aniStage >= totalWallFrames && currentAniType == aniTypes.Wall) { aniStage = 0; }
			if (aniStage >= 2 && demandStumble) { aniStage = 0; demandStumble = false; }
			if (aniStage >= totalDeadFrames && currentAniType == aniTypes.Dead) { aniStage = 0; playedFirstDead = true; }
			if (aniStage >= 4 && currentAniType == aniTypes.Dead2) { aniStage = 0; }
		}

	}
}
