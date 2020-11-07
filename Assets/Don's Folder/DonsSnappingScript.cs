using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonsSnappingScript : MonoBehaviour
{

	Vector3 SnapFunction(Vector3 pos, float gridSize)
	{		
		if(gridSize == 0) {return new Vector3(-9999,-9999,-9999); }

		pos.x /= gridSize;
		pos.y /= gridSize;

		pos.x = Mathf.Round(pos.x);
		pos.y = Mathf.Round(pos.y);

		pos.x *= gridSize;
		pos.y *= gridSize;

		return pos;
	}

	void Start()
	{
		transform.position = SnapFunction(transform.position,3);


	}

	void Update()
	{

	}
}
