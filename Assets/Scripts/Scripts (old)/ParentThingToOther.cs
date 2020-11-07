using UnityEngine;
using System.Collections;

public class ParentThingToOther : MonoBehaviour {
	
	public GameObject thing;
	public GameObject newParent;
	public bool newPosition;
	public Vector3 newLocalPos;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(enabled)
		{
			thing.transform.parent = newParent.transform;
	
			if(newPosition)
			{
				thing.transform.localPosition = newLocalPos;
			}
	
			Destroy(this);
		}
	}
}
