using UnityEngine;
using System.Collections;

public class SetHealth : MonoBehaviour
{
    public float setToThis = 0;
    public float startingDelayInSeconds = 0;
    float timeSet = 0;

    void Start()
    {
        timeSet = fa.time;
    }

    void Update()
    {
        if (fa.time > timeSet + startingDelayInSeconds)
        {
            HealthScript healthScript;
            healthScript = this.gameObject.GetComponent<HealthScript>();
            if (healthScript)
            {
                healthScript.health = setToThis;
            }
            this.enabled = false;
        }
    }
}
