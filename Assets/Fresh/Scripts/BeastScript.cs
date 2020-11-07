using UnityEngine;

public class BeastScript : MonoBehaviour
{
	public bool checkForWalls = false;
	public bool destroyWhenFarAwayFromPlayer = true;
	public HealthScript healthScript;
	public GameObject checkForGroundPoint;
	public GameObject checkForWallsPoint;
	public GameObject groundDistObj;
	public GameObject wallsDistObj;
	public float moveSpeed = 5;

	public bool unicorn = false;
	public float unicornWalkSpd = 5;
	public float unicornChargeSpd = 15;
	public int unicornWalkAni = 2;
	public int unicornChargeAni = 1;
	bool charging = false;
	public GameObject impactDust = null;

	float checkForGroundDist = 0;
	float checkForWallsDist = 0;

	Ray ray;
	RaycastHit hit;
	LayerMask mask;
	bool falling = false;

	Vector3 vel = Vector3.zero;
	float gravity = 0.1f;



	void Start()
	{
		checkForGroundDist = Vector3.Distance(checkForGroundPoint.transform.position, groundDistObj.transform.position);

		checkForWallsDist = Vector3.Distance(checkForWallsPoint.transform.position, wallsDistObj.transform.position);

		if(unicorn) {gravity = 0.3f; }
	}

	void Update()
	{
		if (healthScript != null && healthScript.health <= 0) { return; }

		if (unicorn)
		{
			if (CheckRaycast() || charging)
			{
				charging = true;
				moveSpeed = unicornChargeSpd;
			}
			else
			{

				moveSpeed = unicornWalkSpd;

			}
		}

		//If the player is far enough away from me, destroy me (as I'm probably a spawned monster)
		if (destroyWhenFarAwayFromPlayer)
		{
			if (xa.player != null) { if (Vector2.Distance(xa.player.transform.position, transform.position) > 50) { Destroy(this.gameObject); } }
		}

		//Check for ground
		ray.origin = new Vector3(checkForGroundPoint.transform.position.x, checkForGroundPoint.transform.position.y, xa.GetLayer(xa.layers.RaycastLayer));
		ray.direction = new Vector3(0, -1, 0);
		mask = 1 << 19;
		//Debug.DrawLine(new Vector3(ray.origin.x, ray.origin.y, 15), ray.GetPoint(checkForGroundDist), Color.yellow);
		if (Physics.Raycast(ray, out hit, checkForGroundDist, mask))
		{
			transform.SetY(hit.point.y + checkForGroundDist);
			falling = false;
		}
		else
		{
			falling = true;
		}
		
		//Check for walls
		if (checkForWalls)
		{
			ray.origin = new Vector3(checkForWallsPoint.transform.position.x, checkForWallsPoint.transform.position.y, xa.GetLayer(xa.layers.RaycastLayer));
			ray.direction = new Vector3(-transform.localScale.x, 0, 0);
			mask = 1 << 19 | 1 << 25;
			//Debug.DrawLine(new Vector3(ray.origin.x, ray.origin.y, 15), ray.GetPoint(checkForWallsDist), Color.yellow);
			if (Physics.Raycast(ray, out hit, checkForWallsDist, mask))
			{
				transform.SetScaleX(-transform.localScale.x);

				if (charging)
				{
					charging = false;
					Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.RockImpact);
					ScreenShakeCamera.Screenshake(0.2f, 0.15f, ScreenShakeCamera.ScreenshakeMethod.Basic);


					Vector3 pos = hit.point;
					pos.y += 0.19f;
					pos.z = xa.GetLayer(xa.layers.Explo1);
					Vector3 dir = new Vector3(0, 0, 0);
					if (transform.localScale.x == 1) { dir = new Vector3(0, -90, 90); }
					if (transform.localScale.x == -1) { dir = new Vector3(-180, -90, 90); }

					GameObject tempGo = Instantiate(impactDust, pos, Quaternion.Euler(dir));
					tempGo.transform.SetParent(xa.createdObjects.transform);

				}
			}
		}


















		//is the ground under me?
		if (falling)
		{
			vel.y -= gravity * Time.deltaTime * fa.pausedFloat;
			if (vel.y < -8) { vel.y = -8; }

			//vel.x += 0.2f * Time.deltaTime;
			//if(vel.x > 0) {vel.x = 0; }

			vel.x = 0;
		}
		else
		{
			vel.x = -moveSpeed * Time.deltaTime * fa.pausedFloat * transform.localScale.x;
			vel.y = 0;
		}

		transform.Translate(vel);

	}

	bool CheckRaycast()
	{
		float hitBlocksDist = 20;

		//raycast for blocks
		LayerMask mask = 1 << 19 | 1 << 25;//Only hits hitboxes on the NovaBlock layer & the solidForUnicorns layer
		Ray ray = new Ray();
		RaycastHit hit = new RaycastHit();
		float raycastLayer = xa.GetLayer(xa.layers.RaycastLayer);
		ray.origin = new Vector3(transform.position.x, transform.position.y, raycastLayer);
		ray.direction = new Vector3(-transform.localScale.x, 0, 0);

		if (Physics.Raycast(ray, out hit, 20, mask))
		{
			hitBlocksDist = Vector2.Distance(ray.origin, hit.point);
		}


		//raycast for player
		mask = 1 << 11;//Only hits hitboxes on the player's layer
		ray = new Ray();
		hit = new RaycastHit();
		ray.origin = new Vector3(transform.position.x, transform.position.y, 110);
		ray.direction = new Vector3(-transform.localScale.x, 0, 0);

		if (Physics.Raycast(ray, out hit, hitBlocksDist, mask))
		{
			return true;
		}
		return false;



	}
}
