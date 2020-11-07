using UnityEngine;

public class FrEdFrogScript : MonoBehaviour
{
	public int ammoId = -1;

	public bool onlyFireIfOnScreen = false;
	public bool onlyFireIfTriggered = false;
	public Info infoScriptForTriggering;
	public GameObject firingPoint;
	public FreshAni freshAniScript;
	public float delay = 1;
	float timeSet = 0;

	void Start()
	{
		if(transform.localScale.x < 0) {firingPoint.transform.SetAngZ(0); }

	}

	// Update is called once per frame
	void Update()
	{
		if (EditorController.IsEditorActive()) {
			return;
		}

		if (onlyFireIfTriggered)
		{
			if(infoScriptForTriggering == null) {return; }
			if(!infoScriptForTriggering.triggered) { return;}
		}
		//	Debug.Log("Cam.x: " + fa.cameraPos.x + ", frog.x: " + transform.position.x + ", dist: " + (transform.position.x - fa.cameraPos .x));
		bool passed = false;

		if (!onlyFireIfOnScreen) { passed = true; }

		if (
			((transform.position.x - fa.cameraPos.x) < 14) &&
			((transform.position.y - fa.cameraPos.y) < 14)) { passed = true; }


		if (passed)
		{
			if (ammoId != -1)
			{
				if (fa.time > (timeSet + delay))
				{
					timeSet = fa.time;
					freshAniScript.PlayAnimation(1);

					GameObject go = FrEdNodeScript.instance.CreatePrefab(ammoId);
					if (go != null) {
						go.transform.position = firingPoint.transform.position;
						go.transform.rotation = firingPoint.transform.rotation;
					} else {
						Debug.LogError("Unable to find custom prefab id:" + ammoId);
					}
				}
			}
		}
	}


}
