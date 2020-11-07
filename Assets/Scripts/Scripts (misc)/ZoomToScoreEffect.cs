using UnityEngine;
using System.Collections;

public class ZoomToScoreEffect : MonoBehaviour
{
    public Behaviour enableThis;
    bool moving = false;
   // public GameObject SoundOnScored;
    void Start()
    {
        xa.glx = transform.position;
        xa.glx.y -= 300 + Camera.main.GetComponent<Camera>().transform.position.y;
        xa.glx.x -= Camera.main.GetComponent<Camera>().transform.position.x;
        xa.glx.z = xa.GetLayer(xa.layers.exploOvertop1);
        transform.position = xa.glx;


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
            pathArray[1].x = za.inworldScorePos.x;
            pathArray[1].y = za.inworldScorePos.y;
            pathArray[1].z = transform.position.z;
            iTween.MoveTo(this.gameObject, iTween.Hash("path", pathArray, "time", 0.7f, "easetype", iTween.EaseType.easeInSine, "oncomplete", "finishedMoving", "oncompletetarget", this.gameObject));

        }
    }

    void finishedMoving()
    {

      //  xa.tempobj = (GameObject)(Instantiate(SoundOnScored, xa.glx, xa.null_quat));
     //   xa.tempobj.transform.parent = xa.createdObjects.transform;
        if (enableThis) { enableThis.enabled = true; }
        this.enabled = false;
    }
}
