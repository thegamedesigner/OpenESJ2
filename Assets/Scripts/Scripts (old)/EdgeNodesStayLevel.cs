using UnityEngine;
using System.Collections;

public class EdgeNodesStayLevel : MonoBehaviour
{
	//Michael Todd approves this horrible hack!


	public GameObject back = null;
	public GameObject front = null;
	public GameObject top = null;
	public GameObject bottom = null;

	float backBuffer = 2;
	float frontBuffer = 2;
	float topBuffer = 4;
	float bottomBuffer = 2;



	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	int frameCheck = 0;
	void Update()
	{
		xa.glx = transform.eulerAngles;
		xa.glx.z = 0;
		transform.eulerAngles = xa.glx;

		if (frameCheck < 8)
		{
			frameCheck += 1;
		}
		else
		{
			frameCheck = 0;
			//place me
			int index2 = 0;
			while (index2 < 4)
			{
				int index = 0;
				Vector3 vec1 = Vector3.zero;
				Vector3 vec2 = Vector3.zero;
				vec1 = Camera.main.GetComponent<Camera>().transform.position;
				vec1.z = 30;
				vec2 = Camera.main.GetComponent<Camera>().transform.position;
				vec2.z = 30;
				if (index2 == 0) { vec1.x -= 5f; }
				if (index2 == 1) { vec1.x += 5f; }
				if (index2 == 2) { vec1.y += 5f; }
				if (index2 == 3) { vec1.y -= 5f; }
				while (index < 999)
				{
					//vec1 = Camera.main.camera.transform.position;

					if (index2 == 0) { vec1.x -= 3f; }
					if (index2 == 1) { vec1.x += 3f; }
					if (index2 == 2) { vec1.y += 3f; }
					if (index2 == 3) { vec1.y -= 3f; }
					if (Camera.main.GetComponent<Camera>().pixelRect.Contains(Camera.main.GetComponent<Camera>().WorldToScreenPoint(vec1)))
					{

					}
					else
					{
						break;
					}

					index++;
				}
				if (index2 == 0) { vec1.x -= backBuffer; back.transform.position = vec1; }
				if (index2 == 1) { vec1.x += frontBuffer; front.transform.position = vec1; }
				if (index2 == 2) { vec1.y += topBuffer; top.transform.position = vec1; }
				if (index2 == 3) { vec1.y -= bottomBuffer; bottom.transform.position = vec1; }
				index2++;
			}
			// transform.position = vec1;
		}
	}
}
