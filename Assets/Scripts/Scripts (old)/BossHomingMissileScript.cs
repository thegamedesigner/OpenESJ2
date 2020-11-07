using UnityEngine;
using System.Collections;

public class BossHomingMissileScript : MonoBehaviour
{
	public Vector3 forcedTarget = Vector3.zero;
	public Vector2 forceOffset  = Vector2.zero;
	public float speed          = 0.0f;
	public float turnSpeed      = 0.0f;
	public bool forceTarget     = false;
	public bool useForceOffset  = false;
	public bool nonRandom       = false;
    public bool dontTurnLessOnGettingCloser = false;
	Vector3 target              = Vector3.zero;
	Vector3 offset              = Vector3.zero;
	Vector3 vec1                = Vector3.zero;
	Vector3 vec2                = Vector3.zero;
	Vector3 vec3                = Vector3.zero;
	Vector3 ang1                = Vector3.zero;
	Vector3 ang2                = Vector3.zero;
	Vector3 ang3                = Vector3.zero;
	Vector2 offsetAmount        = Vector2.zero;
	float baseSpeed             = 0.0f;
	float baseTurnSpeed         = 0.0f;
	float result1               = 0.0f;
	float result2               = 0.0f;
	float result3               = 0.0f;
	float counter               = 0.0f;
	float resetOffsetDelay      = 15.0f;

	void Start()
	{


        if (!za.killSoundEffects && xa.musicVolume > 0 && xa.muteSound != 0)
        {
            if (xa.sn) { xa.sn.playSound(GC_SoundScript.Sounds.Missile); }
            //Setup.playSound(Setup.snds.Missile);
        }
		offsetAmount.x = 2;
		offsetAmount.y = 2;

		if (useForceOffset)
		{
			offsetAmount = forceOffset;
		}

		if (!nonRandom)
		{
			offset.x = Random.Range(-offsetAmount.x, offsetAmount.y);
			offset.y = Random.Range(-offsetAmount.x, offsetAmount.y);
		}

		//speed      *= Random.Range(.5f, 1.5f);
		baseSpeed     = speed;
		turnSpeed    *= Random.Range(.8f, 1.2f);
		baseTurnSpeed = turnSpeed;
	}

	void Update()
	{
		if (!nonRandom)
		{
			counter += 10 * fa.deltaTime;
			if (counter > resetOffsetDelay)
			{
				counter = 0;
				offset.x = Random.Range(-offsetAmount.x, offsetAmount.y);
				offset.y = Random.Range(-offsetAmount.x, offsetAmount.y);
			}
		}
		//move forward
		transform.Translate(speed * fa.deltaTime, 0, 0);

		if (forceTarget)
		{
			target = forcedTarget;
			target.z = transform.position.z;
		}
		else
		{
			if (xa.player)
			{
				target = xa.player.transform.position;
				target += offset;
				target.z = transform.position.z;
			}
		}
		ang1 = ang2 = ang3 = transform.localEulerAngles;
		ang1.z += 4;
		vec1 = Setup.projectVec(transform.position, ang1, 5, Vector3.right);
		ang2.z -= 4;
		vec2 = Setup.projectVec(transform.position, ang2, 5, Vector3.right);
		vec3 = Setup.projectVec(transform.position, ang3, 5, Vector3.right);

		result1 = Vector3.Distance(target, vec1);
		result2 = Vector3.Distance(target, vec2);
		result3 = Vector3.Distance(target, vec3);

        if (!dontTurnLessOnGettingCloser)
        {
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
        }

		//if (result3 < result1 && result3 < result2)
		xa.glx = transform.localEulerAngles;
		if ((result3 > result1 || result3 > result2) && result1 < result2)
		{
			xa.glx.z += turnSpeed * fa.deltaTime;
		}
		else
		{
			xa.glx.z -= turnSpeed * fa.deltaTime;
		}
		transform.localEulerAngles = xa.glx;
	}
}
