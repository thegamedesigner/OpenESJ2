using UnityEngine;
using System.Collections;

public class SetActiveScript : MonoBehaviour
{
	public bool setTo = false;
    public GameObject setThisGO = null;
    public bool setBlocksToDirty = false;//forces a recheck of all blocks. Use when spawning new blocks
    public bool recheckStars = false;//forces StarCollector to update it's star list
	void Update()
	{
		setThisGO.SetActive(setTo);
        if (setBlocksToDirty) { xa.onScreenObjectsDirty = true; }
        if (recheckStars) { za.relookForStars = true; }
		this.enabled = false;
	}
}
