using UnityEngine;
using System.Collections;

public class ZoomToVector : MonoBehaviour
{
    public Vector3 vector = Vector3.zero;
    public float time = 0.7f;
    public Behaviour enableThis;
    bool moving = false;
    public bool isElectricalBolt = false;

    void Start()
    {
        //xa.glx = transform.position;
        //xa.glx.y -= 300 + Camera.main.camera.transform.position.y;
        //xa.glx.x -= Camera.main.camera.transform.position.x;
        //transform.position = xa.glx;

    }

    void Update()
    {

        if (fa.time > 0.3f && !moving)
        {
            moving = true;
            Vector3[] pathArray = new Vector3[2];
            pathArray[0].x = transform.position.x + Random.Range(-6, 6);
            pathArray[0].y = transform.position.y + Random.Range(-6, 6);
            pathArray[0].z = transform.position.z;
            pathArray[1].x = vector.x;
            pathArray[1].y = vector.y;
            pathArray[1].z = transform.position.z;
            iTween.MoveTo(this.gameObject, iTween.Hash("path", pathArray, "time", time, "easetype", iTween.EaseType.easeInSine, "oncomplete", "finishedMoving", "oncompletetarget", this.gameObject));

        }
    }

    void finishedMoving()
    {
        enableThis.enabled = true;
        this.enabled = false;
        if (isElectricalBolt) { za.numberOfElectricalBolts++; }

    }
}
