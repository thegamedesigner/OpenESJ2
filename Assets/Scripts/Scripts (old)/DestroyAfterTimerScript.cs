using UnityEngine;
using System.Collections;

public class DestroyAfterTimerScript : MonoBehaviour
{
	public float timer = 0;
	public float timeInSeconds = 0;
	public bool useTimeInSeconds = false;
	public GameObject go = null;
    public float counter = 0;
    public bool setHealthToZero = false;
    public GameObject deathExplo = null;
	public bool triggerScaleOutScriptOnDeath = false;
	public bool flickerBeforeDestroying = false;
	public GameObject flickeringObj;
    public float whenToTriggerFlicker = 0;
    public float whenToTriggerFlickerInSeconds = 0;
	public float flickerSpeed = 0;
    public bool dontActuallyDestroyMe = false;
	bool flickering = false;
	float flickerCounter = 0;
	float timeSave = 0;

	// Use this for initialization
	void Start()
	{
		timeSave = fa.time;
	}

	// Update is called once per frame
	void Update()
	{
		counter += 10 * fa.deltaTime;

		if (flickerBeforeDestroying)
		{
			if (flickering)
			{
				 updateFlicking();
			}
			else
			{
                if (whenToTriggerFlickerInSeconds == 0)
                {
                    if (fa.time >= timeSave + whenToTriggerFlickerInSeconds)
                    {
                        flickering = true;
                    }
                }
                else
                {
                    if (counter > whenToTriggerFlicker)
                    {
                        flickering = true;
                    }
                }
			}
		}
		if (!useTimeInSeconds)
		{
			if (counter > timer)
            {
                destroyMeAndStuff();
			}
		}
		else
		{

			if ((timeInSeconds + timeSave) <= fa.time)
            {
                destroyMeAndStuff();
			}
		}
	}

    void destroyMeAndStuff()
    {
        if (setHealthToZero)
        {
            HealthScript healthScript = null;
            healthScript = this.gameObject.GetComponent<HealthScript>();
            if (healthScript)
            {
                healthScript.health = 0;
            }
        }

        if (deathExplo)
        {
            xa.glx = transform.position;
            xa.glx.z = xa.GetLayer(xa.layers.Explo1);
            xa.tempobj = (GameObject)(Instantiate(deathExplo, xa.glx, transform.localRotation));
            xa.tempobj.transform.parent = xa.createdObjects.transform;
        }
        if (go) { Destroy(go); }

        if (!triggerScaleOutScriptOnDeath && !dontActuallyDestroyMe)
        {
            Destroy(this.gameObject);
        }

        if(triggerScaleOutScriptOnDeath)
        {
            ScaleOutAndDestroyScript scaleOutScript = null;
            scaleOutScript = this.gameObject.GetComponent<ScaleOutAndDestroyScript>();
            if (scaleOutScript)
            {
                scaleOutScript.counter = 999;
            }
        }
        this.enabled = false;//turn off this script.
    }

	void updateFlicking()
	{
		flickerCounter += flickerSpeed * fa.deltaTime;
		if (flickerCounter > 1.4f) { flickerCounter = 0; }//loops

		if (flickerCounter > 0.7f) { flickeringObj.GetComponent<Renderer>().enabled = false; }
		else { flickeringObj.GetComponent<Renderer>().enabled = true; }//flicker
	}
}
