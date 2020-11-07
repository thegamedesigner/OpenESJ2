using UnityEngine;

public class LoadingScreenScript : MonoBehaviour
{
	public static LoadingScreenScript self;
	public GameObject puppet;

	void Start()
	{
		self = this;
		transform.SetY(0);
		LoadingScreenOn();
	}
	void Update()
	{
		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex != 0)
		{
			LoadingScreenOff();
		}
	}

	public static void LoadingScreenOff()
	{
		if (self != null)
		{
			self.puppet.SetActive(false);
		}
	}
	public static void LoadingScreenOn()
	{
		if (self != null)
		{
			self.puppet.SetActive(true);
		}
	}
}
