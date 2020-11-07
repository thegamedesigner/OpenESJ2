using UnityEngine;
using System.Collections;

public class PlayRandomAni : MonoBehaviour
{
    public AnimationScript_Generic script = null;
    public int maxAni = 0;
    void Update()
    {
        if (script)
        {
            script.playAnimation(Random.Range(0, maxAni));
        }
        this.enabled = false;
    }

}
