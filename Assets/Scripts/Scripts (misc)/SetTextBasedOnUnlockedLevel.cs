using UnityEngine;
using System.Collections;

public class SetTextBasedOnUnlockedLevel : MonoBehaviour
{
    public string levelName = "";
    public string unlocked = "";
    public string locked = "";
    public TextMesh textMesh;

    void Update()
    {
        if (LevelInfo.unlocked[LevelInfo.getSceneNumFromName(levelName)])
        {
            textMesh.text = unlocked;

        }
        else
        {
            textMesh.text = locked;
        }
        this.enabled = false;
    }
}
