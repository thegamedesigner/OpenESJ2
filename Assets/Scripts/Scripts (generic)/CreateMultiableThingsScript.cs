using UnityEngine;
using System.Collections;

public class CreateMultiableThingsScript : MonoBehaviour
{
    public bool useMyRotation = false;
    public bool useFiringPointRotation = false;
    public bool triggerAsSoonAsEnabled = false;
    public bool disableOnEnd = false;
    public float offsetZAngleByX = 0;
    public GameObject[] createGOs = new GameObject[0];
    public GameObject[] creationPoints = new GameObject[0];
    public bool useDelays = false;
    public bool printDebugPerObject = false;
    public float[] delayInSeconds = new float[0];
    public float startingDelay = 0;
    public bool createAsChild = false;//or this, if not using firing point
    public string Note = "The delay is *after* firing the bullet.";

    Quaternion rotation;
    float timeSet = 0;
    bool triggeredExternally = false;
    int index = 0;

    void Start()
    {
        timeSet = fa.time;

    }

    void Update()
    {
        if (this.enabled)
        {
            if (fa.time > timeSet + startingDelay)
            {
                if (triggerAsSoonAsEnabled || triggeredExternally)
                {
                    //actually create
                    if (useDelays)
                    {
                        if (fa.time >= (timeSet + delayInSeconds[index]))
                        {
                        Top:
                            create();
                            index++;
                            if (index >= createGOs.Length) { triggeredExternally = false; }
                            if (disableOnEnd && index >= createGOs.Length) { this.enabled = false; }
                            if (index >= delayInSeconds.Length) { index = 0; }
                            else
                            {
                                if (delayInSeconds[index] == 0)
                                {
                                    goto Top;//if the next delay in seconds is ZERO, then don't wait a frame (update loop) to create it
                                }
                            }
                            timeSet = fa.time;
                        }

                    }
                    else
                    {
                        index = 0;
                        while (index < createGOs.Length)
                        {
                            create();
                            index++;
                        }
                        if (disableOnEnd) { this.enabled = false; }
                    }
                }
            }


        }
    }

    public void triggerCreate()
    {
        timeSet = fa.time;
        triggeredExternally = true;
        index = 0;
    }

    void create()
    {
        if (useMyRotation) { rotation = transform.rotation; }
        else if (useFiringPointRotation) { rotation = creationPoints[index].transform.rotation; }
        else { if (createGOs[index]) { rotation = createGOs[index].transform.rotation; } }
        xa.glx = rotation.eulerAngles;
        xa.glx.z += offsetZAngleByX;
        rotation.eulerAngles = xa.glx;
        if (createGOs[index])
        {
            xa.tempobj = (GameObject)(Instantiate(createGOs[index], creationPoints[index].transform.position, rotation));
            if (createAsChild) { xa.tempobj.transform.parent = transform; }

        }
    }
}
