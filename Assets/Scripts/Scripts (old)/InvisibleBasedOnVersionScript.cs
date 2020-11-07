using UnityEngine;
using System.Collections;

public class InvisibleBasedOnVersionScript : MonoBehaviour
{
	public bool invisibleForAndroid = false;
	public bool invisibleForIPad = false;
	public bool invisibleForPCMacLinux = false;
	
	void Start()
	{
#if ANDROID
		if (invisibleForAndroid) this.gameObject.renderer.enabled = false;
#elif IOS
		if (invisibleForIPad) this.gameObject.renderer.enabled = false;
#else
		if (invisibleForPCMacLinux) this.gameObject.GetComponent<Renderer>().enabled = false;
#endif
	}
}
