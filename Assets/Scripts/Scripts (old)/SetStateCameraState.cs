using UnityEngine;
using System.Collections;

public class SetStateCameraState : MonoBehaviour
{
	public bool SetOnlyIfPlayerIsOnRightHalfOfScreen = false;
	public bool setState = false;
	public StateBasedCamera.stateTypes state = StateBasedCamera.stateTypes.None;
	public bool setTrackXOrY = false;
	public float trackXOrY = 0;
	public bool setPos = false;
	public Vector2 pos = Vector2.zero;//this is a pos the camera will tween to & stay at.
	public bool setScrollSpeed = false;
	public float scrollSpeed = 0;
	public bool setMin = false;
	public float min = 0;
	public bool setMax = false;
	public float max = 0;
	public bool setMin2 = false;
	public float min2 = 0;
	public bool setMax2 = false;
	public float max2 = 0;

	public bool setMinX = false;
	public float minX = 0;
	public bool setMaxX = false;
	public float maxX = 0;
	public bool setMinY = false;
	public float minY = 0;
	public bool setMaxY = false;
	public float maxY = 0;

	StateBasedCamera script = null;

	public bool triggerExternally = false;

	void Start()
	{
		script = Camera.main.GetComponent<Camera>().gameObject.GetComponent<StateBasedCamera>();
		if (script == null)
		{
			//check in parent, might be a screen-shake camera
			script = Camera.main.GetComponent<Camera>().gameObject.transform.parent.gameObject.GetComponent<StateBasedCamera>();

		}
	}

	void Update()
	{
		if (!triggerExternally)
		{
			setStats();
			this.enabled = false;
		}
	}

	public void setStats()
	{
		if (SetOnlyIfPlayerIsOnRightHalfOfScreen)
		{
			if (xa.playerPos.x <= Camera.main.GetComponent<Camera>().gameObject.transform.position.x)
			{
				return;
			}
		}
		if (script)
		{
			if (setState) { script.state = state; }
			if (setTrackXOrY) { script.trackXOrY = trackXOrY; }
			if (setScrollSpeed) { script.scrollSpeed = scrollSpeed; }
			if (setPos) { script.pos = pos; }
			if (setMin) { script.min = min; }
			if (setMax) { script.max = max; }
			if (setMin2) { script.min2 = min2; }
			if (setMax2) { script.max2 = max2; }

			if (setMinX) { script.minX = minX; }
			if (setMaxX) { script.maxX = maxX; }
			if (setMinY) { script.minY = minY; }
			if (setMaxY) { script.maxY = maxY; }
		}
	}

	public void SetFromEditor(string data, Vector3 node)
	{
		setState = true;
		Vector3 posData = transform.position;
		if (node != new Vector3(0, 0, 0))
		{
			posData = node;//Use node's pos, not just the center point of this camera trigger
		}
		if (data == "")
		{
			state = StateBasedCamera.stateTypes.None;
			return;
		}
		int type = int.Parse(data);
		switch (type)
		{
			case 1: //"Follow player";
				state = StateBasedCamera.stateTypes.HorVerPush;
				break;
			case 2://"Follow player horiztonally";
				state = StateBasedCamera.stateTypes.HorPush;
				setTrackXOrY = true;
				trackXOrY = posData.y;
				break;
			case 3://"Follow player vertically";
				state = StateBasedCamera.stateTypes.VerPush;
				setTrackXOrY = true;
				trackXOrY = posData.x;
				break;
			case 4://"Move to position of\nattached node";
				state = StateBasedCamera.stateTypes.Pos;
				setPos = true;
				pos.x = posData.x;
				pos.y = posData.y;
				break;
			case 5://"Move to position of\nattached node (slowly)";
				state = StateBasedCamera.stateTypes.VerySlowPos;
				setPos = true;
				pos.x = posData.x;
				pos.y = posData.y;
				break;
			case 6://"Scroll horiztonally";
				state = StateBasedCamera.stateTypes.HorScrolling;
				setTrackXOrY = true;
				trackXOrY = posData.y;
				break;
			case 7://"Scroll vertically";
				state = StateBasedCamera.stateTypes.VerScrolling;
				setTrackXOrY = true;
				trackXOrY = posData.x;
				break;
			case 8://"Scroll horiztonally\nWith Bump Right";
				state = StateBasedCamera.stateTypes.HorScrollingWithBumpRight;
				setTrackXOrY = true;
				trackXOrY = posData.y;
				break;
			case 9://"Scroll vertically\nWith Bump Up";
				state = StateBasedCamera.stateTypes.VerScrollingWithBumpUp;
				setTrackXOrY = true;
				trackXOrY = posData.x;
				break;
			case 10://Snap to position instantly
				state = StateBasedCamera.stateTypes.InstaPos;
				setPos = true;
				pos.x = posData.x;
				pos.y = posData.y;
				break;
		}
	}
}
