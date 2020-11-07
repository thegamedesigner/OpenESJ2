using UnityEngine;
using System.Collections;

public class DeathCutsceneControllerScript_Biplane : MonoBehaviour
{
    public GameObject aliveStuff = null;
    public GameObject deadStuff = null;
    
    void Update()
    {
        za.destroyBossMissiles = true;
        deadStuff.SetActive(true);
        aliveStuff.SetActive(false);
        this.enabled = false;
    }
}
