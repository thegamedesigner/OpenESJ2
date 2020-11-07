using UnityEngine;
using System.Collections;

public class DieAndRestartLevel : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{
		xa.re.cleanLoadLevel(Restart.RestartFrom.RESTART_FROM_CHECKPOINT, "");
	}

}
