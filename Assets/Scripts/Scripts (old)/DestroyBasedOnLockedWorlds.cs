using UnityEngine;
using System.Collections;

public class DestroyBasedOnLockedWorlds : MonoBehaviour
{
	public bool destroyIfWorld1IsNotLocked = false;
	public bool destroyIfWorld2IsNotLocked = false;
	public bool destroyIfWorld3IsNotLocked = false;
	public bool destroyIfNothingIsLocked = false;
	bool result = false;
	void Start()
	{
		if (destroyIfWorld1IsNotLocked && !xa.lockWorld1) { result = true; }
		if (destroyIfWorld2IsNotLocked && !xa.lockWorld2) { result = true; }
		if (destroyIfWorld3IsNotLocked && !xa.lockWorld3) { result = true; }
		if (destroyIfNothingIsLocked && !xa.lockWorld1 && !xa.lockWorld2 && !xa.lockWorld3) { result = true; }
		if (result) { Destroy(this.gameObject); }
	}
}
