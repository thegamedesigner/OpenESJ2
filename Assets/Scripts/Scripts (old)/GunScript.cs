  using UnityEngine;

public class GunScript : MonoBehaviour
{
	public bool skipGlobalStartDelay = false;
	public float reloadDelay = 0;
	public float firingDelay = 0;
	public float delayBeforeFiring = 0;
	public float delayAfterFiring = 0;
	public int pauseEveryXShots = 0;
	public float pauseForX = 0;
	public int numOfBulletsPerClip = 0;
	public GameObject bullet = null;
	public GameObject animatingObject = null;
	public GameObject muzzlePoint = null;
	public GameObject muzzleFlash = null;
	public bool hasAnimationScript = false;
	public int stopAfterFiringXBullets = 0;
	public bool onlyActiveBetweenMusicStartAndStop = false;
	public float musicStart = 0;
	public float musicStop = 0;
	float slightGlobalStartingDelay = 0;

	public float firingAngleMin = 0;
	public float firingAngleMax = 0;
	public bool fireAnArc = false;
	public int numOfBulletsPerShot = 0;
	public float[] firingAngles = new float[20];
	public bool activeGun = true;
	public bool waitForBeat = false;
	public bool dontAliveUnlessTriggeredExternally = false;

	public bool takeAngleFromObject = false;

	Quaternion firingRotation;
	float firingAngle = 0;
	float counter = 0;
	int clip = 0;
	int index = 0;
	[HideInInspector]
	public float delayBeforeFiringCounter = 0;
	[HideInInspector]
	public float delayAfterFiringCounter = 0;
	[HideInInspector]
	public float pauseForXCounter = 0;
	AniScript_TriggeredAni script;
	int bulletsFired = 0;//number of bullets fired
	int shotsFired = 0;//number of clips emptied
	int shotsCounted = 0;//number of clips emptied

	[HideInInspector]
	public bool firing = false;
	[HideInInspector]
	public bool reloading = false;

	public bool alive = false;

	public int fireABullet = 0;

	public int requireBossStage = 0;
	public bool dontTriggerOnLoopingSecondTrack = false;
	public bool onlyTriggerOnLoopingSecondTrack = false;

	void Start()
	{
		slightGlobalStartingDelay = transform.position.x;//Random.Range(0, 35f);

		while (slightGlobalStartingDelay > 40) { slightGlobalStartingDelay -= 40; }
		clip = numOfBulletsPerClip;

		if (hasAnimationScript)
		{
			script = animatingObject.GetComponent<AniScript_TriggeredAni>();
		}

		reloading = true;

	}
	void Update()
	{
		if (slightGlobalStartingDelay > 0 && !skipGlobalStartDelay)
		{
			slightGlobalStartingDelay -= 10 * fa.deltaTime;
			/*
			if (slightGlobalStartingDelay <= 0)
			{
				Setup.GC_DebugLog("Done " + Time.realtimeSinceStartup);
			}*/
		}
		else
		{
			if ((!dontTriggerOnLoopingSecondTrack && !onlyTriggerOnLoopingSecondTrack) ||
				(dontTriggerOnLoopingSecondTrack && !xa.playingLoopingSecondTrack) ||
				(onlyTriggerOnLoopingSecondTrack && xa.playingLoopingSecondTrack))
			{
				if (transform.position.x < (xa.frontEdgeOfScreen - 1) && !dontAliveUnlessTriggeredExternally) { alive = true; }
				if (transform.position.x < (xa.backEdgeOfScreen + 1) && !dontAliveUnlessTriggeredExternally) { alive = false; }
				if (!alive) { return; }
				firing = false;

				if (!onlyActiveBetweenMusicStartAndStop || (onlyActiveBetweenMusicStartAndStop && xa.music_Time >= musicStart && xa.music_Time <= musicStop))
				{
					if (activeGun)
					{
						float explo1Layer = xa.GetLayer(xa.layers.Explo1);
						if (waitForBeat)
						{
							if (fireABullet > 0)
							{
								fireABullet--;


								//create bullet
								if (fireAnArc)
								{
									if (muzzleFlash != null)
									{
										xa.glx = muzzlePoint.transform.position;
										xa.glx.z = explo1Layer;
										xa.tempobj = (GameObject)(Instantiate(muzzleFlash, xa.glx, xa.null_quat));
										xa.tempobj.transform.parent = xa.createdObjects.transform;
									}
									index = 0;
									while (index < numOfBulletsPerShot)
									{
										createBullet(firingAngles[index]);
										index++;
									}
								}
								else
								{
									firingAngle = Random.Range(firingAngleMin, firingAngleMax);
									if (takeAngleFromObject)
									{
										firingAngle = transform.localEulerAngles.z + 90;
									}
									createBullet(firingAngle);
								}
							}
						}
						else
						{
							//normal gun
							if (xa.resetFiringPatterns > 0)
							{
								reloading = true;
								counter = 0;
								delayBeforeFiringCounter = 0;
								delayAfterFiringCounter = 0;
								pauseForXCounter = 0;
								bulletsFired = 0;
								clip = numOfBulletsPerClip;
							}

							if (reloading)
							{
								if (delayAfterFiringCounter > 0)
								{
									delayAfterFiringCounter -= 10 * fa.deltaTime;
								}
								else
								{
									if (pauseForXCounter > 0)
									{
										pauseForXCounter -= 10 * fa.deltaTime;
									}
									else
									{
										counter += 10 * fa.deltaTime;
										if (counter > reloadDelay)
										{
											counter = 999;
											delayBeforeFiringCounter = delayBeforeFiring;
											delayAfterFiringCounter = delayAfterFiring;
											reloading = false;
											if (hasAnimationScript) { script.triggerAni2(); }
										}
									}
								}
							}
							else
							{
								if (delayBeforeFiringCounter > 0)
								{
									delayBeforeFiringCounter -= 10 * fa.deltaTime;
								}
								else
								{
									firing = true;
									counter += 10 * fa.deltaTime;
									if (counter > firingDelay)
									{
										counter = 0;
										if (clip > 0)
										{
											clip--;
											bulletsFired++;
											if (stopAfterFiringXBullets != 0 && bulletsFired >= stopAfterFiringXBullets)
											{
												activeGun = false;
											}

											//create bullet
											if (fireAnArc)
											{
												if (muzzleFlash != null)
												{
													xa.glx = muzzlePoint.transform.position;
													xa.glx.z = explo1Layer;
													xa.tempobj = (GameObject)(Instantiate(muzzleFlash, xa.glx, xa.null_quat));
													xa.tempobj.transform.parent = xa.createdObjects.transform;
												}
												index = 0;
												while (index < numOfBulletsPerShot)
												{
													createBullet(firingAngles[index]);
													index++;
												}
											}
											else
											{
												firingAngle = Random.Range(firingAngleMin, firingAngleMax);
												if (takeAngleFromObject)
												{
													firingAngle = transform.localEulerAngles.z + 90;
												}
												createBullet(firingAngle);
											}

										}
										if (clip <= 0)
										{
											reloading = true;
											clip = numOfBulletsPerClip;
											shotsFired++;
											shotsCounted++;
											if (shotsCounted >= pauseEveryXShots)
											{
												shotsCounted = 0;
												pauseForXCounter = pauseForX;
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
	}
	public void makeAlive()
	{
		//Setup.GC_DebugLog("made alive");
		alive = true;
	}
	void createBullet(float angle)
	{
		float explo1Layer = xa.GetLayer(xa.layers.Explo1);
		if (xa.emptyObj)
		{
			xa.emptyObj.transform.localEulerAngles = this.gameObject.transform.localEulerAngles;
			xa.glx = xa.emptyObj.transform.localEulerAngles;
			xa.glx.z = angle;
			xa.emptyObj.transform.localEulerAngles = xa.glx;
			firingRotation = xa.emptyObj.transform.rotation;
		}

		if (bullet != null)
		{
			xa.glx = muzzlePoint.transform.position;
			xa.glx.z = explo1Layer;
			xa.tempobj = (GameObject)(Instantiate(bullet, xa.glx, firingRotation));
			xa.tempobj.transform.parent = xa.createdObjects.transform;
		}

		if (!fireAnArc)
		{
			if (muzzleFlash != null)
			{
				xa.glx = muzzlePoint.transform.position;
				xa.glx.z = explo1Layer;
				xa.tempobj = (GameObject)(Instantiate(muzzleFlash, xa.glx, firingRotation));
				xa.tempobj.transform.parent = xa.createdObjects.transform;
			}
		}
	}

	public void ForceAliveFunc()
	{
		alive = true;
	}
}
