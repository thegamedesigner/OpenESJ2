using UnityEngine;
using System.Collections;

public class SetToMultilineString : MonoBehaviour
{
    [Multiline]
    public string str = "";

    void Start()
    {
        TextMesh textMesh = this.gameObject.GetComponent<TextMesh>();
        textMesh.text = str;
    }

}
