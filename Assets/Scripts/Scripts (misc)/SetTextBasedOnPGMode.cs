using UnityEngine;
using System.Collections;

public class SetTextBasedOnPGMode : MonoBehaviour
{
    public string PGText = "";
    public string NonPGText = "";
    public TextMesh textMesh;
    bool isPG = false;

    void Update()
    {
        if (isPG)
        {
            if (!xa.pgMode)
            {
                textMesh.text = NonPGText;
                isPG = false;
            }
        }
        else
        {
            if (xa.pgMode)
            {
                textMesh.text = PGText;
                isPG = true;
            }
        }
    }
}
