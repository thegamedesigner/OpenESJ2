using UnityEngine;
using System.Collections;

public class SetActiveIfLevelIsUnlocked : MonoBehaviour
{
    public GameObject[] GOs = new GameObject[0];
    public bool useSelectionBoxValue = false;
    public int useThisLevel = -1;
    public bool setToIfUnlocked = false;
    bool oldState = false;
    int index = 0;
    int id = 0;
    
    void Update()
    {
        if (useThisLevel != -1)
        {
            id = useThisLevel;
        }
        if (useSelectionBoxValue) { id = za.menuSelectionBoxValue; }


        if(oldState != LevelInfo.unlocked[id])
        {
            oldState = LevelInfo.unlocked[id];

            if (oldState)
            {
                index = 0;
                while (index < GOs.Length)
                {
                    GOs[index].SetActive(setToIfUnlocked);
                    index++;
                }
            }
            else
            {
                index = 0;
                while (index < GOs.Length)
                {
                    GOs[index].SetActive(!setToIfUnlocked);
                    index++;
                }
            }
        }
    }
}
