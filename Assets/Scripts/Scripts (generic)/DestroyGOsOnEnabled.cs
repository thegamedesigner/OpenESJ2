using UnityEngine;
using System.Collections;

public class DestroyGOsOnEnabled : MonoBehaviour
{
    public GameObject[] GOs = new GameObject[0];
    public bool ifNullIgnore = false;
    int index = 0;
    void Update()
    {
        index = 0;
        while(index < GOs.Length)
        {
            if (!ifNullIgnore || GOs[index])
            {
                //Setup.GC_DebugLog("Doing stuff!");
                if (!GOs[index]) { GOs[index] = this.gameObject; }
                Destroy(GOs[index]);
                this.enabled = false;
            }
            index++;
        }
    }
}
