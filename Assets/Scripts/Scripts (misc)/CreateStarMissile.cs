using UnityEngine;
using System.Collections;

public class CreateStarMissile : MonoBehaviour
{
    public GameObject missile;
    void Update()
    {
        xa.glx = za.inMainCameraWorldScorePos;
       xa.tempobj = (GameObject)(Instantiate(missile,xa.glx,missile.transform.rotation));

        this.enabled = false;
    }
}
