using UnityEngine;
using System.Collections;

public class SetVolumeDisplay : MonoBehaviour
{
	public enum VolumeType { NONE, MUSIC, SFX };
	public VolumeType volumeType = VolumeType.NONE;
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
		UpdateDisplay();
	
	}
	
	public void UpdateDisplay ()
	{
		switch(volumeType)
		{
		case VolumeType.NONE :
			//Debug.LogError("VolumeType set to NONE! Destroying self.");
			Destroy(this.gameObject);
			break;
		case VolumeType.MUSIC :
			display.text = Mathf.RoundToInt(xa.musicVolume * 10).ToString();
			break;
		case VolumeType.SFX :
			display.text = Mathf.RoundToInt(xa.soundVolume * 10).ToString();
			break;
		}
	}
}
