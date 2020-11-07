using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConBravoScript : MonoBehaviour
{
	public TextMesh textMesh;
	// Use this for initialization
	void Start()
	{
		float t = 0;
		FreshLevels.Type type = FreshLevels.Type.MusicLvl2;
		if (PlayerPrefs.HasKey("LevelTime_" + type))
		{
			t = PlayerPrefs.GetFloat("LevelTime_" + type, -1);
		}

		textMesh.text = "Your score is:\n" + t;
	}

	// Update is called once per frame
	void Update()
	{

	}
}
