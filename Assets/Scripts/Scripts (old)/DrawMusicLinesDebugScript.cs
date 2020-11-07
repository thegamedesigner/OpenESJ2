using UnityEngine;
using System.Collections;

public class DrawMusicLinesDebugScript : MonoBehaviour
{
	public Color color = Color.red;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

		Debug.DrawLine(new Vector3(1 + transform.position.x, transform.position.y, transform.position.z), new Vector3(1 + transform.position.x, transform.position.y + (0.1f * 15), transform.position.z), Color.red);
		int index = 0;
		while (index < xa.music_Spectrum.Length)
		{
			Debug.DrawLine(new Vector3(2 + transform.position.x + (index * 0.1f), transform.position.y, transform.position.z), new Vector3(2 + transform.position.x + (index * 0.1f), transform.position.y + (xa.music_Spectrum[index] * 15), transform.position.z), color);
			index++;
		}

	}
}
