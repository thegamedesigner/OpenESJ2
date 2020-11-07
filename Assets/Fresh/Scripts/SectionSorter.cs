using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


[ExecuteInEditMode]
public class SectionSorter : MonoBehaviour
{
	public bool sort = false;
	public bool unsort = false;
	public bool done = false;
	List<GameObject> sections;
	public GameObject world;

	void Start()
	{

	}

	void Update()
	{
		if(done) {return; }
		if (sort) { Sort(); }

		if (unsort) {  Unsort(); }

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

	void Sort()
	{
		UpdateSections();
		for (int b = 0; b < 2000; b++)
		{
			if (world.transform.childCount == 0) { break; }
			for (int i = 0; i < world.transform.childCount; i++)
			{
				GameObject go = world.transform.GetChild(i).gameObject;

				int closest = -1;
				float closestDist = 9999;
				for (int a = 0; a < sections.Count; a++)
				{
					float dist = Vector2.Distance(go.transform.position, sections[a].transform.position);
					if (dist < closestDist)
					{
						closest = a;
						closestDist = dist;
					}

					if (closest != -1)
					{
						go.transform.SetParent(sections[closest].transform);
					}
				}
				break;
			}
		}
		done = true;
	}

	void Unsort()
	{
		UpdateSections();
		List<GameObject> u = new List<GameObject>();
		for (int a = 0; a < sections.Count; a++)
		{
			for (int b = 0; b < 2000; b++)
			{
				for (int i = 0; i < sections[a].transform.childCount; i++)
				{
					GameObject go = sections[a].transform.GetChild(i).gameObject;
					u.Add(go);
					//go.transform.SetParent(world.transform);
				}
			}
		}

		List<GameObject> s = u.OrderBy(go => go.name).ToList();

		for (int i = 0; i < s.Count; i++)
		{
			s[i].transform.SetParent(world.transform);
		}
		done = true;
	}
}
