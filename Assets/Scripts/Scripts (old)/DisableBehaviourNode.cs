using UnityEngine;
using System.Collections;

public class DisableBehaviourNode : MonoBehaviour
{
	public GameObject goToCheckDistAgainst = null;
	public GameObject goToDisableStuffOn = null;
	public float dist = 0;
	public new string name = "";
	public bool enable = false;
	public bool dontDisableThisAfterTriggering = false;

	Behaviour result;

	void Start()
	{

	}

	void Update()
	{
		if (goToCheckDistAgainst)
		{
			xa.glx = transform.position;
			xa.glx.z = goToCheckDistAgainst.transform.position.z;
			if (Vector3.Distance(xa.glx, goToCheckDistAgainst.transform.position) < dist)
			{
				result = (Behaviour)(goToDisableStuffOn.GetComponent(name));
				if (result.enabled) { result.enabled = false; }
				if (enable && !result.enabled) { result.enabled = true; }
				if (!dontDisableThisAfterTriggering) { this.enabled = false; }
			}
		}
	}
}
