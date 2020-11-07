using UnityEngine;
using System.Collections;

public class TeachFiringNodeController : MonoBehaviour {
	
	public AniScript_Loop1Animation animationScript;
	
	void Awake ()
	{
		//animationScript = gameObject.GetComponent<AniScript_Loop1Animation>();
        if (animationScript == null)
        { }
        //Debug.LogError("No animation script assigned to TeachFiringNodeController!");
	}
	
	// Update is called once per frame
	void Update ()
	{
		////Debug.Log ("ControlType: " + SetRendererBasedOnControlsType.controlType.ToString() + " y1: " + animationScript.y1.ToString());
		if(SetRendererBasedOnControlsType.controlType == SetRendererBasedOnControlsType.controlTypes.Keyboard && animationScript.y1 != 7)
		{
			////Debug.Log ("Setting y1 to 7");
			//animationScript.y1 = 7;
			animationScript.changeAnimation(0, 7, 2);
		}
		if(SetRendererBasedOnControlsType.controlType == SetRendererBasedOnControlsType.controlTypes.Xbox && animationScript.y1 != 6)
		{
			////Debug.Log ("Setting y1 to 6");
			//animationScript.y1 = 6;
			animationScript.changeAnimation(0, 6, 2);
		}
	}
}
