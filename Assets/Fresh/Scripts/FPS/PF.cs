using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PF : MonoBehaviour
{
	public static List<Node> nodes = new List<Node>();

	public class Node
	{
		public GameObject go;
		public int steps = 0;
		public TextMesh textMesh;
		public List<Node> nodes = new List<Node>();
	}

	public static void InitNodes()
	{
		nodes = new List<Node>();

		GameObject go = GameObject.FindGameObjectWithTag("pfNodeController");
		
		GameObject[] gos = GameObject.FindGameObjectsWithTag("node");
		for (int i = 0; i < gos.Length; i++)
		{
			Node n = new Node();
			n.go = gos[i];
			n.textMesh = gos[i].GetComponentInChildren<TextMesh>();
			nodes.Add(n);
		}

		//Figure out all the nodes that each node can see
		Ray ray = new Ray();
		RaycastHit hit;
		LayerMask mask = 1 << 19;
		float dist = 0;

		for (int i = 0; i < nodes.Count; i++)
		{
			for (int a = 0; a < nodes.Count; a++)
			{
				if (a != i)
				{
					//Can I see this other node?
					nodes[i].go.transform.LookAt(nodes[a].go.transform.position);
					ray.origin = nodes[i].go.transform.position;
					ray.direction = nodes[i].go.transform.forward;
					dist = Vector3.Distance(ray.origin, nodes[a].go.transform.position);
					nodes[i].go.transform.localEulerAngles = Vector3.zero;

					if (!Physics.Raycast(ray, out hit, dist, mask))
					{
						nodes[i].nodes.Add(nodes[a]);
					}
				}
			}
		}


	}

	public static void CalcAllPaths()
	{
		//Can I see the player?


		Ray ray = new Ray();
		RaycastHit hit;
		LayerMask mask = 1 << 19;
		float dist = 0;

		//Can any nodes see the player?
		for (int i = 0; i < nodes.Count; i++)
		{
			nodes[i].steps = 999;
			nodes[i].go.transform.LookAt(FPSMainScript.playerPos);
			ray.origin = nodes[i].go.transform.position;
			ray.direction = nodes[i].go.transform.forward;
			nodes[i].go.transform.localEulerAngles = Vector3.zero;
			dist = Vector3.Distance(ray.origin, FPSMainScript.playerPos);
			if (Physics.Raycast(ray, out hit, dist, mask))
			{
				Debug.DrawLine(ray.origin, hit.point, Color.red);
			}
			else
			{
				Debug.DrawLine(ray.origin, ray.GetPoint(dist), Color.green);
				nodes[i].steps = 0;
			}
		}

		//Loop outwards again and again, from the nodes that can see the player
		for (int i = 0; i < 100; i++)
		{
			bool foundAnything = false;
			for (int a = 0; a < nodes.Count; a++)
			{
				//If this node can see the player, spread that knowledge
				if (nodes[a].steps <= i)
				{
					int result = i + 1;
					//go through each contact
					for (int b = 0; b < nodes[a].nodes.Count; b++)
					{
						if (nodes[a].nodes[b].steps > result)
						{
							foundAnything = true;
							nodes[a].nodes[b].steps = result;
						}
					}
				}
			}
			if(!foundAnything) {break; }//we're done. No node had a connection that could be set lower.
		}

		for (int i = 0; i < nodes.Count; i++)
		{
			nodes[i].textMesh.text = "" + nodes[i].steps;

		}

	}
}
