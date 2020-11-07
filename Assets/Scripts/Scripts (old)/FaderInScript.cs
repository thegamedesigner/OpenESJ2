using UnityEngine;

public class FaderInScript : MonoBehaviour
{
	private float fadeSpd       = 1.0f;
	private bool fadeFast       = false;
	private int fullScreenPause = 0;
	private Renderer myRenderer   = null;

	void Awake()
	{
		xa.fadingOut  = false;
		xa.fadingIn   = true;
		this.myRenderer = this.gameObject.GetComponent<Renderer>();
	}

	void Start()
	{
		if (fa.useBlackFaders)
		{
			myRenderer.material = xa.de.FaderMatBlack;
		}

		xa.faderIn = this.gameObject;
		xa.allowPlayerInput = true;
		Random.InitState(1984); // Set seed to something standard for boss fights (damn those speed runners are annyoing ;))

		Color temp = myRenderer.material.color;
		temp.a = 1.0f;
		myRenderer.material.color = temp;

		if (xa.fadeInFast) {
			this.fadeFast = true;
		}
	}

	void Update()
	{
		this.fullScreenPause++;
		if (this.fullScreenPause > 3) {
			if (xa.frozenCamera) {
				xa.frozenCamera = false;
			}

			this.fadeSpd += 0.1f * fa.deltaTime;
			Color tempColor = myRenderer.material.color;
			tempColor.a -= this.fadeSpd * fa.deltaTime;

			if (this.fadeFast) {
				tempColor.a -= 4 * fa.deltaTime;
			}

			myRenderer.material.color = tempColor;

			if (tempColor.a <= 0.0f) {
				xa.fadingAtAll = false;
				xa.fadingIn = false;
				myRenderer.enabled = false;
				this.enabled = false;
			}
		}
	}
}
