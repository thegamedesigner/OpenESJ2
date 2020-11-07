using UnityEngine;
using System.Collections;

public class NodePathBossScript : MonoBehaviour
{
	public GameObject[] nodes = new GameObject[0];

	/*
	 Each node contains a tag & a script. 
	 Each node has a few simple options, mostly contained in a single enum choice.
	 */

	int index                     = 0;
	NodePathNodeScript nodeScript = null;
	bool moving                   = false;
	//bool callActionFunc           = false;
	//float timeSet                 = 0;

	void Start()
	{

	}

	void Update()
	{
		if (!moving)
		{
			if (nodes[index])//find the next node
			{
				//find the script
				nodeScript = null;
				nodeScript = nodes[index].GetComponent<NodePathNodeScript>();
				if (nodeScript)
				{
					doAction();
					//callActionFunc = true;
				}
			}
		}

		//if (callActionFunc)
		//{
		//  doAction();
		//}
	}

	void doAction()
	{
		//Setup.GC_DebugLog("Do the action! Enable the behaviour!");


		//trigger the behaviour
		if (nodeScript.enableThisBehaviour) { nodeScript.enableThisBehaviour.enabled = true; }

		//move (if desired)
	   // Setup.GC_DebugLog("Start movement itween!");
		iTween.MoveTo(this.gameObject, iTween.Hash("x", nodes[index].transform.position.x, "y", nodes[index].transform.position.y, "time", nodeScript.moveTime, "easetype", nodeScript.moveType, "oncomplete", "arrivedAtNode", "oncompletetarget", this.gameObject));
		//callActionFunc = false;
		moving = true;

	}

	public void arrivedAtNode()
	{
	   // Setup.GC_DebugLog("Arrived At Node " + index);
		moving = false;
		index++;
		if (index >= nodes.Length) { index = 0; }
	}

	public void jumpToNode(string nodeNumber)
	{
		index = int.Parse(nodeNumber);
		moving = false;
	   // Setup.GC_DebugLog("Jumped to " + index);
	}
}
