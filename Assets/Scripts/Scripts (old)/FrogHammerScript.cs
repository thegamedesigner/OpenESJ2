using UnityEngine;

public class FrogHammerScript : MonoBehaviour
{
	public float firingSpeed = 0;
	public float dist = 0;
	public float speed = 0;
	public float startDelay = 0;
	public GameObject frogPuppet = null;
	
	AniScript_TriggeredAni aniScript;

	float counter = 0;
	float startCounter = 0;
	bool moving = false;

	void Start()
	{
		aniScript = frogPuppet.GetComponent<AniScript_TriggeredAni>();
		frogPuppet.transform.parent = null;
		xa.glx = frogPuppet.transform.position;
		xa.glx.z = transform.position.z;
		frogPuppet.transform.position = xa.glx;
	}

	void Update()
	{
		if (startCounter > startDelay)
		{
			if (!moving)
			{
				counter += 10 * fa.deltaTime;
				if (counter > firingSpeed)
				{
					counter = 0;
					fireTongue();
				}
			}
		}
		else
		{
			startCounter += 10 * fa.deltaTime;
		}
	}

	void fireTongue()
	{
		//find the distance (make sure it's not blocked by blocks)
		//call two itweens
		//draw a line between the tongue & the frog.
		moving = true;
		aniScript.triggerAni2();
		iTween.MoveBy(this.gameObject, iTween.Hash("time", speed, "x", -dist, "easetype", iTween.EaseType.easeOutSine, "oncomplete", "peakOfArc", "oncompletetarget", this.gameObject, "islocal", true));
	
	}

	void endOfArc()
	{
		moving = false;
		counter = 0;
	}

	void peakOfArc()
	{
		iTween.MoveBy(this.gameObject, iTween.Hash("time", speed, "x", dist, "easetype", iTween.EaseType.easeInSine, "oncomplete", "endOfArc", "oncompletetarget", this.gameObject, "islocal", true));
	}

	float getDistance()
	{
		Ray ray = new Ray();
		RaycastHit hit;
		float dist = 0;

		dist = 45;
		//check if beam is hitting anything
		xa.glx = transform.position;
		xa.glx.z = xa.GetLayer(xa.layers.PlayerAndBlocks);
		ray.origin = xa.glx;
		ray.direction = -transform.up;
		if (Physics.Raycast(ray, out hit, dist, 11))
		{
			if (hit.collider.gameObject.tag == "solidThing")
			{
				Debug.DrawLine(ray.origin, hit.point, Color.red);
				dist = hit.distance;
				xa.glx = hit.point;
				xa.glx.z = transform.position.z - 1;
			}
		}

		return (dist);
	}
}
