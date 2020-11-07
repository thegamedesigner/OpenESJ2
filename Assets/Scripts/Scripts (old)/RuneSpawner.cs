using UnityEngine;
using System.Collections;

public class RuneSpawner : MonoBehaviour
{
	public GameObject runePrefab;
	float delay1 = 0;
	float delay2 = 0;

	// Use this for initialization
	void Start()
	{
		xa.runes = 0;
		xa.runesExisting = 0;
		xa.runeFinalText = 0;
		xa.runesCollected = 0;
	}

	// Update is called once per frame
	void Update()
	{
		if (xa.runesCollected < 11)
		{
			if (xa.runesExisting <= 0)
			{
				delay1 += 10 * fa.deltaTime;
				if (delay1 > 10)
				{
					delay1 = 0;
					xa.runesExisting = 1;
					spawnNewRune();
				}
			}
		}
		else
		{
			if (xa.runeFinalText == 0)
			{
				delay2 += 10 * fa.deltaTime;
				if (delay2 > 5)
				{
					xa.runeFinalText = 1;

					iTweenEvent.GetEvent(Camera.main.GetComponent<Camera>().gameObject, "shakeUp").Play();
					iTweenEvent.GetEvent(Camera.main.GetComponent<Camera>().gameObject.transform.parent.transform.gameObject, "shakeUp2").Play();
				}
			}
		}
	}

	int lastSpawnId = 0;
	void spawnNewRune()
	{
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("ammoSpawn");
		int result = (int)(Random.Range(0, gos.Length));
		if (lastSpawnId == result) { result += 1; if (result >= gos.Length) { result = 0; } }
		lastSpawnId = result;
		xa.glx = gos[result].transform.position;
		xa.tempobj = (GameObject)(Instantiate(runePrefab, xa.glx, xa.null_quat));
	}
}
