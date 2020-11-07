using UnityEngine;
using System.Collections;

public class SetScaleAndTriggerITweens : MonoBehaviour
{
    void Update()
    {
        //I gutted this from being generic. I only used it in one spot (ruined city explo music effects) & I needed it to do some exact stuff


            //set scale
        transform.localScale = new Vector3(1, 1, 1);
        GetComponent<Renderer>().material.color = new Color(GetComponent<Renderer>().material.color.r, GetComponent<Renderer>().material.color.g, GetComponent<Renderer>().material.color.b, 1);
        
        iTween.ScaleTo(this.gameObject,iTween.Hash("x", 222, "y", 222, "time", 1.8, "easetype", iTween.EaseType.easeOutSine));
        iTween.FadeTo(this.gameObject,iTween.Hash("alpha", 0, "time", 1.8, "easetype", iTween.EaseType.easeInCirc));
            
            this.enabled = false;

    }
}
