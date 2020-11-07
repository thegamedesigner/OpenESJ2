using UnityEngine;

public class LegController2Script : MonoBehaviour
{
	//THis badly named class is the Player's Animation Class
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

	public float forceXScale = 2;//This is the x scale the player's puppet is set to when being flipped (2 or -2 for example)

	public GameObject puppet;

	public Material sheet;


	float aniProgress = 0;
	int aniStage = 0;
	bool flipped = false;
	bool demandStumble = false;
	public enum aniTypes { None, Stand, Walk, Jump, Fall, Pound, Dead, Dead2, Wall, Stumble1, Stumble2, Ascend, Hover, Land1, Land2, OffWall, Crouch, AirSword, WallFloat, Petting }
	aniTypes currentAniType = aniTypes.Stand;
	aniTypes oldAniType = aniTypes.None;
	bool oldMoving = false;
	bool oldOnGround = false;
	bool oldDead = false;
	bool playedFirstDead = false;

	bool oldMoving2 = false;
	bool oldOnGround2 = false;
	aniTypes simpleAni = aniTypes.None;
	aniTypes oldSimpleAni = aniTypes.None;


	//local, and not updated freq! That's ok!

	//float plSize = 0.9f;
	//float plHalf = 0.45f;
	//float plHeight = 1.3f;//1.5
	//float plHeightHalf = 0.65f;


	public AnimationScript_Generic animationScript;


	void Start()
	{
	}

	void Update()
	{
		chooseAnimation();
		//animateFrames();

		getSimpleAniType();

		simpleAniFunc();
		//Setup.GC_DebugLog(simpleAni);
		//Setup.GC_DebugLog(fa.deltaTime);

		fa.playerAni = currentAniType;
	}

	void simpleAniFunc()
	{
		int ani = animationScript.currentlyPlayingAni;
		int stumble1 = 0;
		int walk = 1;
		int stand = 2;
		int ascend = 3;
		int fall = 4;
		int dead = 5;
		int wall = 6;
		int hover = 7;
		int stumble2 = 8;
		int land1 = 9;
		int land2 = 10;
		int offWall = 11;
		int crouch = 12;
		int airsword = 13;
		int wallFloating = 14;
		int petting = 15;

		xa.glx = animationScript.forceAnimateThisGO.transform.localScale;
		if (flipped) { xa.glx.x = -forceXScale; }
		if (!flipped) { xa.glx.x = forceXScale; }
		animationScript.forceAnimateThisGO.transform.localScale = xa.glx;

		if (simpleAni == oldSimpleAni && simpleAni == aniTypes.Walk && ani != -1 && ani != walk) { oldSimpleAni = aniTypes.None; }
		if (oldSimpleAni != simpleAni)
		{
			oldSimpleAni = simpleAni;

			//Stumble1 is short, and Stumble2 is the short. But stumble1 leads into stumble2, and stumble2 can be interupted by walking

			//If not playing stand, and should be, play it.
			if (simpleAni == aniTypes.Stand && ani != stumble1 && ani != stumble2 && ani != land1 && ani != land2)//and dont let stand interupt a stumble
			{
				animationScript.playAnimation(stand);
				//Setup.GC_DebugLog("Played Stand Animation");
			}

			//If not playing walk, and should be, play it.
			if (simpleAni == aniTypes.Walk && ani != stumble1 && ani != land1)//and dont let walk interupt a stumble
			{
				animationScript.playAnimation(walk);
				//Setup.GC_DebugLog("Played Walk Animation");
			}

			//If not playing stumble1, and should be, play it.
			if (simpleAni == aniTypes.Stumble1)
			{
				animationScript.playAnimation(stumble1);
				//Setup.GC_DebugLog("Played Stumble1 Animation");
			}

			//If not playing stumble2, and should be, play it.
			if (simpleAni == aniTypes.Stumble2)
			{
				animationScript.playAnimation(stumble2);
				//Setup.GC_DebugLog("Played Stumble2 Animation");
			}

			//If not playing land1, and should be, play it.
			if (simpleAni == aniTypes.Land1)
			{
				animationScript.playAnimation(land1);
				//Setup.GC_DebugLog("Played Land1 Animation");
			}

			//If not playing land2, and should be, play it.
			if (simpleAni == aniTypes.Land2)
			{
				animationScript.playAnimation(land2);
				//Setup.GC_DebugLog("Played Land2 Animation");
			}

			//If not playing ascend, and should be, play it.
			if (simpleAni == aniTypes.Ascend)
			{
				animationScript.playAnimation(ascend);
				// Setup.GC_DebugLog("Played Ascend Animation");
			}

			//If not playing hover, and should be, play it.
			if (simpleAni == aniTypes.Hover)
			{
				animationScript.playAnimation(hover);
				// Setup.GC_DebugLog("Played Hover Animation");
			}

			//If not playing fall, and should be, play it.
			if (simpleAni == aniTypes.Fall)
			{
				animationScript.playAnimation(fall);
				//Setup.GC_DebugLog("Played Fall Animation");
			}

			//If not playing wall, and should be, play it.
			if (simpleAni == aniTypes.Wall)
			{
				animationScript.playAnimation(wall);
				// Setup.GC_DebugLog("Played Wall Animation");
			}

			//If not playing OffWall, and should be, play it.
			if (simpleAni == aniTypes.OffWall)
			{
				animationScript.playAnimation(offWall);
				//Setup.GC_DebugLog("Played OffWall Animation");
			}

			//If not playing Crouch, and should be, play it.
			if (simpleAni == aniTypes.Crouch)
			{
				animationScript.playAnimation(crouch);
			}

			//If not playing AirSword, and should be, play it.
			if (simpleAni == aniTypes.AirSword)
			{
				animationScript.playAnimation(airsword);
			}
			
			//If not playing WallFloat, and should be, play it.
			if (simpleAni == aniTypes.WallFloat)
			{
				animationScript.playAnimation(wallFloating);
			}

			//If not playing Petting, and should be, play it.
			if (simpleAni == aniTypes.Petting)
			{
				animationScript.playAnimation(petting);
			}

			//If not playing dead, and should be, play it.
			if (simpleAni == aniTypes.Dead)
			{
				animationScript.playAnimation(dead);
				//Setup.GC_DebugLog("Played Dead Animation");
			}
		}

		//turn animation frame into a single int
		if (xa.playerScript)
		{
			Vector2 vec1 = animationScript.GetAnimationFrame();
			xa.playerScript.SetAniFrameInt(Setup.AniFrameToInt((int)vec1.x, (int)vec1.y));
		}
	}

	void getSimpleAniType()
	{
		if (!xa.playerStuck)
		{
			if ((xa.playerDir == -1 && !xa.playerMoveRight) || xa.playerMoveLeft) { flipped = true; }
			if ((xa.playerDir == 1 && !xa.playerMoveLeft) || xa.playerMoveRight) { flipped = false; }
		}

		simpleAni = aniTypes.Stand;


		if ((xa.inZoneIce <= 0 && xa.playerMoving) || (xa.inZoneIce > 0 && (xa.playerMoveLeft || xa.playerMoveRight))) { simpleAni = aniTypes.Walk; }

		if (!xa.playerOnAniGround)
		{
			if (xa.playerVel.y < -0.1f)
			{
				simpleAni = aniTypes.Fall;
			}
			else if (xa.playerVel.y > 0.1f)
			{
				simpleAni = aniTypes.Ascend;
			}
			else
			{
				simpleAni = aniTypes.Hover;
			}
		}


		if (xa.playerStuck)
		{
			simpleAni = aniTypes.Wall;//default
			if (xa.playerDir == 1 && xa.playerStuckDir == -1) { simpleAni = aniTypes.WallFloat; }
			if (xa.playerDir == -1 && xa.playerStuckDir == 1) { simpleAni = aniTypes.WallFloat; }
		}

		if (xa.playerCrouching) { simpleAni = aniTypes.Crouch; }
		if (xa.playerAirSwording) { simpleAni = aniTypes.AirSword; }
		if (xa.playerDead) { simpleAni = aniTypes.Dead; }
		if (xa.nearDog) { simpleAni = aniTypes.Petting; }

		if (oldMoving2 != xa.playerMoving)
		{
			oldMoving2 = xa.playerMoving;
			if (!xa.playerMoving && !xa.playerStuck)
			{
				simpleAni = aniTypes.Stumble1;
			}
		}

		if (oldOnGround2 != xa.playerOnAniGround)
		{
			oldOnGround2 = xa.playerOnAniGround;
			if (xa.playerOnAniGround == true)
			{
				simpleAni = aniTypes.Land1;
			}
		}

	//	Debug.Log(simpleAni);
				

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
				setTexture(aniStage, 1);
				// Setup.GC_DebugLog("stumbled");
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
				else if (xa.playerOnAniGround)
				{
					if (currentAniType == aniTypes.Stand)
					{
						setTexture(0, 0);
					}
					if (currentAniType == aniTypes.Walk)
					{
						setTexture((int)(aniStage + 2), 0);
					}
				}
				else
				{
					//jump stuff
					if (xa.playerVel.y < -0.1f)
					{
						setTexture((int)(aniStage + 6), 1);
					}
					else if (xa.playerVel.y > 0.1f)
					{
						setTexture((int)(aniStage + 2), 1);
					}
					else
					{
						setTexture((int)(aniStage + 4), 1);
					}
				}
			}
		}
	}

	void setTexture(int v1, int v2)
	{
		float x1 = 0;
		float y1 = 0;
		float x2 = 0;
		float y2 = 0;

		x1 = 0.125f;
		y1 = 0.125f;
		x2 = 0.125f * v1;
		y2 = 1 - ((0.125f * v2) + 0.125f);

		if ((!flipped && xa.forcePlayerDirection == 0) || xa.forcePlayerDirection == 1)
		{
			puppet.GetComponent<Renderer>().material.mainTextureScale = new Vector2(x1, y1);
			puppet.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(x2, y2);
		}
		else if ((flipped && xa.forcePlayerDirection == 0) || xa.forcePlayerDirection == -1)
		{
			puppet.GetComponent<Renderer>().material.mainTextureScale = new Vector2(-x1, y1);
			puppet.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(x2 + 0.125f, y2);
		}
		//turn animation frame into a single int
		if (xa.playerScript)
		{
			xa.playerScript.SetAniFrameInt(Setup.AniFrameToInt(v1, v2));
		}

	}

	void chooseAnimation()
	{
		//update flipped bool
		//if ((xa.playerDir == -1 && !xa.playerMoveRight) || xa.playerMoveLeft) { flipped = true; }
		//if ((xa.playerDir == 1 && !xa.playerMoveLeft) || xa.playerMoveRight) { flipped = false; }

		currentAniType = aniTypes.Stand;
		if ((xa.inZoneIce <= 0 && xa.playerMoving) || (xa.inZoneIce > 0 && (xa.playerMoveLeft || xa.playerMoveRight))) { currentAniType = aniTypes.Walk; }

		//Sticky floor

		if (!xa.playerOnAniGround) { currentAniType = aniTypes.Fall; }
		if (xa.playerJumped) { currentAniType = aniTypes.Jump; }
		/*if (xa.playerWallSticking == xa.playerWallStickingMax)
        {
            if (xa.playerVel.y <= xa.playerWallStickingMaxFallingVel && xa.playerPoundTimer <= 0)
            {
                if (xa.playerVel.x > -0.04f && xa.playerVel.x < 0.04f)
                {
                    currentAniType = aniTypes.Wall;
                }
            }
        }*/
		if (xa.playerStuck)
		{
			//currentAniType = aniTypes.Wall;
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

		if (oldOnGround != xa.playerOnAniGround)
		{
			oldOnGround = xa.playerOnAniGround;
			if (xa.playerOnAniGround == true && !demandStumble && xa.playerPoundTimer == 0)
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
		if (!xa.playerOnAniGround && demandStumble)
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
