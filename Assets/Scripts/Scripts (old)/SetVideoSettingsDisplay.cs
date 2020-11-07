using UnityEngine;
using System.Collections;

public class SetVideoSettingsDisplay : MonoBehaviour
{
    /*
	public enum VidSettingsType { NONE, RESOLUTION, FULLSCREEN, VSYNC };
	public VidSettingsType vidSettingsType = VidSettingsType.NONE;
	public TextMesh display;
	// Use this for initialization
	void Start ()
	{
		//display = gameObject.GetComponent<TextMesh>();
		if(display == null)
		{
			//Debug.LogError("No TextMesh component detected! Destroying Self.");
			Destroy(this.gameObject);
		}
		xa.setResolutionIndex = xa.currentResolutionIndex;
		UpdateDisplay();
	
	}
	
	public void UpdateDisplay ()
	{
		switch(vidSettingsType)
		{
		case VidSettingsType.NONE :
			//Debug.LogError("VidSettingsType set to NONE! Destroying self.");
			Destroy(this.gameObject);
			break;
		case VidSettingsType.RESOLUTION :
			display.text = xa.supportedResolutions[xa.setResolutionIndex].width.ToString() + " x " + xa.supportedResolutions[xa.setResolutionIndex].height.ToString();
			//Debug.Log ("SetVideoSettingsSDisplay: " + xa.supportedResolutions.Length.ToString() + " supported resolutions found!");
			break;
		case VidSettingsType.FULLSCREEN :
			//if(xa.fullscreen)
			//	display.text = "On";
			//else
			//	display.text = "Off";
			break;
		case VidSettingsType.VSYNC :
			if(xa.vSync == 0)
				display.text = "Off";
			else
				display.text = (60 / xa.vSync).ToString() + "FPS";
			break;
		}
	}*/
}
