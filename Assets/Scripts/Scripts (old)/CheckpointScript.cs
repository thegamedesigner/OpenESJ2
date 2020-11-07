using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
	public GameObject SantaToSetActive;//For SantaStuckScript.cs
	public GameObject SantaToSetUnactive;//For SantaStuckScript.cs
	public bool startTriggered = false;
	
	public bool useSetGOToX = false;
	public GameObject GOToSetXOf;
	public float XToSetTo = 0;
	
	[HideInInspector]
	public bool triggered = false;
	bool onceFlag = true;
	[HideInInspector]
	public bool createdExplo = false;
	public GameObject explo;
	public GameObject spawnPoint;

	public float forcePointInMusic = 0;
	public bool forceRestartingMusic = false;
	public bool useSnapCamera = false;
	public Vector3 snapCameraTo = Vector3.zero;
	public bool useSnapCameraAng = false;
	public Vector3 snapCameraAng = Vector3.zero;
	public bool useSnapCameraSpeed = false;
	public float snapCameraSpeed = 0;
	
	public bool snapToMyX = false;

	float triggerDistance = 2f;

	void Start()
	{
		triggered = startTriggered;
	}

	void Update()
	{
		if (!xa.playerDead)
			checkForTrigger();

		if (!triggered || xa.fadingOut)
			return;

		if (onceFlag)
		{
			onceFlag = false;

			//set x
			if(useSetGOToX)
			{
				GOToSetXOf.transform.SetX(XToSetTo);

			}

			//save abilities node script stuff
			if (SaveAbilitiesNodeScript.self != null)
			{
				SaveAbilitiesNodeScript.HitCheckpoint();
			}


			xa.checkpointSecondsLeft = xa.countdownSecondsLeft;
			xa.checkpointScore = xa.realScore;
			StarScript.saveCollectedStarsToCheckpointedStars();

			xa.checkpointedStarsThisLevel = xa.checkpointedStarsThisLevel | xa.carryingStars;
			//Setup.GC_DebugLog("carrying stars: " + xa.checkpointedStarsThisLevel);

			xa.hasCheckpointed = true;

			if (useSnapCamera)
			{
				za.useSnapCameraToCheckpoint = true;
				snapCameraTo.z = Camera.main.GetComponent<Camera>().transform.position.z;
				if (snapCameraTo.x == -9999)
				{
					snapCameraTo.x = Camera.main.GetComponent<Camera>().transform.position.x;
				}
				za.snapCameraToThisPos = snapCameraTo;
			}
			//if (xa.lastSpawnPoint.x == spawnPoint.transform.position.x && xa.lastSpawnPoint.y == spawnPoint.transform.position.y)
			//{
			if (xa.firstCheckpointTriggered == false)
			{
				xa.firstCheckpointTriggered = true;


				if (SantaToSetActive != null) { SantaToSetActive.SetActive(true); }
				if (SantaToSetUnactive != null) { SantaToSetUnactive.SetActive(false); }


				if (useSnapCameraSpeed)
				{
					CameraScript daScript = Camera.main.gameObject.GetComponent<CameraScript>();
					daScript.forceSpd = snapCameraSpeed;
				}

				if (useSnapCamera)
				{
					useSnapCamera = false;
					//Setup.GC_DebugLog("Stopping itweens");
					iTween.Stop(Camera.main.GetComponent<Camera>().gameObject);
					snapCameraTo.z = Camera.main.GetComponent<Camera>().transform.position.z;
					if (snapCameraTo.x == -9999)
					{
						snapCameraTo.x = Camera.main.GetComponent<Camera>().transform.position.x;
					}
					Camera.main.GetComponent<Camera>().transform.position = snapCameraTo;
					xa.camGoalX = Camera.main.GetComponent<Camera>().transform.position.x;
				}
				if (useSnapCameraAng)
				{
					Camera.main.GetComponent<Camera>().transform.localEulerAngles = snapCameraAng;
				}

				if (snapToMyX)
				{
					xa.glx = Camera.main.GetComponent<Camera>().transform.position;
					xa.glx.x = transform.position.x;
					Camera.main.GetComponent<Camera>().transform.position = xa.glx;
				}

			}
			//}
			xa.lastSpawnPoint.x = spawnPoint.transform.position.x;
			xa.lastSpawnPoint.y = spawnPoint.transform.position.y;
		}

		if (!createdExplo)
		{
			createdExplo = true;
			if (explo)
			{
				xa.glx = transform.position;
				xa.glx.z = xa.GetLayer(xa.layers.Explo3);//by default, create far back to prevent flickering. They snap to the correct z anyway.
				xa.tempobj = (GameObject)(Instantiate(explo, xa.glx, xa.null_quat));
				xa.tempobj.transform.parent = xa.createdObjects.transform;
			}
		}
	}
	

	void checkForTrigger()
	{
		if (xa.fadingOut) return;
		if (xa.player == null) { return; }

		xa.glx = xa.player.transform.position;
		xa.glx.z = transform.position.z;

		if (Vector3.Distance(xa.glx, transform.position) < triggerDistance)
		{
			triggered = true;
		}
	}
}
