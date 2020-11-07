using UnityEngine;

public class CameraScript : MonoBehaviour
{
	public bool setAngleBasedOnXPos = false;
	public bool useStartAtThisPos = false;
	public Vector3 startAtThisPos = new Vector3(0, 0, 0);
	public bool neverCatchCamera = false;
	public float neverCatchScreenPoint = 8;
	public float neverCatchAmount = 28;
	public float neverCatchTime = 0.3f;
	public bool cameraGoesVertical = false;
	public bool centerCameraOnPlayerOnSpawn = true;
	public bool centerCameraOnPlayerYOnSpawn = false;
	public bool cameraFollowsPlayer = false;
	public bool cameraFollowsPlayerY = false;
	public bool cameraFollowsPlayerYUpwardsOnly = false;
	public bool waitForPlayerToMove = false;
	public bool stayAtStartingPos = false;
	public bool cameraFollowsOnXInStages = false;
	public bool bumpCameraOnRightSide = false;
	public bool bumpCameraOnTop = false;
	public float forceBumpDist = 0;
	public bool useGoToVec = false;
	public Vector3 goToVec = new Vector3(0, 0, 0);
	public float goToVecSpeed = 0;
	public bool scroll = false;
	public float scrollSpd = 0;
	public float addSpd = 0;
	public float startAtThisSpd = 0;
	public float delay = 0;
	public float forceSpd = 0;
	float spd = 0;
	bool itweeningAhead = false;
	public bool useHaltGlorg = false;
	public bool useStopScrollingAtThisX = false;
	public float stopScrollingAtThisX = 0;

	public float extraSpdMax = 0;
	public float extraSpdAdd = 0;
	public float extraSpdSubtract = 0;
	public float extraSpdThreshold = 0;

	public float maxScrollSpeed = 99;

	public bool useClamp = false;
	public Vector2 maxClamp = Vector2.zero;
	public Vector2 minClamp = Vector2.zero;

	float extraSpd = 0;
	float bumpDist = 0;
	float distAcrossScreen = 0;

	public float goToY = 0;
	Vector3 result;

	void Awake()
	{
		fa.mainCameraObject = this.gameObject;
	}

	void Start()
	{
		if (useStartAtThisPos)
		{
			transform.position = startAtThisPos;
		}
		spd = startAtThisSpd;
		//if (stayAtStartingPos) { transform.position = xa.cameraStartPos; }
		xa.camGoalX = transform.position.x;
	}

	void stoppedItweeningAhead()//for never catching the camera
	{
		itweeningAhead = false;
	}

	public void UpdateEditorCamera(Vector3 delta)
	{
		if (EditorController.IsEditorActive()) {
			this.transform.position += delta;
		}
	}

	void Update()
	{
		if (EditorController.IsEditorActive()) {
			return;
		}

		if (setAngleBasedOnXPos)
		{
			if (xa.player != null)
			{
				float result = xa.player.transform.position.x * 0.1f;
				transform.SetAngZ(result);
			}
		}

		fa.cameraPos = transform.position;
		if ((scroll && !xa.frozenCamera) || cameraFollowsOnXInStages || (neverCatchCamera && !scroll))
		{
			if (xa.player) {
				result = Camera.main.GetComponent<Camera>().WorldToViewportPoint(xa.player.transform.position);
			}
		}

		if (useStopScrollingAtThisX && scroll)
		{
			if (transform.position.x >= stopScrollingAtThisX)
			{
				scroll = false;
				if (useHaltGlorg) { xa.haltForSecondGlorg = true; }
			}
		}

		if (bumpCameraOnRightSide)
		{
			//was 16
			bumpDist = 15;
			if (forceBumpDist != 0) { bumpDist = forceBumpDist; }
			if (xa.player && xa.player.transform.position.x > (transform.position.x + bumpDist) && !xa.playerDead)
			{
				xa.glx = transform.position;
				xa.glx.x = (xa.player.transform.position.x - bumpDist);
				transform.position = xa.glx;
			}
		}
		if (bumpCameraOnTop)
		{
			//was 16
			bumpDist = 15;
			if (forceBumpDist != 0) { bumpDist = forceBumpDist; }
			if (xa.player.transform.position.y > (transform.position.y + bumpDist) && !xa.playerDead)
			{
				xa.glx = transform.position;
				xa.glx.y = (xa.player.transform.position.y - bumpDist);
				transform.position = xa.glx;
			}
		}
		//xa.glx = transform.localEulerAngles;
		//xa.glx.z += 25 * fa.deltaTime;
		//transform.localEulerAngles = xa.glx;

		if (neverCatchCamera && ((waitForPlayerToMove && xa.playerHasMoved) || !waitForPlayerToMove))
		{

			distAcrossScreen = Mathf.RoundToInt(result.x * 10);
			if (distAcrossScreen > neverCatchScreenPoint && !itweeningAhead)
			{
				itweeningAhead = true;
				iTween.MoveBy(this.gameObject, iTween.Hash("time", neverCatchTime, "x", neverCatchAmount, "easetype", iTween.EaseType.easeInOutSine, "oncomplete", "stoppedItweeningAhead", "oncompletetarget", this.gameObject, "islocal", true));

			}
		}
		if (cameraFollowsOnXInStages)
		{
			if (xa.camGoalX == 0 || Mathf.Abs(xa.camGoalX - transform.position.x) < 5)
			{
				distAcrossScreen = Mathf.RoundToInt(result.x * 10);
				//Setup.GC_DebugLog(goalX + " dist: " + distAcrossScreen);
				if (distAcrossScreen > 8)
				{
					xa.camGoalX = Camera.main.GetComponent<Camera>().transform.position.x + 32;
					iTween.MoveTo(this.gameObject, new Vector3(xa.camGoalX, transform.position.y, transform.position.z), 2);
				}
			}
		}

		//Setup.GC_DebugLog(Mathf.Abs(xa.camGoalX - transform.position.x) + " " + xa.camGoalX);

		if (useGoToVec)
		{
			iTween.MoveTo(this.gameObject, goToVec, goToVecSpeed);
			useGoToVec = false;
		}

		if (scroll && !xa.frozenCamera) {
			if (spd < scrollSpd) {
				spd += addSpd * fa.deltaTime;
			}
			if (spd > scrollSpd) {
				spd = scrollSpd;
			}


			if (waitForPlayerToMove)
			{
				spd = 0;
				extraSpd = 0;
				if (xa.playerHasMoved)
				{
					waitForPlayerToMove = false;
				}
			}
			else
			{
				if (delay > 0)
				{
					spd = 0;
					extraSpd = 0;
					delay -= 10 * fa.deltaTime;
				}
			}

			if (forceSpd != 0)
			{
				spd = forceSpd;
				forceSpd = 0;
			}

			if (xa.player)
			{
				//result = Camera.main.camera.WorldToViewportPoint(xa.player.transform.position);
				distAcrossScreen = Mathf.RoundToInt(result.x * 10);

				if (distAcrossScreen >= extraSpdThreshold)
				{
					extraSpd += extraSpdAdd * fa.deltaTime;

					if (distAcrossScreen < 9 && (extraSpd > extraSpdMax * 0.9f)) { extraSpd = extraSpdMax * 0.9f; }
					if (distAcrossScreen < 8.5f && (extraSpd > extraSpdMax * 0.5f)) { extraSpd = extraSpdMax * 0.5f; }
					if (distAcrossScreen < 7.7f && (extraSpd > extraSpdMax * 0.3f)) { extraSpd = extraSpdMax * 0.3f; }
					if (distAcrossScreen < 7.3f && (extraSpd > extraSpdMax * 0.1f)) { extraSpd = extraSpdMax * 0.1f; }
				}
				else
				{
					extraSpd -= extraSpdSubtract * fa.deltaTime;
				}
				if (extraSpd < 0) { extraSpd = 0; }
				if (extraSpd > extraSpdMax) { extraSpd = extraSpdMax; }

				//if (result.x > 0.7f) { extraSpd += 1.8f * fa.deltaTime; }
				//else { extraSpd -= 2.8f * fa.deltaTime; }
				//if (extraSpd < 0) { extraSpd = 0; }
				//if (extraSpd > 6) { extraSpd = 6; }
				//if (extraSpd > (Mathf.RoundToInt(result.x * 10) * 0.4f)) { extraSpd = (Mathf.RoundToInt(result.x * 10) * 0.4f); }
				//Setup.GC_DebugLog(result.x);
			}


			if (spd > 0)
			{
				if (cameraGoesVertical)
				{
					transform.Translate(0, (spd + extraSpd) * fa.deltaTime, 0);
				}
				else
				{
					xa.glx.x = (spd + extraSpd) * fa.deltaTime;
					if (xa.glx.x > maxScrollSpeed) { xa.glx.x = maxScrollSpeed; }
					transform.Translate(xa.glx.x, 0, 0);
				}
			}

			if (useClamp)
			{
				Vector3 clampedPosition = transform.position;

				if (maxClamp.x != 0 && transform.position.x > maxClamp.x)
				{
					clampedPosition.x = maxClamp.x;
				}
				if (maxClamp.y != 0 && transform.position.y > maxClamp.y)
				{
					clampedPosition.y = maxClamp.y;
				}
				if (minClamp.x != 0 && transform.position.x < minClamp.x)
				{
					clampedPosition.x = minClamp.x;
				}
				if (minClamp.y != 0 && transform.position.y < minClamp.y)
				{
					clampedPosition.y = minClamp.y;
				}

				transform.position = clampedPosition;
			}
		}
	}

	public void catchUpToPlayerX()
	{
		if (xa.player)
		{
			iTween.MoveTo(this.gameObject, iTween.Hash("position", new Vector3(xa.player.transform.position.x, transform.position.y, transform.position.z), "time", 3, "easetype", iTween.EaseType.easeInOutSine));
		}
	}
	public void catchUpToPlayerY()
	{
		if (xa.player)
		{
			iTween.MoveTo(this.gameObject, iTween.Hash("position", new Vector3(transform.position.x, xa.player.transform.position.y, transform.position.z), "time", 3, "easetype", iTween.EaseType.easeInOutSine));
		}
	}
}
