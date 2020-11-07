using UnityEngine;

public class TextScript : MonoBehaviour
{
	public float destroyAfterTime = 3;
	[UnityEngine.Serialization.FormerlySerializedAs("renderer")]
	public Renderer textRenderer;
	public Material zeroAlphaMat;
	public GameObject triggerGO;
	public bool upsidedown = false;

	float x;
	float y;
	float px = -999;
	float py = -999;
	Vector3 halfScale;

	float timeSet = 0;

	bool pged = false;

	enum State
	{
		None,
		Init,
		WaitingToTrigger,
		Triggering,
		Waiting,
		Destroy,
		End
	}
	State state = State.Init;

	void Start()
	{
	}

	void Update()
	{
		if (!pged)
		{
			pged = true;
			//Debug.Log("PGMode: " + xa.pgMode + ", Checking: " + textRenderer.gameObject.GetComponent<TextMesh>().text); 
			if (xa.pgMode)
			{
				string s = textRenderer.gameObject.GetComponent<TextMesh>().text;
				textRenderer.gameObject.GetComponent<TextMesh>().text = CensorTextScript.CensorText(s);
			}
		}
		if (xa.player)
		{
			x = triggerGO.transform.position.x;
			y = triggerGO.transform.position.y;

			px = xa.player.transform.position.x;
			py = xa.player.transform.position.y;

			halfScale = triggerGO.transform.localScale * 0.5f;
			if (xa.player && !xa.playerDead)
			{
				if ((x + halfScale.x) > (px - (xa.playerBoxWidth * 0.5f)) &&
					(x - halfScale.x) < (px + (xa.playerBoxWidth * 0.5f)) &&
					(y + halfScale.y) > (py - (xa.playerBoxHeight * 0.5f)) &&
					(y - halfScale.y) < (py + (xa.playerBoxHeight * 0.5f)))
				{
					if (state == State.WaitingToTrigger)
					{
						state = State.Triggering;
					}
				}
			}
		}
		switch (state)
		{
			case State.Init:
				if (!upsidedown) { transform.AddY(-1f); }
				else { transform.AddY(1f); }

				textRenderer.material = zeroAlphaMat;
				state = State.WaitingToTrigger;
				break;
			case State.WaitingToTrigger:
				break;
			case State.Triggering:
				iTween.FadeTo(this.gameObject, iTween.Hash("alpha", 1, "time", 0.4f, "easetype", iTween.EaseType.easeOutSine));
				if (!upsidedown) { iTween.MoveBy(this.gameObject, iTween.Hash("y", 1, "time", 0.4f, "easetype", iTween.EaseType.easeOutSine)); }
				else { iTween.MoveBy(this.gameObject, iTween.Hash("y", -1, "time", 0.4f, "easetype", iTween.EaseType.easeOutSine)); }

				timeSet = fa.time;
				state = State.Waiting;
				break;
			case State.Waiting:
				if (fa.time > (timeSet + destroyAfterTime))
				{
					state = State.Destroy;
				}
				break;
			case State.Destroy:
				iTween.FadeTo(this.gameObject, iTween.Hash("alpha", 0, "time", 0.4f, "easetype", iTween.EaseType.easeInSine));



				if (!upsidedown) { iTween.MoveBy(this.gameObject, iTween.Hash("y", 1, "time", 0.4f, "easetype", iTween.EaseType.easeInSine)); }
				else { iTween.MoveBy(this.gameObject, iTween.Hash("y", -1, "time", 0.4f, "easetype", iTween.EaseType.easeInSine)); }
				this.enabled = false;
				break;
		}
	}



}
