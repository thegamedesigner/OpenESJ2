using UnityEngine;

public class ChunkZone : MonoBehaviour
{
    public GameObject chunk;
    float x;
    float y;
    float px;
    float py;
    Vector3 halfScale;
    Vector3 pHalfScale;
    bool desiredState = true;
    float bufferDist = 18;//this used to be 46

    void Start()
    {
        xa.glx = transform.localScale;
       // xa.glx.x += bufferDist;
       // xa.glx.y += bufferDist;
        transform.localScale = xa.glx;

        pHalfScale.x = 18;
        pHalfScale.y = 13;
    }

    void Update()
    {
        //is the camera in my zone?

        x = transform.position.x;
        y = transform.position.y;
        px = Camera.main.GetComponent<Camera>().transform.position.x;//xa.player.transform.position.x;
        py = Camera.main.GetComponent<Camera>().transform.position.y;//xa.player.transform.position.y;
        halfScale = transform.localScale * 0.5f;
        desiredState = (x + halfScale.x) > (px - pHalfScale.x) &&
		               (x - halfScale.x) < (px + pHalfScale.x) &&
		               (y + halfScale.y) > (py - pHalfScale.y) &&
		               (y - halfScale.y) < (py + pHalfScale.y);

        if (desiredState != chunk.activeSelf)
        {
            chunk.SetActive(desiredState);
            xa.onScreenObjectsDirty = true; 
            za.relookForStars = true;
            //print states

        }
    }
}
