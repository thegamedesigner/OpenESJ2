using UnityEngine;
using System.Collections;

public class MetaCheckpointScript : MonoBehaviour
{
    float x;
    float y;
    float px;
    float py;
    Vector3 halfScale;

    void Start()
    {

    }

    void Update()
    {
        x = transform.position.x;
        y = transform.position.y;
        px = xa.player.transform.position.x;
        py = xa.player.transform.position.y;
        halfScale = transform.localScale * 0.5f;
        if (xa.player && !xa.playerDead)
        {
            if ((x + halfScale.x) > (px - (xa.playerBoxWidth * 0.5f)) &&
                (x - halfScale.x) < (px + (xa.playerBoxWidth * 0.5f)) &&
                (y + halfScale.y) > (py - (xa.playerBoxHeight * 0.5f)) &&
                (y - halfScale.y) < (py + (xa.playerBoxHeight * 0.5f)))
            {
                saveToCheckpoint();
            }
        }
    }

    void saveToCheckpoint()
    {
        if (za.playerPosInMetaWorld != transform.position)
        {
            za.usePlayerMetaPos = true;
            za.playerPosInMetaWorld = transform.position; 
        }
    }
}
