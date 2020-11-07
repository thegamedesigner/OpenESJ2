using UnityEngine;
using System.Collections;

public class EnableToPlayAni : MonoBehaviour
{
    public int aniX = 0;

    public GameObject GOWithAnimationScript_GenericScript = null;
    AnimationScript_Generic script = null;

    void Start()
    {
        script = GOWithAnimationScript_GenericScript.GetComponent<AnimationScript_Generic>();
    }

    void Update()
    {
        if (script)
        {
            script.playAnimation(aniX);
        }
        this.enabled = false;
    }
}
