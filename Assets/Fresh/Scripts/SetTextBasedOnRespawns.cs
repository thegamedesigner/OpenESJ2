using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTextBasedOnRespawns : MonoBehaviour
{
	public TextMesh textMesh;
	public int offset = 0;
	public string[] strings = new string[0];

	void Start()
	{
		int index = 0;
		index -= offset;
		if(index < 0) {return; }
		index = xa.mst_timesALevelHasBeenLoaded;
		index -= 1;
		if(index < 0) {index = 0; }
		index = (int)Mathf.Repeat(index,strings.Length);
		textMesh.text = strings[index];

	}

	void Update()
	{

	}
}
