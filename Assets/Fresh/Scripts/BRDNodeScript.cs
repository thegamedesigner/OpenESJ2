using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BRDNodeScript : MonoBehaviour
{
	public GameObject WhiteCirclePrefab;
	public ParticleSystem DiamondRain;
	public GameObject pinwheelController;
	public GameObject pinwheelStar;

	[System.Serializable]
	public class Event
	{
		public float time = 0;
		public bool triggerRetroactively = false;
		public GameObject spawn;//spawns this object (and forgets about it)
		public ParticleSystem playPS;//startings this particle system playing
		public ParticleSystem stopPS;//Stops this particle system playing
		public GameObject scaleToOne;//Tweens this gameobject to scale to 1,1,1
		public GameObject scaleToZero;//Tweens this gameobject to scale to 0,0,0
		public GameObject triggerTween;//Triggers an itween on this gameobject, called "RemoteItween1"
	}

	Event[] events = new Event[0];

	int index = 0;

	void Start()
	{
		events = BRD_BRNS_info();
	}

	void Update()
	{
		if (xa.music_Time < 1f && index > 10) { index = 0; }

		if (index >= events.Length)
		{
			if (xa.music_Time < 1f) { index = 0; }
			return;
		}
		else
		{
			if (events[index] != null)
			{
				if (xa.music_Time >= events[index].time)
				{
					if (xa.music_Time > (events[index].time + 1) && !events[index].triggerRetroactively)
					{
						//then skip this one

					}
					else
					{
						if (events[index].spawn != null)
						{
							GameObject go = Instantiate(events[index].spawn, transform.position, transform.rotation);
						}
						if (events[index].playPS != null)
						{
							events[index].playPS.Play();
						}
						if (events[index].stopPS != null)
						{
							events[index].stopPS.Stop();
						}
						if (events[index].scaleToOne != null)
						{
							//Is an event about to also scale this to zero?
							bool skip = false;
							for (int a = 0; a < events.Length; a++)
							{
								if (events[a] != null &&
									events[a].scaleToZero != null &&
									events[a].scaleToZero == events[index].scaleToOne &&
									events[a].time < xa.music_Time &&
									events[a].triggerRetroactively)
								{
									skip = true;
								}
							}

							if (!skip)
							{
								iTween.ScaleTo(events[index].scaleToOne, iTween.Hash("x", 1, "y", 1, "time", 0.4f, "easetype", iTween.EaseType.easeInOutSine));
							}

						}
						if (events[index].scaleToZero != null)
						{
							iTween.ScaleTo(events[index].scaleToZero, iTween.Hash("x", 0, "y", 0, "time", 0.7f, "easetype", iTween.EaseType.easeInOutSine));
						}
						if (events[index].triggerTween != null)
						{
							iTweenEvent.GetEvent(events[index].triggerTween, "RemoteItween1").Play();
						}
					}
					index++;

				}
			}
		}
	}

	Event[] BRD_BRNS_info()
	{
		Event[] p = new Event[100];
		int i = 0;
		float t = 0;

		i = 0;
		p[i] = new Event();
		p[i].time = 05.076f;
		p[i].spawn = WhiteCirclePrefab;

		i++;
		p[i] = new Event();
		p[i].time = 10.081f;
		p[i].spawn = WhiteCirclePrefab;

		i++;
		p[i] = new Event();
		p[i].time = 15.157f;
		p[i].spawn = WhiteCirclePrefab;

		i++;
		p[i] = new Event();
		p[i].time = 20.232f;
		p[i].spawn = WhiteCirclePrefab;

		i++;
		p[i] = new Event();
		p[i].time = 25.261f;
		p[i].spawn = WhiteCirclePrefab;

		i++;
		p[i] = new Event();
		p[i].time = 30.337f;
		p[i].spawn = WhiteCirclePrefab;

		i++;
		p[i] = new Event();
		p[i].time = 35.389f;
		p[i].spawn = WhiteCirclePrefab;

		i++;
		p[i] = new Event();
		p[i].time = 39.945f;
		p[i].triggerRetroactively = true;
		p[i].scaleToOne = pinwheelController;

		t = 41.701f; i++; p[i] = new Event(); p[i].time = t; p[i].triggerTween = pinwheelStar;
		t = 42.970f; i++; p[i] = new Event(); p[i].time = t; p[i].triggerTween = pinwheelStar;
		t = 44.230f; i++; p[i] = new Event(); p[i].time = t; p[i].triggerTween = pinwheelStar;
		t = 45.502f; i++; p[i] = new Event(); p[i].time = t; p[i].triggerTween = pinwheelStar;
		t = 46.750f; i++; p[i] = new Event(); p[i].time = t; p[i].triggerTween = pinwheelStar;
		t = 48.014f; i++; p[i] = new Event(); p[i].time = t; p[i].triggerTween = pinwheelStar;
		t = 49.282f; i++; p[i] = new Event(); p[i].time = t; p[i].triggerTween = pinwheelStar;
		t = 50.534f; i++; p[i] = new Event(); p[i].time = t; p[i].triggerTween = pinwheelStar;
		t = 51.820f; i++; p[i] = new Event(); p[i].time = t; p[i].triggerTween = pinwheelStar;
		t = 53.042f; i++; p[i] = new Event(); p[i].time = t; p[i].triggerTween = pinwheelStar;
		t = 54.335f; i++; p[i] = new Event(); p[i].time = t; p[i].triggerTween = pinwheelStar;
		t = 55.604f; i++; p[i] = new Event(); p[i].time = t; p[i].triggerTween = pinwheelStar;
		t = 56.861f; i++; p[i] = new Event(); p[i].time = t; p[i].triggerTween = pinwheelStar;
		t = 58.100f; i++; p[i] = new Event(); p[i].time = t; p[i].triggerTween = pinwheelStar;
		t = 59.399f; i++; p[i] = new Event(); p[i].time = t; p[i].triggerTween = pinwheelStar;
		t = 60.668f; i++; p[i] = new Event(); p[i].time = t; p[i].triggerTween = pinwheelStar;
		t = 61.925f; i++; p[i] = new Event(); p[i].time = t; p[i].triggerTween = pinwheelStar;
		t = 63.200f; i++; p[i] = new Event(); p[i].time = t; p[i].triggerTween = pinwheelStar;
		t = 64.457f; i++; p[i] = new Event(); p[i].time = t; p[i].triggerTween = pinwheelStar;
		t = 65.714f; i++; p[i] = new Event(); p[i].time = t; p[i].triggerTween = pinwheelStar;
		t = 66.977f; i++; p[i] = new Event(); p[i].time = t; p[i].triggerTween = pinwheelStar;
		t = 68.240f; i++; p[i] = new Event(); p[i].time = t; p[i].triggerTween = pinwheelStar;
		t = 69.479f; i++; p[i] = new Event(); p[i].time = t; p[i].triggerTween = pinwheelStar;
		t = 70.772f; i++; p[i] = new Event(); p[i].time = t; p[i].triggerTween = pinwheelStar;
		t = 72.035f; i++; p[i] = new Event(); p[i].time = t; p[i].triggerTween = pinwheelStar;
		t = 73.298f; i++; p[i] = new Event(); p[i].time = t; p[i].triggerTween = pinwheelStar;
		t = 74.555f; i++; p[i] = new Event(); p[i].time = t; p[i].triggerTween = pinwheelStar;
		t = 75.818f; i++; p[i] = new Event(); p[i].time = t; p[i].triggerTween = pinwheelStar;
		t = 77.081f; i++; p[i] = new Event(); p[i].time = t; p[i].triggerTween = pinwheelStar;
		t = 78.344f; i++; p[i] = new Event(); p[i].time = t; p[i].triggerTween = pinwheelStar;
		t = 79.596f; i++; p[i] = new Event(); p[i].time = t; p[i].triggerTween = pinwheelStar;

		t = 78.391f; i++; p[i] = new Event(); p[i].time = t; p[i].triggerTween = pinwheelStar;
		t = 85.935f; i++; p[i] = new Event(); p[i].time = t; p[i].triggerTween = pinwheelStar;
		t = 88.443f; i++; p[i] = new Event(); p[i].time = t; p[i].triggerTween = pinwheelStar;
		t = 90.934f; i++; p[i] = new Event(); p[i].time = t; p[i].triggerTween = pinwheelStar;
		t = 93.501f; i++; p[i] = new Event(); p[i].time = t; p[i].triggerTween = pinwheelStar;
		t = 96.003f; i++; p[i] = new Event(); p[i].time = t; p[i].triggerTween = pinwheelStar;
		t = 98.612f; i++; p[i] = new Event(); p[i].time = t; p[i].triggerTween = pinwheelStar;
		t = 101.442f; i++; p[i] = new Event(); p[i].time = t; p[i].triggerTween = pinwheelStar;
		t = 103.635f; i++; p[i] = new Event(); p[i].time = t; p[i].triggerTween = pinwheelStar;
		t = 106.149f; i++; p[i] = new Event(); p[i].time = t; p[i].triggerTween = pinwheelStar;
		t = 108.663f; i++; p[i] = new Event(); p[i].time = t; p[i].triggerTween = pinwheelStar;
		t = 111.207f; i++; p[i] = new Event(); p[i].time = t; p[i].triggerTween = pinwheelStar;
		t = 113.733f; i++; p[i] = new Event(); p[i].time = t; p[i].triggerTween = pinwheelStar;
		t = 116.248f; i++; p[i] = new Event(); p[i].time = t; p[i].triggerTween = pinwheelStar;

		i++;
		p[i] = new Event();
		p[i].time = 118.000f;
		p[i].triggerRetroactively = true;
		p[i].scaleToZero = pinwheelController;

		//diamond rain
		i++;
		p[i] = new Event();
		p[i].time = 125.785f;
		p[i].triggerRetroactively = true;
		p[i].playPS = DiamondRain;

		i++;
		p[i] = new Event();
		p[i].time = 207.187f;
		p[i].triggerRetroactively = true;
		p[i].stopPS = DiamondRain;

		return p;

	}
}
