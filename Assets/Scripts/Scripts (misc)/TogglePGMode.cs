using UnityEngine;
using System.Collections;

public class TogglePGMode : MonoBehaviour
{
    void Update()
    {
        xa.pgMode = !xa.pgMode;

        this.enabled = false;
    }
}
