using UnityEngine;
using System.Collections;

public class AntiGenericBossMissileScript : MonoBehaviour
{
	public float speed = 0;
	float baseSpeed = 0;
	public float turnSpeed = 0;
	float baseTurnSpeed = 0;
	public float exploRange = 0;
	public float damage = 0;

	Vector3 target = Vector3.zero;
	//Vector3 offset = Vector3.zero;
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
	float resetOffsetDelay = 3;
	Vector2 offsetAmount = Vector2.zero;



	void Start()
	{
		Setup.playSound(Setup.snds.MissileLong);
		offsetAmount.x = 2;
		offsetAmount.y = 2;
		baseSpeed = 0.5f;
		//speed *= Random.Range(.5f, 1.5f);
		//baseSpeed = speed;
	   // turnSpeed *= Random.Range(.8f, 1.2f);
	   // baseTurnSpeed = turnSpeed;
	}

	void Update()
	{
		if (Vector3.Distance(target, transform.position) < exploRange)
		{
			HealthScript healthScript = this.gameObject.GetComponent<HealthScript>();
			healthScript.health = 0;
			xa.genericBossHealth -= damage;
		}
		baseSpeed += (0.1f + baseSpeed) * fa.deltaTime;
		if (baseSpeed > speed) { baseSpeed = speed; }

		if (baseSpeed > 1)
		{
			baseTurnSpeed += (4 + baseTurnSpeed) * fa.deltaTime;
			if (baseTurnSpeed > turnSpeed) { baseTurnSpeed = turnSpeed; }
		}


		//Setup.GC_DebugLog(baseSpeed);
		counter += 10 * fa.deltaTime;
		if (counter > resetOffsetDelay)
		{
			counter = 0;
			findNearestBossTarget();
		}
		//move forward
		transform.Translate(baseSpeed * fa.deltaTime, 0, 0);


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
			if (result1 < result2)
			{
				xa.glx = transform.localEulerAngles;
				xa.glx.z += baseTurnSpeed * fa.deltaTime;
				transform.localEulerAngles = xa.glx;
			}
			else
			{
				xa.glx = transform.localEulerAngles;
				xa.glx.z -= baseTurnSpeed * fa.deltaTime;
				transform.localEulerAngles = xa.glx;
			}
		}

	}

	void findNearestBossTarget()
	{
		float closestDist = 9999;
		float dist;
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("genericBossHeart");

		foreach (GameObject go in gos)
		{
			xa.glx = transform.position;
			xa.glx.z = go.transform.position.z;
			dist = Vector3.Distance(go.transform.position, xa.glx);
			if (dist < closestDist)
			{
				closestDist = dist;
				target.x = go.transform.position.x;
				target.y = go.transform.position.y;
				target.z = transform.position.z;
			}
		}
	}
}
