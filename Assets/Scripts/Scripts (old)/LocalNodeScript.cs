using UnityEngine;

public class LocalNodeScript : MonoBehaviour
{
	// Level stuff
	[Header("Level")]
	[UnityEngine.Serialization.FormerlySerializedAs("ltColour")]
	public Color lightColour;
	[UnityEngine.Serialization.FormerlySerializedAs("dkColour")]
	public Color darkColour;

	public float gravityMultiplier             = 1.0f;
	public float maxPlayerFallSpeed            = 0.0f;
	public bool rainLevel                      = false;
	public bool dontShowScoreOrCountdownTimers = false;
	public bool disableRespawnKey              = false;

	// Audio stuff
	[Header("Audio")]
	public AudioClip music   = null;
	public bool usePlayPoint = false;//Doesn't work with skaldScript (used to work with bardScript)
	public float playPoint   = 0.0f;

	[Header("Super Smooth(tm) Audio")]
	public bool useSuperSmoothTransferIntoLooping = false;
	public AudioClip SuperSmoothSytem_introTrack  = null;
	public AudioClip SuperSmoothSytem_loopTrack   = null;

	private void Awake()
	{
		xa.localNodeScript                    = this;
		xa.haltForSecondGlorg                 = false;
		xa.showCountdown                      = !this.dontShowScoreOrCountdownTimers;
		xa.showScore                          = !this.dontShowScoreOrCountdownTimers;
		za.thisLevelShouldntHaveScoreOrTimers = this.dontShowScoreOrCountdownTimers;
		za.dontUseQuitPopupOnThisLevel        = false;
		za.allowUseWorldPopupOnThisLevel      = false;
		za.hideMobileControlsOnThisLevel      = false;
		za.numberOfElectricalBolts            = 0;
	}
}
