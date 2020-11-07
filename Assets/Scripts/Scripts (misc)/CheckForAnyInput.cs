using UnityEngine;
using System.Collections;

public class CheckForAnyInput : MonoBehaviour
{
    public Behaviour enableThis = null;
    public Behaviour[] enableThese = new Behaviour[0];
    public bool setToThis = true;

    void Update()
    {
        if (checkForAnyInputFunc())
        {
            enableThis.enabled = setToThis;

            if (enableThese.Length > 0)
            {
                int index = 0;
                while (index < enableThese.Length)
                {
                    enableThese[index].enabled = setToThis;
                    index++;
                }
            }

            this.enabled = false;
        }
    }

    bool checkForAnyInputFunc()
    {
        return Controls.GetAnyKeyDown();
    }
}
