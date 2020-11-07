using UnityEngine;
using System.Collections;

public class MenuStats : MonoBehaviour
{
	public enum statType { None, title, playerSpeedrun, devSpeedrun, playerDeaths, devDeaths, options, soundOptions }
	public GameObject deathIcon      = null;
	public GameObject zeroDeathFlair = null;
	public statType type             = statType.None;
	public static bool dirty         = true;
	TextMesh textMesh                = null;
	int offset                       = 0;
	int lastHighlightedLevel         = -1; // To run through the update function first time.

	//Level Select Outline
	//LevelSelectOutline levelSelectOutline;

	void Start()
	{
		//levelSelectOutline = GameObject.Find("LevelSelectOutlineObject").GetComponent<LevelSelectOutline>();
		textMesh = this.gameObject.GetComponent<TextMesh>();
	}

	void Update()
	{
	}
}
