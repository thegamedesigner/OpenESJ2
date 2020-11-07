using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MommyYellScript : MonoBehaviour
{
	public TextMesh textMesh;
	public GameObject go;
	float timeset;
	float delay = 5;

	void Start()
	{
		delay = 2.5f;
		iTween.FadeTo(go, iTween.Hash("alpha", 0, "time", 0.2f));
		iTween.MoveBy(go, iTween.Hash("y", -0.5f, "time", 0.2f));
	}

	void Update()
	{
		if (fa.time > (timeset + delay))
		{
			timeset = fa.time;
			delay = Random.Range(5, 7);


			int r = Random.Range(0, 9);
			switch (r)
			{
				case 0: textMesh.text = "Oh la la!"; break;
				case 1: textMesh.text = "What a firm grip!"; break;
				case 2: textMesh.text = "Weee!"; break;
				case 3: textMesh.text = "Oh dearie me!"; break;
				case 4: textMesh.text = "Mmm Musky!"; break;
				case 5: textMesh.text = "Glory be!"; break;
				case 6: textMesh.text = "Gripping hard!"; break;
				case 7: textMesh.text = "Mmm Slimy!"; break;
				case 8: textMesh.text = "Yay!"; break;
			}
			iTween.FadeTo(go, iTween.Hash("alpha", 1, "time", 0.2f));
			iTween.FadeTo(go, iTween.Hash("alpha", 0, "time", 0.2f, "delay", 3));
			iTween.MoveBy(go, iTween.Hash("y", 1, "time", 0.2f));
			iTween.MoveBy(go, iTween.Hash("y", 1, "time", 0.2f, "delay", 3));
			iTween.MoveBy(go, iTween.Hash("y", -2, "time", 0.01f, "delay", 3.3f));
		}
	}
}
