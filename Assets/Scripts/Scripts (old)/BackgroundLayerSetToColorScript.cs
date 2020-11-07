using UnityEngine;
using System.Collections;

public class BackgroundLayerSetToColorScript : MonoBehaviour
{
	public bool isLayer1 = false;
	public bool isLayer2 = false;
	public bool isLayer3 = false;
	public bool isLayer4 = false;
	public bool isSky = false;
	void Start()
	{
		setToColor();
	}


	public void setToColor()
	{
		if (isLayer1) { GetComponent<Renderer>().material.color = za.merpsBackgroundLayer1Color; }
		if (isLayer2) { GetComponent<Renderer>().material.color = za.merpsBackgroundLayer2Color; }
		if (isLayer3) { GetComponent<Renderer>().material.color = za.merpsBackgroundLayer3Color; }
		if (isLayer4) { GetComponent<Renderer>().material.color = za.merpsBackgroundLayer4Color; }
		if (isSky) { GetComponent<Renderer>().material.color = za.merpsBackgroundSkyColor; }
	}
}
