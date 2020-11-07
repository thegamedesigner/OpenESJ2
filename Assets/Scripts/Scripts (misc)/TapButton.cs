using UnityEngine;
using System.Collections;

public class TapButton : MonoBehaviour
{
    public Behaviour enableThis = null;
    public bool useOverlayCamera = false;
    public bool dontDisableOnTapped = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    Ray ray = new Ray();
    RaycastHit hit;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //look for button
            if (!useOverlayCamera) { ray = Camera.main.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition); }
            else { ray = xa.overlayCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition); }
            
            if (this.GetComponent<Collider>().Raycast(ray, out hit, 100))
            {

                enableThis.enabled = true;
                if (!dontDisableOnTapped) { this.enabled = false; }
            }
        }
    }
}
