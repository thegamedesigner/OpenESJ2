using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class TextStitcher : MonoBehaviour
{
	public Text[] texts = new Text[0];

	void Start()
	{

	}

	void Update()
	{
		float soFar = 20;
		for (int i = 0; i < texts.Length; i++)
		{
			texts[i].rectTransform.anchoredPosition = new Vector3(0, -soFar, 0);
			soFar += texts[i].rectTransform.sizeDelta.y;

		}
	}
}
