using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Structs;

[ExecuteInEditMode]
public class SetNSWE_WorldMap : MonoBehaviour
{
	public bool setNSWE = false;
	public bool done = false;

	void Start()
	{

	}

	void Update()
	{
		if (done) { return; }
		if (setNSWE) { setNSWEFunc(); }
	}

	void setNSWEFunc()
	{
		Debug.Log("Setting NSWE...");
		List<Int3> nodes = new List<Int3>();
		GameObject[] gos = GameObject.FindGameObjectsWithTag("metaMapNode");
		NodeScript nodeScript = null;


		for (int i = 0; i < gos.Length; i++)
		{
			Int3 int3 = new Int3(Mathf.RoundToInt(gos[i].transform.position.x), Mathf.RoundToInt(gos[i].transform.position.y), i);
			nodes.Add(int3);
			//Debug.Log("Nodes counted: " + nodes.Count);
		}

		for (int i = 0; i < gos.Length; i++)
		{
			nodeScript = gos[i].GetComponent<NodeScript>();

			if (nodeScript != null && !nodeScript.dontAutoSet)
			{
				nodeScript.north = null;
				nodeScript.south = null;
				nodeScript.west = null;
				nodeScript.east = null;

				int x = Mathf.RoundToInt(gos[i].transform.position.x);
				int y = Mathf.RoundToInt(gos[i].transform.position.y);
				int xOff = 0;
				int yOff = 0;

				Debug.Log("Checking: " + gos[i].name);
				//now search for a node to the north
				for (int a = 0; a < nodes.Count; a++)
				{
					xOff = 0; yOff = 0;
					if (Mathf.RoundToInt(gos[i].transform.localEulerAngles.z) == 0) { yOff = 6; }
					if (Mathf.RoundToInt(gos[i].transform.localEulerAngles.z) == 90) { xOff = -6; }
					if (Mathf.RoundToInt(gos[i].transform.localEulerAngles.z) == 180) { yOff = -6; }
					if (Mathf.RoundToInt(gos[i].transform.localEulerAngles.z) == 270) { xOff = 6; }

					if (nodes[a].x == (x + xOff) && nodes[a].y == (y + yOff))
					{
						Debug.Log("Match!");
						nodeScript.north = gos[nodes[a].z].GetComponent<NodeScript>();
					}
				}
				//now search for a node to the south
				for (int a = 0; a < nodes.Count; a++)
				{
					xOff = 0; yOff = 0;
					if (Mathf.RoundToInt(gos[i].transform.localEulerAngles.z) == 0) { yOff = -6; }
					if (Mathf.RoundToInt(gos[i].transform.localEulerAngles.z) == 180) { yOff = 6; }
					if (Mathf.RoundToInt(gos[i].transform.localEulerAngles.z) == 90) { xOff = 6; }
					if (Mathf.RoundToInt(gos[i].transform.localEulerAngles.z) == 270) { xOff = -6; }

					if (nodes[a].x == (x + xOff) && nodes[a].y == (y + yOff))
					{
						Debug.Log("Match!");
						nodeScript.south = gos[nodes[a].z].GetComponent<NodeScript>();
					}
				}
				//now search for a node to the east
				for (int a = 0; a < nodes.Count; a++)
				{
					xOff = 0; yOff = 0;
					if (Mathf.RoundToInt(gos[i].transform.localEulerAngles.z) == 0) { xOff = 6; }
					if (Mathf.RoundToInt(gos[i].transform.localEulerAngles.z) == 180) { xOff = -6; }
					if (Mathf.RoundToInt(gos[i].transform.localEulerAngles.z) == 90) { yOff = 6; }
					if (Mathf.RoundToInt(gos[i].transform.localEulerAngles.z) == 270) { yOff = -6; }

					if (nodes[a].x == (x + xOff) && nodes[a].y == (y + yOff))
					{
						Debug.Log("Match!");
						nodeScript.east = gos[nodes[a].z].GetComponent<NodeScript>();
					}
				}
				//now search for a node to the west
				for (int a = 0; a < nodes.Count; a++)
				{
					xOff = 0; yOff = 0;
					if (Mathf.RoundToInt(gos[i].transform.localEulerAngles.z) == 0) { xOff = -6; }
					if (Mathf.RoundToInt(gos[i].transform.localEulerAngles.z) == 180) { xOff = 6; }
					if (Mathf.RoundToInt(gos[i].transform.localEulerAngles.z) == 90) { yOff = -6; }
					if (Mathf.RoundToInt(gos[i].transform.localEulerAngles.z) == 270) { yOff = 6; }

					if (nodes[a].x == (x + xOff) && nodes[a].y == (y + yOff))
					{
						Debug.Log("Match!");
						nodeScript.west = gos[nodes[a].z].GetComponent<NodeScript>();
					}
				}
			}
		}

		Debug.Log("Done!");
		done = true;

	}
}
