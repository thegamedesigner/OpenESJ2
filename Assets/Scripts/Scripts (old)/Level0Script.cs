using UnityEngine;
using System.Collections;
using System.IO;

public class Level0Script : MonoBehaviour
{
	int index = 0;
	void Update()
	{
		if (index > 5)
		{
			xa.beenToLevel0 = true;
			//Setup.GC_DebugLog("On level0");
			//Application.LoadLevel(xa.levelBeforeGoingToZero);

			if (xa.levelBeforeGoingToZero == "")
			{
				//Debug.Log("hi 1");
				//Debug.Log("LOADING LEVEL: 1");
				UnityEngine.SceneManagement.SceneManager.LoadScene(1);

				// Application.LoadLevel("editorLevel");
				//Application.LoadLevel("levelTitle");
			}
			else
			{
				//Debug.Log("hi 2");
				xa.re.cleanLoadLevel(0, xa.levelBeforeGoingToZero);
			}
		}
		else
		{
			index++;
		}
	}
}
