using UnityEngine;
using System.Collections;

public class NodePathNodeScript : MonoBehaviour
{
	[Multiline]
	public string Instructions = "1) The Boss enables a behaviour (if there is one).\n2) The Boss moves to *this* node's position.";
   // public bool useDelay = false;
	//public float delayTimeInSeconds = 0;
	
	public Behaviour enableThisBehaviour = null;

	public float moveTime = 0;
	public iTween.EaseType moveType = iTween.EaseType.easeInOutSine;

}
