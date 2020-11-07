using UnityEngine;
using System.Collections;

public class TriggerOnRaycastScript : MonoBehaviour
{
	public bool[] raycastInLeftRightUpDown = new bool[4];
	//public string instructions = "(1,0,0) is Right, -1 Left, (0,1,0) is Down, -1 up";
	public float[] dist = { 0.5f, 0.5f, 0.5f, 0.5f };
	public Vector3[] posOffset = new Vector3[4];
	public bool disableMeOnTrigger = false;
	public LayerMask mask = new LayerMask();
	public bool useRequiredTag = false;
	public string requiredTag = "";
	public Behaviour[] disableTheseFirst = new Behaviour[0];
	public Behaviour[] enableTheseSecond = new Behaviour[0];

	RaycastHit hit;
	Ray ray = new Ray();
	bool triggered = false;
	int index = 0;
	void Start()
	{

	}

	void Update()
	{
		triggered = false;
		index = 0;
		while (index < raycastInLeftRightUpDown.Length)
		{
			if (raycastInLeftRightUpDown[index])
			{
				ray.origin = this.gameObject.transform.position + posOffset[index];
				if (index == 0) { ray.direction = new Vector3(-1, 0, 0); }
				if (index == 1) { ray.direction = new Vector3(1, 0, 0); }
				if (index == 2) { ray.direction = new Vector3(0, -1, 0); }
				if (index == 3) { ray.direction = new Vector3(0, 1, 0); } 
				rayCast(dist[index]);
			}
			index++;
		}


		if (triggered)
		{
			index = 0;
			while (index < disableTheseFirst.Length)
			{
				if(disableTheseFirst[index]){ disableTheseFirst[index].enabled = false;}
				index++;
			}
			index = 0;
			while (index < enableTheseSecond.Length)
			{
			   //Setup.GC_DebugLog("GOT HERE");
				if (enableTheseSecond[index]) { enableTheseSecond[index].enabled = true; }
				index++;
			}

			if (disableMeOnTrigger) { this.enabled = false; }
			
		}
	}

	void rayCast(float dist)
	{
		Debug.DrawLine(ray.origin, ray.GetPoint(dist), Color.red);
		if (Physics.Raycast(ray, out hit, dist, mask) == true)
		{
			if (useRequiredTag)
			{
				if (hit.collider.gameObject.tag == requiredTag)
				{
					triggered = true;
				}
			}
			else
			{
				triggered = true;
			}
		}
	}
}
