using UnityEngine;
using System.Collections;

public class SetInWorldScorePos : MonoBehaviour
{
    void Update()
    {
        za.inworldScorePos = transform.position;
        za.inMainCameraWorldScorePos = Camera.main.GetComponent<Camera>().transform.position + transform.localPosition;
        //Debug.DrawLine(transform.position, za.inworldScorePos, Color.green);
    }
}
