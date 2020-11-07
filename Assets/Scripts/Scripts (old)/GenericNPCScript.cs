using UnityEngine;
using System.Collections;

public class GenericNPCScript : MonoBehaviour
{
	public GameObject animateThisGO = null;
	public string standAni = "";
	public string[] fidgets = new string[0];

	float fidgetTimeSet = 0;
	float fidgetDelayMin = 2;////12;//in seconds
	float fidgetDelayMax = 4;////25;//these are not public because they'd always be about the same, and it makes the script simpler to look at.
	float fidgetDelayResult = 0;
	int result1 = 0;

	void Start()
	{
		fidgetTimeSet = fa.time;
		fidgetDelayResult = Random.Range(fidgetDelayMin, fidgetDelayMax);
		//animateThisGO.SendMessage(standAni);
	}

	void Update()
	{
		if (fa.time > (fidgetTimeSet + fidgetDelayResult))
		{
			fidgetTimeSet = fa.time;
			fidgetDelayResult = Random.Range(fidgetDelayMin, fidgetDelayMax);
			result1 = (int)(Random.Range(0, fidgets.Length));
			if (result1 >= fidgets.Length) { result1 = 0; }
			else { animateThisGO.SendMessage(fidgets[result1]); }


		}

	}

	public void finishedFidgetingAnimation()
	{
		animateThisGO.SendMessage(standAni);
	}
}
