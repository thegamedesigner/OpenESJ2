using UnityEngine;
using System.Collections;

public class SnapToCameraCornerScript : MonoBehaviour
{
    public enum Locations { None, BottomLeftCorner, BottomRightCorner, TopCenter }
    public Locations location = Locations.None;
    public bool useMainCamera = false;//Uses overlay camera by default
	[UnityEngine.Serialization.FormerlySerializedAs("camera")]
    Camera cornerCamera;
    void Start()
    {
        StartCoroutine(SlowCheck());
    }

    IEnumerator SlowCheck()
    {
        while (true)
        {
            if (useMainCamera)
            {
                cornerCamera = Camera.main.GetComponent<Camera>();
            }
            else
            {
                if (xa.overlayCamera) { cornerCamera = xa.overlayCamera.GetComponent<Camera>(); }
            }
            if (cornerCamera)
            {
                if (location == Locations.BottomLeftCorner)
                {
                    transform.position = cornerCamera.ScreenToWorldPoint(new Vector3(0, 0, 30));
                }
                if (location == Locations.BottomRightCorner)
                {
                    transform.position = cornerCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 30));
                }
                if (location == Locations.TopCenter)
                {
                    transform.position = cornerCamera.ScreenToWorldPoint(new Vector3(Screen.width * 0.5f, Screen.height, 30));
                }

            }


            yield return new WaitForSeconds(1.0f);
        }
    }
}
