using UnityEngine;
using System.Collections;

public class TeleporterScript : MonoBehaviour
{
	public GameObject targetObj = null;
	public Vector3 target = Vector3.zero;
	public float snapThreshold = 0;
	public float dist = 1;
	public bool snapCamera = false;
	public bool snapCameraY = false;
	public bool snapCameraIfOffScreen = false;
	public bool useVector = false;
	public bool useObjectPos = false;

	public bool useSnapCameraToThisVector = false;
	public Vector2 snapCameraToThisVector = Vector2.zero;
	public GameObject cameraGOToSnap = null;

	public Behaviour enableThisOnTeleport = null;
	public SetStateCameraState cameraScript = null;

	//Inflove stuff
	public float increaseCameraSpeedMultiplierBy = 0.05f;
	public bool isInfLove = false;
	static int infLoveLastUsedPortal = -1;
	static int infLoveOriginPortal = -1;
	static GameObject[] infLoveStartPoints = null;
	bool teleporting = false;
	bool isInitialized = false;


	void Start()
	{
		if (!isInitialized)
		{
			if (isInfLove)
			{
				infLoveStartPoints = GameObject.FindGameObjectsWithTag("infLoveStartPoint");
				infLoveLastUsedPortal = -1;
			}
			isInitialized = true;
		}
	}

	bool spawnedStompHitbox = false;
	void spawnStompHitbox()
	{
		if (!spawnedStompHitbox)
		{
			if (xa.de)
			{
				spawnedStompHitbox = true;
				xa.tempobj = (GameObject)(Instantiate(xa.de.stompHitbox, transform.position, xa.null_quat));
				xa.tempobj.transform.parent = this.gameObject.transform;
			}
		}
	}

	void Update()
	{
		if ((isInfLove == true && teleporting == false) || isInfLove == false)
		{
			spawnStompHitbox();
			if (xa.player)
			{
				if (!xa.playerDead)
				{
					xa.glx = xa.player.transform.position;
					xa.glx.z = transform.position.z;
					if (Vector3.Distance(xa.glx, transform.position) < dist)
					{
						teleporting = true;
						
						FreshLevels.Type levelType = FreshLevels.GetTypeOfCurrentLevel();
						if(levelType == FreshLevels.Type.Alp5)
						{
							fa.teleportedOnJumpingMassacre = true;
						}


						if (cameraScript) { cameraScript.setStats(); }

						if (enableThisOnTeleport)
						{ enableThisOnTeleport.enabled = true; }
						
						xa.glx = Vector3.zero;
						if (useVector)
						{
							xa.glx = target;
						}
						else if (useObjectPos)
						{
							if (!isInfLove)
							{
								xa.glx = targetObj.transform.position;
							}
							else
							{
								xa.glx = getRandomObjPos();
							}
						}

						//play sound
						if (xa.player.transform.position.x < xa.glx.x)
						{
							Setup.playSound(Setup.snds.TeleporterLeftToRight);
						}
						else
						{
							Setup.playSound(Setup.snds.TeleporterRightToLeft);
						}


						xa.glx.z = xa.player.transform.position.z;
						xa.player.transform.position = xa.glx;
						teleporting = false;

						
						NovaPlayerScript plScript = xa.player.GetComponent<NovaPlayerScript>();
						if (plScript != null)
						{
					if (fa.time < (plScript.lastStompedOnAFreshStompHitbox + 0.1f))//Stomped on a stomphitbox in the last 0.3 seconds, which probably means you stomped on this item
					{
					}

						}
						if (useSnapCameraToThisVector)
						{
							xa.glx = cameraGOToSnap.transform.position;
							xa.glx.x = snapCameraToThisVector.x;
							xa.glx.y = snapCameraToThisVector.y;
							cameraGOToSnap.transform.position = xa.glx;
						}
						if (snapCamera)
						{
							////Debug.Log ("SNAPPEDCAMERA FROM TELEPORTER");
							xa.glx = cameraGOToSnap.transform.position;
							xa.glx.x = xa.player.transform.position.x;
							cameraGOToSnap.transform.position = xa.glx;
						}
						if (snapCameraY)
						{
							xa.glx = cameraGOToSnap.transform.position;
							xa.glx.y = xa.player.transform.position.y;
							cameraGOToSnap.transform.position = xa.glx;
						}
						else
						{
							if (snapCameraIfOffScreen)
							{
								if (useVector && (target.x + snapThreshold) > xa.frontEdgeOfScreen)
								{
									xa.glx = cameraGOToSnap.transform.position;
									xa.glx.x = xa.player.transform.position.x;
									cameraGOToSnap.transform.position = xa.glx;
								}
								else if (useObjectPos && (targetObj.transform.position.x + snapThreshold) > xa.frontEdgeOfScreen)
								{
									xa.glx = cameraGOToSnap.transform.position;
									xa.glx.x = xa.player.transform.position.x;
									cameraGOToSnap.transform.position = xa.glx;
								}
							}
						}
					}
				}
			}
		}
	}

	IEnumerator ResetPlayerTrail()
	{
		xa.player.transform.Find("trail").GetComponent<TrailRenderer>().time = 0;
		yield return new WaitForEndOfFrame();
		xa.player.transform.Find("trail").GetComponent<TrailRenderer>().time = 4;
	}

	Vector3 getRandomObjPos()
	{
		int nextPortal = infLoveLastUsedPortal;
		while (nextPortal == infLoveLastUsedPortal || nextPortal == infLoveOriginPortal)
		{
			nextPortal = Random.Range(0, infLoveStartPoints.Length);
		}
		//Setup.GC_DebugLog("NextPortal: " + nextPortal);
		infLoveLastUsedPortal = nextPortal;
		return infLoveStartPoints[nextPortal].transform.position;
	}
}
