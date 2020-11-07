using UnityEngine;
using System.Collections;

public class StarCollector : MonoBehaviour
{
    //public GameObject coinSndEffect = null;
    public static int totalMerpsCoins = 0;
    GameObject[] gos;
    float coinCollectionDist = 1;
    StarScript starScript = null;

    void Start()
    {
        gos = GameObject.FindGameObjectsWithTag("merpsCoin");
    }

    void Update()
    {
        if (za.relookForStars)
        {
            za.relookForStars = false;

            gos = GameObject.FindGameObjectsWithTag("merpsCoin");
        }
        foreach (GameObject go in gos)
        {
            if (go)//I don't update this array except on start, so some of the GO's may be null
            {
                if (go.activeInHierarchy)
                {
                    xa.glx = go.transform.position;
                    xa.glx.z = transform.position.z;
                    if (Vector3.Distance(xa.glx, transform.position) < coinCollectionDist)
                    {
                        //xa.tempobj = (GameObject)(Instantiate(coinSndEffect, go.transform.position, coinSndEffect.transform.rotation));
                        go.tag = "Untagged";
                        starScript = null;
                        starScript = go.GetComponent<StarScript>();
                        if (starScript)
                        {
                            if (!starScript.dead)
                            {

                                starScript.collectThisStar();
                            }
                        }
                    }
                }
            }
        }
    }
}
