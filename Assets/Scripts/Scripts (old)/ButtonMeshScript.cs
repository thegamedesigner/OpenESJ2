using UnityEngine;
using System.Collections;

public class ButtonMeshScript : MonoBehaviour
{
	public GameObject myCamera = null;

	void Start()
	{

	}

	void Update()
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
		// put ios touch code here
#else
		if (Input.GetMouseButtonDown(0))
		{
			checkButton(Input.mousePosition);
			//checkButton(AspectUtility.mousePosition);
		}
#endif
	}

	void checkButton(Vector3 inputVec)
	{
		Ray ray = new Ray();
		RaycastHit hit;
		ray = myCamera.GetComponent<Camera>().ScreenPointToRay(inputVec);
		if (this.gameObject.GetComponent<Collider>().Raycast(ray, out hit, 100) == true)
		{
			if (hit.collider.gameObject.tag == "buttonMeshScriptTag")
			{
				callActivateFuncInActivateScript();
			}
		}
	}

	void callActivateFuncInActivateScript()
	{
		ActivateScript activateScript = null;
		activateScript = this.gameObject.GetComponent<ActivateScript>();
		if (activateScript)
		{
			activateScript.activateFunc();
		}

	}
}
