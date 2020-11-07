using UnityEngine;
using System.Collections;

public class DrSwingerGunTurretScript : MonoBehaviour
{
    float delayBeforeMovingUp = 6;
    bool movedUp = false;
    public GameObject mainPod = null;
    public GameObject moveUpLeg1 = null;
    public GameObject moveUpLeg2 = null;

    float delayBeforeMovingForward = 10;
    bool movedForward = false;
    public GameObject upperPod = null;
    public GameObject moveForwardLeg = null;

    float delayBeforeUpperCannon = 11.5f;
    bool stuckOutUpperCannon = false;
    public GameObject upperCannon = null;
    public GameObject upperCannonEnableBehaviourObj = null;

    float delayBeforeImpSpawner = 16;
    bool stuckOutImpSpawner = false;
    public GameObject impSpawner = null;
    public GameObject impSpawnerLeg = null;
    public GameObject impSpawnerEnableBehaviourObj = null;

    float delayBeforeUpperMissileLauncher = 17;
    bool stuckOutUpperMissileLauncher = false;
    public GameObject upperMissileLauncher = null;
    public GameObject upperMissileLauncherEnableBehaviourObj = null;

    float timeSet = 0;
    
    void Start()
    {
        timeSet = fa.time;
    }

    void Update()
    {
        if (fa.time >= delayBeforeMovingUp && !movedUp)
        {
            iTween.MoveTo(mainPod, iTween.Hash("y", 10, "time", 3));
            iTween.MoveTo(moveUpLeg1, iTween.Hash("y", 5.5f, "time", 3));
            iTween.ScaleTo(moveUpLeg1, iTween.Hash("y", 8, "time", 3));
            iTween.MoveTo(moveUpLeg2, iTween.Hash("y", 5.5f, "time", 3));
            iTween.ScaleTo(moveUpLeg2, iTween.Hash("y", 8, "time", 3));
            movedUp = true;
        }
        if (fa.time >= delayBeforeMovingForward && !movedForward)
        {
            iTween.MoveTo(mainPod, iTween.Hash("x", 11f, "time", 3));

            xa.glx = upperPod.transform.position;
            xa.glx.y = 10;
            upperPod.transform.position = xa.glx;

            xa.glx = moveForwardLeg.transform.position;
            xa.glx.x = 15.8f;
            xa.glx.y = 9.8f;
            moveForwardLeg.transform.position = xa.glx;

            iTween.MoveTo(moveForwardLeg, iTween.Hash("x", 13.4f, "time", 3));
            iTween.ScaleTo(moveForwardLeg, iTween.Hash("x", 5, "time", 3));

            movedForward = true;
        }
        if (fa.time >= delayBeforeUpperCannon && !stuckOutUpperCannon)
        {
            xa.glx = upperCannon.transform.position;
            xa.glx.y = 10;
            xa.glx.x = 11.5f;
            upperCannon.transform.position = xa.glx;

            iTween.MoveTo(upperCannon, iTween.Hash("x", 10.5f, "time", 1));

            upperCannonEnableBehaviourObj.SendMessage("enableBehaviour");
            stuckOutUpperCannon = true;
        }
        if (fa.time >= delayBeforeImpSpawner && !stuckOutImpSpawner)
        {
            xa.glx = impSpawner.transform.position;
            xa.glx.y = 9.7f;
            xa.glx.x = 11f;
            impSpawner.transform.position = xa.glx;

            iTween.MoveTo(impSpawner, iTween.Hash("y", 7f, "time", 2));

            xa.glx = impSpawnerLeg.transform.position;
            xa.glx.y = 9.7f;
            xa.glx.x = 11f;
            impSpawnerLeg.transform.position = xa.glx;
            iTween.MoveTo(impSpawnerLeg, iTween.Hash("y", 8.42f, "time", 2));
            iTween.ScaleTo(impSpawnerLeg, iTween.Hash("y", 2.5f, "time", 2));

            stuckOutImpSpawner = true;

            impSpawnerEnableBehaviourObj.SendMessage("enableBehaviour");
        }
        if (fa.time >= delayBeforeUpperMissileLauncher && !stuckOutUpperMissileLauncher)
        {
            xa.glx = upperMissileLauncher.transform.position;
            xa.glx.y = 10f;
            xa.glx.x = 15.41f;
            upperMissileLauncher.transform.position = xa.glx;

            iTween.MoveTo(upperMissileLauncher, iTween.Hash("x", 16.6f, "time", 2));

            upperMissileLauncherEnableBehaviourObj.SendMessage("enableBehaviour");
            stuckOutUpperMissileLauncher = true;
        }
    }
}
