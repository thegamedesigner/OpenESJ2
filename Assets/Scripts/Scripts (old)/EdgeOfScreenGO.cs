using UnityEngine;
using System.Collections;

public class EdgeOfScreenGO : MonoBehaviour
{
	public bool isBackOfScreen = false;
	public bool isFrontOfScreen = false;
	public bool isTopOfScreen = false;
	public bool isBottomOfScreen = false;

	void Start()
	{
		transform.parent = null;
	}

	void Update()
	{
		if (isBackOfScreen) { xa.backEdgeOfScreen = transform.position.x; }
		if (isFrontOfScreen) { xa.frontEdgeOfScreen = transform.position.x; }
		if (isTopOfScreen) { xa.topEdgeOfScreen = transform.position.y; }
		if (isBottomOfScreen) { xa.bottomEdgeOfScreen = transform.position.y; }
		/*
		Vector3 vec1 = Camera.main.camera.transform.position;
		vec1.z = 30;

		if (isBackOfScreen) { vec1.x -= 200; }
		if (isFrontOfScreen) { vec1.x += 200; }
		if (isTopOfScreen) { vec1.y += 100; }
		if (isBottomOfScreen) { vec1.y -= 100; }

		if (isBackOfScreen) { xa.backEdgeOfScreen = transform.position.x; }
		if (isFrontOfScreen) { xa.frontEdgeOfScreen = transform.position.x; }
		if (isTopOfScreen) { xa.topEdgeOfScreen = transform.position.y; }
		if (isBottomOfScreen) { xa.bottomEdgeOfScreen = transform.position.y; }

		//place me
		int index = 0;
		Vector3 vec1 = Vector3.zero;

		Setup.GC_DebugLog(Camera.main.camera.WorldToScreenPoint(Camera.main.camera.transform.position));
		Setup.GC_DebugLog(Camera.main.pixelRect);

		while (index < 999)
		{
			//vec1 = Camera.main.camera.transform.position;
			vec1 = transform.position;
			vec1.z = 30;
			if (isBackOfScreen) { vec1.x -= 1; }
			if (isFrontOfScreen) { vec1.x += 1; }
			if (isTopOfScreen) { vec1.y += 1; }
			if (isBottomOfScreen) { vec1.y -= 1; }
			transform.position = vec1;

			if (Camera.main.camera.pixelRect.Contains(Camera.main.camera.WorldToScreenPoint(vec1)))
			{

			}
			else
			{
				break;
			}

			index++;
		}*/
	}
}
