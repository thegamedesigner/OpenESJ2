using UnityEngine;
using System.Collections;

public class ChangeCameraScript : MonoBehaviour
{
	public bool triggerExternally = false;

	public bool untriggerWhenPlayerLeaves = false;

	public bool useSnapCameraBumpHoriztonal = false;
	public bool snapCameraBumpHoriztonal = false;
	public bool useSnapCameraBumpTop = false;
	public bool snapCameraBumpTop = false;
	public float forceBumpDist = 0;

	public bool useSnapCameraScrolling = false;
	public bool snapCameraScrolling = false;

	public bool useSnapCameraGoesVertical = false;
	public bool snapCameraGoesVertical = false;

	public bool useSnapCameraFollowsPlayer = false;
	public bool snapCameraFollowsPlayer = false;

	public bool useSnapCameraFollowsPlayerY = false;
	public bool snapCameraFollowsPlayerY = false;

	public bool useSnapCameraFollowsPlayerYUpwardsOnly = false;
	public bool snapCameraFollowsPlayerYUpwardsOnly = false;

	public bool useSnapCameraSpeed = false;
	public float snapCameraSpeed = 0;
	
	
	public bool useSnapCamera = false;
	public bool snapCameraLocal = false;
	public Vector3 snapCameraTo = Vector3.zero;
	
	
	public bool useSnapMaxCameraSpeed = false;
	public float snapMaxCameraSpeed = 0;
	
	bool setBoxStats = false;
	bool triggered = false;
	
	float plBoxHeight = 0;
	float plBoxWidth = 0;
	
	void Start()
	{

	}
	
	void changeCamera() {
		CameraScript camScript = Camera.main.gameObject.GetComponent<CameraScript>();

		if (useSnapCameraBumpHoriztonal)
		{
			camScript.bumpCameraOnRightSide = snapCameraBumpHoriztonal;
			if (forceBumpDist != 0)
			{
				camScript.forceBumpDist = forceBumpDist;
			}
		}
		if (useSnapCameraBumpTop)
		{
			camScript.bumpCameraOnTop = snapCameraBumpTop;
			if (forceBumpDist != 0)
			{
				camScript.forceBumpDist = forceBumpDist;
			}
		}
		if (useSnapCameraScrolling)
		{
			camScript.scroll = snapCameraScrolling;
		}
		if (useSnapCameraGoesVertical)
		{
			camScript.cameraGoesVertical = snapCameraGoesVertical;
		}
		if (useSnapCameraFollowsPlayer)
		{
			camScript.cameraFollowsPlayer = snapCameraFollowsPlayer;
		}
		if (useSnapCameraFollowsPlayerY)
		{
			camScript.cameraFollowsPlayerY = snapCameraFollowsPlayerY;
		}
		if (useSnapCameraFollowsPlayerYUpwardsOnly)
		{
			camScript.cameraFollowsPlayerYUpwardsOnly = snapCameraFollowsPlayerYUpwardsOnly;
		}

  //  public bool useSnapCameraFollowsPlayerYUpwardsOnly = false;
   // public bool snapCameraFollowsPlayerYUpwardsOnly = false;

		if (useSnapCameraSpeed) {
			camScript.forceSpd = snapCameraSpeed;
		}
		if (useSnapCamera)
		{
			snapCameraTo.z = Camera.main.GetComponent<Camera>().transform.position.z;
			if (snapCameraTo.x == -9999)
			{
				snapCameraTo.x = Camera.main.GetComponent<Camera>().transform.position.x;
			}
			if(snapCameraLocal == false)
			{
				Camera.main.GetComponent<Camera>().transform.position = snapCameraTo;
			}
			else
			{
				Camera.main.GetComponent<Camera>().transform.localPosition = snapCameraTo;
			}
			////Debug.Log ("SNAPPINGFROM CHANGE CAMERA");
		}
	
		if (useSnapMaxCameraSpeed) {
			camScript.scrollSpd = snapMaxCameraSpeed;
		}
	
	
	}

	void Update()
	{
		//For triggering externally
		if(triggerExternally && enabled)
		{
			triggered = true;
			changeCamera();
		}
	
		if (triggered && (triggerExternally ? true : untriggerWhenPlayerLeaves))
		{
			if (xa.player)
			{
				if (!setBoxStats)
				{
					setBoxStats = true;
					plBoxHeight = xa.playerBoxHeight;
					plBoxWidth = xa.playerBoxWidth;
				}

				if ((transform.position.x + (transform.localScale.x * 0.5f)) > (xa.player.transform.position.x - (plBoxWidth * 0.5f)) &&
					(transform.position.x - (transform.localScale.x * 0.5f)) < (xa.player.transform.position.x + (plBoxWidth * 0.5f)) &&
					(transform.position.y + (transform.localScale.y * 0.5f)) > (xa.player.transform.position.y - (plBoxHeight * 0.5f)) &&
					(transform.position.y - (transform.localScale.y * 0.5f)) < (xa.player.transform.position.y + (plBoxHeight * 0.5f)))
				{
				}
				else
				{
					triggered = false;
				}
			}
	
			if(triggerExternally)
				enabled = false;
		}
		else if(triggerExternally == false)
		{
			if (xa.player)
			{
				if (!setBoxStats)
				{
					setBoxStats = true;
					plBoxHeight = xa.playerBoxHeight;
					plBoxWidth = xa.playerBoxWidth;
				}

				if ((transform.position.x + (transform.localScale.x * 0.5f)) > (xa.player.transform.position.x - (plBoxWidth * 0.5f)) &&
					(transform.position.x - (transform.localScale.x * 0.5f)) < (xa.player.transform.position.x + (plBoxWidth * 0.5f)) &&
					(transform.position.y + (transform.localScale.y * 0.5f)) > (xa.player.transform.position.y - (plBoxHeight * 0.5f)) &&
					(transform.position.y - (transform.localScale.y * 0.5f)) < (xa.player.transform.position.y + (plBoxHeight * 0.5f)))
				{
					triggered = true;
					changeCamera();
				}
				else if (untriggerWhenPlayerLeaves)
				{
					triggered = false;
				}
			}
		}
	}
}
