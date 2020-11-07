using UnityEngine;
using System.Collections;

public class MerpsSetup : MonoBehaviour
{
	//this is a collection of static functions



	public static void setTexture(int v1, int v2, float multi, GameObject rendererGO)
	{
		float x1 = 0;
		float y1 = 0;
		float x2 = 0;
		float y2 = 0;

		x1 = 0.125f * multi;
		y1 = 0.125f * multi;
		x2 = (0.125f * multi) * v1;
		y2 = 1 - (((0.125f * multi) * v2) + (0.125f * multi));

		if (rendererGO)
		{
			rendererGO.GetComponent<Renderer>().material.mainTextureScale = new Vector2(x1, y1);
			rendererGO.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(x2, y2);
		}
	}

	public static float basedOnRealTime(float timeSet, float timeInSeconds)//returns 1-to-0 based on how far into timeInSeconds, starting from timeSet
	{
		float result = (timeSet + timeInSeconds) - Time.realtimeSinceStartup;
		if (result < 0) { result = 0; }
		result = result / timeInSeconds;
		
		return (result);
	}
}
