using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareCannonScript : MonoBehaviour
{
	public ParticleSystem particles;
	float[] pts;

	int index = 0;

	void Start()
	{
		pts = GetSix_NoInterruptions_Info();
	}

	void Update()
	{
		if (index >= pts.Length)
		{
			if(xa.music_Time < 1f) {index = 0; }
			return;
		}
		else
		{
			if (xa.music_Time >= pts[index])
			{
				//transform.AddAngZ(45);
				//particles.Play();
				iTween.RotateBy(this.gameObject,iTween.Hash("z",0.45f,"time",0.15f,"easetype",iTween.EaseType.easeInOutSine,"looptype", iTween.LoopType.none));
				iTween.PunchScale(this.gameObject,iTween.Hash("x", 2, "y", 2, "z", 2,"time",0.15f,"easetype",iTween.EaseType.easeInOutSine,"looptype", iTween.LoopType.none));
				index++;
			}
		}
	}

	float[] GetSix_NoInterruptions_Info()
	{
		List<float> p = new List<float>();
		
		p.Add(3 + (0.171f * 0f));
		p.Add(3 + (0.171f * 1f));
		p.Add(3 + (0.171f * 2f));
		p.Add(3 + (0.171f * 3f));

		p.Add(49.043f + (0.171f * 0f));
		p.Add(49.043f + (0.171f * 1f));
		p.Add(49.043f + (0.171f * 2f));
		p.Add(49.043f + (0.171f * 3f));

		float[] r = new float[p.Count];
		for (int i = 0; i < p.Count; i++)
		{
			r[i] = p[i];
		}
		return r;

	}
}
