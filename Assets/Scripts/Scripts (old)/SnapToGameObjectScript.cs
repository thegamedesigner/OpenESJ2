using UnityEngine;
using System.Collections;

public class SnapToGameObjectScript : MonoBehaviour
{
	public GameObject snapToThis = null;

	// Use this for initialization
	void Start()
	{
		if(xa.createdObjects)
			transform.parent = xa.createdObjects.transform;
	}

	// Update is called once per frame
	void Update()
	{
		if (snapToThis)
		{
			transform.position = snapToThis.transform.position;
		}
	}
}
