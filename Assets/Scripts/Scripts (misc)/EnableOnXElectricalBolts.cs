using UnityEngine;
using System.Collections;

public class EnableOnXElectricalBolts : MonoBehaviour
{
    public int goalNumOfBolts = 0;
    public Behaviour enableThis;
    void Update()
    {
        if (za.numberOfElectricalBolts >= goalNumOfBolts)
        {
            enableThis.enabled = true;
            this.enabled = false;
        }
    }
}
