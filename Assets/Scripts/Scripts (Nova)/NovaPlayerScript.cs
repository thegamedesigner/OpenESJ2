using System.Collections.Generic;
using UnityEngine;

public class NovaPlayerScript : MonoBehaviour
{
	public enum State
	{
		NovaPlayer,
		SwordState,
		PunchState,
		End
	}


	public static bool has3rdJumped = false;
	public static bool hasStomped = false;


	public TextMesh debugText;
	public PlayerState_AirSword playerState_AirSword = null;
	public bool ThreeDee = false;
	public GameObject animationController = null;
	public GameObject playerPrefab = null;
	public GameObject deathExplo = null;
	public GameObject poundExplo = null;
	public GameObject airSwordExplo = null;
	public GameObject airSwordDust = null;
	public GameObject tempAbilityVisual = null;
	public GameObject tempAbilityVisualController = null;
	public GameObject bounceExplo = null;
	public GameObject landSounds = null;
	public GameObject wallStickingSound = null;
	public GameObject trail = null;
	public GameObject myHitBox = null;
	public GameObject airswordIconEffect = null;
	public GameObject airJumpExplo = null;
	public GameObject explosiveJumpExplo = null;
	public GameObject jumpSounds = null;
	public GameObject doubleJumpSound = null;
	public GameObject AirJumpStarPrefab = null;
	public GameObject PunchPrefab = null;
	public GameObject AirJumpStarExploPrefab = null;

	[SerializeField]
	bool isMiniPlayer = false;
	public bool hasTripleJump = false;
	public bool hasExplosiveJump = false;
	public bool hasQuintupleJump = false;
	float jumpAmount = 0.5f;
	float heavyJumpAmount = 0.65f;//if you're carrying Mom on your back

	public bool damageFromPound = false;
	public bool createPoundEffect = false;
	public bool startFlipped = false;
	public bool onFreshMovingPlat = false;
	public float movingPlatVelY = 0;
	public bool hasSword = false;
	public bool hasPunch = false;
	public int punches = 0;
	public int maxPunches = 0;
	public HealthScript hpScript = null;
	public State state = State.NovaPlayer;
	[HideInInspector]
	public Vector3 vel = Vector3.zero;
	[HideInInspector]
	public bool poundToggle = false;
	[HideInInspector]
	public float merpsAfterGroundCounter = 0;
	[HideInInspector]
	public float merpsAfterGroundTime = 2;
	[HideInInspector]
	public bool DidAirSwordImpact = false;
	[HideInInspector]
	public float lastStompedOnAFreshStompHitbox = -999;
	[HideInInspector]
	public int playerNumber = 0;//0 = player1, 1 = player2, etc, 

	ExplodingBlockScript explodingBlockScript = null;
	CameraScript cameraScript = null;
	float animationControllerYOffset = 0;
	float orginalGravity = 0.03f;
	float orginalGravityFast = 0.1f;
	float gravity = 0.03f;
	float gravityFast = 0.1f;
	float friction = 0.5f;
	float rainFriction = 0.0078f;
	float groundAccel = 0.05f;
	float iceGroundAccel = 0.005f;
	float airAccel = 0.03f;
	float jetpackAmount = 10f;
	float stompDelay = 0.5f;
	float stompTimeSet = 0;
	bool killInstantly = false;
	float maxVelX = 0.15f;//0.15f;
	float maxIceVelX = 0.4f;
	float maxFallingVelY = 4;
	float maxVerticalVelY = 40;
	float maxVerticalVelYWhileJetpacking = 0.3f;
	float checkpointDist = 2;
	float poundRadius = 1.8f;//1.3f;//2.5f;
	float invincibleTimeAfterPound = 1.15f;//2;//1//4
	float delayAfterHittingBouncePad = 1;
	float bouncePadBounciness = 1.6f;
	float bounceCoinBounciness = 1.1f;
	float maxFallingVelWhileSticking = -0.01f;
	float wallStickingAmount = 1f;
	float plSize = 0.9f;
	float plHalf = 0.45f;
	public float plHeight = 1.3f;//1.5
	float plHeightHalf = 0.65f;      // changing this screws stuff up, because it's from the center, and the player doesn't touch the floor properly. Change if you are willing to tweak footLength and so forth
	float footLength = 0.66f;
	float aniFootLength = 0.9f;       // From an animation / soundeffect pov, this is how far to check for ground
	float itemDist = 1f;         // dist at which the player cares enough to get the script of an item.
	float deathSpeed = 0;
	float poundStartY = 0;
	float tempAbilityLifespan = 0;
	float beforeGroundTime = 1;
	float afterGroundTime = 1;
	float beforeGroundCounter = 0;
	float afterGroundCounter = 0;
	int startingInvinciblity = 0;
	int maxAirJumps = 1;
	int airJumps = 0;
	bool revertFromTempDoubleJump = false;
	bool revertFromTempFlight = false;
	bool revertFromTempCrush = false;
	bool onGround = false;
	bool onAniGround = false;//On ground from an animation pov (but possibly just above the ground from a physics pov)
	bool storedJump = false;
	bool storedJetpack = false;
	bool dontFastGrav = false;
	bool oldOnGround = false;
	bool moveLeft = false;
	bool moveRight = false;
	bool dead = false;
	bool oldDead = false;
	bool hasTempAbility = false;
	bool fadedTempAbility = false;
	bool storedPossibleJump = false;
	bool dontDieZone = false;
	bool testingJump = false;
	bool createdStickySound = false;
	int stickyDir = 0;//0 is none, -1 is -X, 1 is +X
	int defaultStickDir = 0;//set on impact with a stickywall
	bool stuck = false;
	bool unstuckable = false;//if true, can't stick. Set by moveDown key
	float stuckTimestamp = 0;
	bool nextToStickyWall = false;//true if next to a sticky wall, reguardless of actually being stuck
	bool merpRulesOn = false;
	bool fallingAgainstSomethingSolid = false;
	int framesStuck = 0;
	int aniFrameAsInt = -1;
	float tinyJumpTimer = 0;
	bool tinyJumped = false;
	float AirSwordDelay = 0.2f;//0.2f;
	float LastAirSwordTimeSet = 0.0f;
	int airSwords = 0;
	int maxAirSwords = 2;
	GameObject mom = null;
	FreshAni momsAnimationScript = null;
	MomScript momScript = null;
	bool momExists = false;
	bool carryingMom = false;
	float doubleJumpKillRadius = 3;//2.1f;//1.6f;
	List<Info> airJumpStars = new List<Info>();
	Vector3[] breadCrumbs = new Vector3[200];
	//float superJumpAmount                                       = 0.7f;
	//bool wasInIceZone                                           = false;
	bool tooLateToHop = false;
	public ParticleSystem altTrailPS_Normal;
	public ParticleSystem altTrailPS_Airsword;
	float fartDelay = 0.5f;
	float fartTimeset = 0;

	public void SetAirJumps(int j)
	{
		airJumps = j;
	}
	public void SetMaxAirJumps(int j)
	{
		maxAirJumps = j;
	}

	public void ConfigureTrail(float length, Color colour)
	{
		TrailRenderer trailRenderer = this.trail.GetComponent<TrailRenderer>();
		if (trailRenderer)
		{
			//trailRenderer.minVertexDistance = 0.001f;
			trailRenderer.material.color = colour;
			trailRenderer.time = length;
			trailRenderer.enabled = false;//length > float.Epsilon;
		}
	}

	void Start()
	{
		if (airswordIconEffect != null)
		{
			airswordIconEffect.transform.SetScaleY(0);
		}
		if (ThreeDee)
		{
			transform.SetZ(30);
		}
		NovaPlayerStart();
		//trail.GetComponent<TrailRenderer>().material.renderQueue = 4000;
	}

	void Update()
	{





		if (xa.fresh_localNode != null && xa.fresh_localNode.ThisLevelHasMom)
		{
			if (!momExists)
			{
				FindMom();
			}
		}
		if (EditorController.IsEditorActive())
		{
			return;
		}

		if (animationController != null)
		{
			animationController.transform.LocalSetY(animationControllerYOffset);
		}
		if (fa.paused)
		{
			return;
		}
		switch (state)
		{
			case State.NovaPlayer:
				NovaPlayerUpdate();
				break;
			case State.SwordState:
				playerState_AirSword.SwordUpdate();
				break;
			case State.PunchState:
				playerState_AirSword.PunchUpdate();
				break;
		}

		//FreshBoostPlatformScript.HandleFreshMovingPlatforms(this.gameObject);

		xa.playerPos = transform.position;


		if (ThrowingBallScript.ReadOnly_DaddysLove == 0) { hpScript.health = 0; killInstantly = true; }

	}

	void FixedUpdate()
	{
		if (fa.paused)
		{
			return;
		}
		switch (state)
		{
			case State.NovaPlayer:
				NovaPlayerFixedUpdate();
				break;
		}

		if (fa.useGhosts)
		{
			if (!Ghosts.recording)
			{
				Ghosts.StartRecording();
			}
			Ghosts.UpdateRecording();
		}
	}


	/// <summary>
	/// NOVA PLAYER STUFF
	/// </summary>

	void NovaPlayerStart()
	{
		if (za.useSuperForgivingJump)
		{
			afterGroundTime = 3;
			beforeGroundTime = 3;
		}

		applyESJ4SqJumpRules();

		breadCrumbs = new Vector3[100];
		airJumpStars = new List<Info>();
		for (int i = 0; i < 5; i++)
		{
			GameObject go = Instantiate(AirJumpStarPrefab, new Vector3(-100, -100, 22), AirJumpStarPrefab.transform.rotation);
			Info infoScript = go.GetComponent<Info>();
			airJumpStars.Add(infoScript);
		}
		if (PunchPrefab != null)
		{
			for (int i = 5; i < 10; i++)
			{
				GameObject go = Instantiate(PunchPrefab, new Vector3(-100, -100, 22), PunchPrefab.transform.rotation);
				Info infoScript = go.GetComponent<Info>();
				airJumpStars.Add(infoScript);
			}
		}

		xa.playerHitBox = myHitBox;
		xa.freezePlayer = false;
		xa.playerDead = false;
		xa.playerMoving = false;
		xa.playerJumped = false;
		xa.playerJumpedFresh = false;
		xa.playerPoundTimer = 0;
		xa.playerVel = new Vector2(0, 0);
		xa.playerDir = 1;
		xa.inZoneIce = 0;
		//wasInIceZone                     = false;
		xa.playerBoxWidth = plSize;
		xa.playerBoxHeight = plHeight;
		cameraScript = Camera.main.GetComponent<Camera>().GetComponent<CameraScript>();
		airJumps = maxAirJumps;
		punches = maxPunches;
		airSwords = maxAirSwords;
		hpScript = this.gameObject.GetComponent<HealthScript>();
		xa.player = this.gameObject;
		xa.playerScript = this;//This makes the playerScript not function and the NovaPlayerScript.cs function

		xa.lastSpawnPoint = transform.position;
		if (startFlipped) { xa.playerDir = -1; }
		xa.glx = Camera.main.GetComponent<Camera>().transform.position;
		if (cameraScript)
		{
			if (cameraScript.centerCameraOnPlayerOnSpawn)
			{
				xa.glx.x = transform.position.x;
			}
			if (cameraScript.centerCameraOnPlayerYOnSpawn)
			{
				xa.glx.y = transform.position.y;
			}
		}

		if (xa.hasCheckpointed)
		{
			SaveAbilitiesNodeScript.GetSavedAbilities(); //Doesn't do anything unless you put SaveAbilitiesNodeScript.cs on a gameobject in the level. (if you do, it saves which abilities the player has gained, and doesn't let them lose them after respawning)
		}
		else
		{
			has3rdJumped = false;
			hasStomped = false;
			SaveAbilitiesNodeScript.WipeSavedAbilities();//The player has just respawned and xa.hasCheckpointed is false. This might mean they've restarted_from_start/menu, which means wipe all abilities
		}

		if (hasTripleJump) { maxAirJumps = 2; }
		if (hasQuintupleJump) { maxAirJumps = 4; }
		if (hasPunch) { maxPunches = 1; }
		airJumps = maxAirJumps;
		punches = maxPunches;

		Camera.main.GetComponent<Camera>().transform.position = xa.glx;
	}

	//int oldAirStars = 0;
	//bool forceChangeOnLastOne = false;

	void UpdateBreadCrumbs()
	{
		int spacing = 15;
		int currentCount = spacing;
		int starIndex = 0;
		int localAirJumps = 0;
		Vector3 off = new Vector3(-9999, -9999, 22);

		if ((xa.playerHasDoubleJump || hasQuintupleJump || hasTripleJump)) { localAirJumps = airJumps; }
		else { localAirJumps = 0; }

		//make all airstars invisible
		int airstars = 0;
		for (int i = 0; i < airJumpStars.Count; i++)
		{
			if (airJumpStars[i].triggered) { airstars++; }
			airJumpStars[i].triggered = false;
			airJumpStars[i].transform.position = off;
		}

		//for each airjump, place one airstar
		for (int j = localAirJumps; j > 0; j--)
		{
			Vector3 v = breadCrumbs[currentCount];
			airJumpStars[starIndex].transform.position = v;
			airJumpStars[starIndex].triggered = true;
			//if (!airJumpStars[starIndex].triggered) { airJumpStars[starIndex].triggered = true; }
			currentCount += spacing;
			starIndex++;
		}

		if (airstars > localAirJumps)
		{
			//create a explo
			Vector3 v = breadCrumbs[currentCount];
			GameObject go = Instantiate(AirJumpStarExploPrefab, v, AirJumpStarPrefab.transform.localRotation);
		}

		//for each airsword, place one airsword
		starIndex = 5;
		for (int h = punches; h > 0; h--)
		{
			if (starIndex >= airJumpStars.Count) { break; }
			Vector3 v = breadCrumbs[currentCount];
			airJumpStars[starIndex].transform.position = v;
			currentCount += spacing;
			starIndex++;
		}



		//find first empty spot
		for (int i = breadCrumbs.Length - 1; i > 0; i--)
		{
			breadCrumbs[i] = breadCrumbs[i - 1];
		}

		breadCrumbs[0] = transform.position;


		for (int i = 0; i < breadCrumbs.Length - 1; i++)
		{
			//Debug.DrawLine(breadCrumbs[i], breadCrumbs[i + 1], Color.red);
		}


	}

	public void CheckHurtZones()
	{
		if (HurtZoneScript.HZs == null) { return; }
		for (int i = 0; i < HurtZoneScript.HZs.Count; i++)
		{
			Vector3 v1 = new Vector3(HurtZoneScript.HZs[i].pos.x, HurtZoneScript.HZs[i].pos.y, 33);
			Vector3 v2 = new Vector3(transform.position.x, transform.position.y, 33);
			//Debug.DrawLine(v1, v2, Color.green);
		}
	}

	void NovaPlayerUpdate()
	{

		//Debug.Log("NextToStickyWall: " + nextToStickyWall);
		CheckHurtZones();
		HandleAirSwordIcon();

		//breadcrumbs
		UpdateBreadCrumbs();

		UpdateMom();

		maxAirJumps = 1;
		if (hasTripleJump) { maxAirJumps = 2; }
		if (hasQuintupleJump) { maxAirJumps = 4; }
		if (hasPunch) { maxPunches = 1; }

		gravity = orginalGravity * xa.localNodeScript.gravityMultiplier;
		gravityFast = orginalGravityFast * xa.localNodeScript.gravityMultiplier;

		if (onGround)
		{
			xa.hasBeenOnGroundSincePress = true;
		}
		else
		{
			if (xa.hasBeenOnGroundSincePress)
				xa.hasBeenOffGroundSincePress = true;
		}

		if (hpScript && xa.cheat_invinciblePlayer)
		{
			hpScript.invincibleTimer = 100;
		}
		if (hpScript && !xa.cheat_invinciblePlayer && hpScript.invincibleTimer > 95)
		{
			hpScript.invincibleTimer = 100;
		}

		if (startingInvinciblity < 10)
		{
			++startingInvinciblity;
		}

		this.tempAbilityVisualController.SetActive(hasTempAbility);
		if (hasTempAbility)
		{
			tempAbilityLifespan -= 10 * fa.deltaTime;

			xa.glx = tempAbilityVisualController.transform.localScale; // *((tempAbilityLifespan / 100) + 0.5f);
																	   //xa.glx.x = (tempAbilityLifespan / 100) + 0.5f;
																	   //xa.glx.y = (tempAbilityLifespan / 100) + 0.5f;
			xa.glx.x = xa.glx.y = (tempAbilityLifespan / 100) + 0.5f;
			tempAbilityVisualController.transform.localScale = xa.glx;

			if (!fadedTempAbility)
			{
				fadedTempAbility = true;
				iTweenEvent.GetEvent(tempAbilityVisual, "fadeIn").Play();
			}
			if (tempAbilityLifespan <= 0)
			{
				hasTempAbility = false;
				if (revertFromTempCrush) { xa.playerHasGroundPound = false; }
				if (revertFromTempDoubleJump) { xa.playerHasDoubleJump = false; }
				if (revertFromTempFlight) { xa.playerHasJetpack = false; }
			}
		}
		else
		{
			if (fadedTempAbility)
			{
				fadedTempAbility = false;
				iTweenEvent.GetEvent(tempAbilityVisual, "fadeOut").Play();
			}
		}

		if (merpsAfterGroundCounter > 0) { merpsAfterGroundCounter -= 10 * fa.deltaTime; }

		xa.glx = transform.position;
		xa.glx.y += afterGroundCounter;
		//Debug.DrawLine(transform.position, xa.glx, Color.yellow);
		xa.glx = transform.position;
		xa.glx.x += afterGroundCounter;
		// Debug.DrawLine(transform.position, xa.glx, Color.cyan);

		//Setup.GC_DebugLog(afterGroundCounter);

		if (!xa.freezePlayer)
		{

			if (Controls.GetInput(Controls.Type.Ability1, 0))
			{

				if (!xa.playerHasGroundPound &&
				   !xa.playerHasDoubleJump &&
				   !xa.playerHasJetpack &&
				   !xa.playerHasSuperJump &&
				   !hasQuintupleJump &&
					   !hasSword &&
					   !hasTripleJump &&
					   !hasExplosiveJump &&
					   !hasPunch &&
					   Time.timeSinceLevelLoad > 1 &&
					   hpScript.health > 0)
				{
					//Debug.Log("FARTED! Stomp:" + xa.playerHasGroundPound + ", 2xJump:" + xa.playerHasDoubleJump + ", 5xJump:" + hasQuintupleJump
					//	+ ", Sword:" + hasSword + ", 3xJump:" + hasTripleJump + ", Explo:" + hasExplosiveJump + ", Punch:" + hasPunch);
					if (fa.time >= (fartTimeset + fartDelay))
					{
						xa.fakeRandom += 3;
						fartTimeset = fa.time;
						Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.Multifart);
						GameObject go = Instantiate(xa.de.fartParticles, transform.position, xa.de.fartParticles.transform.rotation);
					}
				}
			}



			if (hpScript)
			{
				if (hpScript.invincibleTimer > 0)
				{
					hpScript.invincibleTimer -= 10 * fa.deltaTime;
				}
			}

			if (hpScript.health <= 0)
			{
				if (!dead)
				{
					za.deaths++;
					dead = true;
				}
			}
			if (!dead)
			{
				if (!dontDieZone && startingInvinciblity >= 10 && !checkPlayerDeathBox(transform.position) && !ThreeDee)//Setup.checkVecOnScreen(transform.position + new Vector3(0, -1, 0), true))
				{
					hpScript.health = 0;
				}
			}
			xa.playerDead = dead;

			if (oldDead != dead)
			{
				oldDead = dead;
				xa.totalLevelTime += fa.time;
				if (dead)
				{
					xa.playerHitBox = null;
					// if (transform.position.y < xa.bottomEdgeOfScreen)
					//{
					xa.glx = transform.position;
					xa.glx.z = xa.GetLayer(xa.layers.Explo1);
					//xa.glx.y = xa.topEdgeOfScreen - 4;//since it's normally 14 and I want this to be at 10

					if (!merpRulesOn)
					{
						int index = 0;
						while (index < 999)
						{
							//vec1 = Camera.main.camera.transform.position;
							xa.glx.y += 0.1f;
							if (!Setup.checkVecOnScreen(xa.glx, true))
							//if (!Camera.main.camera.pixelRect.Contains(Camera.main.camera.WorldToScreenPoint(xa.glx)))
							{ break; }
							index++;
						}
					}
					xa.deathCountThisLevel++;

					//Setup.GC_DebugLog("died, updating deaths to " + xa.deathCountThisLevel + " and dropping stars");


					xa.carryingStars = 0;
					if (!merpRulesOn)
					{
						xa.tempobj = (GameObject)(Instantiate(deathExplo, xa.glx, xa.null_quat));
						if (xa.createdObjects)
						{
							xa.tempobj.transform.parent = xa.createdObjects.transform;
						}
					}
				}
			}

			if (!dead)
			{
				if (xa.allowPlayerInput)
					triggerPresses();

				if (xa.playerPoundTimer > 0) { xa.playerPoundTimer -= 10 * fa.deltaTime; }
				else { xa.playerPoundTimer = 0; }
				//checkForCheckpoints();
				checkForItems(itemDist, transform.position);
			}
			else
			{
				if (xa.allowPlayerInput)
				{
					trail.transform.SetParent(null);
					xa.glx = transform.position;
					xa.glx.z = xa.GetLayer(xa.layers.PlayerDeath);
					//do death stuff
					deathSpeed += 14 * fa.deltaTime;
					xa.glx.y += deathSpeed * fa.deltaTime;
					transform.position = xa.glx;
					if (xa.glx.y > (xa.topEdgeOfScreen + 2) && !xa.fadingOut && !xa.fadingIn || merpRulesOn)
					{
						if (!za.dontRespawn)
						{
							xa.re.cleanLoadLevel(Restart.RestartFrom.RESTART_FROM_CHECKPOINT, "");
						}
					}
					if (Controls.GetAnyKeyDown() || killInstantly)
					{
						killInstantly = false;
						if (!za.dontRespawn)
						{
							xa.re.cleanLoadLevel(Restart.RestartFrom.RESTART_FROM_CHECKPOINT, "");
						}
					}
				}
			}
		}

		if (DidAirSwordImpact)
		{
			DidAirSwordImpact = false;//airSwordExplo
									  //checkPoundBox();

			Vector3 pos = transform.position;
			pos.z = xa.GetLayer(xa.layers.Explo1);
			Vector3 dir = new Vector3(0, 0, 0);
			if (xa.playerDir == 1) { dir = new Vector3(0, -90, 90); }
			if (xa.playerDir == -1) { dir = new Vector3(-180, -90, 90); }

			GameObject tempGo = Instantiate(airSwordDust, pos, Quaternion.Euler(dir));
			tempGo.transform.SetParent(xa.createdObjects.transform);
		}


		FreshBoostPlatformScript.HandleFreshMovingPlatforms();
		FreshBoostPlatformScript.CheckIfOnMovingPlat();


		FreshBoostPlatformScript.StickMeToMovingPlatform();

	}

	void NovaPlayerFixedUpdate()
	{
		if (!xa.freezePlayer)
		{
			if (!xa.se || this.dead)
			{
				return; // OOPS NO SE THIS WILL ONLY HAPPEN ONCE OR TWICE UPON STARTUP
			}
			movePlayer();

			if (afterGroundCounter > 0) { afterGroundCounter -= 10 * 0.0166f; }

			if (beforeGroundCounter > 0)
			{
				beforeGroundCounter -= 10 * 0.0166f;
				if (beforeGroundCounter <= 0)
				{
					storedPossibleJump = false;
				}
			}
			xa.playerOnGround = false;
			if (onGround) { xa.playerOnGround = true; }

			//xa.playerMoving = false;
			//if (Mathf.Abs(vel.x) > 0.05f) { xa.playerMoving = true; }

			//check zones (new)
			//handleZones();


			//resolve
			resolveVel();


			//Setup.GC_DebugLog("velY: " + vel.y + " Falling against something solid: " + fallingAgainstSomethingSolid);

			//fix for player getting stuck on the corner of blocks, still in 'falling animation'
			if (vel.y == 0 && !onGround && fallingAgainstSomethingSolid && !xa.playerHasJetpack)
			{
				framesStuck++;
				if (framesStuck >= 2)
				{
					//Setup.GC_DebugLog("THIS CASE " + fa.time);
					vel.x += 0.04f;//push off or on the edge. Either is better then being stuck.
				}
			}
			else
			{
				framesStuck = 0;
			}


			xa.playerStuck = stuck;
			//xa.playerWallSticking = wallSticking;

			xa.playerVel.x = vel.x;
			xa.playerVel.y = vel.y;

			handleCameraInFixedUpdate();
		}
	}

	void FindMom()
	{
		mom = GameObject.FindGameObjectWithTag("Mom");
		if (mom != null)
		{
			momsAnimationScript = mom.GetComponent<FreshAni>();
			momScript = mom.GetComponent<MomScript>();
			momExists = true;
		}
	}

	void UpdateMom()
	{
		if (momExists)
		{
			if (mom.transform.position.y < -25) { hpScript.health = 0; killInstantly = true; xa.faderOutScript.triggerFailureMsg = true; }
			if (carryingMom)
			{
				mom.transform.position = transform.position;
				mom.transform.AddY(0.45f);
				mom.transform.AddX(-0.2f * xa.playerDir);
				mom.transform.SetScaleX(xa.playerDir);

				if (Controls.GetInput(Controls.Type.Ability1, playerNumber))
				{
					carryingMom = false;
					momScript.carried = false;
					momScript.ThrowMom();


					plSize = 0.9f;
					plHalf = 0.45f;
					plHeight = 1.3f;//1.5
					plHeightHalf = 0.65f;//0.75 - changing this screws stuff up, because it's from the center, and the player doesn't touch the floor properly. Change if you are willing to tweak footLength and so forth
					footLength = 0.66f;//half height + 1
					aniFootLength = 0.9f;//From an animation / soundeffect pov, this is how far to check for ground
					animationControllerYOffset = 0;

				}
			}
			else
			{
				if (Vector2.Distance(mom.transform.position, transform.position) < 1)
				{
					if (!momScript.flying)
					{
						bool clear = true;

						//check for being in the roof
						float raycastLayer = xa.GetLayer(xa.layers.RaycastLayer);
						Ray ray = new Ray();
						RaycastHit hit;
						LayerMask mask = 1 << 19;
						ray.direction = new Vector3(0, 1, 0);
						ray.origin = new Vector3(transform.position.x, transform.position.y, raycastLayer);
						if (Physics.Raycast(ray, out hit, 2, mask))
						{
							clear = false;
						}

						if (clear)
						{
							momsAnimationScript.PlayAnimation(2);
							carryingMom = true;
							momScript.carried = true;

							plSize = 0.9f;
							plHalf = 0.45f;

							plHeight = 2.2f;//1.5
							plHeightHalf = 1.1f;//0.75 - changing this screws stuff up, because it's from the center, and the player doesn't touch the floor properly. Change if you are willing to tweak footLength and so forth
							footLength = 1.11f;//half height + 1
							aniFootLength = 1.35f;//From an animation / soundeffect pov, this is how far to check for ground
							animationControllerYOffset = -0.45f;

							transform.AddY(0.45f);
						}
					}

				}
			}

		}
	}

	void handleCameraInFixedUpdate()
	{
		if (cameraScript)
		{
			if (cameraScript.cameraFollowsPlayer)
			{
				xa.glx = Camera.main.GetComponent<Camera>().transform.position;
				xa.glx.x = transform.position.x;
				Camera.main.GetComponent<Camera>().transform.position = xa.glx;
			}
			if (cameraScript.cameraFollowsPlayerY)
			{
				xa.glx = Camera.main.GetComponent<Camera>().transform.position;
				xa.glx.y = transform.position.y;
				Camera.main.GetComponent<Camera>().transform.position = xa.glx;
			}
			if (cameraScript.cameraFollowsPlayerYUpwardsOnly)
			{
				xa.glx = Camera.main.GetComponent<Camera>().transform.position;
				if (transform.position.y > xa.glx.y) { xa.glx.y = transform.position.y; }
				// xa.glx.y = transform.position.y;
				Camera.main.GetComponent<Camera>().transform.position = xa.glx;
			}
		}
	}
	void handleZones()//Many old zones aren't dealt with here (and should be moved here & to the new system)
	{
		//float iceSpeed = 0.05f;
		// if (xa.inZoneIce > 0 && xa.playerOnGround)
		// {
		//if (vel.x > -iceSpeed && vel.x < iceSpeed)
		//{
		//	if (vel.x < 0) { vel.x = -iceSpeed; }
		//	if (vel.x > 0) { vel.x = iceSpeed; }
		//}
		//}

	}

	bool checkForZone(Vector3 position, string zoneName)
	{
		GameObject[] gos = null;
		if (zoneName == "windZone")
		{
			gos = xa.levelWindZones;
		}
		else if (zoneName == "reverseWindZone")
		{
			gos = xa.levelRevWindZones;
		}
		else if (zoneName == "dontDieZone")
		{
			gos = xa.levelDontDieZones;
		}

		foreach (GameObject go in gos)
		{
			if (go == null) continue;
			if (position.x > (go.transform.position.x - (go.transform.localScale.x * 0.5)) &&
				position.x < (go.transform.position.x + (go.transform.localScale.x * 0.5)) &&
				position.y > (go.transform.position.y - (go.transform.localScale.y * 0.5)) &&
				position.y < (go.transform.position.y + (go.transform.localScale.y * 0.5)))
			{
				return true;
			}
		}
		return false;
	}

	void HurtMonsters(float dist, bool fromStomp)
	{
		if (GenericMonsterScript.monsters != null)
		{
			for (int i = 0; i < GenericMonsterScript.monsters.Count; i++)
			{
				if (GenericMonsterScript.monsters[i] == null) { continue; }
				if (GenericMonsterScript.monsters[i].spiky) { continue; }
				if (GenericMonsterScript.monsters[i].dead) { continue; }
				Vector3 v1 = GenericMonsterScript.monsters[i].gameObject.transform.position;
				Vector3 v2 = transform.position;
				v1.z = v2.z;//make it flat
				if (Vector3.Distance(v1, v2) < dist)
				{
					GenericMonsterScript.monsters[i].hpScript.health = 0;
				}
			}
		}
	}

	void HurtMonstersInBox()
	{
		float boxHalfSize = 1.6f;//1;
		if (GenericMonsterScript.monsters != null)
		{
			for (int i = 0; i < GenericMonsterScript.monsters.Count; i++)
			{
				if (GenericMonsterScript.monsters[i] == null) { continue; }
				if (GenericMonsterScript.monsters[i].spiky) { continue; }
				if (GenericMonsterScript.monsters[i].dead) { continue; }
				Vector3 v1 = GenericMonsterScript.monsters[i].gameObject.transform.position;
				if (v1.y < poundStartY && v1.y >= transform.position.y && v1.x > (transform.position.x - boxHalfSize) && v1.x < (transform.position.x + boxHalfSize))
				{
					GenericMonsterScript.monsters[i].hpScript.health = 0;
				}
			}
		}


	}


	void checkForItems(float dist, Vector3 pos)
	{
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("item");


		foreach (GameObject go in gos)
		{
			xa.glx = go.transform.position;
			xa.glx.z = pos.z;
			if (Vector3.Distance(xa.glx, pos) < 10)//just check theyre not, like, on the other side of the map
			{
				float tempDist = dist;
				ItemScript itemScript = go.GetComponent<ItemScript>();
				if (itemScript != null) { if (itemScript.itemActivationRadiusOverride != -1) { tempDist = itemScript.itemActivationRadiusOverride; } }

				if (Vector3.Distance(xa.glx, pos) < tempDist)
				{
					useFoundItem(go);
				}
			}
		}
	}

	void useFoundItem(GameObject go)
	{
		ItemScript itemScript;
		itemScript = go.GetComponent<ItemScript>();
		if (itemScript)
		{
			itemScript.pickUpItem();

			if (itemScript.type == "bounceCoin")
			{
				//do unique bounceCoin things
				if (itemScript.usedUp == false)
				{
					itemScript.usedUp = true;

					//if (damageFromPound) // STOMPED ON
					//{
					//damageFromPound = false;
					//xa.glx = transform.position;
					//xa.glx.x = go.transform.position.x;
					//xa.glx.y = go.transform.position.y;
					//transform.position = xa.glx;
					//HurtMonsters(poundRadius);
					//checkPoundBox();
					//createPoundEffect = true;

					//forceJumpPlayer(3);
					//}
					//else
					//{
					//damageFromPound = false;
					//forceJumpPlayer(1);
					//}
					if (fa.time < (lastStompedOnAFreshStompHitbox + 0.1f))//Stomped on a stomphitbox in the last 0.3 seconds, which probably means you stomped on this item
					{
						forceJumpPlayer(ForcedJumpType.BounceCoinBoosted);
					}
					else
					{
						forceJumpPlayer(ForcedJumpType.BounceCoinNormal);
					}

					dontFastGrav = true;
					airJumps = maxAirJumps;
					punches = maxPunches;
					airSwords = maxAirSwords;
				}
			}
			else if (itemScript.type == "bouncePad")
			{
				//same as bounce coin
				if (itemScript.usedUp == false)
				{
					itemScript.usedUp = true;

					BouncePadScript script;
					script = itemScript.gameObject.GetComponent<BouncePadScript>();
					script.triggered = 1;
					/*
					if (damageFromPound) // STOMPED ON
					{
						damageFromPound = false;
						xa.glx = transform.position;
						xa.glx.x = go.transform.position.x;
						xa.glx.y = go.transform.position.y;
						transform.position = xa.glx;
						createPoundEffect = true;

						forceJumpPlayer(2);
					}
					else
					{*/
					//forceJumpPlayer(2);
					//}

					if (transform.position.y < itemScript.gameObject.transform.position.y)
					{
						transform.SetY(itemScript.gameObject.transform.position.y);
					}

					if (fa.time < (lastStompedOnAFreshStompHitbox + 0.1f))//Stomped on a stomphitbox in the last 0.3 seconds, which probably means you stomped on this item
					{
						if (itemScript.useToHurtBoss && HurtBoss.self != null)
						{
							HurtBoss.self.HurtBossFunc(7);//deal 3 damage to megasatan
						}
						forceJumpPlayer(ForcedJumpType.BouncePadBoosted);
					}
					else
					{
						forceJumpPlayer(ForcedJumpType.BouncePadNormal);
					}
					dontFastGrav = true;
					airJumps = maxAirJumps;
					punches = maxPunches;
					airSwords = maxAirSwords;
				}
			}
			else if (itemScript.type == "gainDoubleJumpCoin")
			{
				//do unique things
				xa.playerHasDoubleJump = true;
				revertFromTempDoubleJump = true;
				hasTempAbility = true;
				tempAbilityLifespan = 100;//always 100, set speed instead.

				//revertTo = 0;
			}
			else if (itemScript.type == "gainFlightCoin")
			{
				//do unique things
				xa.playerHasJetpack = true;
				revertFromTempFlight = true;
				hasTempAbility = true;
				tempAbilityLifespan = 20;//always 100, set speed instead.

				//revertTo = 0;
			}
			else if (itemScript.type == "coin")
			{
				//do unique coin things
			}
			else if (itemScript.type == "star")
			{
				//do unique star things
			}
			else if (itemScript.type == "key")
			{
				//do unique key things
			}
		}
	}

	void checkForCheckpoints()
	{
		if (xa.fadingOut)
			return;

		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("checkpoint");

		foreach (GameObject go in gos)
		{
			xa.glx = go.transform.position;
			xa.glx.z = transform.position.z;
			if (Vector3.Distance(xa.glx, transform.position) < checkpointDist)
			{
				CheckpointScript script;
				script = go.GetComponent<CheckpointScript>();
				script.triggered = true;
			}

		}

	}

	void triggerPresses()
	{
		xa.jumpButtonHeld = false;
		if (Controls.GetInput(Controls.Type.Jump, playerNumber))
		{
			xa.jumpButtonHeld = true;
			if (!tooLateToHop)
			{
				//Might be this becoming false?
				tinyJumpTimer++;
			}
			if (tinyJumpTimer >= 16)
			{
				tooLateToHop = true;
			}
		}
		else
		{
			tooLateToHop = false;//reset if the jump key is released
		}
		if (Controls.GetInputDown(Controls.Type.Jump, playerNumber) && !xa.playerHasJetpack)
		{
			//Fresh_InGameMenus.self.DebugDisplay.text += "\n" + Time.time + ", Jump key pressed down";

			xa.fakeRandom += 1;
			//afterGroundCounter = 0;
			if (((onGround || afterGroundCounter > 0) || stuck || nextToStickyWall || ((xa.playerHasDoubleJump || hasQuintupleJump || hasTripleJump) && !onGround)))
			{
				//afterGroundCounter = 0;
				storedJump = true;
			}
			else
			{
				//pressed the jump key but wasn't on the ground
				//store a possible jump
				storedPossibleJump = true;
				beforeGroundCounter = beforeGroundTime;
			}
		}


		if (Controls.GetInput(Controls.Type.Jump, playerNumber) && xa.playerHasJetpack)
		{
			storedJetpack = true;
		}

		if (!onGround && xa.playerHasGroundPound && Controls.GetInputDown(Controls.Type.Ability1, playerNumber))
		{
			hasStomped = true;

			//beforeGroundCounter = 0;
			xa.fakeRandom += 1;
			//poundToggle = true;

			if (fa.time + 0.1f >= (stompTimeSet + stompDelay))//Can queue up a stomp 0.1 seconds before can actually do it.
			{
				poundToggle = true;
			}
		}

		if (!onGround && hasSword && Controls.GetInputDown(Controls.Type.Ability1, playerNumber))
		{
			//if (!usedAirSword)//fa.time > (LastAirSwordTimeSet + AirSwordDelay))
			//if(airSwords > 0)
			if (fa.time > (LastAirSwordTimeSet + AirSwordDelay))
			{
				airSwords--;
				state = State.SwordState;
				LastAirSwordTimeSet = fa.time;
				if (altTrailPS_Normal != null)
				{
					altTrailPS_Normal.Stop();
					altTrailPS_Airsword.Play();
				}

				airswordIconEffect.transform.SetScaleY(12);
				iTween.ScaleTo(airswordIconEffect, iTween.Hash("y", 0, "time", AirSwordDelay, "easetype", iTween.EaseType.easeInSine));
			}

		}

		if (!onGround && hasPunch && Controls.GetInputDown(Controls.Type.Ability1, playerNumber))
		{
			if (punches > 0)
			{
				punches--;
				state = State.PunchState;

				airswordIconEffect.transform.SetScaleY(12);
				iTween.ScaleTo(airswordIconEffect, iTween.Hash("y", 0, "time", AirSwordDelay, "easetype", iTween.EaseType.easeInSine));
			}

		}

	}

	public void ExternalPossibleJump()//used by airsword state
	{
		storedJump = true;
		//storedPossibleJump = true;
		//beforeGroundCounter = beforeGroundTime;
	}

	void HandleAirSwordIcon()
	{
	}

	void handleInput()
	{
		if (EditorController.IsEditorActive())
		{
			//return;
		}

		moveLeft = false;
		moveRight = false;
		//if (Input.GetKey(KeyCode.LeftArrow))
		xa.playerControlsHeldDir = xa.playerDir;//default to whatever direction the player is facing, but override below with the key direction being held
		if (Controls.GetInput(Controls.Type.MoveLeft, playerNumber))
		{ moveLeft = true; xa.playerControlsHeldDir = -1; }
		//if (Input.GetKey(KeyCode.RightArrow))
		if (Controls.GetInput(Controls.Type.MoveRight, playerNumber))
		{ moveRight = true; xa.playerControlsHeldDir = 1; }
		if (xa.button1Pressed) { moveLeft = true; }
		if (xa.button2Pressed) { moveRight = true; }


		xa.playerMoving = false;
		if (moveLeft || moveRight)
		{
			xa.fakeRandom += 1;
			xa.playerMoving = true;
			xa.playerHasMoved = true;
		}
		xa.playerMoveLeft = moveLeft;
		xa.playerMoveRight = moveRight;
	}

	void movePlayer()
	{
		float raycastLayer = xa.GetLayer(xa.layers.RaycastLayer);
		//update input bool's from current keys pressed.
		if (!xa.freezePlayerForCutscene)
		{
			handleInput();

			//jump
			jumpFunc();

			//jetpack
			jetpackFunc();

			//side movement
			moveLeftOrRight();

		}
		else
		{
			moveLeft = false;
			moveRight = false;
			xa.playerMoving = false;
			xa.playerHasMoved = false;
			xa.playerMoving = false;
			vel.x = 0;
			xa.playerMoveLeft = moveLeft;
			xa.playerMoveRight = moveRight;
		}

		//find ground
		//findGround();
		//FindGround();

		//add gravity
		//(Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Z) || xa.button3Tapped)

		if (!xa.jumpButtonHeld && tinyJumpTimer < 16 && tinyJumpTimer > 0 && !tinyJumped)
		{
			//tex
			tinyJumped = true;
			vel.y = 0.2f;//both sets & naturally caps the total vel, leading to a tiny jump if you only just tap the button
		}


		//uncomment for variable height jumping
		float localJumpAmount = jumpAmount;
		if (carryingMom) { localJumpAmount = heavyJumpAmount; }

		if (vel.y > localJumpAmount / 3 && !xa.jumpButtonHeld && !dontFastGrav)
		{
			vel.y -= gravityFast;

		}
		else
		{
			vel.y -= gravity;
		}

		//cap falling speed
		if (vel.y < xa.localNodeScript.maxPlayerFallSpeed && xa.localNodeScript.maxPlayerFallSpeed != 0.0f)
		{
			vel.y = xa.localNodeScript.maxPlayerFallSpeed;
		}

		//cap for wall sticking
		if (stuck)
		{
			if (vel.y < maxFallingVelWhileSticking)
			{
				vel.y = maxFallingVelWhileSticking;
			}
		}

		if (poundToggle && (fa.time >= (stompTimeSet + stompDelay)))
		{
			stompTimeSet = fa.time;
			if (hpScript)
			{
				hpScript.invincibleTimer = invincibleTimeAfterPound;
			}
			if (!onGround)
			{
				poundStartY = transform.position.y;

				if (altTrailPS_Normal != null)
				{
					altTrailPS_Normal.Stop();
					altTrailPS_Airsword.Stop();
				}
				xa.playerStuck = false;
				xa.playerJumped = false;
				xa.playerJumpedFresh = false;

				//raycast for blocks
				LayerMask blockMask = 1 << 22;//Only hits hitboxes on the Fresh_StompHitbox layer
				Ray ray = new Ray();
				RaycastHit hit = new RaycastHit();
				ray.direction = new Vector3(0, -1, 0);
				bool hitGround = false;
				float width = 0.23f;//0.4f;
				if (!hitGround)
				{
					//Centre
					ray.origin = new Vector3(transform.position.x, transform.position.y, raycastLayer);
					if (Physics.Raycast(ray, out hit, 999, blockMask)) { hitGround = true; lastStompedOnAFreshStompHitbox = fa.time; }
				}
				if (!hitGround)
				{
					//left
					ray.origin = new Vector3(transform.position.x - width, transform.position.y, raycastLayer);
					if (Physics.Raycast(ray, out hit, 999, blockMask)) { hitGround = true; lastStompedOnAFreshStompHitbox = fa.time; }
				}
				if (!hitGround)
				{
					//right
					ray.origin = new Vector3(transform.position.x + width, transform.position.y, raycastLayer);
					if (Physics.Raycast(ray, out hit, 999, blockMask)) { hitGround = true; lastStompedOnAFreshStompHitbox = fa.time; }
				}

				//raycast for blocks
				blockMask = 1 << 19;//Only hits hitboxes on the NovaBlock layer
				ray.direction = new Vector3(0, -1, 0);
				if (!hitGround)
				{
					//Centre
					ray.origin = new Vector3(transform.position.x, transform.position.y, raycastLayer);
					if (Physics.Raycast(ray, out hit, 999, blockMask)) { hitGround = true; }
				}

				if (!hitGround)
				{
					//left
					ray.origin = new Vector3(transform.position.x - width, transform.position.y, raycastLayer);
					if (Physics.Raycast(ray, out hit, 999, blockMask)) { hitGround = true; }
				}

				if (!hitGround)
				{
					//right
					ray.origin = new Vector3(transform.position.x + width, transform.position.y, raycastLayer);
					if (Physics.Raycast(ray, out hit, 999, blockMask)) { hitGround = true; }
				}



				if (hitGround)
				{
					if (altTrailPS_Normal != null)
					{
						altTrailPS_Normal.Play();
						altTrailPS_Airsword.Stop();
					}
					transform.SetY(hit.point.y + (plHeight * 0.5f) + 0.001f);
					vel.x = 0;
					vel.y = -1;//1

					ScreenShakeCamera.Screenshake(1, 0.15f, ScreenShakeCamera.ScreenshakeMethod.Basic);
					//checkPoundBox();
					createPoundEffect = true;
					//createPoundEffect_func();//Creates the effect
					FindGround();
				}
				else
				{
					//Missed the ground!

					if (altTrailPS_Normal != null)
					{
						altTrailPS_Normal.Play();
						altTrailPS_Airsword.Stop();
					}
					Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.Pound);
					ScreenShakeCamera.Screenshake(1, 0.15f, ScreenShakeCamera.ScreenshakeMethod.Basic);
					vel.x = 0;
					vel.y = -1;
					transform.SetY(ray.origin.y - 22);


				}


				//vel.x = 0;
				//vel.y = -999;
				//triggerStompHitboxes();
			}
			xa.playerPoundTimer = 5;
			poundToggle = false;
			damageFromPound = true;

		}


		//vel.y = Mathf.Clamp(vel.y, -maxFallingVelY, maxVerticalVelY);
		if (vel.y > maxVerticalVelY) { vel.y = maxVerticalVelY; }
		if (vel.y < -maxFallingVelY && vel.y > -500) { vel.y = -maxFallingVelY; }



		//system for damaging in radius while pounding
		if (damageFromPound)
		{
			updateWhilePounding(vel.y);
		}

		//add friction //see: moveLeftOrRight
		//vel.x *= friction;

		//min vel.x
		if (vel.x > -0.01f && vel.x < 0.01f && xa.inZoneIce <= 0) { vel.x = 0; }

	}

	void moveLeftOrRight()
	{
		if (onGround)
		{
			if (moveLeft != moveRight)
			{
				if (xa.inZoneIce <= 0)
				{
					//vel.x += (moveRight) ? groundAccel : -groundAccel;
					//if (vel.x != 0) vel.x *= friction;
					if (moveLeft)
					{
						vel.x -= groundAccel;
						if (vel.x > 0)
						{
							vel.x *= friction;
						}
					}
					if (moveRight)
					{
						vel.x += groundAccel;
						if (vel.x < 0)
						{
							vel.x *= friction;
						}
					}
				}
				else
				{
					vel.x += (moveRight) ? iceGroundAccel : -iceGroundAccel;
					//if (moveLeft) { vel.x -= iceGroundAccel; }
					//if (moveRight) { vel.x += iceGroundAccel; }
				}
			}
			else
			{
				if (xa.inZoneIce <= 0)
				{
					//add friction
					if (!xa.localNodeScript.rainLevel)
					{
						vel.x *= friction;
					}
					else
					{
						if (vel.x > rainFriction)
						{
							vel.x -= rainFriction;
						}
						else if (vel.x < -rainFriction)
						{
							vel.x += rainFriction;
						}
						else
						{
							vel.x = 0;
						}
					}
				}
				else
				{
					//add friction
				}
			}
		}
		else
		{

			//air accel stuff
			if (moveLeft != moveRight)
			{
				if (moveLeft)
				{
					if (vel.x > -maxVelX) { vel.x -= airAccel; if (vel.x < -maxVelX) { vel.x = -maxVelX; } }
				}
				if (moveRight)
				{
					if (vel.x < maxVelX) { vel.x += airAccel; if (vel.x > maxVelX) { vel.x = maxVelX; } }
				}

			}
			else if (vel.x != 0)
			{
				if (vel.x > 0) vel.x -= airAccel / 2;
				if (vel.x < 0) vel.x += airAccel / 2;
				if (Mathf.Abs(vel.x) <= airAccel / 2) { vel.x = 0; }
			}
		}

		if (onGround)
		{
			if (xa.inZoneIce > 0)
			{
				vel.x = Mathf.Clamp(vel.x, -maxIceVelX, maxIceVelX);
			}
			else
			{
				vel.x = Mathf.Clamp(vel.x, -maxVelX, maxVelX);
			}
		}
		vel.y = Mathf.Clamp(vel.y, -maxFallingVelY, maxVerticalVelY);


	}

	void jetpackFunc()
	{
		if (storedJetpack)
		{
			storedJetpack = false;
			vel.y += jetpackAmount * fa.deltaTime;

			if (vel.y > maxVerticalVelYWhileJetpacking) { vel.y = maxVerticalVelYWhileJetpacking; }
		}
	}
	void jumpFunc()
	{

		//CHeck explosive link
		if (hasExplosiveJump && GenericMonsterScript.monsters != null && airJumps > 0)
		{
			for (int i = 0; i < GenericMonsterScript.monsters.Count; i++)
			{
				if (GenericMonsterScript.monsters[i] == null) { continue; }
				if (GenericMonsterScript.monsters[i].spiky) { continue; }
				if (GenericMonsterScript.monsters[i].dead) { continue; }
				float dist = doubleJumpKillRadius + GenericMonsterScript.monsters[i].deathFromDoubleJumpExploDist;
				Vector3 v1 = GenericMonsterScript.monsters[i].gameObject.transform.position;
				Vector3 v2 = transform.position;
				v1.z = v2.z;//make it flat
				if (Vector3.Distance(v1, v2) < dist)
				{
					if (!GenericMonsterScript.monsters[i].near)
					{
						GenericMonsterScript.monsters[i].near = true;
						GenericMonsterScript.monsters[i].explosiveLinkGO = Instantiate(xa.de.explosiveLinkPrefab, GenericMonsterScript.monsters[i].transform.position, GenericMonsterScript.monsters[i].transform.localRotation);
						GenericMonsterScript.monsters[i].explosiveLinkGO2 = Instantiate(xa.de.explosiveLinkPrefab, GenericMonsterScript.monsters[i].transform.position, GenericMonsterScript.monsters[i].transform.localRotation);
					}
				}
				else
				{
					if (GenericMonsterScript.monsters[i].near)
					{
						Destroy(GenericMonsterScript.monsters[i].explosiveLinkGO);
						Destroy(GenericMonsterScript.monsters[i].explosiveLinkGO2);
					}
					GenericMonsterScript.monsters[i].near = false;
				}
			}
		}

		if (storedJump)
		{
			//Fresh_InGameMenus.self.DebugDisplay.text += "\n" + Time.time + ", storedJump = false;";
			//Setup.GC_DebugLog("Tried to jump" + Time.realtimeSinceStartup + "/" + airJumps);
			storedJump = false;

			if (((onGround || afterGroundCounter > 0) || stuck || nextToStickyWall || ((xa.playerHasDoubleJump || hasQuintupleJump || hasTripleJump) && !onGround)))
			{
				if (!nextToStickyWall && (xa.playerHasDoubleJump || hasQuintupleJump || hasTripleJump) && !onGround && !stuck && afterGroundCounter <= 0)
				{
					if (airJumps > 0)
					{
						if (carryingMom) { Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.Fart); }

						xa.playerJumped = true;
						xa.playerJumpedFresh = true;

						//damage monsters with double jump
						bool hitMonster = false;
						if (hasExplosiveJump && GenericMonsterScript.monsters != null)
						{
							for (int i = 0; i < GenericMonsterScript.monsters.Count; i++)
							{
								if (GenericMonsterScript.monsters[i] == null) { continue; }
								if (GenericMonsterScript.monsters[i].spiky) { continue; }
								if (GenericMonsterScript.monsters[i].dead) { continue; }
								float dist = doubleJumpKillRadius + GenericMonsterScript.monsters[i].deathFromDoubleJumpExploDist;
								Vector3 v1 = GenericMonsterScript.monsters[i].gameObject.transform.position;
								Vector3 v2 = transform.position;
								v1.z = v2.z;//make it flat
								if (Vector3.Distance(v1, v2) < dist)
								{
									hitMonster = true;
									GenericMonsterScript.monsters[i].hpScript.health = 0;
									if (GenericMonsterScript.monsters[i].explosiveLinkGO != null)
									{ Destroy(GenericMonsterScript.monsters[i].explosiveLinkGO); }
									if (GenericMonsterScript.monsters[i].explosiveLinkGO2 != null)
									{ Destroy(GenericMonsterScript.monsters[i].explosiveLinkGO2); }
									GenericMonsterScript.monsters[i].near = false;

								}
							}
						}


						float localJumpAmount = jumpAmount;
						if (carryingMom) { localJumpAmount = heavyJumpAmount; }
						if (hitMonster)
						{
							if (hpScript)
							{
								hpScript.invincibleTimer = 3;
							}
							ScreenShakeCamera.Screenshake(1, 0.1f, ScreenShakeCamera.ScreenshakeMethod.Basic);
							//Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.DoubleJumpExplo);
							localJumpAmount = jumpAmount * 1.25f;
							airJumps++;
							//forceChangeOnLastOne = true;
						}
						else
						{
							//Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.DoubleJump);
						}

						if (vel.y < localJumpAmount)
						{
							vel.y = localJumpAmount;
						}

						airJumps--;
						if (airJumps == 0 && maxAirJumps == 2) { has3rdJumped = true; }//For the Magic Monk Achivo


						xa.glx = transform.position;
						xa.glx.z = xa.GetLayer(xa.layers.Explo1);
						xa.glx.y -= plHeightHalf;
						if (hasExplosiveJump)
						{
							xa.tempobj = (GameObject)(Instantiate(explosiveJumpExplo, xa.glx, xa.null_quat));
							Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.DoubleJumpExplo);
						}
						else
						{
							xa.tempobj = (GameObject)(Instantiate(airJumpExplo, xa.glx, xa.null_quat));
							Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.DoubleJump);
						}
						xa.tempobj.transform.parent = xa.createdObjects.transform;
						//xa.tempobj.transform.parent = transform;

						Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.DoubleJump);
						//if (xa.sn) { xa.sn.playSound(GC_SoundScript.Sounds.DoubleJump); }
						//xa.tempobj = (GameObject)(Instantiate(doubleJumpSound, xa.glx, xa.null_quat));
						//xa.tempobj.transform.parent = xa.createdObjects.transform;
					}
				}
				else
				{

					if (carryingMom) { Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.Fart); }

					xa.playerJumped = true;
					xa.playerJumpedFresh = true;

					float localJumpAmount = jumpAmount;
					if (carryingMom) { localJumpAmount = heavyJumpAmount; }

					if (vel.y < localJumpAmount) { vel.y = localJumpAmount; }

					if (onFreshMovingPlat)
					{
						vel.y += movingPlatVelY;
					}

					stuck = false;
					//wallSticking = 0;
					//xa.playerWallSticking = wallSticking;

					Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.Jump);
				}
				afterGroundCounter = 0;
				createdStickySound = false;
			}
		}
	}
	/*
	public void GotAirswordBoost()
	{
		ScreenShakeCamera.Screenshake(1, 0.1f, ScreenShakeCamera.ScreenshakeMethod.Basic);
		Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.DoubleJumpExplo);
		float localJumpAmount = jumpAmount * 1.8f;//air sword boost about, //3.10
												  //airJumps++;
		xa.playerJumped = true;
		xa.playerJumpedFresh = true;

		if (vel.y < localJumpAmount)
		{
			vel.y = localJumpAmount;
		}

		xa.glx = transform.position;
		xa.glx.z = xa.GetLayer(xa.layers.Explo1);
		xa.glx.y -= plHeightHalf;
		xa.tempobj = (GameObject)(Instantiate(airJumpExplo, xa.glx, xa.null_quat));
		xa.tempobj.transform.parent = xa.createdObjects.transform;

		hpScript.invincibleTimer = 1;
	}*/

	public enum ForcedJumpType
	{
		None,
		BounceCoinNormal,
		BounceCoinBoosted,
		BouncePadNormal,
		BouncePadBoosted,
		End
	}

	void forceJumpPlayer(ForcedJumpType type)
	{
		if (type == ForcedJumpType.BounceCoinNormal)
		{
			stuck = false;
			xa.playerStuck = stuck;

			xa.playerJumped = true;
			xa.playerJumpedFresh = true;

			float localJumpAmount = jumpAmount;
			if (carryingMom) { localJumpAmount = heavyJumpAmount; }
			vel.y = (localJumpAmount) * bounceCoinBounciness;
		}
		if (type == ForcedJumpType.BounceCoinBoosted)
		{
			stuck = false;
			xa.playerStuck = stuck;

			xa.playerJumped = true;
			xa.playerJumpedFresh = true;
			float localJumpAmount = jumpAmount;
			if (carryingMom) { localJumpAmount = heavyJumpAmount; }
			vel.y = (localJumpAmount) * bounceCoinBounciness * 1.2F;
		}
		if (type == ForcedJumpType.BouncePadNormal)
		{

			stuck = false;
			xa.playerStuck = stuck;

			xa.playerJumped = true;
			xa.playerJumpedFresh = true;
			float localJumpAmount = jumpAmount;
			vel.y = (localJumpAmount) * bouncePadBounciness;
		}
		if (type == ForcedJumpType.BouncePadBoosted)
		{
			stuck = false;
			xa.playerStuck = stuck;

			xa.playerJumped = true;
			xa.playerJumpedFresh = true;
			float localJumpAmount = jumpAmount;
			vel.y = (localJumpAmount) * bouncePadBounciness * 1.1F;
		}
	}

	public void Unstick()//called by airsword (PlayerState_AirSword) & by jumping/other abilities
	{
		stuck = false;
		xa.playerStuckDir = 0;
		xa.playerStuck = false;
		stickyDir = 0;
		defaultStickDir = 0;
	}

	void resolveVel()
	{
		float raycastLayer = xa.GetLayer(xa.layers.RaycastLayer);
		if (Time.timeSinceLevelLoad < 0.1f) { return; }
		// if (za.paralyzePlayerForCutscenes) { return; }












		if (!xa.cheat_perfectFlight)
		{
			if (damageFromPound)
			{
				Unstick();
			}
			xa.playerStuck = stuck;

			//check for don't die zones ------ move this to level start and store zones in a var. same thing with wind/reverse wind and stickyzones
			dontDieZone = checkForZone(transform.position, "dontDieZone");
			/*
			//check for windzones
			if (checkForZone(transform.position, "windZone"))
			{
				vel.x -= 0.65f * fa.deltaTime;
			}
			if (checkForZone(transform.position, "reverseWindZone"))
			{
				vel.x += 0.65f * fa.deltaTime;
			}
			*/

			xa.playerClungToWallTHISFrame = false;

			//Start Nova Sticky Wall system

			Ray ray = new Ray();
			LayerMask stickyMask = 1 << 20;//Only hits hitboxes on the NovaStickHitbox layer
			float dist = plHalf + 0.3f;
			int tempDir = 0;

			nextToStickyWall = false;
			//check if near sticky wall
			ray.origin = new Vector3(transform.position.x, transform.position.y, raycastLayer);
			ray.direction = new Vector3(1, 0, 0);
			if (Physics.Raycast(ray, dist, stickyMask))
			{
				nextToStickyWall = true;
			}
			ray.origin = new Vector3(transform.position.x, transform.position.y, raycastLayer);
			ray.direction = new Vector3(-1, 0, 0);
			if (Physics.Raycast(ray, dist, stickyMask))
			{
				nextToStickyWall = true;
			}

			dist = plHalf + 0.1f;
			tempDir = 0;
			if (vel.x > 0)
			{
				ray.origin = new Vector3(transform.position.x, transform.position.y, raycastLayer);
				ray.direction = new Vector3(1, 0, 0);
				tempDir = 1;
			}
			else if (vel.x < 0)
			{
				ray.origin = new Vector3(transform.position.x, transform.position.y, raycastLayer);
				ray.direction = new Vector3(-1, 0, 0);
				tempDir = -1;
			}
			if (Controls.GetInput(Controls.Type.MoveDown, playerNumber))
			{
				unstuckable = true;
			}
			else
			{
				unstuckable = false;
			}

			if (tempDir != 0)
			{
				float zDepth = xa.GetLayer(xa.layers.GribblyFront);
				//Debug.DrawLine(new Vector3(ray.origin.x, ray.origin.y, zDepth), new Vector3(ray.GetPoint(dist).x, ray.GetPoint(dist).y, zDepth), Color.green);
				if (Physics.Raycast(ray, dist, stickyMask))
				{
					if (!unstuckable)
					{
						stuck = true;
						stickyDir = tempDir;
						defaultStickDir = tempDir;
						xa.playerStuckDir = stickyDir;
						//Debug.DrawLine(new Vector3(ray.origin.x, ray.origin.y, xa.playerAndBlocksLayer), new Vector3(ray.GetPoint(dist).x, ray.GetPoint(dist).y, xa.playerAndBlocksLayer), Color.green);

						stuckTimestamp = fa.time;
						xa.playerClungToWallTHISFrame = true;
						airJumps = maxAirJumps;
						punches = maxPunches;
						airSwords = maxAirSwords;

						xa.playerStuck = stuck;
						if (!createdStickySound)
						{
							createdStickySound = true;
							Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.StickyWall);
							//if (xa.sn) { xa.sn.playSound(GC_SoundScript.Sounds.StickyWall); }

						}
					}
				}
				else
				{
					if (unstuckable)
					{
						unstuckable = false;

					}
					// Debug.DrawLine(new Vector3(ray.origin.x, ray.origin.y, xa.playerAndBlocksLayer), new Vector3(ray.GetPoint(dist).x, ray.GetPoint(dist).y, xa.playerAndBlocksLayer), Color.red);
				}

			}


			//if(!stuck && unstuckable) {unstuckable = false; }

			if (stuck)
			{
				//debugText.text = "Stuck: True\nStickyDir: " + stickyDir + "\nPlayerDir: " + xa.playerDir + "\nTime: " + Time.time;
				//Fresh_InGameMenus.self.DebugDisplay.text = 

				stickyDir = defaultStickDir;
				if (vel.x < 0) { stickyDir = -1; }//If you move away from the wall, cancel out.
				if (vel.x > 0) { stickyDir = 1; }

				if (vel.x < 0 && defaultStickDir < 0) { stuckTimestamp = fa.time; }
				if (vel.x > 0 && defaultStickDir > 0) { stuckTimestamp = fa.time; }

				xa.playerStuckDir = stickyDir;
				vel.x = 0;

				if (vel.y > 0) { Unstick(); }//Jumped or boosted. Cancel stuck.

				if (fa.time > (stuckTimestamp + 0.3f))
				{
					if (vel.y < maxFallingVelWhileSticking) { vel.y = maxFallingVelWhileSticking; }
				}
				else
				{
					if (vel.y < 0) { vel.y = 0; }//Falling. Dont.
				}


				if (fa.time > (stuckTimestamp + 0.5f))//2 feels good, but it too long
				{
					Unstick();
				}
				if (Controls.GetInput(Controls.Type.MoveDown, playerNumber))
				//if(Input.GetKeyDown(KeyCode.Joystick1Button2))
				{
					Unstick();
				}
			}
			else
			{
				stickyDir = 0;
				defaultStickDir = 0;
				xa.playerStuckDir = 0;

				//Fresh_InGameMenus.self.DebugDisplay.text = "Stuck: False, " + Time.time;
			}

			//End nova sticky wall system
			//----
			//Start nova collision system


			//Try this. First, get a list of every block that I'm overlapping
			bool squished = false;
			List<RaycastHit> listOfHits = new List<RaycastHit>();
			LayerMask mask = 1 << 19;
			ray.direction = new Vector3(0, 0, 1);
			//RaycastHit[] hits;
			RaycastHit hit;
			Vector2 startingVel = new Vector2(vel.x, vel.y);
			Vector3 oldPlPos = transform.position;

			//Handle X
			//Now raycastAll at a bunch of points around the player
			transform.SetX(transform.position.x + vel.x);
			ray.origin = new Vector3(transform.position.x - plHalf, transform.position.y + plHeightHalf, raycastLayer - 2);
			//hits = Physics.RaycastAll(ray, 2, mask); foreach (RaycastHit h in hits) { listOfHits.Add(h); }
			if (Physics.Raycast(ray, out hit, 2, mask)) { listOfHits.Add(hit); }


			ray.origin = new Vector3(transform.position.x - plHalf, transform.position.y - plHeightHalf, raycastLayer - 2);
			//hits = Physics.RaycastAll(ray, 2, mask); foreach (RaycastHit h in hits) { listOfHits.Add(h); }
			if (Physics.Raycast(ray, out hit, 2, mask)) { listOfHits.Add(hit); }

			ray.origin = new Vector3(transform.position.x + plHalf, transform.position.y + plHeightHalf, raycastLayer - 2);
			//hits = Physics.RaycastAll(ray, 2, mask); foreach (RaycastHit h in hits) { listOfHits.Add(h); }
			if (Physics.Raycast(ray, out hit, 2, mask)) { listOfHits.Add(hit); }
			ray.origin = new Vector3(transform.position.x + plHalf, transform.position.y - plHeightHalf, raycastLayer - 2);
			//hits = Physics.RaycastAll(ray, 2, mask); foreach (RaycastHit h in hits) { listOfHits.Add(h); }
			if (Physics.Raycast(ray, out hit, 2, mask)) { listOfHits.Add(hit); }

			//ray.origin = new Vector3(transform.position.x, transform.position.y, raycastLayer - 2);//Center of player
			//hits = Physics.RaycastAll(ray, 2, mask); foreach (RaycastHit h in hits) { listOfHits.Add(h); }
			//if (Physics.Raycast(ray, out hit, 2, mask)) { listOfHits.Add(hit); }

			//These are laggy. I don't think these are needed. 
			ray.origin = new Vector3(transform.position.x + plHalf, transform.position.y, raycastLayer - 2);
			if (Physics.Raycast(ray, out hit, 2, mask)) { listOfHits.Add(hit); }
			ray.origin = new Vector3(transform.position.x - plHalf, transform.position.y, raycastLayer - 2);
			if (Physics.Raycast(ray, out hit, 2, mask)) { listOfHits.Add(hit); }


			//Vector3 v1 = new Vector3(55,-99,0);v1.z = 33;
			//Vector3 v2 = transform.position; v2.z = 33;
			//Debug.DrawLine(v1, v2, Color.cyan, 5);


			//Now get out of from every block here
			for (int i = 0; i < listOfHits.Count; i++)
			{
				RaycastHit h = listOfHits[i];
				/*
				v1 = h.collider.gameObject.transform.position; v1.z = 32;
				v2 = transform.position; v2.z = 32;
				if(i == 0) {Debug.DrawLine(v1, v2, Color.green, 5); }
				if(i == 1) {Debug.DrawLine(v1, v2, Color.blue, 5); }
				if(i == 2) {Debug.DrawLine(v1, v2, Color.grey, 5); }
				if(i == 3) {Debug.DrawLine(v1, v2, Color.white, 5); }
				if(i == 4) {Debug.DrawLine(v1, v2, Color.yellow, 5); }
				*/
				//Collide with wall to the left
				if (h.collider.bounds.max.x > (transform.position.x - plHalf))
				{
					if (h.collider.bounds.center.x < transform.position.x)//If I'm on the right of the block's center
					{
						if (h.collider.bounds.max.y < transform.position.y && h.collider.bounds.max.x > transform.position.x)//If the block's top edge is less than my center, and block's right edge is to the right of me.
						{
							//I'm probably standing on top of a block. Let Y snapping handle this.
						}
						else if (h.collider.bounds.min.y > transform.position.y && h.collider.bounds.max.x > transform.position.x)//If the block's bottom edge is greater than my center, and block's right edge is to the right of me.
						{
							//I'm probably bumping my head against a block. Let Y snapping handle this.
						}
						else
						{
							//Setup.Display("Snapped X, block to the left", "snapRight");
							//No. Just handle x snapping as per normal
							transform.SetX(h.collider.bounds.max.x + plHalf + 0.001f);
							vel.x = 0;
						}
					}
				}

				//Collide with wall to the right
				if (h.collider.bounds.min.x < (transform.position.x + plHalf))//The bottom edge of the block overlaps with the top of the players head
				{
					if (h.collider.bounds.center.x > transform.position.x)//If I'm on the left of the block's center
					{
						if (h.collider.bounds.max.y < transform.position.y && h.collider.bounds.min.x < transform.position.x)
						{
							//I'm probably standing on top of a block. Let Y snapping handle this.
						}
						if (h.collider.bounds.min.y > transform.position.y && h.collider.bounds.min.x < transform.position.x)
						{
							//I'm probably bumping my head against a block. Let Y snapping handle this.
						}
						else
						{
							//Setup.Display("Snapped X, block to the right", "snapLeft");
							//No. Just handle x snapping as per normal
							transform.SetX(h.collider.bounds.min.x - plHalf - 0.001f);
							vel.x = 0;
						}
					}
				}
			}

			//Check if I'm still inside a block
			//Handle Y
			listOfHits = new List<RaycastHit>();
			transform.SetY(transform.position.y + vel.y);
			ray.origin = new Vector3(transform.position.x - plHalf, transform.position.y + plHeightHalf, raycastLayer - 2);
			//hits = Physics.RaycastAll(ray, 2, mask); foreach (RaycastHit h in hits) { listOfHits.Add(h); }
			if (Physics.Raycast(ray, out hit, 2, mask)) { listOfHits.Add(hit); }
			ray.origin = new Vector3(transform.position.x - plHalf, transform.position.y - plHeightHalf, raycastLayer - 2);
			//hits = Physics.RaycastAll(ray, 2, mask); foreach (RaycastHit h in hits) { listOfHits.Add(h); }
			if (Physics.Raycast(ray, out hit, 2, mask)) { listOfHits.Add(hit); }

			ray.origin = new Vector3(transform.position.x + plHalf, transform.position.y + plHeightHalf, raycastLayer - 2);
			//hits = Physics.RaycastAll(ray, 2, mask); foreach (RaycastHit h in hits) { listOfHits.Add(h); }
			if (Physics.Raycast(ray, out hit, 2, mask)) { listOfHits.Add(hit); }
			ray.origin = new Vector3(transform.position.x + plHalf, transform.position.y - plHeightHalf, raycastLayer - 2);
			//hits = Physics.RaycastAll(ray, 2, mask); foreach (RaycastHit h in hits) { listOfHits.Add(h); }
			if (Physics.Raycast(ray, out hit, 2, mask)) { listOfHits.Add(hit); }

			//ray.origin = new Vector3(transform.position.x, transform.position.y, raycastLayer - 2);//Center of player
			//hits = Physics.RaycastAll(ray, 2, mask); foreach (RaycastHit h in hits) { listOfHits.Add(h); }
			//if (Physics.Raycast(ray, out hit, 2, mask)) { listOfHits.Add(hit); }

			ray.origin = new Vector3(transform.position.x + plHalf, transform.position.y, raycastLayer - 2);
			if (Physics.Raycast(ray, out hit, 2, mask)) { listOfHits.Add(hit); }
			ray.origin = new Vector3(transform.position.x - plHalf, transform.position.y, raycastLayer - 2);
			if (Physics.Raycast(ray, out hit, 2, mask)) { listOfHits.Add(hit); }
			/*
            ray.origin = new Vector3(transform.position.x, transform.position.y + plHeightHalf, raycastLayer - 5);
            hits = Physics.RaycastAll(ray, 10, mask); foreach (RaycastHit h in hits) { listOfHits.Add(h); }
            ray.origin = new Vector3(transform.position.x, transform.position.y - plHeightHalf, raycastLayer - 5);
            hits = Physics.RaycastAll(ray, 10, mask); foreach (RaycastHit h in hits) { listOfHits.Add(h); }
            ray.origin = new Vector3(transform.position.x + plHalf, transform.position.y, raycastLayer - 5);
            hits = Physics.RaycastAll(ray, 10, mask); foreach (RaycastHit h in hits) { listOfHits.Add(h); }
            ray.origin = new Vector3(transform.position.x - plHalf, transform.position.y, raycastLayer - 5);
            hits = Physics.RaycastAll(ray, 10, mask); foreach (RaycastHit h in hits) { listOfHits.Add(h); }
            */

			//Now get out of from every block here
			if (listOfHits.Count > 0)
			{
				foreach (RaycastHit h in listOfHits)
				{
					//Collide with ceiling
					if (h.collider.bounds.min.y < (transform.position.y + plHeightHalf))//The top edge of the player overlaps the bottom edge of the block
					{
						if (h.collider.bounds.center.y > transform.position.y)//The center of the block is higher than the center of the player
						{
							//I'm being snapped down. How far am I snapping, because if it's too far, then I'm probably being squished.
							if (Vector2.Distance(new Vector2(h.collider.bounds.min.y - plHeightHalf - 0.001f, 0), new Vector2(oldPlPos.y, 0)) > 0.5f)
							{
								//
								squished = true;
							}
							else
							{
								//Setup.Display("Snapped Y, block above", "snapUp");
								transform.SetY(h.collider.bounds.min.y - plHeightHalf - 0.001f);
							}
						}
					}

					//Collide with ground
					if (h.collider.bounds.max.y > (transform.position.y - plHeightHalf))
					{
						if (h.collider.bounds.center.y < transform.position.y)
						{

							//I'm being snapped up. How far am I snapping, because if it's too far, then I'm probably being squished.
							//if (Vector2.Distance(new Vector2(h.collider.bounds.max.y + plHeightHalf + 0.001f, 0), new Vector2(oldPlPos.y, 0)) > (0.1f + vel.y))
							//{
							//        squished = true;
							// }
							// else
							// {
							//No. Just handle Y snapping as per normal
							//Setup.Display("Snapped Y, block below", "snapDown");
							transform.SetY(h.collider.bounds.max.y + plHeightHalf + 0.001f);
							vel.y = 0;
							// }
						}
					}
				}
			}


			//Handle Squishing

			//check for certain cases

			//What if I snapped more then possible with vel?
			if (Setup.Distance(oldPlPos.x, transform.position.x) > (Mathf.Abs(startingVel.x) + 0.1f)) { squished = true; }
			//
			if (Setup.Distance(oldPlPos.y, transform.position.y) > (Mathf.Abs(startingVel.y) + 0.1f)) { squished = true; }


			//Raycast for overlapping blocks, if so, squish
			if (!squished)
			{
				ray.origin = new Vector3(transform.position.x - plHalf, transform.position.y + plHeightHalf, raycastLayer - 2);
				if (Physics.Raycast(ray, out hit, 2, mask)) { squished = true; }
				ray.origin = new Vector3(transform.position.x - plHalf, transform.position.y - plHeightHalf, raycastLayer - 2);
				if (Physics.Raycast(ray, out hit, 2, mask)) { squished = true; }

				ray.origin = new Vector3(transform.position.x + plHalf, transform.position.y + plHeightHalf, raycastLayer - 2);
				if (Physics.Raycast(ray, out hit, 2, mask)) { squished = true; }
				ray.origin = new Vector3(transform.position.x + plHalf, transform.position.y - plHeightHalf, raycastLayer - 2);
				if (Physics.Raycast(ray, out hit, 2, mask)) { squished = true; }

				ray.origin = new Vector3(transform.position.x + plHalf, transform.position.y, raycastLayer - 2);
				if (Physics.Raycast(ray, out hit, 2, mask)) { squished = true; }
				ray.origin = new Vector3(transform.position.x - plHalf, transform.position.y, raycastLayer - 2);
				if (Physics.Raycast(ray, out hit, 2, mask)) { squished = true; }
			}



			//Check if the player has been squished
			if (squished)
			{
				if (hpScript)
				{
					//transform.position = oldPlPos;
					//hpScript.health = 0;
				}
			}

			//Check if I'm standing on a triggable (exploding) block
			mask = 1 << 23;
			ray.direction = new Vector3(0, -1, 0);
			bool onExplodingBlock = false;
			ray.origin = new Vector3(transform.position.x - plHalf, transform.position.y, raycastLayer);

			if (Physics.Raycast(ray, out hit, plHeightHalf + 0.2f, mask)) { onExplodingBlock = true; }
			ray.origin = new Vector3(transform.position.x + plHalf, transform.position.y, raycastLayer);

			if (!onExplodingBlock)
			{

				if (Physics.Raycast(ray, out hit, plHeightHalf + 0.2f, mask)) { onExplodingBlock = true; }

			}
			if (onExplodingBlock)
			{
				if (hit.transform != null && hit.transform.gameObject != null)
				{
					Info info = hit.transform.gameObject.GetComponent<Info>();
					info.stoodOnByPlayer = true;
				}
			}

			//End nova collision system
			//-----

		}
		else
		{
			float cheat_flight_speed = 0.4f;
			//if (xa.cheat_perfectFlight_speed == 0) { cheat_flight_speed = 0.4f; }
			//if (xa.cheat_perfectFlight_speed == 1) { cheat_flight_speed = 2f; }

			xa.glx = Vector3.zero;
			if (Input.GetKey(KeyCode.UpArrow)) xa.glx.y = cheat_flight_speed;
			if (Input.GetKey(KeyCode.DownArrow)) xa.glx.y = -cheat_flight_speed;
			if (Input.GetKey(KeyCode.LeftArrow)) xa.glx.x = -cheat_flight_speed;
			if (Input.GetKey(KeyCode.RightArrow)) xa.glx.x = cheat_flight_speed;
			transform.position += xa.glx * fa.deltaTime * 30;
		}

		//now set direction, after all vel changes have happened.
		if (vel.x < -0.01f && !stuck)
		{
			xa.playerDir = -1;
		}
		else if (vel.x > 0.01f && !stuck)
		{
			xa.playerDir = 1;
		}



		//now I've snapped out of all the blocks, find the ground

		FindGround();



		//debugText.text = "Stuck: " + stuck + "\nStickyDir: " + stickyDir + "\nPlayerDir: " + xa.playerDir + "\nVel.x: " + vel.x + "\nFlipped: " + "\nTime: " + Time.time;

	}

	void FindGround()
	{
		onGround = false;
		onAniGround = false;
		bool trigger = false;
		bool aniTrigger = false;
		RaycastHit hit;
		Ray ray = new Ray();
		LayerMask mask = 1 << 19;
		ray.direction = new Vector3(0, -1, 0);
		float raycastLayer = xa.GetLayer(xa.layers.RaycastLayer);


		ray.origin = new Vector3(transform.position.x - plHalf, transform.position.y, raycastLayer);
		Debug.DrawLine(ray.origin, ray.GetPoint(footLength), Color.green);
		ray.origin = new Vector3(ray.origin.x, ray.origin.y, raycastLayer);
		if (Physics.Raycast(ray, out hit, footLength, mask))
		{
			trigger = true;
		}

		ray.origin = new Vector3(transform.position.x + plHalf, transform.position.y, raycastLayer);
		Debug.DrawLine(ray.origin, ray.GetPoint(footLength), Color.green);
		ray.origin = new Vector3(ray.origin.x, ray.origin.y, raycastLayer);
		if (Physics.Raycast(ray, out hit, footLength, mask))
		{
			trigger = true;
		}

		ray.origin = new Vector3(transform.position.x - plHalf, transform.position.y, raycastLayer);
		ray.origin = new Vector3(ray.origin.x, ray.origin.y, raycastLayer);
		if (Physics.Raycast(ray, out hit, aniFootLength, mask)) { aniTrigger = true; }

		ray.origin = new Vector3(transform.position.x + plHalf, transform.position.y, raycastLayer);
		ray.origin = new Vector3(ray.origin.x, ray.origin.y, raycastLayer);
		if (Physics.Raycast(ray, out hit, aniFootLength, mask)) { aniTrigger = true; }


		//if hit ground
		if (trigger)
		{
			if (vel.y <= 0)
			{
				tinyJumpTimer = 0;
				tinyJumped = false;
				poundToggle = false;
				onGround = true;

				unstuckable = false;
				afterGroundCounter = afterGroundTime;
				dontFastGrav = false;
				airJumps = maxAirJumps;
				punches = maxPunches;
				airSwords = maxAirSwords;
				vel.y = 0;
				Time.timeScale = 1f;
				if (storedPossibleJump)
				{
					storedPossibleJump = false;
					storedJump = true;
				}
			}
		}

		//If player has hit the ground from a visual/audio pov
		if (aniTrigger)
		{
			onAniGround = true;


		}


		if (createPoundEffect)
		{
			HurtMonstersInBox();
			HurtMonsters(poundRadius, true);
			checkPoundBox();
			createPoundEffect = false;
			createPoundEffect_func();
		}



		if (oldOnGround != onAniGround)//Create the sound
		{
			oldOnGround = onAniGround;
			if (onAniGround)
			{

				Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.Land);
				//if (xa.sn) { xa.sn.playSound(GC_SoundScript.Sounds.Land); }
			}
		}

		xa.playerOnAniGround = onAniGround;
	}



	void updateWhilePounding(float dist)
	{
		float left;
		left = -dist;
		while (left > 1f)
		{
			left -= 1f;
			//checkForBouncePads();
		}
	}
	public void createPoundEffect_func()
	{
		xa.glx = transform.position;
		xa.glx.z = xa.GetLayer(xa.layers.Explo1);
		xa.glx.y -= plHeightHalf;
		xa.tempobj = (GameObject)(Instantiate(poundExplo, xa.glx, xa.null_quat));
		xa.tempobj.transform.parent = xa.createdObjects.transform;
	}

	void checkPoundBox()
	{
		//define a box
		float boxHalfSize = 1.6f;//1;

		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("item");

		foreach (GameObject go in gos)
		{
			if (go.transform.position.y < poundStartY && go.transform.position.y >= transform.position.y && go.transform.position.x > (transform.position.x - boxHalfSize) && go.transform.position.x < (transform.position.x + boxHalfSize))
			{
				useFoundItem(go);
			}
		}
	}

	public static bool checkPlayerDeathBox(Vector3 pos)
	{
		if (za.dontKillPlayerForBeingOffscreen)
		{
			return true;
		}
		else
		{
			return Setup.checkVecOnScreen(pos + new Vector3(0, -3, 0), true) || Setup.checkVecOnScreen(pos + new Vector3(0, 2, 0), true);

		}
	}

	public void infLoveResetPlayerVariables()
	{
		//called when the player hits a portal in InfLove mode. Resets vel, etc.
		vel.x = 0;
		vel.y = 0;
		poundToggle = false;
		damageFromPound = false;
	}

	void applyESJ4SqJumpRules()
	{
		plSize = 0.46f;
		plHalf = 0.23f;
		//1.3f;
		plHeightHalf = 0.67f;//0.75 - changing this screws stuff up, because it's from the center, and the player doesn't touch the floor properly. Change if you are willing to tweak footLength and so forth
		plHeight = 1.34f;
		//footLength = 0.68f;
		footLength = 0.73f;

		//stats
		orginalGravity = 0.027f;
		maxFallingVelY = 0.5f;//0.4f;//0.5f;//not sure which is better
							  //orginalGravityFast = 0.005f;

		if (isMiniPlayer)
		{
			plSize = 0.82f;
			plHalf = 0.41f;
		}
	}

	public void SetAniFrameInt(int var1)
	{
		aniFrameAsInt = var1;
	}

	public int GetAniFrameAsInt()
	{
		return aniFrameAsInt;//May be -1 if has never been set
	}
}
