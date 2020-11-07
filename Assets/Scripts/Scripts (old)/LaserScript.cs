using UnityEngine;

public class LaserScript : MonoBehaviour
{
	public GameObject airswordHitbox;
	public GameObject hitEffect = null;
	public GameObject checkNegOnThisGO = null;
	public bool reverseIfScaleIsNegOnGO = false;
	public bool superLongLaser = false;
	public bool zeroDamageLaser = false;
	public bool doesntHitBLocksLaser = false;
	RaycastHit hit;
	Ray ray = new Ray();
	float dist = 0;
	float distResult = 0;
	float var1 = 0;
	LayerMask mask = 1 << 19;
	void Start()
	{
		if (hitEffect)
		{
			hitEffect.transform.parent = null;
		}
	}

	void Update()
	{
		dist = 45;
		if (superLongLaser)
		{
			dist = 500;
		}
		distResult = dist;
		//check if beam is hitting anything
		xa.glx = transform.position;
		xa.glx.z = xa.GetLayer(xa.layers.RaycastLayer);
		ray.origin = xa.glx;
		ray.direction = transform.up;
		if (reverseIfScaleIsNegOnGO)
		{
			if (checkNegOnThisGO.transform.localScale.x < 0)
			{
				ray.direction = -transform.up;
			}
		}

		if (Physics.Raycast(ray, out hit, dist, mask))
		{
			distResult = hit.distance;

			//ITS THAT THE NEW HITBOXES CREATED ON START FOR NOT STOMPING THROUGH SHIT BLOCK THESE RAYCASTS
			//USE A WEIRD LAYER?
			//Debug.DrawLine(ray.origin, hit.point, Color.red);
			//Debug.DrawLine(ray.origin, ray.GetPoint(hit.distance), Color.red, 0.3f);
			// Debug.DrawLine(ray.origin, hit.point, Color.red);
			xa.glx = hit.point;
			xa.glx.z = transform.position.z - 1;
			if (hitEffect)
			{
				hitEffect.transform.position = xa.glx;
			}
		}

		if (airswordHitbox != null)
		{
			airswordHitbox.transform.position = transform.position;
			airswordHitbox.transform.SetZ(xa.GetLayer(xa.layers.RaycastLayer));
			airswordHitbox.transform.SetScaleY(distResult);
			airswordHitbox.transform.LocalSetY(-distResult * 0.5f);
		}

		if (xa.playerHitBox && xa.player && !zeroDamageLaser)
		{
			if (!xa.playerDead)
			{
				//Debug.Log("Got here 1");
				if (doesntHitBLocksLaser) { var1 = dist; }
				else { var1 = distResult; }

				xa.glx = transform.position;
				xa.glx.z = xa.GetLayer(xa.layers.PlayerAndBlocks);
				ray.origin = xa.glx;
				//Debug.DrawLine(ray.origin, ray.GetPoint(var1), Color.green);
				if (xa.playerHitBox.GetComponent<Collider>().Raycast(ray, out hit, var1))
				{
					//Debug.Log("Got here 2");
					HealthScript script = null;
					script = xa.player.GetComponent<HealthScript>();
					if (script && !xa.cheat_invinciblePlayer)
					{
						script.health = 0;
					}
				}
			}
		}

		xa.glx = transform.localScale;
		if (!doesntHitBLocksLaser) { xa.glx.y = distResult * 6.2f; } else { xa.glx.y = dist * 6.2f; }
		transform.localScale = xa.glx;

		if (distResult == dist || doesntHitBLocksLaser)//didn't hit anything
		{
			xa.glx = ray.GetPoint(dist);
			xa.glx.z = transform.position.z - 1;
			if (hitEffect)
			{
				hitEffect.transform.position = xa.glx;
			}
		}
	}
}
