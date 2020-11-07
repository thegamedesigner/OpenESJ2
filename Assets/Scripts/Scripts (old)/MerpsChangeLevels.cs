using UnityEngine;

public class MerpsChangeLevels : MonoBehaviour
{
	public string level = "";
	public bool onEnabled = false;
	void Update()
	{
		if(onEnabled)
		{
			changeLevel();
		}
	}

	public void changeLevel()
	{
		xa.hasCheckpointed = false;
		Setup.callFadeOutFunc(level, true, UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
		this.enabled = false;
	}

	public void setString0(string input)
	{
		level = input;
	}
}
