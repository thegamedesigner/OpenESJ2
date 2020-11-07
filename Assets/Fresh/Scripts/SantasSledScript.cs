using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantasSledScript : MonoBehaviour
{
	public GameObject[] missile;
	public GameObject[] muzzlepoint;
	public float firingSpeed = 0;
	public GameObject sled;
	public GameObject reindeer1;
	public GameObject reindeer2;
	public GameObject rudolph;
	public GameObject santaText;
	public GameObject textSpawnPoint;

	float timeSet;
	float delay = 4;
	float firingTimeSet;

	void Start()
	{
		iTween.MoveBy(this.gameObject, iTween.Hash("y", 2, "time", 2, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));

		iTween.MoveBy(this.gameObject, iTween.Hash("x", 3, "time", 8, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));

		iTween.MoveBy(sled, iTween.Hash("y", 0.2f, "x", 0.2f, "time", 2, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));
		iTween.MoveBy(reindeer1, iTween.Hash("y", 0.2f, "x", 0.2f, "time", 3, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));
		iTween.MoveBy(reindeer2, iTween.Hash("y", 0.1f, "x", -0.1f, "time", 2, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));
		iTween.MoveBy(rudolph, iTween.Hash("y", 0.2f, "x", 0.2f, "time", 4, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));
	}

	void Update()
	{
		if (fa.time >= (timeSet + delay))
		{
			timeSet = fa.time;
			GameObject go = Instantiate(santaText, textSpawnPoint.transform.position, santaText.transform.rotation);
			go.GetComponentInChildren<TextMesh>().text = GetRandomSantaText();
			go.transform.SetParent(this.gameObject.transform);
			iTween.FadeTo(go, iTween.Hash("alpha", 1, "time", 0.4f, "easetype", iTween.EaseType.easeOutSine));
			iTween.MoveBy(go, iTween.Hash("y", 0.5f, "time", 0.4f, "easetype", iTween.EaseType.easeOutSine));

		}

		if (fa.time >= (firingTimeSet + firingSpeed))
		{
			firingTimeSet = fa.time;
			for (int i = 0; i < muzzlepoint.Length; i++)
			{
				Instantiate(missile[i], muzzlepoint[i].transform.position, muzzlepoint[i].transform.rotation);
			}
		}
	}

	string GetRandomSantaText()
	{
		int r = Random.Range(0, 11);
		switch (r)
		{
			case 0: return "Suck on this!";
			case 1: return "Ho ho ho!";
			case 2: return "Merry Xmas!";
			case 3: return "Rotate on this!";
			case 4: return "Suck my Jingleballs!";
			case 5: return "You're a ho ho ho!";
			case 6: return "Naughty!";
			case 7: return "Naughty, naughty, naughty!";
			case 8: return "Joy-seeking missiles away!";
			case 9: return "Time for the xmas beatings!";
			case 10: return "Your momma's waiting for my milk and cookies!";
		}
		return "Merry Christmas!";
	}

}
