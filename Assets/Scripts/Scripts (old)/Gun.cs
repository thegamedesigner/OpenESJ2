using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
	//GunStates
	enum gs { None, PreReloading, PostReloading, Reloading, Ready, PreFiring, Firing, PostFiring, PauseBetweenBullets, Paused, Stopped }
	gs state = gs.None;
	gs oldState = gs.None;


	public bool usePointTowardsGameObject = false;
	public GameObject pointTowardsThisGameObject = null;
	public float turnSpeed = 0;

	public GameObject bullet = null;
	public GameObject[] bulletCreationPoints;
	public GameObject optionalMuzzleFlash = null;
	public GameObject[] muzzleFlashCreationPoints;

	bool tryingToFire = false;
	bool stopAfterClip = false;

	public int numOfBulletsPerClip = 0;
	public float reloadTime = 0;
	public float firingSpeedTime = 0;
	public bool startLoaded = false;

	public bool useCreateLocal = false;
	public GameObject createLocalToThisGO = null;

	public bool useLimitedArc = false;
	public float centerOfArcAng = 0;
	public float arcWidth = 0;

	public bool startStopped = false;

	float timeOffset = 0;
	int clip = 0;
	GameObject tempGO = null;
	Vector3 targetPos;
	Vector3 tempVec;
	Quaternion tempQuat;

	public bool useDontHitTheseLayers = false;
	public bool[] dontHitTheseLayers = new bool[32];
	int[] bitmasks = new int[32];
	int layerKey = 0;

	public bool dontPrintDebugMsgs = true;

	//How do I transmit the state?

	//state timers
	/*
	public float delayBeforeFiringButAfterPullingTrigger = 0;
	public float delayDirectlyAfterFiring = 0;
	public float delayBetweenFiringBullets = 0;
	public float reloadDelay = 0;
	public int pauseEveryXShots = 0;
	public float pauseForThisLong = 0;
	public int stopAfterFiringXBulletsAndDontStartAgain = 0;

	//instant details for each firing

	//public GameObject muzzlePoint = null;
	//public GameObject muzzleFlashEffect = null;
	//public bool fireAnArc = false;
	//public float firingAngleMin = 0;
	//public float firingAngleMax = 0;
	//public int numOfBulletsPerShot = 0;
	//public float[] firingAngles = new float[20];
	
	//local variables
	Quaternion firingRotation;
	float firingAngle = 0;
	float counter = 0;
	int clip = 0;
	int index = 0;
	float delayBeforeFiringCounter = 0;
	float delayAfterFiringCounter = 0;
	public float pauseForXCounter = 0;
	int bulletsFired = 0;//number of bullets fired
	int shotsFired = 0;//number of clips emptied
	int shotsCounted = 0;//number of clips emptied
	bool firing = false;
	bool reloading = false;
	*/

	void Start()
	{
		if (startStopped) { state = gs.Stopped; }
		updateLayerKey();
	}

	void Update()
	{
		if (usePointTowardsGameObject)
		{
			targetPos = pointTowardsThisGameObject.transform.position;
			targetPos.z = this.gameObject.transform.position.z;

			tempQuat = Quaternion.LookRotation(transform.position - targetPos, Vector3.forward);
			tempQuat.x = 0;
			tempQuat.y = 0;
			transform.rotation = Quaternion.Slerp(transform.rotation, tempQuat, turnSpeed * fa.deltaTime);
		}



		//snap to arc
		if (useLimitedArc)
		{
			tempVec = transform.localEulerAngles;

			//Setup.GC_DebugLog("Returned ang: " + Mathf.DeltaAngle(tempVec.z, centerOfArcAng));
			if (Mathf.DeltaAngle(tempVec.z, centerOfArcAng) > arcWidth)
			{
				tempVec.z = centerOfArcAng + -arcWidth;
			}
			else if (Mathf.DeltaAngle(tempVec.z, centerOfArcAng) < -arcWidth)
			{
				tempVec.z = centerOfArcAng + arcWidth;
			}
			else
			{

			}

			transform.localEulerAngles = tempVec;
		}

		//Debug.DrawLine(transform.position, pointTowardsThisGameObject.transform.position, Color.cyan);

//		handleGun();
	}
	
	void FixedUpdate()
	{
		handleGun();
	}

	void updateLayerKey()
	{
		layerKey = 0;
		//  layerKey = layerKey | 1 << 15;
		// layerKey = layerKey | 1 << 8;

		int index;
		index = 0;
		while (index < bitmasks.Length)
		{
			if (dontHitTheseLayers[index]) { layerKey = layerKey | 1 << index; }
			index++;
		}

	}

	Vector3 projectVec(Vector3 vec1, Vector3 ang, float dist, Vector3 dir)
	{
		Vector3 storedPos = transform.position;
		Vector3 storedAng = transform.localEulerAngles;
		Vector3 result;

		transform.position = vec1;
		transform.localEulerAngles = ang;
		transform.Translate(dir * dist);

		result = transform.position;
		transform.position = storedPos;
		transform.localEulerAngles = storedAng;
		return (result);
	}

	void handleGun()
	{
		if (state == gs.None)
		{
			if (oldState != state)
			{
				oldState = state;
			}
			state = gs.Reloading;//set to starting state, based on if the gun should start loaded or not.

			if (startLoaded)
			{
				clip = numOfBulletsPerClip;
				state = gs.Ready;
			}
			else
			{
				clip = 0;
				state = gs.PreReloading;
			}
		}
		else if (state == gs.PreReloading)
		{
			if (oldState != state)
			{
				oldState = state;
			}
			state = gs.Reloading;
		}
		else if (state == gs.Reloading)
		{
			if (oldState != state)
			{
				oldState = state;

				timeOffset = fa.time;
			}

			if ((fa.time - timeOffset) > reloadTime)
			{
				clip = numOfBulletsPerClip;
				state = gs.PostReloading;
			}
		}
		else if (state == gs.PostReloading)
		{
			if (oldState != state)
			{
				oldState = state;
			}
			state = gs.Ready;


		}
		else if (state == gs.Ready)
		{
			if (oldState != state)
			{
				oldState = state;
			}

			if (tryingToFire)
			{
				state = gs.PreFiring;
			}

		}
		else if (state == gs.PreFiring)
		{
			if (oldState != state)
			{
				oldState = state;
			}
			state = gs.Firing;
		}
		else if (state == gs.Firing)
		{
			if (oldState != state)
			{
				oldState = state;
			}
			createBullets();
			clip--;
			state = gs.PostFiring;
		}
		else if (state == gs.PostFiring)
		{
			if (oldState != state)
			{
				oldState = state;
			}

			//Setup.GC_DebugLog("Bullets left: " + clip + " of " + numOfBulletsPerClip);
			if (clip <= 0)
			{
				if (stopAfterClip)
				{
					//stop, you've fired an entire clip
					state = gs.Stopped;
				}
				else
				{
					//go to reloading
					state = gs.PreReloading;
				}
			}
			else
			{
				state = gs.PauseBetweenBullets;
			}
		}
		else if (state == gs.PauseBetweenBullets)
		{
			if (oldState != state)
			{
				oldState = state;
				timeOffset = fa.time;
			}

			if ((fa.time - timeOffset) > firingSpeedTime)
			{
				state = gs.Ready;
			}
		}
		else if (state == gs.Paused)
		{

		}
		else if (state == gs.Stopped)
		{
			if (oldState != state)
			{
				oldState = state;
			}
		}

		//PauseBetweenBullets, Paused, Stopped }

	}

	public void startFiringGun()
	{
		tryingToFire = true;
	}

	public void stopFiringGun()
	{
		tryingToFire = false;
	}

	public void fireOneClip()
	{
		tryingToFire = true;
		stopAfterClip = true;
		state = gs.PreReloading;
	}

	public void createBullets()
	{
		int index = 0;
		while (index < bulletCreationPoints.Length)
		{
			if (bulletCreationPoints[index])
			{
				createBullet(bulletCreationPoints[index].transform.position, bulletCreationPoints[index].transform.rotation);
			}
			index++;
		}

		if (optionalMuzzleFlash)
		{
			index = 0;
			while (index < muzzleFlashCreationPoints.Length)
			{
				if (muzzleFlashCreationPoints[index])
				{
					createMuzzleFlash(muzzleFlashCreationPoints[index].transform.position, muzzleFlashCreationPoints[index].transform.rotation);
				}
				index++;
			}
		}
	}

	public void createBullet(Vector3 firingPoint, Quaternion firingAngle)
	{
		tempGO = (GameObject)(Instantiate(bullet, firingPoint, firingAngle));
		if (useCreateLocal)
		{
			tempGO.transform.parent = createLocalToThisGO.transform;
		}

	}

	public void createMuzzleFlash(Vector3 createPoint, Quaternion createAngle)
	{
		tempGO = (GameObject)(Instantiate(optionalMuzzleFlash, createPoint, createAngle));
	}
}
