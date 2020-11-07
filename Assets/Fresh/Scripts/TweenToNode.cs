using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Holoville.HOTween;
using Holoville.HOTween.Plugins;

public class TweenToNode : MonoBehaviour
{
	public iTween.EaseType easeType;
	public iTween.LoopType loopType;
	public float time;
	public GameObject node;

	void Start()
	{
		node.GetComponent<MeshRenderer>().enabled = false;
		iTween.MoveTo(this.gameObject, iTween.Hash("time", time, "easetype", easeType, "x", node.transform.position.x, "y", node.transform.position.y, "looptype", loopType));
	}

}
