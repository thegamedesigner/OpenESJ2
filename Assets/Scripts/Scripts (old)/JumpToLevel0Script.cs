using UnityEngine;

public class JumpToLevel0Script : MonoBehaviour
{
	public bool forceGoToLevelZero = false;
	void Start()
	{
		// Screen.lockCursor = true;//for pax build
		// Screen.showCursor = false;
		if (!xa.beenToLevel0 || forceGoToLevelZero) {
			xa.calledOncePerRunningTheOfGame();
			xa.levelBeforeGoingToZero = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
			xa.frozenCamera = true;
			UnityEngine.SceneManagement.SceneManager.LoadScene(0);
		}
	}
}
