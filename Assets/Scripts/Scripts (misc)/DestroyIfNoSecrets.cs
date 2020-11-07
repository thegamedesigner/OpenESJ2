using UnityEngine;
using System.Collections;

public class DestroyIfNoSecrets : MonoBehaviour
{
    void Update()
    {
        if (!LevelInfo.HasUnlockedSecrets())
        {
            Destroy(this.gameObject);
        }
    }
}
