using UnityEngine;
using System.Collections;

public class SetTextMeshToMultilineString : MonoBehaviour
{
    [Multiline]
    public string text = "";
    public TextMesh textMesh;

    void Update()
    {
        textMesh.text = text;
        this.enabled = false;
    }
}
