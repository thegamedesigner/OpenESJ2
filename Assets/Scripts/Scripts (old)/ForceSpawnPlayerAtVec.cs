using UnityEngine;
using System.Collections;

public class ForceSpawnPlayerAtVec : MonoBehaviour
{
	public Vector3 vec1 = Vector3.zero;
	public GameObject playerPrefab;
	public GameObject useThisTransform = null;

	void Start()
	{
	}

	void Update()
	{
		if (this.enabled)
		{
			if (useThisTransform != null)
			{
				vec1 = useThisTransform.transform.position;
			}
			xa.player = (GameObject)(Instantiate(playerPrefab, transform.position, xa.null_quat));
			//xa.firstCheckpointTriggered = true;//fake this
			this.enabled = false;
		}
	}
}
