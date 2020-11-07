using UnityEngine;

public class MomScript : MonoBehaviour
{
	public FreshAni aniScript;
	[HideInInspector]
	public bool carried = false;
	[HideInInspector]
	public bool flying = false;
	Vector3 vel = Vector3.zero;
	float gravity = 0.03f;
	bool oldOnGround = true;
	bool onGround = false;
	int myDir = 1;

	void Start()
	{

	}

	void Update()
	{
		flying = false;
		if (vel.y != 0 || vel.x > 0.5f || vel.x < -0.5f) { flying = true; }

	}

	void FixedUpdate()
	{
		if(fa.paused) {return; }
		if (!carried)
		{
			vel *= 0.9f;
			vel.y -= gravity;


			if (onGround)
			{
				if (aniScript.currentAniId != 4)
				{
					aniScript.PlayAnimation(4);
				}
				vel.x = 0.05f * myDir;
			}


			onGround = false;

			for (int i = 0; i < 10; i++)
			{
				checkForItems(1.2f, transform.position);
				float raycastLayer = xa.GetLayer(xa.layers.RaycastLayer);

				Ray ray = new Ray();
				RaycastHit hit;
				LayerMask mask = 1 << 19;
				float foot = 1;
				float height = 1.4f;
				float width = 1.2f;
				float verySmallAmount = 0.001f;

				Vector3 endOfRay;
				//check for X walls
				int dirX = 1;
				if (vel.x < 0) { dirX = -1; }
				ray.direction = new Vector3(dirX, 0, 0);
				ray.origin = new Vector3(transform.position.x, transform.position.y + (height * 0.5f), raycastLayer);
				endOfRay = ray.GetPoint(width * 0.5f);
				endOfRay.z = transform.position.z;
				Debug.DrawLine(new Vector3(ray.origin.x, ray.origin.y, transform.position.z), endOfRay, Color.red);
				if (Physics.Raycast(ray, out hit, (width * 0.5f), mask))
				{
					vel.x = 0;
					if (dirX == 1)
					{
						transform.SetX(hit.point.x - ((width * 0.5f) + verySmallAmount));
					}
					else
					{
						transform.SetX(hit.point.x + (width * 0.5f) + verySmallAmount);
					}
				}
				ray.origin = new Vector3(transform.position.x, transform.position.y - (height * 0.5f), raycastLayer);
				endOfRay = ray.GetPoint(width * 0.5f);
				endOfRay.z = transform.position.z;
				Debug.DrawLine(new Vector3(ray.origin.x, ray.origin.y, transform.position.z), endOfRay, Color.red);
				if (Physics.Raycast(ray, out hit, (width * 0.5f), mask))
				{
					vel.x = 0;
					if (dirX == 1)
					{
						transform.SetX(hit.point.x - ((width * 0.5f) + verySmallAmount));
					}
					else
					{
						transform.SetX(hit.point.x + (width * 0.5f) + verySmallAmount);
					}
				}


				//check for Y roofs
				ray.direction = new Vector3(0, 1, 0);
				ray.origin = new Vector3(transform.position.x + (width * 0.5f), transform.position.y, raycastLayer);
				endOfRay = ray.GetPoint(height * 0.5f);
				endOfRay.z = transform.position.z;
				Debug.DrawLine(new Vector3(ray.origin.x, ray.origin.y, transform.position.z), endOfRay, Color.red);
				if (Physics.Raycast(ray, out hit, (height * 0.5f), mask))
				{
					vel.y = 0;
					transform.SetY(hit.point.y - ((height * 0.5f) + verySmallAmount));
				}
				ray.origin = new Vector3(transform.position.x - (width * 0.5f), transform.position.y, raycastLayer);
				endOfRay = ray.GetPoint(height * 0.5f);
				endOfRay.z = transform.position.z;
				Debug.DrawLine(new Vector3(ray.origin.x, ray.origin.y, transform.position.z), endOfRay, Color.red);
				if (Physics.Raycast(ray, out hit, (height * 0.5f), mask))
				{
					vel.y = 0;
					transform.SetY(hit.point.y - ((height * 0.5f) + verySmallAmount));
				}

				ray.direction = new Vector3(0, -1, 0);
				ray.origin = new Vector3(transform.position.x - (width * 0.5f), transform.position.y, raycastLayer);
				endOfRay = ray.GetPoint(height * 0.5f);
				endOfRay.z = transform.position.z;
				Debug.DrawLine(new Vector3(ray.origin.x, ray.origin.y, transform.position.z), endOfRay, Color.red);
				if (Physics.Raycast(ray, out hit, foot, mask))
				{
					onGround = true;
					transform.SetY(hit.point.y + (height * 0.5f) + verySmallAmount);
				}
				if (!onGround)
				{
					ray.origin = new Vector3(transform.position.x + (width * 0.5f), transform.position.y, raycastLayer);
					endOfRay = ray.GetPoint(height * 0.5f);
					endOfRay.z = transform.position.z;
					Debug.DrawLine(new Vector3(ray.origin.x, ray.origin.y, transform.position.z), endOfRay, Color.red);
					if (Physics.Raycast(ray, out hit, foot, mask))
					{
						onGround = true;
						transform.SetY(hit.point.y + (height * 0.5f) + verySmallAmount);
					}
				}
				if (onGround)
				{
					if (vel.y < 0) { vel.y = 0; }
				}

				transform.position += vel * 0.1f;

			}

			//Called once, when onGround changes
			if (oldOnGround != onGround)
			{
				if (onGround)
				{
					aniScript.PlayAnimation(1);
				}
				if (!onGround)
				{
					aniScript.PlayAnimation(3);
				}
				oldOnGround = onGround;
			}


		}
		else
		{
			vel = Vector3.zero;

		}
	}

	public void ThrowMom()
	{
		vel.y = 1.4f;
		vel.x = (1.4f * xa.playerDir);
		myDir = xa.playerDir;
		onGround = false;
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
					if (itemScript.type == "coin" ||
					itemScript.type == "key")
					{
						useFoundItem(go);
					}

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
		}
	}


}
