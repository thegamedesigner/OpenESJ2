using UnityEngine;
using System.Collections;

public class HomingMissileScript : MonoBehaviour
{
	public float speed = 0;
	public float turnSpeed = 0;
	public bool forceTarget = false;
	public Vector3 forcedTarget = Vector3.zero;
	public bool useForceOffset = false;
	public Vector2 forceOffset = Vector2.zero;
	public bool antiPopeMissile = false;
	public GameObject popeExplo = null;
	public GameObject popeWreckageExplo = null;

	Vector3 target = Vector3.zero;
	Vector3 offset = Vector3.zero;
	Vector3 vec1;
	Vector3 vec2;
	Vector3 vec3;
	Vector3 ang1;
	Vector3 ang2;
	Vector3 ang3;
	float result1;
	float result2;
	float result3;

	float counter = 0;
	float resetOffsetDelay = 15;
	Vector2 offsetAmount = Vector2.zero;
 


	void Start()
	{
		//play missile sound
		Setup.playSound(Setup.snds.Missile);
	


		offsetAmount.x = 2;
		offsetAmount.y = 2;

		if (useForceOffset)
		{
			offsetAmount = forceOffset;
		}

		offset.x = Random.Range(-offsetAmount.x, offsetAmount.y);
		offset.y = Random.Range(-offsetAmount.x, offsetAmount.y);
	}

	void Update()
	{
		if (antiPopeMissile)
		{
			turnSpeed += 10 * fa.deltaTime;
		}
		counter += 10 * fa.deltaTime;
		if (counter > resetOffsetDelay)
		{
			counter = 0;
			offset.x = Random.Range(-offsetAmount.x, offsetAmount.y);
			offset.y = Random.Range(-offsetAmount.x, offsetAmount.y);
		}

		//move forward
		transform.Translate(speed * fa.deltaTime, 0, 0);

		if (antiPopeMissile)
		{
			if (xa.pope)
			{
				target = xa.pope.transform.position;
				target.z = transform.position.z;

				if (Vector3.Distance(target, transform.position) < 1f && !PopeIgnoreMissilesHack.ignoreMissiles)
				{
					xa.popeHits++;
					//Setup.GC_DebugLog("PopeHits: " + xa.popeHits);
					xa.tempobj = (GameObject)(Instantiate(popeExplo, transform.position, xa.null_quat));
					xa.tempobj = (GameObject)(Instantiate(popeWreckageExplo, target, xa.null_quat));
					xa.tempobj.transform.parent = xa.pope.transform;
					xa.pope.transform.Translate(Vector3.zero);
					Destroy(this.gameObject);
					this.enabled = false;
					return;
				}
			}
		}
		else
		{
			if (forceTarget)
			{
				target = forcedTarget;
				target.z = transform.position.z;
			}
			else
			{
				//if player
				if (xa.player)
				{
					target = xa.player.transform.position;
					target += offset;
					target.z = transform.position.z;
				}
			}
		}
		ang1 = transform.localEulerAngles;
		ang1.z += 4;
		vec1 = Setup.projectVec(transform.position, ang1, 5, -Vector3.left);
		ang2 = transform.localEulerAngles;
		ang2.z -= 4;
		vec2 = Setup.projectVec(transform.position, ang2, 5, -Vector3.left);
		ang3 = transform.localEulerAngles;
		vec3 = Setup.projectVec(transform.position, ang3, 5, -Vector3.left);

		result1 = Vector3.Distance(target, vec1);
		result2 = Vector3.Distance(target, vec2);
		result3 = Vector3.Distance(target, vec3);

		if (result3 < result1 && result3 < result2)
		{
			//deadzone of half four on either side of center (-2,2)
		}
		else
		{
			Vector3 glx;
			if (result1 < result2)
			{
				glx = transform.localEulerAngles;
				glx.z += turnSpeed * fa.deltaTime;
				transform.localEulerAngles = glx;
			}
			else
			{
				glx = transform.localEulerAngles;
				glx.z -= turnSpeed * fa.deltaTime;
				transform.localEulerAngles = glx;
			}
		}

		//Debug.DrawLine(transform.position, vec1, Color.yellow);
		// Debug.DrawLine(transform.position, vec2, Color.yellow);
		// Debug.DrawLine(transform.position, vec3, Color.yellow);

	}
}
