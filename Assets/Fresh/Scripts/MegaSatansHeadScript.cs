using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaSatansHeadScript : MonoBehaviour
{
	public GameObject muzzlePoint;
	public GameObject scythe;
	float timeset;
	float delay = 4f;

	void Start()
	{
		timeset = fa.time + 1.5f;
		iTween.MoveBy(this.gameObject, iTween.Hash("y", 12, "x", 1, "time", 4, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));
	}

	// Update is called once per frame
	void Update()
	{
		if (fa.time > (timeset + delay))
		{
			timeset = fa.time;
			GameObject go = Instantiate(scythe,muzzlePoint.transform.position,muzzlePoint.transform.rotation);

		}
	}
}
