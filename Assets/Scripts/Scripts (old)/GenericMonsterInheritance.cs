using UnityEngine;

public class GenericMonsterInheritance : MonoBehaviour
{
    public bool checkForFloors(float dist, LayerMask layerMask)//returns true if it detects a drop off.
    {
        RaycastHit hit;
        Ray ray = new Ray();
        xa.glx = transform.position;
        xa.glx.z = xa.GetLayer(xa.layers.PlayerAndBlocks);
        ray.origin = xa.glx;
        ray.direction = transform.right;
        if (!Physics.Raycast(ray, out hit, dist, layerMask))
        {
            return (true);
        }
        return (false);
    }
}
