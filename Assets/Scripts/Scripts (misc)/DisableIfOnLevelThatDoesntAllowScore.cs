using UnityEngine;
using System.Collections;

public class DisableIfOnLevelThatDoesntAllowScore : MonoBehaviour
{
    public GameObject disableObject;
    bool disableMe = false;
    void Update()
    {
        disableMe = false;
        if (za.thisLevelShouldntHaveScoreOrTimers) { disableMe = true; }
        if (za.dontUseQuitPopupOnThisLevel) { disableMe = true; }

        if (!disableMe)
        {
            if (!disableObject.activeSelf)
            {
                disableObject.SetActive(true);
            }
        }
        else
        {
            if (disableObject.activeSelf)
            {
                disableObject.SetActive(false);
            }
        }
    }
}
