using UnityEngine;

public class GoombaScript : MonoBehaviour
{
	public GameObject puppet;
	public GameObject deathExplo;
	public bool move = false;
	public float speed = 0;
	float currentSpeed = 0;

	public bool bounceOffWalls = false;
	public bool checkForFloors = false;
	public float footLength = 0;

	public float boxSize = 0.6f;
	public float boxHalf = 0.3f;
	public float boxHeight = 1.5f;
	public float boxHeightHalf = 0.75f;
	//float blockSize = 1f;
	float blockHalf = 0.5f;

	public bool loopAni1 = false;
	public float ani1Spd = 0;
	public float ani1TimePerFrame = 0;
	public int ani1Frames = 0;

	bool bumped = false;
	bool flipped = false;
	float dontFlipAgainDelay = 0;

	float aniProgress = 0;
	int aniStage = 0;

	HealthScript hpScript;
	bool dead;

	int frameCount = 0;//for things that only need to check once every five frames or so

	void Start()
	{
		hpScript = this.gameObject.GetComponent<HealthScript>();
		frameCount = (int)(Random.Range(0, 5));

	}

	void Update()
	{
		//check if dead
		if (!dead)
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
		else
		{
			//create a cool effect and destroy me

			xa.glx = transform.position;
			xa.glx.z = xa.GetLayer(xa.layers.Explo1);
			xa.tempobj = (GameObject)(Instantiate(deathExplo, xa.glx, transform.localRotation));
			xa.tempobj.transform.parent = xa.createdObjects.transform;

			Destroy(this.gameObject);
		}
	}



	void handleMe()
	{
		frameCount++;
		if (frameCount >= 5) { frameCount = 0; }
		//move
		if (move)
		{
			currentSpeed = -speed;
			if (!flipped) { currentSpeed = -speed; }
			else { currentSpeed = speed; }
			transform.Translate(currentSpeed * fa.deltaTime, 0, 0);
		}

		dontFlipAgainDelay -= 10 * fa.deltaTime;

		//check for floors
		if (checkForFloors && frameCount == 0)
		{
			if (dontFlipAgainDelay <= 0)
            {
                LayerMask mask = 1 << 19;
				RaycastHit hit;
				Ray ray = new Ray();
				bool trigger = false;
				ray.direction = new Vector3(0, -1, 0);
                ray.origin = new Vector3(transform.position.x, transform.position.y, xa.GetLayer(xa.layers.RaycastLayer));
				Debug.DrawLine(ray.origin, ray.GetPoint(footLength), Color.green);
				if (Physics.Raycast(ray, out hit, footLength, mask))
				{
						trigger = true;
				}
				if (trigger == false)
				{
					dontFlipAgainDelay = 10;
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
				//GameObject[] gos;
				//gos = GameObject.FindGameObjectsWithTag("solidThing");
				// this could be bad if ai is going on off screen...
				//List<GameObject> gos = xa.onScreenSolid;
				GameObject[] gos = xa.onScreenSolid;

				foreach (GameObject go in gos)
				{
					if ((transform.position.x + boxHalf) > (go.transform.position.x - blockHalf) &&
						(transform.position.x - boxHalf) < (go.transform.position.x + blockHalf) &&
						(transform.position.y + boxHeightHalf) > (go.transform.position.y - blockHalf) &&
						(transform.position.y - boxHeightHalf) < (go.transform.position.y + blockHalf))
					{
						bumped = true;
					}
				}

				if (bumped)
				{
					dontFlipAgainDelay = 10;
					bumped = false;
					flipAround();
				}
			}
		}

		animateMe();
	}

	void animateMe()
	{
		if (loopAni1)
		{
			aniProgress += ani1Spd * fa.deltaTime;
			if (aniProgress > ani1TimePerFrame)
			{
				aniProgress = 0;
				aniStage++;

				if (aniStage >= ani1Frames) { aniStage = 0; }
			}
			setTexture(aniStage, 0);
		}
	}

	void flipAround()
	{
		if (flipped) { flipped = false; }
		else { flipped = true; }
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

		if (!flipped)
		{
			puppet.GetComponent<Renderer>().material.mainTextureScale = new Vector2(x1, y1);
			puppet.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(x2, y2);
		}
		else
		{
			puppet.GetComponent<Renderer>().material.mainTextureScale = new Vector2(-x1, y1);
			puppet.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(x2 + 0.125f, y2);
		}
	}

}
