using UnityEngine;
using System.Collections;

public class ScaleOutAndDestroyScript : MonoBehaviour
{

	//This script waits for a delay, scales something down, then destroys a game object

	public float delay = 0;
	public GameObject destroyThis = null;
	public bool dieOnTimer = false;

	public bool ScaleOnX = false;
	public bool ScaleOnY = false;
	public bool ScaleOnZ = false;
	public float ScaleXSpeed = 0;
	public float ScaleYSpeed = 0;
	public float ScaleZSpeed = 0;
	public float ScaleXMin = 0;
	public float ScaleYMin = 0;
	public float ScaleZMin = 0;

	bool done = false;
	bool died = false;

	public bool useTriggerScript = false;
	public Behaviour behaviour = null;
	bool triggeredBehaviour = false;

	[HideInInspector]
	public float counter = 0;
	// Use this for initialization
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		if (counter <= delay)
		{
			if (delay != 0) { counter += 10 * fa.deltaTime; }//0 means don't have a timer
		}
		else
		{

			if (dieOnTimer && !died)
			{
				died = true;
				HealthScript script;
				script = destroyThis.GetComponent<HealthScript>();
				script.health = 0;
			}

			if (!done)
			{
				if (useTriggerScript)
				{
					if (!triggeredBehaviour)
					{
						behaviour.enabled = true;
						triggeredBehaviour = true;
					}
				}
				xa.glx = transform.localScale;
				xa.glx.x -= ScaleXSpeed * fa.deltaTime;
				xa.glx.y -= ScaleYSpeed * fa.deltaTime;
				xa.glx.z -= ScaleZSpeed * fa.deltaTime;
				if (ScaleOnX) { xa.glx.x = Mathf.Clamp(xa.glx.x, ScaleXMin, 9999); }
				if (ScaleOnY) { xa.glx.y = Mathf.Clamp(xa.glx.y, ScaleYMin, 9999); }
				if (ScaleOnZ) { xa.glx.z = Mathf.Clamp(xa.glx.z, ScaleZMin, 9999); }

				if (((xa.glx.x <= ScaleXMin && ScaleOnX) || (!ScaleOnX)) && ((xa.glx.y <= ScaleYMin && ScaleOnY) || (!ScaleOnY)) && ((xa.glx.z <= ScaleZMin && ScaleOnZ) || (!ScaleOnZ)))
				{

					done = true;
					GameObject.Destroy(destroyThis);


				}
				transform.localScale = xa.glx;
			}
		}

	}
}
