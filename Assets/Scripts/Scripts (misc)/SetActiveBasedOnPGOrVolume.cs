using UnityEngine;
using System.Collections;

public class SetActiveBasedOnPGOrVolume : MonoBehaviour
{
    public GameObject[] GOs = new GameObject[0];
    public bool ActiveBasedOnPGMode = false;
    public bool ActiveBasedOnMuted = false;
    bool? oldState = null;
    int index = 0;
    int id = 0;
    bool? result = null;
    void Update()
    {
        if (ActiveBasedOnPGMode) 
        {
            result = (bool)!xa.pgMode;
        }

        if (ActiveBasedOnMuted) 
        {
            result = false;
            if (xa.muteMusic == 0 && xa.muteSound == 0)
            {
                result = true;
            }
        }
        if (oldState != result)
        {
            oldState = (bool)result;

            if (oldState == true)
            {
                index = 0;
                while (index < GOs.Length)
                {
                    GOs[index].SetActive(true);
                    index++;
                }
            }
            if(oldState == false)
            {
                index = 0;
                while (index < GOs.Length)
                {
                    GOs[index].SetActive(false);
                    index++;
                }
            }
        }
    }
}
