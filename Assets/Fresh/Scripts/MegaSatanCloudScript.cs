using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaSatanCloudScript : MonoBehaviour
{
	public GameObject NormalHead;
	public GameObject ScreamingHead;
	public GameObject Arms;
	public GameObject Body;
	public GameObject Controller;
	public GameObject Muzzlepoint;
	public ParticleSystem beam;
	public GameObject beamHurtZones;

	int stage = 0;
	float timeset = 0;
	float delay = 5;

	void Start()
	{
		//iTween.MoveBy(NormalHead, iTween.Hash("y", -0.3f, "time", 0.2f, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));
		//iTween.MoveBy(ScreamingHead, iTween.Hash("y", -0.3f, "time", 0.2f, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));
	//	iTween.MoveBy(Arms, iTween.Hash("y", 0.3f, "time", 0.2f, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));
		//iTween.MoveBy(Controller, iTween.Hash("y", 1f, "time", 2f, "easetype", iTween.EaseType.easeInOutSine, "looptype", iTween.LoopType.pingPong));
	}

	void Update()
	{
		float fireOut = 0.7f;
		float fireIn = 0.3f;
		switch (stage)
		{
			case 0:
				//move to the top
				NormalHead.SetActive(true);
				ScreamingHead.SetActive(false);
				beam.Stop();
				iTween.MoveTo(beamHurtZones, iTween.Hash("islocal", true, "x", 42, "time",fireIn, "easetype", iTween.EaseType.linear));
				
				iTween.MoveTo(this.gameObject, iTween.Hash("islocal", true, "y", 1.35f, "x", 16f, "time", 1f, "easetype", iTween.EaseType.easeInOutSine));
				stage = 1;
				timeset = fa.time;
				delay = 2;
				break;
			case 1:
				//wait
				if (fa.time > (timeset + delay))
				{
					stage = 2;
				}
				break;
			case 2:
				//Breath fire
				NormalHead.SetActive(false);
				ScreamingHead.SetActive(true);
				beam.Play();
				iTween.MoveTo(beamHurtZones, iTween.Hash("islocal", true, "x", -2, "time", fireOut, "easetype", iTween.EaseType.linear));
				Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.MegaSatan_Scream);
				timeset = fa.time;
				delay = 5;
				stage = 3;
				break;
			case 3:
				//wait to stop breathing fire
				if (fa.time > (timeset + delay))
				{
					stage = 4;
				}
				break;
			case 4:
				//stop breathing fire, move down
				NormalHead.SetActive(true);
				ScreamingHead.SetActive(false);
				beam.Stop();
				iTween.MoveTo(beamHurtZones, iTween.Hash("islocal", true, "x", 42, "time", fireIn, "easetype", iTween.EaseType.linear));

				iTween.MoveTo(this.gameObject, iTween.Hash("islocal", true, "y", -9.8f, "x", 16f, "time", 1f, "easetype", iTween.EaseType.easeInOutSine));
				stage = 5;
				timeset = fa.time;
				delay = 2;
				break;
			case 5:
				//wait to stop moving down, 
				if (fa.time > (timeset + delay))
				{
					stage = 6;
				}
				break;
			case 6:
				//breath fire
				NormalHead.SetActive(false);
				ScreamingHead.SetActive(true);
				beam.Play();
				iTween.MoveTo(beamHurtZones, iTween.Hash("islocal", true, "x", -2, "time", fireOut, "easetype", iTween.EaseType.linear));

				timeset = fa.time;
				delay = 5;
				stage = 7;
				break;
			case 7:
				//wait to stop breathing fire, 
				if (fa.time > (timeset + delay))
				{
					stage = 0;
				}
				break;
		}
	}
}
