using UnityEngine;

public class GoomaScript : MonoBehaviour
{
	public GameObject puppet;
	//  public GameObject deathExplo = null;
	//  public GameObject deathExplo2 = null;
	public bool move = false;
	public float speed = 0;
	public float chargeSpeed = 0;
	float currentSpeed = 0;
	public GameObject traceDownPoint = null;
	public GameObject fallOffPoint = null;
	public bool hasHealthScript = true;
	public bool charging = false;
	public float chargeBoxY = 1;
	public float chargeBoxX = 5;
	public GameObject chargingPuppet = null;

	public bool bounceOffWalls = false;
	public bool checkForFloors = false;
	public float footLength = 0;

	public float boxSize = 0.6f;
	public float boxHalf = 0.3f;
	public float boxHeight = 1.5f;
	public float boxHeightHalf = 0.75f;
	//float blockSize = 1f;
	float blockHalf = 0.5f;

	bool bumped = false;
	bool flipped = false;
	public float dontFlipAgainDelay = 0;
	//Vector3 startingScale = Vector3.zero;

	HealthScript hpScript;
	bool dead;
	bool charge = false;
	float dontThinkJustCharge = 0;

	public bool useImpLogic = false;
	public bool animateBigImp = false;
	public bool animateHammerHead = false;
	public bool animateLaserBack = false;
	bool onGround = false;
	public float impLegHeight = 0;
	public float impFallSpeed = 0;
	int impAniState = 0;
	public bool impBounceOffWalls = false;
	public bool impCheckForFloors = false;
	public bool dontKillIfOffBottom = false;

	LayerMask mask = 1 << 19;

	int frameCount = 0;//for things that only need to check once every five frames or so

	void Start()
	{
		if (hasHealthScript)
		{
			hpScript = this.gameObject.GetComponent<HealthScript>();
		}
		frameCount = (int)(Random.Range(0, 5));
		//startingScale = transform.localScale;

		if (useImpLogic)
		{
			bounceOffWalls = false;
			checkForFloors = false;
		}

		updateSpeedAndVisuals();
	}

	void checkCharging()
	{
		charge = false;
		if (xa.player)
		{
			if (!xa.playerDead)
			{
				if (xa.player.transform.position.y < (transform.position.y + chargeBoxY) && xa.player.transform.position.y > (transform.position.y - chargeBoxY))
				{
					if (xa.player.transform.position.x < transform.position.x)
					{
						if (xa.player.transform.position.x < (transform.position.x) && xa.player.transform.position.x > (transform.position.x - chargeBoxX))
						{
							dontFlipAgainDelay = 0;
							dontThinkJustCharge = 10;
							counter = 0;
							aniIndex = 0;
							aniFrameX = 0;
							flipped = false;
							updateSpeedAndVisuals();
							charge = true;
						}
					}
					else
					{
						if (xa.player.transform.position.x < (transform.position.x + chargeBoxX) && xa.player.transform.position.x > (transform.position.x))
						{
							dontFlipAgainDelay = 0;
							flipped = true;
							updateSpeedAndVisuals();
							dontThinkJustCharge = 10;
							charge = true;
						}
					}
				}
			}
		}
	}

	void Update()
	{
		if (EditorController.IsEditorActive())
		{
			return;
		}

		if (charging && dontFlipAgainDelay <= 0 && dontThinkJustCharge <= 0) { checkCharging(); }
		dontThinkJustCharge -= 10 * fa.deltaTime;
		//check if dead
		if (hasHealthScript && !dead)
		{
			if (hpScript.health <= 0)
			{
				dead = true;
			}
		}

		if (!dead)
		{
			handleMe();
		}

		/*else
		{
			//create a cool effect and destroy me

			if (deathExplo)
			{
				xa.glx = transform.position;
				xa.glx.z = xa.explo1Layer;
				xa.tempobj = (GameObject)(Instantiate(deathExplo, xa.glx, transform.localRotation));
				xa.tempobj.transform.parent = xa.createdObjects.transform;
			}
			if (deathExplo2)
			{
				xa.glx = transform.position;
				xa.glx.z = xa.explo1Layer;
				xa.tempobj = (GameObject)(Instantiate(deathExplo2, xa.glx, transform.localRotation));
				xa.tempobj.transform.parent = xa.createdObjects.transform;
			}

			Destroy(this.gameObject);
		}*/

		if (charging)
		{
			animateChargingGoomba();
		}
	}


	RaycastHit hit;
	Ray ray = new Ray();
	RaycastHit hit2;
	Ray ray2 = new Ray();

	void handleMe()
	{
		float raycastLayer = xa.GetLayer(xa.layers.RaycastLayer);
		if (useImpLogic)
		{
			//am I below the bottom of the screen by a fair buffer?
			if (transform.position.y < xa.bottomEdgeOfScreen - 8 && dontKillIfOffBottom == false)
			{
				dead = true;
				Destroy(this.gameObject);
			}


			//am I on the ground?
			onGround = false;
			ray.direction = new Vector3(0, -1, 0);
			ray2.direction = new Vector3(0, -1, 0);
			xa.glx = fallOffPoint.transform.position;
			xa.glx.y += 0.7f;
			xa.glx.z = raycastLayer;
			ray.origin = xa.glx;

			xa.glx = transform.position;
			xa.glx.y += 0.7f;
			xa.glx.z = raycastLayer;
			ray2.origin = xa.glx;// transform.position;

			//Debug.DrawLine(ray.origin, ray.GetPoint(impLegHeight + 1.4f), Color.cyan);
			//Debug.DrawLine(ray2.origin, ray2.GetPoint(impLegHeight + 1.4f), Color.green);
			if (Physics.Raycast(ray, out hit, impLegHeight + 1.4f, mask))
			{
				onGround = true;
				xa.glx = transform.position;
				xa.glx.y = hit.point.y + impLegHeight;
				transform.position = xa.glx;
			}
			if (Physics.Raycast(ray2, out hit2, impLegHeight + 1.4f, mask))
			{
				onGround = true;
				xa.glx = transform.position;
				xa.glx.y = hit2.point.y + impLegHeight;
				transform.position = xa.glx;
			}


			if (onGround)
			{
				//walk
				impAniState = 0;
				move = true;

			}
			else
			{
				//fall
				impAniState = 1;
				move = false;
				transform.Translate(0, impFallSpeed * fa.deltaTime, 0);
			}

			animateImp();
		}



		frameCount++;
		if (frameCount >= 5) { frameCount = 0; }
		//move
		if (move)
		{
			updateSpeedAndVisuals();
			if (charge)
			{
				if (!flipped) { currentSpeed = -chargeSpeed; }
				else { currentSpeed = chargeSpeed; }
			}
			transform.Translate(currentSpeed * fa.deltaTime, 0, 0);
		}

		dontFlipAgainDelay -= 10 * fa.deltaTime;

		//check for floors
		if (checkForFloors && frameCount == 0)
		{
			if (dontFlipAgainDelay <= 0)
			{
				bool trigger = false;
				ray.direction = new Vector3(0, -1, 0);
				xa.glx = transform.position;
				if (traceDownPoint != null) { xa.glx = traceDownPoint.transform.position; }
				xa.glx.z = raycastLayer;
				ray.origin = xa.glx;
				//Debug.DrawLine(ray.origin, ray.GetPoint(footLength), Color.green);
				if (Physics.Raycast(ray, out hit, footLength, mask) == true)
				{
					trigger = true;
				}
				if (trigger == false)
				{
					//Setup.GC_DebugLog("flips");
					dontFlipAgainDelay = 3;
					flipAround();
				}
			}

		}

		//bounce
		if (bounceOffWalls && frameCount == 0)
		{
			if (dontFlipAgainDelay <= 0)
			{
				bumped = false;
				//GameObject[] gos = GameObject.FindGameObjectsWithTag("solidThing");
				//List<GameObject> gos = xa.onScreenSolid;
				GameObject[] gos = xa.onScreenSolid;//This spits an error, should be replaced - Michael, ESJ2
													// this could screw things up if ai is going offscreen...
				if (gos != null)
				{
					foreach (GameObject go in gos)
					{
						if (go == null) continue;
						if ((transform.position.x + boxHalf) > (go.transform.position.x - blockHalf) &&
							(transform.position.x - boxHalf) < (go.transform.position.x + blockHalf) &&
							(transform.position.y + boxHeightHalf) > (go.transform.position.y - blockHalf) &&
							(transform.position.y - boxHeightHalf) < (go.transform.position.y + blockHalf))
						{
							bumped = true;
						}
					}

				}
				if (bumped)
				{
					dontFlipAgainDelay = 10;
					bumped = false;
					charge = false;
					flipAround();
				}
			}
		}

		//bounce (imp)
		if (impBounceOffWalls)
		{
			if (flipped) { ray.direction = new Vector3(1, 0, 0); }
			if (!flipped) { ray.direction = new Vector3(-1, 0, 0); }
			xa.glx = transform.position;
			xa.glx.z = raycastLayer;
			ray.origin = xa.glx;
			//Debug.DrawLine(ray.origin, ray.GetPoint(0.52f), Color.green);
			if (Physics.Raycast(ray, out hit, 0.52f, mask))
			{
				flipAround();
			}
		}


		//check for floors (imp)
		if (impCheckForFloors)
		{
			ray.direction = new Vector3(0, -1, 0);
			xa.glx = traceDownPoint.transform.position;
			xa.glx.z = raycastLayer;
			ray.origin = xa.glx;
			//Debug.DrawLine(ray.origin, ray.GetPoint(1.5f), Color.red);
			if (Physics.Raycast(ray, out hit, 1.5f, mask) != true)
			{
				flipAround();
			}

		}


	}

	public void updateSpeedAndVisuals()
	{
		if (flipped)
		{
			xa.glx = transform.localScale;
			xa.glx.x = -1;
			transform.localScale = xa.glx;
			currentSpeed = speed;
		}
		if (!flipped)
		{
			xa.glx = transform.localScale;
			xa.glx.x = 1;
			transform.localScale = xa.glx;
			currentSpeed = -speed;
		}
	}

	public void flipAround()
	{
		if (flipped) { flipped = false; }
		else { flipped = true; }
		updateSpeedAndVisuals();
	}


	float counter = 0;
	int aniFrameX = 0;
	int aniIndex = 0;

	void animateChargingGoomba()
	{
		if (dontThinkJustCharge > 0)
		{
			counter += 10 * fa.deltaTime;
			if (counter >= 0.5f)
			{
				counter = 0;
				aniFrameX++;
				aniIndex++;
				if (aniIndex >= 6)
				{
					aniFrameX = 0;
					aniIndex = 0;
				}
				setTexture(aniFrameX, 3);
			}
		}
		else
		{
			counter += 10 * fa.deltaTime;
			if (counter >= 1.2f)
			{
				counter = 0;
				aniFrameX++;
				aniIndex++;
				if (aniIndex >= 4)
				{
					aniFrameX = 0;
					aniIndex = 0;
				}
				setTexture(aniFrameX, 2);
			}
		}
	}

	void animateImp()
	{
		int walkCycleTotalFrames = 4;
		int yRow = 4;
		int yRowForWalking = 4;

		if (animateBigImp) { yRow = 0; yRowForWalking = 0; walkCycleTotalFrames = 4; }
		if (animateHammerHead) { yRow = 1; yRowForWalking = 1; walkCycleTotalFrames = 4; }
		if (animateLaserBack) { yRow = 2; yRowForWalking = 2; walkCycleTotalFrames = 4; }

		if (impAniState == 0)//walk
		{
			counter += 10 * fa.deltaTime;
			if (counter >= 0.5f)
			{
				counter = 0;
				aniFrameX++;
				aniIndex++;
				if (aniIndex >= walkCycleTotalFrames)
				{
					aniFrameX = 0;
					aniIndex = 0;
				}
				setTexture(aniFrameX, yRowForWalking);
			}
		}
		if (impAniState == 1)//fall
		{
			setTexture(5, yRow);
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
		x2 = (0.125f) * v1;
		y2 = 1 - (((0.125f) * v2) + (0.125f));

		puppet.GetComponent<Renderer>().material.mainTextureScale = new Vector2(x1, y1);
		puppet.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(x2, y2);
	}
}
