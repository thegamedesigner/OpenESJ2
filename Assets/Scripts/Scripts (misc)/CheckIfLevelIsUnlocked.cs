using UnityEngine;
using System.Collections;

public class CheckIfLevelIsUnlocked : MonoBehaviour
{
    public Behaviour enableThisIfUnlocked = null;
    public Behaviour enableThisIfLocked = null;
    public int forceUseThisLevel = -1;
    void Update()
    {
        if ((forceUseThisLevel < 0 && LevelInfo.unlocked[za.menuSelectionBoxValue]) || (forceUseThisLevel >= 0 && LevelInfo.unlocked[forceUseThisLevel]))
        {
            if (enableThisIfUnlocked) { enableThisIfUnlocked.enabled = true; }
        }
        else
        {
            if (enableThisIfLocked) { enableThisIfLocked.enabled = true; }
        }
        this.enabled = false;
    }
}
