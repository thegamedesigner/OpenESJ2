using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicLineManager : MonoBehaviour
{
	public float multiply                    = 300.0f;
	Dictionary<int, List<GameObject> > lines = new Dictionary<int, List<GameObject> >();
	float[] goal                             = new float[xa.music_Spectrum.Length];
	float counter                            = 0.0f;
	float spd                                = 125.0f;

	public void AddLine(GameObject obj, int spectrum)
	{
		if (lines.ContainsKey(spectrum))
		{
			lines[spectrum].Add(obj);
			return;
		}
		List<GameObject> l = new List<GameObject>();
		l.Add(obj);
		lines.Add(spectrum, l);
	}

	// Update is called once per frame
	void Update()
	{
		counter += 10 * fa.deltaTime;
		if (counter > 1.0f)
		{
			counter = 0.0f;
			foreach (KeyValuePair<int, List<GameObject> > entry in lines)
			{
				goal[entry.Key] = (xa.music_Spectrum[entry.Key] * multiply) + 100.0f;

				foreach (GameObject obj in entry.Value)
				{
					MusicLineScript spec = obj.GetComponent<MusicLineScript>();
					Vector3 pos = obj.transform.localScale;
					if (pos.y < goal[spec.num])
					{
						pos.y += spd * fa.deltaTime;
					}
					else if (pos.y > goal[spec.num])
					{
						pos.y -= spd * fa.deltaTime;
					}
					obj.transform.localScale = pos;
				}
			}
		}
	}
}
