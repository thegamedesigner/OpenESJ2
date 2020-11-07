using UnityEngine;
using System.Collections;

public class SpawnLibraryScript : MonoBehaviour
{
	public GameObject[] libraryPrefabs = new GameObject[200];
	public string[] libraryNames = new string[200];

	void Start()
	{
		xa.spawnLibraryScript = this;
	}

	public GameObject returnPrefab(string name)
	{
		int index = 0;
		while (index < libraryNames.Length)
		{
			if (libraryNames[index] != null)
			{
				if (libraryNames[index] == name)
				{
					return (libraryPrefabs[index]);
				}
			}
			index++;
		}
		return (null);

	}
}
