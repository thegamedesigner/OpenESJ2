using UnityEngine;
using System.Collections;

public class SetActiveIfPlayerIsDead : MonoBehaviour
{
    public GameObject setThis = null;
    public bool ToThis = false;
    void Update()
    {
        if (xa.player)
        {
            if (xa.playerDead)
            {
                setThis.SetActive(ToThis);
                this.enabled = false;
            }
        }

    }
}
