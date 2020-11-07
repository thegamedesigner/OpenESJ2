using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CensorTextScript : MonoBehaviour
{
	public static string CensorText(string s)
	{
		if (s.Contains("Whadda think of my sexy butt?"))
		{
			return "Stomp on my butt!";
		}
		if (s.Contains("Motherfucker"))
		{
			return "#@!";
		}
		if (s.Contains("Shit"))
		{
			return "@#*!";
		}
		if (s.Contains("shit"))
		{
			return "@#$%^&!";
		}
		if (s.Contains("Sexy"))
		{
			return "@#*!";
		}
		if (s.Contains("sexy"))
		{
			return "#@!";
		}
		return s;
	}
	bool pged = false;

	void Update()
	{
		if (!pged)
		{
			pged = true;
			if (xa.pgMode)
			{
				TextMesh t = this.gameObject.GetComponent<TextMesh>();
				t.text = CensorText(t.text);
			}
		}
	}

}
