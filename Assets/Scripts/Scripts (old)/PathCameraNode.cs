using UnityEngine;

public class PathCameraNode : MonoBehaviour
{
	int id             = 0;
	int previousNodeID = -1;
	int nextNodeID     = -1;
	bool isInitialized = false;

	// Use this for initialization
	public void Init(Vector3 position, int newID, int prevNode, int nextNode)
	{
		if (!isInitialized)
		{
			isInitialized                 = true;
			gameObject.transform.position = position;
			if (newID != -1) id           = newID;
			SetPreviousNode(prevNode);
			SetNextNode(nextNode);
		}
	}

	public void Disable()
	{
		isInitialized = false;
	}
	
	public int GetID()
	{
		if (!isInitialized) throw new UnityException();
		return id;
	}

	public int GetNextNode()
	{
		if (!isInitialized) throw new UnityException();
		return nextNodeID;
	}
	
	public int GetPreviousNode()
	{
		if (!isInitialized) throw new UnityException();
		return previousNodeID;
	}

	public void SetNextNode(PathCameraNode node)
	{
		if (!isInitialized) throw new UnityException();
		SetNextNode(node.GetID());
	}
	
	public void SetNextNode(int id)
	{
		if (!isInitialized) throw new UnityException();
		nextNodeID = id;
	}

	public void SetPreviousNode(PathCameraNode node)
	{
		if (!isInitialized) throw new UnityException();
		SetPreviousNode(node.GetID());
	}
	
	public void SetPreviousNode(int id)
	{
		if (!isInitialized) throw new UnityException();
		previousNodeID = id;
	}

	// Update is called once per frame
	void Update()
	{
		if (!isInitialized) return;
	}
}
