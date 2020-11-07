using UnityEngine;
using System.Collections;

public class FoundSecretAreaScript : MonoBehaviour
{
    public bool useForceID = false;
    public string label = "Use high #, 30-39";
    public int forceID = 0;//use a high number, 30-40. This will break if a normal secret area uses this slot as well.
    public bool playItween = false;
    public GameObject iTweenObject;
    public string iTweenName;


}
