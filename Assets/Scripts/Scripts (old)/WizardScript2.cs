using UnityEngine;

public class WizardScript2 : MonoBehaviour
{
	public GameObject puppet;
	public GameObject gun;
	public GameObject textObj;
	public GameObject portal;

	public int z1 = 0;
	public int x1 = 0;
	public int y1 = 0;
	public int numOfFrames1 = 0;
	public float speed1 = 0;

	[HideInInspector]
	public int saySomething = 0;
	float counter = 0;
	TextMesh textMesh;
	WizardScript wizardScript = null;
	bool triggered = false;


	int stage = 0;

	void Start()
	{
		x1 = 0;
		y1 = 3;
		//setPuppetsTexture(x1, y1);
		wizardScript = this.gameObject.GetComponent<WizardScript>();
	}

	public void triggerMe()
	{
		triggered = true;
	}
	void Update()
	{
		float spd = 35;
		if (triggered)
		{


			//Setup.GC_DebugLog("HERE!");
			counter += 10 * fa.deltaTime;
			if (stage == 0)
			{
				if (counter > spd)
				{
					counter = 0;
					stage = 1;
					if (textObj)
					{
					  //  xa.glx = transform.position;
					  //  xa.glx.z = xa.explo1Layer;
					  //  xa.glx.y += 1;
					  //  xa.tempobj = (GameObject)(Instantiate(textObj, xa.glx, xa.null_quat));
					   // textMesh = xa.tempobj.GetComponent<TextMesh>();
					  //  textMesh.text = "Fine...";
					  //  xa.tempobj.transform.parent = xa.createdObjects.transform;
					}


					GameObject[] gos;
					HealthScript script = null;
					gos = GameObject.FindGameObjectsWithTag("monster");

					foreach (GameObject go in gos)
					{
						script = null;
						script = go.GetComponent<HealthScript>();
						if (script)
						{
							script.health = 0;
						}
					}
				}
			}
			else if (stage == 1)
			{
				if (counter > spd)
				{
					counter = 0;
					stage = 2;
					if (textObj)
					{
					   // xa.glx = transform.position;
					  // xa.glx.z = xa.explo1Layer;
					  //  xa.glx.y += 1;
					  //  xa.tempobj = (GameObject)(Instantiate(textObj, xa.glx, xa.null_quat));
					  //  textMesh = xa.tempobj.GetComponent<TextMesh>();
					  //  textMesh.text = "You wanna\ndance?";
					  //  xa.tempobj.transform.parent = xa.createdObjects.transform;
					}
				}
			}
			else if (stage == 2)
			{
				if (counter > spd)
				{
					counter = 0;
					stage = 3;
					if (textObj)
					{
					  //  xa.glx = transform.position;
					 //   xa.glx.z = xa.explo1Layer;
					 //   xa.glx.y += 1;
					 //   xa.tempobj = (GameObject)(Instantiate(textObj, xa.glx, xa.null_quat));
					 //   textMesh = xa.tempobj.GetComponent<TextMesh>();
					 //   textMesh.text = "Alright...";
					 //   xa.tempobj.transform.parent = xa.createdObjects.transform;
					}
				}
			}
			else if (stage == 3)
			{
				if (counter > spd)
				{
					counter = 0;
					stage = 4;
					if (textObj)
					{
					  //  xa.glx = transform.position;
					  //  xa.glx.z = xa.explo1Layer;
					 //   xa.glx.y += 1;
					 //   xa.tempobj = (GameObject)(Instantiate(textObj, xa.glx, xa.null_quat));
					  //  textMesh = xa.tempobj.GetComponent<TextMesh>();
					  //  textMesh.text = "Let's dance!";
					 //   xa.tempobj.transform.parent = xa.createdObjects.transform;
						wizardScript.enabled = false;

						counter = 0;
						x1 = 0;
						setPuppetsTexture(4, 3);
						z1 = 4;

						//setPuppetsTexture(1, 3);
					}
				}
			}

			if (stage == 4)
			{
				counter += 10 * fa.deltaTime;
				if (counter >= 3)
				{
					counter = 0;
					z1++;
					if (z1 > 5)
					{
						z1 = 4;
						x1++;
					}

					setPuppetsTexture(z1, 3);
				}

				if (x1 > 5)
				{

					counter = 0;
					setPuppetsTexture(1, 2);
					z1 = 1;
					stage = 5;
					xa.glx = transform.position;
					xa.glx.z = xa.GetLayer(xa.layers.Explo1);
					xa.glx.x -= 12;
					xa.glx.y -= 1;
					xa.tempobj = (GameObject)(Instantiate(portal, xa.glx, xa.null_quat));
					xa.tempobj.transform.parent = xa.createdObjects.transform;
					iTweenEvent.GetEvent(this.gameObject, "flyUp").Play();


				}
			}

			if (stage == 5)
			{


				counter += 10 * fa.deltaTime;
				if (counter >= 2)
				{
					counter = 0;
					z1++;
					if (z1 > 3)
					{
						z1 = 1;
						x1++;
					}

					setPuppetsTexture(z1, 2);
				}
			}

		}

	}

	void setPuppetsTexture(int v1, int v2)
	{
		float x1 = 0;
		float y1 = 0;
		float x2 = 0;
		float y2 = 0;

		x1 = 0.125f;
		y1 = 0.125f;
		x2 = 0.125f * v1;
		y2 = 1 - ((0.125f * v2) + 0.125f);

		puppet.GetComponent<Renderer>().material.mainTextureScale = new Vector2(x1, y1);
		puppet.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(x2, y2);
	}
}
