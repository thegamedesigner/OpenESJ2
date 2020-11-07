using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionController : MonoBehaviour
{
	List<GameObject> sections;

	void Start()
	{
		UpdateSections();
	}

	void UpdateSections()
	{
		sections = new List<GameObject>();
		MeshRenderer[] gos = this.gameObject.GetComponentsInChildren<MeshRenderer>();
		for (int i = 0; i < gos.Length; i++)
		{
			if (gos[i].gameObject.tag == "levelSection")
			{
				sections.Add(gos[i].gameObject);
			}
		}
	}

	void Update()
	{
		float turnOnDist = 50;
		for (int i = 0; i < sections.Count; i++)
		{
			float dist = Vector2.Distance(fa.cameraPos, sections[i].transform.position);
			if (dist < turnOnDist)
			{
				sections[i].SetActive(true);
			}
			else
			{
				sections[i].SetActive(false);
			}
		}
	}
}
