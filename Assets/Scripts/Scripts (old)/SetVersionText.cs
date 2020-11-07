using UnityEngine;
using System.Collections;

public class SetVersionText : MonoBehaviour
{
	string versionText;
	public TextMesh version;
	
	void Start ()
	{
		initVersionText();
	}
	
	void initVersionText()
	{
		versionText = "nullVerNum";//xa.versionNumberString;
		if (xa.postToAltLeaderboards) versionText += "L";
#if CHEATS
		versionText += "C";
#endif
	
#if DEBUG
		versionText = versionText + "D";
#endif
	
#if STEAMWORKS
		versionText += " Steam";
#endif
	
#if IOS
		versionText += " iOS";
#endif
	
#if ANDROID
		versionText += " Android";
#endif
	
		version.text = versionText;
	}
}
