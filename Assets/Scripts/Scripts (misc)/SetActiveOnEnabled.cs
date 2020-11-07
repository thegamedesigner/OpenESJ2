using UnityEngine;
using System.Collections;

public class SetActiveOnEnabled : MonoBehaviour
{
    public GameObject go = null;
    public bool setToThis = false;

    void Update()
    {
        go.SetActive(setToThis);
        this.enabled = false;
    }
}
