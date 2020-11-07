using UnityEngine;
using System.Collections;

public class TriggerOnDistFromPlayer : MonoBehaviour
{
	public float dist = 1;
	public Behaviour[] behaviours = new Behaviour[0];
	public bool disableBehaviours = false;
	public bool whenGreaterThanDist = false;
	void Start()
	{

	}

	void Update()
	{
		if (xa.player)
		{
			xa.glx = xa.player.transform.position;
			xa.glx.z = transform.position.z;
			if ((Vector3.Distance(xa.glx, transform.position) < dist && !whenGreaterThanDist) ||
				(Vector3.Distance(xa.glx, transform.position) > dist && whenGreaterThanDist))
			{
				foreach (Behaviour co in behaviours)
				{
					if (disableBehaviours) { co.enabled = false; }
					else { co.enabled = true; }
				}
				
				this.enabled = false;
			}
		}
	}
}
