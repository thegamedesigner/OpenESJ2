using UnityEngine;

public class FreshHomingMissileScript : MonoBehaviour
{
	public bool foreverMissile = false;
	public float foreverResetX = -11;//How far back the missile snaps on respawn
	public float foreverResetY = 10;//How far back the missile snaps on respawn
	bool foreverSnap = false;
	public bool slowMissile = false;
	public GameObject flickeringObj;
	public HealthScript health;
	public float lifespanInSeconds = 5;
	public float speed = 10;
	public float turnSpeed = 70;
	public float lockToThisZ = 25;
	Vector3 target = Vector3.zero;
	float baseTurnSpeed = 0;
	float baseSpeed = 0;
	float flickerCounter = 0;
	float timeSet = 0;
	bool doneScalingIn = false;
	float flickerSpeed = 10;

	void Start()
	{
		if (slowMissile)
		{
			lifespanInSeconds = 8;
			speed = 7;
			turnSpeed = 55;
		}

		timeSet = fa.time;
		baseSpeed = speed;
		baseTurnSpeed = turnSpeed;

		Vector3 scale = transform.localScale;
		scale.x = 0.01f;
		scale.y = 0.01f;
		transform.localScale = scale;
	}

	void Update()
	{
		if (foreverMissile && !foreverSnap)
		{
			if (xa.player != null)
			{
				transform.SetPos(xa.player.transform.position.x + foreverResetX, xa.player.transform.position.y + foreverResetY, lockToThisZ);
				foreverSnap = true;
			}
			else
			{
				return;//wait for player to exist
			}

		}
		if (lockToThisZ != -999)
		{
			transform.SetZ(lockToThisZ);
		}
		transform.Translate(speed * fa.deltaTime, 0.0f, 0.0f);

		if (!doneScalingIn)
		{
			Vector3 scale = transform.localScale;
			scale.x += 3.0f * fa.deltaTime;
			scale.y += 3.0f * fa.deltaTime;
			scale.x = Mathf.Clamp01(scale.x);
			scale.y = Mathf.Clamp01(scale.y);

			if (scale.x >= 1.0f && scale.y >= 1.0f)
			{
				doneScalingIn = true;
			}
			transform.localScale = scale;
		}

		if (xa.player)
		{
			target = xa.player.transform.position;
			target.z = transform.position.z;
		}

		Vector3 straight = transform.localEulerAngles;
		Vector3 right = straight;
		Vector3 left = straight;
		right.z += 4;
		left.z -= 4;

		Vector3 projStraight = Setup.projectVec(transform.position, straight, 5, Vector3.right);
		Vector3 projRight = Setup.projectVec(transform.position, right, 5, Vector3.right);
		Vector3 projLeft = Setup.projectVec(transform.position, left, 5, Vector3.right);

		float targetDistStraight = Vector3.Distance(target, projStraight);
		float targetDistRight = Vector3.Distance(target, projRight);
		float targetDistLeft = Vector3.Distance(target, projLeft);

		float targetDistance = Vector3.Distance(target, transform.position);
		if (targetDistance > 6)
		{
			turnSpeed = baseTurnSpeed * ((targetDistance - 3.0f) / 3.0f);
		}
		else
		{
			turnSpeed = baseTurnSpeed;
			speed = baseSpeed;
		}

		Vector3 currentHeading = transform.localEulerAngles;
		if (targetDistStraight > targetDistRight || targetDistStraight > targetDistLeft)
		{
			if (targetDistRight < targetDistLeft)
			{
				currentHeading.z += turnSpeed * fa.deltaTime;
			}
			else
			{
				currentHeading.z -= turnSpeed * fa.deltaTime;
			}
		}
		transform.localEulerAngles = currentHeading;

		if (!foreverMissile)
		{
			if (fa.time > (timeSet + lifespanInSeconds - 1))
			{
				updateFlicking();
			}
			if (fa.time > (timeSet + lifespanInSeconds))
			{
				health.health = 0;
			}
		}
	}

	void updateFlicking()
	{
		flickerCounter += flickerSpeed * fa.deltaTime;
		//loops
		if (flickerCounter > 1.4f)
		{
			flickerCounter = 0;
		}

		// flicker
		flickeringObj.GetComponent<Renderer>().enabled = !(flickerCounter > 0.7f);
	}
}
