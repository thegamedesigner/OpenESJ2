using UnityEngine;

public class StateBasedCamera : MonoBehaviour
{
	public enum stateTypes {
		None,
		HorPush,
		VerPush,
		Pos,
		HorScrolling,
		VerScrolling,
		HorScrollingWithBumpRight,
		VerScrollingWithBumpUp,
		HorPushWideBox,
		SlowPos,
		HorPushLookAhead,
		HorVerPush,
		VerySlowPos,
		VerPushHighBox,
		InstaPos
	}

	[HideInInspector] public stateTypes state  = stateTypes.None;
	[HideInInspector] public float trackXOrY   = 0;            //the x or y of the rail on the non-movement axis (eg: the height of a hor rail) the camera will run along in a Hor or Ver state (y if hor, x if ver)
	[HideInInspector] public Vector2 pos       = Vector2.zero; //moves to this pos & stays there, if in Pos state
	[HideInInspector] public float scrollSpeed = 0;            //if in a scrolling state, will scroll at this speed
	[HideInInspector] public float min         = -9999;        //won't go less than this on the movement axis (x if hor, y if ver)
	[HideInInspector] public float max         = 9999;         //won't go further than this on the movement axis (x if hor, y if ver)
	[HideInInspector] public float min2        = -9999;        //won't go less than this on the movement axis (used as a second min/max for y, if x & y min/maxes are required)
	[HideInInspector] public float max2        = 9999;         //won't go less than this on the movement axis (used as a second min/max for y, if x & y min/maxes are required)
	[HideInInspector] public float minX        = -9999;
	[HideInInspector] public float maxX        = 9999;
	[HideInInspector] public float minY        = -9999;
	[HideInInspector] public float maxY        = 9999;
	float minDist                              = 0.2f;
	float timeToTarget                         = 0.5f;
	float timeToTargetSlow                     = 2f;
	float timeToTargetVerySlow                 = 7f;
	bool itweening                             = false;

	void arrivedAtPos()
	{
		itweening = false;
	}

	void Start()
	{
		//translate min1/2 into x/y
		//if minX/Y have not been used or set.
		if (minX == -9999 && maxX == 9999 && minY == -9999 && maxY == 9999) {
			//translate min1/2 into them
			if (state == stateTypes.HorPush) {
				minX = min;
				maxX = max;
			}
			if (state == stateTypes.VerPush) {
				minY = min;
				maxY = max;
			}
			if (state == stateTypes.HorVerPush) {
				minX = min;
				maxX = max;
				minY = min2;
				maxY = max2;
			}
			if (state == stateTypes.HorScrolling) {
				minX = min;
				maxX = max;
			}
			if (state == stateTypes.VerScrolling) {
				minY = min;
				maxY = max;
			}
			if (state == stateTypes.HorScrollingWithBumpRight) {
				minX = min;
				maxX = max;
			}
			if (state == stateTypes.VerScrollingWithBumpUp) {
				minY = min;
				maxY = max;
			}
			if (state == stateTypes.HorPushWideBox) {
				minX = min;
				maxX = max;
			}
			if (state == stateTypes.HorPushLookAhead) {
				minX = min;
				maxX = max;
			}
			if (state == stateTypes.VerPushHighBox) {
				minY = min;
				maxY = max;
			}
		}
	}

	void Update()
	{
		if (EditorController.IsEditorActive()) {
			return;
		}

		handleItweening();
		if (!itweening)
		{
			if (state == stateTypes.HorPush)
			{
				//adjust on x
				if (xa.player.transform.position.x > (transform.position.x + 5) && !xa.playerDead)
				{
					xa.glx = transform.position;
					xa.glx.x = (xa.player.transform.position.x - 5);
					transform.position = xa.glx;
				}
				if (xa.player.transform.position.x < (transform.position.x - 5) && !xa.playerDead)
				{
					xa.glx = transform.position;
					xa.glx.x = (xa.player.transform.position.x + 5);
					transform.position = xa.glx;
				}

				//handle min/max
				xa.glx = transform.position;
				xa.glx.x = Mathf.Clamp(xa.glx.x,minX, maxX);
					transform.position = xa.glx;
	
			}
			if (state == stateTypes.VerPush)
			{
				//adjust on y
				if (xa.player.transform.position.y > (transform.position.y + 3) && !xa.playerDead)
				{
					xa.glx = transform.position;
					xa.glx.y = (xa.player.transform.position.y - 3);
					transform.position = xa.glx;
				}
				if (xa.player.transform.position.y < (transform.position.y - 5) && !xa.playerDead)
				{
					xa.glx = transform.position;
					xa.glx.y = (xa.player.transform.position.y + 5);
					transform.position = xa.glx;
				}
				//handle min/max
				xa.glx = transform.position;
				xa.glx.y = Mathf.Clamp(xa.glx.y, minY, maxY);
				transform.position = xa.glx;
			}
			if (state == stateTypes.HorVerPush)
			{
				//adjust on x
				if (xa.player.transform.position.x > (transform.position.x + 5) && !xa.playerDead)
				{
					xa.glx = transform.position;
					xa.glx.x = (xa.player.transform.position.x - 5);
					transform.position = xa.glx;
				}
				if (xa.player.transform.position.x < (transform.position.x - 5) && !xa.playerDead)
				{
					xa.glx = transform.position;
					xa.glx.x = (xa.player.transform.position.x + 5);
					transform.position = xa.glx;
				}

				//adjust on y
				if (xa.player.transform.position.y > (transform.position.y + 3) && !xa.playerDead)
				{
					xa.glx = transform.position;
					xa.glx.y = (xa.player.transform.position.y - 3);
					transform.position = xa.glx;
				}
				if (xa.player.transform.position.y < (transform.position.y - 5) && !xa.playerDead)
				{
					xa.glx = transform.position;
					xa.glx.y = (xa.player.transform.position.y + 5);
					transform.position = xa.glx;
				}

				//handle min/max
				xa.glx = transform.position;
				xa.glx.x = Mathf.Clamp(xa.glx.x, minX, maxX);
				xa.glx.y = Mathf.Clamp(xa.glx.y, minY, maxY);
				transform.position = xa.glx;

			}
			if (state == stateTypes.Pos)
			{
			}
			if (state == stateTypes.HorScrolling)
			{
				//scroll on x
				if (!xa.playerDead)
				{
					xa.glx = transform.position;
					xa.glx.x += scrollSpeed * fa.deltaTime;
					transform.position = xa.glx;
				}
				//handle min/max
				xa.glx = transform.position;
				xa.glx.x = Mathf.Clamp(xa.glx.x, minX, maxX);
				transform.position = xa.glx;
			}
			if (state == stateTypes.VerScrolling)
			{
				//scroll on y
				if (!xa.playerDead)
				{
					xa.glx = transform.position;
					xa.glx.y += scrollSpeed * fa.deltaTime;
					transform.position = xa.glx;
				}
				//handle min/max
				xa.glx = transform.position;
				xa.glx.y = Mathf.Clamp(xa.glx.y, minY, maxY);
				transform.position = xa.glx;
			}
			if (state == stateTypes.HorScrollingWithBumpRight)
			{
				//scroll on x
				if (!xa.playerDead)
				{
					xa.glx = transform.position;
					xa.glx.x += scrollSpeed * fa.deltaTime;
					transform.position = xa.glx;
				}

				//adjust on x
				if (xa.player.transform.position.x > (transform.position.x + 4) && !xa.playerDead)
				{
					xa.glx = transform.position;
					xa.glx.x = (xa.player.transform.position.x - 4);
					transform.position = xa.glx;
				}

				//handle min/max
				xa.glx = transform.position;
				xa.glx.x = Mathf.Clamp(xa.glx.x, minX, maxX);
				transform.position = xa.glx;
			}
			if (state == stateTypes.VerScrollingWithBumpUp)
			{
				//scroll on y
				if (!xa.playerDead)
				{
					xa.glx = transform.position;
					xa.glx.y += scrollSpeed * fa.deltaTime;
					transform.position = xa.glx;
				}

				//adjust on y
				if (xa.player.transform.position.y > (transform.position.y + 6) && !xa.playerDead)
				{
					xa.glx = transform.position;
					xa.glx.y = (xa.player.transform.position.y - 6);
					transform.position = xa.glx;
				}

				//handle min/max
				xa.glx = transform.position;
				xa.glx.y = Mathf.Clamp(xa.glx.y, minY, maxY);
				transform.position = xa.glx;
			}
			if (state == stateTypes.HorPushWideBox)
			{
				//adjust on x
				if (xa.player.transform.position.x > (transform.position.x + 7.5f) && !xa.playerDead)
				{
					xa.glx = transform.position;
					xa.glx.x = (xa.player.transform.position.x - 7.5f);
					transform.position = xa.glx;
				}
				if (xa.player.transform.position.x < (transform.position.x - 7.5f) && !xa.playerDead)
				{
					xa.glx = transform.position;
					xa.glx.x = (xa.player.transform.position.x + 7.5f);
					transform.position = xa.glx;
				}

				//handle min/max
				xa.glx = transform.position;
				xa.glx.x = Mathf.Clamp(xa.glx.x, minX, maxX);
				transform.position = xa.glx;

			}
			if (state == stateTypes.SlowPos)
			{
			}
			if (state == stateTypes.HorPushLookAhead)
			{
				//adjust on x
				if (xa.player.transform.position.x > (transform.position.x - 1) && !xa.playerDead)
				{
					xa.glx = transform.position;
					xa.glx.x = (xa.player.transform.position.x + 1);
					transform.position = xa.glx;
				}
				if (xa.player.transform.position.x < (transform.position.x - 5) && !xa.playerDead)
				{
					xa.glx = transform.position;
					xa.glx.x = (xa.player.transform.position.x + 5);
					transform.position = xa.glx;
				}

				//handle min/max
				xa.glx = transform.position;
				xa.glx.x = Mathf.Clamp(xa.glx.x, minX, maxX);
				transform.position = xa.glx;

			}
			if (state == stateTypes.VerySlowPos)
			{
			}
			if (state == stateTypes.VerPushHighBox)
			{
				//adjust on y
				if (xa.player.transform.position.y > (transform.position.y + 3) && !xa.playerDead)
				{
					xa.glx = transform.position;
					xa.glx.y = (xa.player.transform.position.y - 3);
					transform.position = xa.glx;
				}
				if (xa.player.transform.position.y < (transform.position.y + 1) && !xa.playerDead)
				{
					xa.glx = transform.position;
					xa.glx.y = (xa.player.transform.position.y - 1);
					transform.position = xa.glx;
				}
				//handle min/max
				xa.glx = transform.position;
				xa.glx.y = Mathf.Clamp(xa.glx.y, minY, maxY);
				transform.position = xa.glx;
			}
			if (state == stateTypes.InstaPos)
			{
			}
		}
	}

	void handleItweening()
	{
		if (!itweening)
		{
			//drift to correct x or y
			if (state == stateTypes.HorPush)
			{
				//adjust to the correct y (because the movement is along the x)
				if (Vector3.Distance(new Vector3(transform.position.y, 0, 0), new Vector3(trackXOrY, 0, 0)) > minDist)
				{
					itweening = true;
					iTween.MoveTo(this.gameObject, iTween.Hash("x", xa.player.transform.position.x, "y", trackXOrY, "time", timeToTarget, "easetype", iTween.EaseType.easeInOutSine, "oncomplete", "arrivedAtPos", "oncompletetarget", this.gameObject));
				}
			}
			if (state == stateTypes.VerPush)
			{
				//adjust to the correct x (because the movement is along the y)
				if (Vector3.Distance(new Vector3(transform.position.x, 0, 0), new Vector3(trackXOrY, 0, 0)) > minDist)
				{
					itweening = true;
					iTween.MoveTo(this.gameObject, iTween.Hash("y", xa.player.transform.position.y, "x", trackXOrY, "time", timeToTarget, "easetype", iTween.EaseType.easeInOutSine, "oncomplete", "arrivedAtPos", "oncompletetarget", this.gameObject));
				}
			}
			if (state == stateTypes.Pos)
			{
				//adjust to the correct pos
				if (Vector3.Distance(new Vector3(transform.position.x, transform.position.y, 0), new Vector3(pos.x, pos.y, 0)) > 0.5f)
				{
					itweening = true;
					iTween.MoveTo(this.gameObject, iTween.Hash("x", pos.x, "y", pos.y, "time", timeToTarget, "easetype", iTween.EaseType.easeInOutSine, "oncomplete", "arrivedAtPos", "oncompletetarget", this.gameObject));
				}
			}
			if (state == stateTypes.HorScrolling)
			{
				//adjust to the correct y (because the movement is along the x)
				if (Vector3.Distance(new Vector3(transform.position.y, 0, 0), new Vector3(trackXOrY, 0, 0)) > minDist)
				{
					itweening = true;
					iTween.MoveTo(this.gameObject, iTween.Hash("x", xa.player.transform.position.x, "y", trackXOrY, "time", timeToTarget, "easetype", iTween.EaseType.easeInOutSine, "oncomplete", "arrivedAtPos", "oncompletetarget", this.gameObject));
				}
			}
			if (state == stateTypes.HorScrollingWithBumpRight)
			{
				//adjust to the correct y (because the movement is along the x)
				if (Vector3.Distance(new Vector3(transform.position.y, 0, 0), new Vector3(trackXOrY, 0, 0)) > minDist)
				{
					itweening = true;
					iTween.MoveTo(this.gameObject, iTween.Hash("x", xa.player.transform.position.x, "y", trackXOrY, "time", timeToTarget, "easetype", iTween.EaseType.easeInOutSine, "oncomplete", "arrivedAtPos", "oncompletetarget", this.gameObject));
				}
			}
			if (state == stateTypes.VerScrolling)
			{
				//adjust to the correct x (because the movement is along the y)
				if (Vector3.Distance(new Vector3(transform.position.x, 0, 0), new Vector3(trackXOrY, 0, 0)) > minDist)
				{
					itweening = true;
					iTween.MoveTo(this.gameObject, iTween.Hash("y", xa.player.transform.position.y, "x", trackXOrY, "time", timeToTarget, "easetype", iTween.EaseType.easeInOutSine, "oncomplete", "arrivedAtPos", "oncompletetarget", this.gameObject));
				}
			}
			if (state == stateTypes.HorPushWideBox)
			{
				//adjust to the correct y (because the movement is along the x)
				if (Vector3.Distance(new Vector3(transform.position.y, 0, 0), new Vector3(trackXOrY, 0, 0)) > minDist)
				{
					itweening = true;
					iTween.MoveTo(this.gameObject, iTween.Hash("x", xa.player.transform.position.x, "y", trackXOrY, "time", timeToTarget, "easetype", iTween.EaseType.easeInOutSine, "oncomplete", "arrivedAtPos", "oncompletetarget", this.gameObject));
				}
			}
			if (state == stateTypes.SlowPos)
			{
				//adjust to the correct pos
				if (Vector3.Distance(new Vector3(transform.position.x, transform.position.y, 0), new Vector3(pos.x, pos.y, 0)) > 0.5f)
				{
					itweening = true;
					iTween.MoveTo(this.gameObject, iTween.Hash("x", pos.x, "y", pos.y, "time", timeToTargetSlow, "easetype", iTween.EaseType.easeInOutSine, "oncomplete", "arrivedAtPos", "oncompletetarget", this.gameObject));
				}
			}
			if (state == stateTypes.HorPushLookAhead)
			{
				//adjust to the correct y (because the movement is along the x)
				if (Vector3.Distance(new Vector3(transform.position.y, 0, 0), new Vector3(trackXOrY, 0, 0)) > minDist)
				{
					itweening = true;
					iTween.MoveTo(this.gameObject, iTween.Hash("x", xa.player.transform.position.x + 2, "y", trackXOrY, "time", timeToTarget, "easetype", iTween.EaseType.easeInOutSine, "oncomplete", "arrivedAtPos", "oncompletetarget", this.gameObject));
				}
			}
			if (state == stateTypes.HorVerPush)
			{
			}
			if (state == stateTypes.VerySlowPos)
			{
				//adjust to the correct pos
				if (Vector3.Distance(new Vector3(transform.position.x, transform.position.y, 0), new Vector3(pos.x, pos.y, 0)) > 0.5f)
				{
					itweening = true;
					iTween.MoveTo(this.gameObject, iTween.Hash("x", pos.x, "y", pos.y, "time", timeToTargetVerySlow, "easetype", iTween.EaseType.easeInOutSine, "oncomplete", "arrivedAtPos", "oncompletetarget", this.gameObject));
				}
			}
			if (state == stateTypes.VerPushHighBox)
			{
				//adjust to the correct x (because the movement is along the y)
				if (Vector3.Distance(new Vector3(transform.position.x, 0, 0), new Vector3(trackXOrY, 0, 0)) > minDist)
				{
					itweening = true;
					iTween.MoveTo(this.gameObject, iTween.Hash("y", xa.player.transform.position.y, "x", trackXOrY, "time", timeToTarget, "easetype", iTween.EaseType.easeInOutSine, "oncomplete", "arrivedAtPos", "oncompletetarget", this.gameObject));
				}
			}
			if (state == stateTypes.InstaPos)
			{
				//adjust to the correct pos
				if (Vector3.Distance(new Vector3(transform.position.x, transform.position.y, 0), new Vector3(pos.x, pos.y, 0)) > 0.5f)
				{
					itweening = true;
					iTween.MoveTo(this.gameObject, iTween.Hash("x", pos.x, "y", pos.y, "time", 0, "easetype", iTween.EaseType.easeInOutSine, "oncomplete", "arrivedAtPos", "oncompletetarget", this.gameObject));
				}
			}
		}
	}
}
