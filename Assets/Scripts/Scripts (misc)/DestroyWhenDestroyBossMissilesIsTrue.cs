using UnityEngine;
using System.Collections;

public class DestroyWhenDestroyBossMissilesIsTrue : MonoBehaviour
{
    public GameObject destroyThis = null;
    public bool lowerHealthToZeroInstead = false;
    void Update()
    {
        if (za.destroyBossMissiles)
        {
            if(lowerHealthToZeroInstead)
            {
                HealthScript script = destroyThis.GetComponent<HealthScript>();
                script.health = 0;
            }
            else
            {
            Destroy(destroyThis);
            }
            if (this) { this.enabled = false; }
        }
    }
}
