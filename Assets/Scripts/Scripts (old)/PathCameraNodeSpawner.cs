using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PathCameraNodeSpawner : MonoBehaviour
{
	List<PathCameraNode> cameraPath = new List<PathCameraNode>();
	Queue<int> freeNodes			= new Queue<int>();

	int GetUnusedID()
	{
		int node = -1;
		if (freeNodes.Count > 0)
		{
			node = freeNodes.Dequeue();
		}
		return node;
	}
	
	PathCameraNode CreateNode(Vector3 position, int previousNodeID, int nextNodeID = -1)
	{
		int id = GetUnusedID();
		PathCameraNode node = null;
		if (id == -1)
		{
			// create a new node.
			node = new PathCameraNode();
			position.z = -10.0f;
			node.Init(position, cameraPath.Count, previousNodeID, nextNodeID);
			cameraPath.Add(node);
		}
		else
		{
			node = cameraPath[id];
			position.z = -10.0f;
			node.Init(position, -1, previousNodeID, nextNodeID); // keep the same ID (-1)
		}
		return node;
	}

	void RemoveNode(int id)
	{
		RemoveNode(cameraPath[id]);
	}
	
	void RemoveNode(PathCameraNode node)
	{
		PathCameraNode prev = cameraPath[node.GetPreviousNode()];
		PathCameraNode next = cameraPath[node.GetNextNode()];
		prev.SetNextNode(next.GetID());
		next.SetPreviousNode(prev.GetID());
		freeNodes.Enqueue(node.GetID());
		node.Disable();
	}
}
