using UnityEngine;
using System.Collections;

public class OpenURLScript : MonoBehaviour
{

	public string goToThisURL = "";
	public float delay = 0;
	bool triggered = false;

	void Start()
	{

	}

	void Update()
	{
		if (!triggered)
		{
			if (delay > 0)
			{
				delay -= 10 * fa.deltaTime;
				if (delay <= 0)
				{
					triggered = true;
					Application.OpenURL(goToThisURL);
				}
			}
			else
			{
#if ANDROID
				foreach (Touch touch in Input.touches)
				{
					if (touch.phase == TouchPhase.Began)
					{
						checkButton(touch.position);
					}
				}
#elif IOS
				//put ios touch code here
#else
				if (Input.GetMouseButtonDown(0))
				{
					checkButton(Input.mousePosition);
					//checkButton(AspectUtility.mousePosition);
				}
#endif
			}
		}
	}

	void checkButton(Vector3 inputVec)
	{
		Ray ray = new Ray();
		RaycastHit hit;
		ray = Camera.main.GetComponent<Camera>().ScreenPointToRay(inputVec);
		if (this.gameObject.GetComponent<Collider>().Raycast(ray, out hit, 100) == true)
		{
			if (hit.collider.gameObject.tag == "metaNode")
			{
					triggered = true;
					Application.OpenURL(goToThisURL);
			}
		}
	}
}
