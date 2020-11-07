using UnityEngine;
using System.Collections;

public class SetMatBasedOnIfLevelIsUnlocked : MonoBehaviour
{
    public int levelNum = 0;
    public Material matLocked = null;
    public Material matUnlocked = null;
    void Update()
    {
        if (LevelInfo.unlocked[levelNum])
        {
            GetComponent<Renderer>().material = matUnlocked;
        }
        else
        {
            GetComponent<Renderer>().material = matLocked;
        }
        this.enabled = false;
    }
}
