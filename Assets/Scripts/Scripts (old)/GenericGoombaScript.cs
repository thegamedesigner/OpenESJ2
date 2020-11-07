using UnityEngine;

public class GenericGoombaScript : MonoBehaviour
{
	public GameObject puppet = null;
	public GameObject animateThis = null;
	public GameObject traceDownPoint = null;
	public GameObject fallOffPoint = null;
	public GameObject feetLevelObject = null;
	public GameObject traceForwardObject = null;
	public int walkAni = 0;
	public int fallAni = 0;
	public int standAni = 0;
    AnimationScript_Generic animationScript = null;
	public float pauseAmount = 0;
	public float fallSpeed = 0;
	public float speed = 0;
	public bool move = false;
	public bool hasHealthScript = true;
	public bool bounceOffWalls = false;
	public bool checkForFloors = false;
	public bool pauseOnWallBump = false;
	public bool flipOnStart = false;
	HealthScript hpScript;
	RaycastHit hit;
	RaycastHit hit2;
	Ray ray = new Ray();
	Ray ray2 = new Ray();
	float currentSpeed = 0.0f;
	float legHeight = 0.0f;
	float pauseTimeSet = 0.0f;
	float checkForWallsDist = 0.52f;
	float checkForFloorsDist = 0;
	int aniState = 0;
	int oldAniState = 0;
	bool paused = false;
	bool pausedBreakLoop = false;
	bool sentAnimationMessage = false;
	bool flipped = false;
	bool dead = false;
	bool onGround = false;

	void Start()
	{
		if (flipOnStart) { flipAround(); }

        if (hasHealthScript)
        {
            hpScript = this.gameObject.GetComponent<HealthScript>();
        }

        animationScript = animateThis.GetComponent<AnimationScript_Generic>();

		legHeight = -feetLevelObject.transform.localPosition.y;
		checkForFloorsDist = legHeight + 0.4f;
		//Setup.GC_DebugLog("Leg height " + legHeight);
		if (traceForwardObject) { checkForWallsDist = Mathf.Abs(traceForwardObject.transform.localPosition.x); }
		updateSpeedAndVisuals();
	}

	void Update()
	{
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


		animateMonster();
	}

	void animateMonster()
	{
		if (oldAniState != aniState) { oldAniState = aniState; sentAnimationMessage = false; }
		if (!sentAnimationMessage)
		{
			if (aniState == 0)//walk
			{
                animationScript.playAnimation(walkAni);
			}
			if (aniState == 1)//fall
            {
                animationScript.playAnimation(fallAni);
			}
			if (aniState == 2)//stand
            {
                animationScript.playAnimation(standAni);
			}
			sentAnimationMessage = true;
		}

	}

	void handleMe()
	{
		float playerAndBlocksLayer = xa.GetLayer(xa.layers.PlayerAndBlocks);
		//am I on the ground?
		onGround = false;
		ray.direction = new Vector3(0, -1, 0);
		ray2.direction = new Vector3(0, -1, 0);
		xa.glx = fallOffPoint.transform.position;
		xa.glx.z = playerAndBlocksLayer;
		ray.origin = xa.glx;

		xa.glx = transform.position;
		xa.glx.z = playerAndBlocksLayer;
		ray2.origin = xa.glx;// transform.position;

		Debug.DrawLine(ray.origin, ray.GetPoint(legHeight), Color.cyan);
		Debug.DrawLine(ray2.origin, ray2.GetPoint(legHeight), Color.green);
		if (Physics.Raycast(ray, out hit, legHeight + 0.1f, 11) == true)
		{
			if (hit.collider.gameObject.tag == "solidThing")
			{
				onGround = true;
				xa.glx = transform.position;
				xa.glx.y = hit.point.y + legHeight;
				transform.position = xa.glx;
			}
		}
		if (Physics.Raycast(ray2, out hit2, legHeight + 0.1f, 11) == true)
		{
			if (hit2.collider.gameObject.tag == "solidThing")
			{
				onGround = true;
				xa.glx = transform.position;
				xa.glx.y = hit2.point.y + legHeight;
				transform.position = xa.glx;
			}
		}


		if (onGround)
		{
			//walk
			aniState = 0;
			move = true;

		}
		else
		{
			//fall
			aniState = 1;
			move = false;
			transform.Translate(0, fallSpeed * fa.deltaTime, 0);
		}

		if (pauseOnWallBump && fa.time < (pauseTimeSet + pauseAmount) && pauseTimeSet != 0)
		{
			paused = true;
			aniState = 2;
		}
		else
		{
			paused = false;
			pauseTimeSet = 0;
		}


		//move
		if (move && !paused)
		{
			updateSpeedAndVisuals();
			transform.Translate(currentSpeed * fa.deltaTime, 0, 0);
		}

		//bounce (imp)
		if (bounceOffWalls && !paused)
		{

			if (flipped) { ray.direction = new Vector3(1, 0, 0); }
			if (!flipped) { ray.direction = new Vector3(-1, 0, 0); }
			xa.glx = transform.position;
			xa.glx.z = playerAndBlocksLayer;
			ray.origin = xa.glx;
			//Debug.DrawLine(ray.origin, ray.GetPoint(0.52f), Color.green);
			if (Physics.Raycast(ray, out hit, checkForWallsDist, 11) == true)
			{
				if (hit.collider.gameObject.tag == "solidThing")
				{
					if (pauseOnWallBump && !pausedBreakLoop)
					{
						pauseTimeSet = fa.time;
						paused = true;
						pausedBreakLoop = true;
					}
					else
					{
						pausedBreakLoop = false;
						flipAround();
					}
				}
			}
		}


		//check for floors (imp)
		if (checkForFloors && onGround)
		{
			ray.direction = new Vector3(0, -1, 0);
			xa.glx = traceDownPoint.transform.position;
			xa.glx.z = playerAndBlocksLayer;
			ray.origin = xa.glx;
			Debug.DrawLine(ray.origin, ray.GetPoint(checkForFloorsDist), Color.red);
			if (Physics.Raycast(ray, out hit, checkForFloorsDist, 11) != true)
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
}
