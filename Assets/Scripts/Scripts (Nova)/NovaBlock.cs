using UnityEngine;

public class NovaBlock : MonoBehaviour
{
    public GameObject hitbox;

    void Awake()
    {
        transform.SetZ(xa.GetLayer(xa.layers.PlayerAndBlocks));//Set the main game object to the block layer
        hitbox.transform.SetZ(xa.GetLayer(xa.layers.RaycastLayer));//Set the hitbox to the raycasting layer
    }

}
