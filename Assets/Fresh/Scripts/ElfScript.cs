using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElfScript : MonoBehaviour
{
	public GameObject freedomExplo;
	public GameObject text;
	public FreshAni aniScript;
	public bool rescued = false;
	float timeSet = 0;
	bool fadeText = false;

	void Start()
	{
		iTween.FadeTo(text, iTween.Hash("alpha", 0, "time", 0));
		iTween.MoveBy(text, iTween.Hash("y", -1, "time", 0));


	}

	void Update()
	{
		if (xa.player != null && !rescued)
		{
			if (Vector2.Distance(xa.player.transform.position, transform.position) < 2)
			{
				rescued = true;
				aniScript.PlayAnimation(1);
				iTween.FadeTo(text, iTween.Hash("alpha", 1, "time", 0.4f, "easetype", iTween.EaseType.easeOutSine));
				iTween.MoveBy(text, iTween.Hash("y", 1, "time", 0.4f, "easetype", iTween.EaseType.easeOutSine));
				fadeText = true;
				timeSet = fa.time;
				GameObject go = Instantiate(freedomExplo, transform.position,transform.rotation);
				Fresh_SoundEffects.PlaySound(Fresh_SoundEffects.Type.ElfSound);

			}
		}

		if (rescued)
		{
			if (fadeText)
			{
				if (fa.time > (timeSet + 3))
				{
					fadeText = false;
					iTween.FadeTo(text, iTween.Hash("alpha", 0, "time", 1f, "easetype", iTween.EaseType.easeOutSine));

				}
			}
		}

	}
}
