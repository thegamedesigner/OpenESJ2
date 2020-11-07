using UnityEngine;

[RequireComponent(typeof(HealthScript))]
public class BasicPlayer : MonoBehaviour
{
	public static bool[] savedAbilties                     = new bool[10];//0 is QuintupleJump, 1 is sword,
	public GameObject playerPrefab                         = null;
	public GameObject deathExplo                           = null;
	public GameObject poundExplo                           = null;
	public GameObject airJumpExplo                         = null;
	public GameObject tempAbilityVisual                    = null;
	public GameObject tempAbilityVisualController          = null;
	public GameObject bounceExplo                          = null;
	public GameObject jumpSounds                           = null;
	public GameObject doubleJumpSound                      = null;
	public GameObject landSounds                           = null;
	public GameObject wallStickingSound                    = null;
	public GameObject trail                                = null;
	public GameObject myHitBox                             = null;
	HealthScript hpScript                                  = null;
	[HideInInspector] public Vector3 vel                   = Vector3.zero;
	[HideInInspector] public float wallSticking            = 0.0f;
	[HideInInspector] public float merpsAfterGroundCounter = 0.0f;
	[HideInInspector] public float merpsAfterGroundTime    = 2.0f;
	[HideInInspector] public bool poundToggle              = false;
	[HideInInspector] public bool hasQuintupleJump         = false;
	public bool isMiniPlayer                               = false;
	public bool damageFromPound                            = false;
	public bool createPoundEffect                          = false;
	public bool startFlipped                               = false;
	public bool hasSword                                   = false;
	public bool hasFreshSmash                              = false;

	void Awake()
	{
		hpScript  = this.gameObject.GetComponent<HealthScript>();
		xa.player = this.gameObject;
	}
}
