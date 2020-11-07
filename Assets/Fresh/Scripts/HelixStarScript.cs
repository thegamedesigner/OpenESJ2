using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixStarScript : MonoBehaviour
{
	public GameObject spawnEffect;
	public GameObject go;
	List<float> times;
	List<float> spawns;
	float[] tempTimes;
	float[] tempSpawns;

	float start = -1;
	float end = -1;
	bool recharge = false;
	Vector2 myScale = new Vector2(1, 1);



	void Start()
	{
		myScale.x = Random.Range(0.3f, 1.2f);
		myScale.y = myScale.x;
		transform.SetAngZ(Random.Range(0, 360));
		start = 0.05f;
		end = 999f;
		times = new List<float>();
		times.Add(0.078f);
		times.Add(0.744f);
		times.Add(1.299f);
		times.Add(2.331f);
		times.Add(2.898f);
		times.Add(3.586f);
		times.Add(4.049f);
		times.Add(4.740f);
		times.Add(4.910f);
		times.Add(5.076f);
		times.Add(5.246f);
		times.Add(5.420f);
		times.Add(5.593f);
		times.Add(6.361f);
		times.Add(6.811f);
		times.Add(7.666f);
		times.Add(8.002f);
		times.Add(8.357f);
		times.Add(9.031f);
		times.Add(9.657f);
		times.Add(10.194f);
		times.Add(10.425f);
		times.Add(10.758f);
		times.Add(11.118f);
		times.Add(11.807f);
		times.Add(12.317f);
		times.Add(13.183f);
		times.Add(13.550f);
		times.Add(13.874f);
		times.Add(14.552f);
		times.Add(14.907f);
		times.Add(15.079f);
		times.Add(15.653f);
		times.Add(15.939f);
		times.Add(16.358f);
		times.Add(16.650f);
		times.Add(16.994f);
		times.Add(17.183f);
		times.Add(17.322f);
		times.Add(17.483f);
		times.Add(17.569f);
		times.Add(17.841f);
		times.Add(18.701f);
		times.Add(19.398f);
		times.Add(19.609f);
		times.Add(19.950f);

		times.Add(77.335f);
		times.Add(77.821f);
		times.Add(78.706f);
		times.Add(79.461f);
		times.Add(80.099f);
		times.Add(80.849f);
		times.Add(81.287f);
		times.Add(81.975f);
		times.Add(82.167f);
		times.Add(82.497f);
		times.Add(82.864f);
		times.Add(83.530f);
		times.Add(84.054f);
		times.Add(84.904f);
		times.Add(85.606f);

		times.Add(88.370f);
		times.Add(89.070f);
		times.Add(89.758f);
		times.Add(90.454f);
		times.Add(91.137f);
		times.Add(91.826f);
		times.Add(92.517f);
		times.Add(93.205f);
		times.Add(93.893f);
		times.Add(94.565f);
		times.Add(95.270f);
		times.Add(95.944f);
		times.Add(96.663f);
		times.Add(97.329f);
		times.Add(98.029f);
		times.Add(99.416f);
		times.Add(100.085f);
		times.Add(100.774f);
		times.Add(102.158f);
		times.Add(103.530f);
		times.Add(104.928f);
		times.Add(106.283f);
		times.Add(107.720f);
		times.Add(109.064f);

		times.Add(182.852f);
		times.Add(183.360f);
		times.Add(184.214f);
		times.Add(184.914f);
		times.Add(185.586f);
		times.Add(186.113f);
		times.Add(186.804f);
		times.Add(186.979f);
		times.Add(187.151f);
		times.Add(187.367f);
		times.Add(187.492f);
		times.Add(187.703f);
		times.Add(188.444f);
		times.Add(188.938f);
		times.Add(189.735f);
		times.Add(189.937f);
		times.Add(190.431f);
		times.Add(191.164f);
		times.Add(191.636f);
		times.Add(192.316f);
		times.Add(192.491f);
		times.Add(192.838f);
		times.Add(193.204f);
		times.Add(193.881f);
		times.Add(194.389f);
		times.Add(195.252f);
		times.Add(195.599f);
		times.Add(195.746f);
		times.Add(195.938f);
		times.Add(196.415f);
		times.Add(196.701f);
		times.Add(196.887f);
		times.Add(197.040f);
		times.Add(197.154f);
		times.Add(197.581f);
		times.Add(198.008f);
		times.Add(198.347f);
		times.Add(198.766f);
		times.Add(199.163f);
		times.Add(199.257f);
		times.Add(199.382f);
		times.Add(199.479f);
		times.Add(199.690f);
		times.Add(199.807f);
		times.Add(199.904f);
		times.Add(200.773f);
		times.Add(201.445f);
		times.Add(201.622f);
		times.Add(201.789f);
		times.Add(201.958f);


		spawns = new List<float>();
		spawns.Add(110.418f);
		spawns.Add(111.767f);
		spawns.Add(112.450f);
		spawns.Add(113.183f);
		spawns.Add(114.554f);
		spawns.Add(115.897f);
		spawns.Add(117.318f);
		spawns.Add(118.006f);
		spawns.Add(118.706f);
		spawns.Add(120.082f);
		spawns.Add(121.459f);
		spawns.Add(122.836f);
		spawns.Add(123.535f);
		spawns.Add(124.223f);
		spawns.Add(125.589f);
		spawns.Add(126.982f);
		spawns.Add(128.370f);
		spawns.Add(129.735f);
		spawns.Add(131.117f);
		spawns.Add(132.483f);
		spawns.Add(132.833f);
		spawns.Add(133.182f);
		spawns.Add(133.515f);
		spawns.Add(133.860f);
		spawns.Add(134.193f);
		spawns.Add(134.531f);
		spawns.Add(134.881f);
		spawns.Add(135.242f);
		spawns.Add(135.433f);
		spawns.Add(135.589f);
		spawns.Add(135.775f);
		spawns.Add(135.941f);
		spawns.Add(136.124f);
		spawns.Add(136.274f);
		spawns.Add(136.457f);
		spawns.Add(136.616f);
		spawns.Add(136.721f);
		spawns.Add(136.807f);
		spawns.Add(136.893f);
		spawns.Add(136.965f);
		spawns.Add(137.035f);
		spawns.Add(137.096f);
		spawns.Add(137.151f);
		spawns.Add(137.240f);
		spawns.Add(137.321f);
		spawns.Add(137.404f);
		spawns.Add(137.490f);
		spawns.Add(137.576f);

		FillTempTime();
	}

	void Update()
	{
		if (xa.music_Time >= start && xa.music_Time <= end)
		{
			recharge = true;
			for (int i = 0; i < tempTimes.Length; i++)
			{
				if (tempTimes[i] != -1)
				{
					if (xa.music_Time >= tempTimes[i])
					{
						TriggerTween(tempTimes[i]);
						tempTimes[i] = -1;
					}
				}
			}

			for (int i = 0; i < tempSpawns.Length; i++)
			{
				if (tempSpawns[i] != -1)
				{
					if (xa.music_Time >= tempSpawns[i])
					{
						SpawnStuff(tempSpawns[i]);
						tempSpawns[i] = -1;
					}
				}
			}
		}
		else if (recharge)
		{
			recharge = false;
			FillTempTime();
		}
	}

	void FillTempTime()
	{
		tempTimes = new float[times.Count];
		for (int i = 0; i < times.Count; i++)
		{
			tempTimes[i] = times[i];
		}

		tempSpawns = new float[spawns.Count];
		for (int i = 0; i < spawns.Count; i++)
		{
			tempSpawns[i] = spawns[i];
		}
	}

	void TriggerTween(float t)
	{
		if (xa.music_Time > t + 2)
		{
			go.transform.SetScaleX(0);
			go.transform.SetScaleY(0);
		}
		else
		{
			go.transform.SetScaleX(myScale.x);
			go.transform.SetScaleY(myScale.y);
			iTween.ScaleTo(go, iTween.Hash("time", Random.Range(0.05f, 0.45f), "easetype", iTween.EaseType.easeOutSine, "x", 0, "y", 0));

		}
	}

	void SpawnStuff(float t)
	{
		if (xa.music_Time > t + 2)
		{
		}
		else
		{
			if (Random.Range(0, 100) < 30)
			{
				GameObject go = Instantiate(spawnEffect, transform.position, transform.rotation);
			}
		}
	}
}
