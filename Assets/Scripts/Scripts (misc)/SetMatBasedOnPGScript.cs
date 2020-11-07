using UnityEngine;
using System.Collections;

public class SetMatBasedOnPGScript : MonoBehaviour
{
    public Material nonPGMaterial;
    public Material PGMaterial;
    bool isPG = false;
    void Update()
    {
        if (isPG)
        {
            if (!xa.pgMode)
            {
                GetComponent<Renderer>().material = nonPGMaterial;
                isPG = false;
            }
        }
        else
        {
            if (xa.pgMode)
            {
                GetComponent<Renderer>().material = PGMaterial;
                isPG = true;
            }
        }
    }
}
