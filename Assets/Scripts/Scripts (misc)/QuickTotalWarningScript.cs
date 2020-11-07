using UnityEngine;
using System.Collections;

public class QuickTotalWarningScript : MonoBehaviour
{
    void Start()
    {
        if (Time.realtimeSinceStartup < 5)
        {
            Time.timeScale = 0;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    float counter = 0;
    void Update()
    {
        counter += 1;
        if (counter > 350)
        {
            Time.timeScale = 1;
            Destroy(this.gameObject);
        }
    }
}
