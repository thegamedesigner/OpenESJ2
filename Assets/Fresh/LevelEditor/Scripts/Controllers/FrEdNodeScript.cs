using System.Collections.Generic;
using System.Text.RegularExpressions;
using B83.Win32;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FrEdNodeScript : MonoBehaviour
{
	public const string EDITOR_OVERLAY_SCENE = "FrEd_Layout";

	public enum State {
		Waiting,
		LoadingLevel,
		WaitForLevelRawToLoad,
		CreatingLevel,
		HandlingLateCreations,
		Done
	}

	[SerializeField] private Transform blockParent   = null;
	[SerializeField] private Transform entityParent  = null;
	[SerializeField] private SkyController sky       = null; // TODO: make private and replace SetSkyScript
	public static string buttFileToOpen              = "CurrentLevel";
	public static FrEdNodeScript instance            = null;
	public static bool forceLoad                     = false;
	public static bool isPGMode                      = false;
	private static List<ButtEntity> levelToCreate    = null;
	private static List<ButtEntity> customPrefabs    = null;
	private static List<ButtEntity> customBehaviours = null;
	private List<ButtEntity> createLate              = null;
	private State state                              = State.Waiting;
	private static string lastButt                   = string.Empty;
	private string levelRawString                    = string.Empty;
	private UnityDragAndDropHook hook                = null; // important to keep the instance alive while the hook is active.

	private void Awake()
	{
		if (!xa.beenToLevel0) {
			//Don't create the level
			return;
		}
		instance = this;

		// must be created on the main thread to get the right thread id.
		if (this.hook == null) {
			this.hook = new UnityDragAndDropHook();
			this.hook.InstallHook();
			this.hook.OnDroppedFiles += this.OnFileDrop;
		}
	}

	private void OnDestroy()
	{
		if (this.hook != null) {
			this.hook.OnDroppedFiles -= this.OnFileDrop;
			this.hook.UninstallHook();
			this.hook = null;
		}
	}

	private void OnFileDrop(List<string> fileList, POINT mousePosition)
	{
		// ignore all the files except the first. Make sure it's a .butt file.
		if (fileList[0].ToLower().EndsWith(".butt") || fileList[0].ToLower().EndsWith(".png") || fileList[0].ToLower().EndsWith(".txt")) {
			buttFileToOpen = fileList[0];
			forceLoad = true;
			Setup.callFadeOutFunc("FrEd_Play", true, SceneManager.GetActiveScene().name);
		}
	}

	public List<ButtEntity> GetButtsInArea(Vector3 start, Vector3 end)
	{
		List<ButtEntity> butts = new List<ButtEntity>();
		Rect box = Utils.RectFromPoints(start, end);
		for (int i = 0; i < levelToCreate.Count; ++i) {
			if (box.Contains(levelToCreate[i].pos)) {
				butts.Add(levelToCreate[i]);
			}
		}
		return butts;
	}

	public bool EntityExistsAtLocation(Vector2 location)
	{
		// this sucks. maybe have a shadow dictionary
		for (int i = 0; i < levelToCreate.Count; ++i) {
			if (levelToCreate[i].pos == location) {
				return true;
			}
		}
		return false;
	}

	public bool AddEntity(ButtEntity ent)
	{
		if (!this.EntityExistsAtLocation(ent.pos)) {
			levelToCreate.Add(ent);
			return true;
		}
		return false;
	}

	public void DeleteEntities(List<ButtEntity> ents)
	{
		for (int i = 0; i < ents.Count; ++i) {
			this.DeleteEntity(ents[i]);
		}
	}

	public void DeleteEntity(ButtEntity ent)
	{
		levelToCreate.Remove(ent);
		GameObject.Destroy(ent.sceneReference);
	}

	public void CreateLevel(List<ButtEntity> items)
	{
		this.createLate = new List<ButtEntity>();
		for (int i = 0; i < items.Count; i++) {

			if (items[i].createLate <= 0.0f) {
				this.CreateItem(items[i]);
			} else {
				this.createLate.Add(items[i]);
			}
		}
	}

	public GameObject CreatePrefab(int id)//creates a gameobject & returns it
	{
		for (int i = 0; i < customPrefabs.Count; i++) {
			if (customPrefabs[i].prefabId == id) {
				return this.CreateItem(customPrefabs[i]);
			}
		}
		return null;
	}

	
	public ButtEntity GetBehaviourItem(int id)
	{
		for (int i = 0; i < customBehaviours.Count; i++) {
			if (customBehaviours[i].behaviourId == id) { return customBehaviours[i]; }
		}
		Debug.Log("BehaviourItem not found: " + id);
		return null;
	}

	private Transform GetParent(ButtEntity item)
	{
		Transform parent = null;
		switch (item.type) {
			case FrEdLibrary.Type.Block:
				parent = this.blockParent;
				break;
			default:
				parent = this.entityParent;
				break;
		}
		return parent;
	}

	public GameObject CreateItem(ButtEntity item)
	{
		GameObject go = null;
		FrEdLibrary.uIds++;
		item.uId = FrEdLibrary.uIds;

		if (item.type == FrEdLibrary.Type.SetSky) {
			this.sky.ConfigureSky(item.color1, item.color2, item.skyBands);
			return null;
		}

		GameObject prefab = FrEdLibrary.instance.GetPrefab(item.type);
		if (prefab != null) {
			go = Instantiate<GameObject>(prefab, new Vector3(item.pos.x, item.pos.y, item.zPos), prefab.transform.rotation, this.GetParent(item));
			item.ApplyToGameObject(go);
			item.sceneReference = go;
		}
		return go;
	}

	void HandleCreateLate()
	{
		if (createLate == null || createLate.Count <= 0) {
			return;
		}

		for (int i = 0; i < createLate.Count; i++) {
			if (Time.timeSinceLevelLoad > createLate[i].createLate) {
				CreateItem(createLate[i]);
				createLate.RemoveAt(i);
			}
		}
	}

	void StartLoadingLevel()
	{
		string levelFile = buttFileToOpen;
		if (!System.IO.Path.HasExtension(levelFile)) {
			levelFile = levelFile + ".butt";
		}
		if (levelFile != lastButt || forceLoad) {
			levelRawString = null;
			MPFile file = new MPFile(new UnityDiskPlatform(), MPFile.DataPath.StreamingAssets);
			if (!file.Exists(levelFile)) {
				return;
			}
			byte[] data = file.ReadAllBytes(levelFile);
			if (System.IO.Path.GetExtension(levelFile) == ".png") {
				// do a dumb search for the PNG EOF token
				byte[] token = new byte[] { 0x49, 0x45, 0x4E, 0x44, 0xAE, 0x42, 0x60, 0x82 };
				int buttIndex = 0;
				for (int i = 0; i < data.Length; ++i) {
					bool tokenFound = true;
					for (int j = 0; j < token.Length; ++j, ++i) {
						if (data[i] != token[j]) {
							tokenFound = false;
							break;
						}
					}

					if (tokenFound) {
						buttIndex = i;
						break;
					}
				}
				// found EOF, now ignore the png, we only want the .butt data.
				data = data.ShallowCopyRange<byte>(buttIndex);
				if (data.Length == 0) {
					// There's no .butt!? who would do such a thing!?
					forceLoad = false;
					return;
				}
			}

			levelRawString = System.Text.Encoding.UTF8.GetString(data);
			Regex r = new Regex(@"\s+|\r|\n|\r\n", RegexOptions.Multiline | RegexOptions.CultureInvariant);
			levelRawString = r.Replace(levelRawString, string.Empty);
			lastButt = levelFile;
			forceLoad = false;
			levelToCreate = this.RawToItemList(levelRawString);
			levelRawString = null;
		}
	}

	List<ButtEntity> RawToItemList(string raw)
	{
		List<ButtEntity> itemList = new List<ButtEntity>();
		customPrefabs             = new List<ButtEntity>();
		customBehaviours          = new List<ButtEntity>();

		string[] wholeItems = raw.Split(new char[] { ';' }, System.StringSplitOptions.RemoveEmptyEntries);

		for (int i = 0; i < wholeItems.Length; i++) {
			ButtEntity item = new ButtEntity();
			bool thisItemIsAPrefab = false;
			bool thisItemIsABehaviour = false;

			if (wholeItems[i].Length > 0) {
				string[] stats = wholeItems[i].Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
				for (int a = 0; a < stats.Length; a++) {
					if (stats[a].Length > 0) {
						string[] parts = stats[a].Split(new char[] { ':' }, System.StringSplitOptions.RemoveEmptyEntries);

						int typeResult = 0;
						if (parts.Length == 2) {
							typeResult = item.SetProperty(parts[0], parts[1]);
						}
						switch (typeResult) {
							default:
							case 0:
								break;
							case 1:
								thisItemIsAPrefab = true;
								break;
							case 2:
								thisItemIsABehaviour = true;
								break;
						}
					}
				}
			}
			item.Initialize();

			if (thisItemIsAPrefab)
			{
				customPrefabs.Add(item);
			}
			else if (thisItemIsABehaviour)
			{
				customBehaviours.Add(item);
			}
			else
			{
				itemList.Add(item);
			}
		}
		return itemList;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.F12)) {
			Scene scene = SceneManager.GetSceneAt(0);
			bool overlayLoaded = false;

			for (int i = 0; i < SceneManager.sceneCount; ++i) {
				scene = SceneManager.GetSceneAt(i);
				if (scene.isLoaded && scene.name == EDITOR_OVERLAY_SCENE) {
					overlayLoaded = true;
					break;
				}
			}

			if (overlayLoaded) {
				SceneManager.UnloadScene(scene);
			} else {
				SceneManager.LoadScene(EDITOR_OVERLAY_SCENE, UnityEngine.SceneManagement.LoadSceneMode.Additive);
			}
		} else if (Input.GetKeyDown(KeyCode.F6)) {
			string leveldata = string.Empty;
			if (customPrefabs != null && customPrefabs.Count > 0) {
				leveldata += Utils.SerializeButtEntities(customPrefabs);
			}
			if (levelToCreate != null && levelToCreate.Count > 0) {
				leveldata += Utils.SerializeButtEntities(levelToCreate);
			}
			if (customBehaviours != null && customBehaviours.Count > 0) {
				leveldata += Utils.SerializeButtEntities(customBehaviours);
			}
			MPFile file = new MPFile(new UnityDiskPlatform(), MPFile.DataPath.StreamingAssets);
			string buttFile = buttFileToOpen;
			if (!buttFile.EndsWith(".butt")) {
				buttFile += ".butt";
			}
			file.WriteAllBytes(buttFile, System.Text.Encoding.ASCII.GetBytes(leveldata));
		} else if (Input.GetKeyDown(KeyCode.F7)) {
			forceLoad = true;
			this.state = State.Waiting;
			xa.re.cleanLoadLevel(Restart.RestartFrom.RESTART_FROM_START, UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
		}

		switch (state)
		{
			case State.Done:
				break;
			case State.Waiting:
				if (FrEdLibrary.instance != null && instance != null)
				{
					state = State.LoadingLevel;
				}
				break;
			case State.LoadingLevel:
				StartLoadingLevel();
				state = State.WaitForLevelRawToLoad;
				break;
			case State.WaitForLevelRawToLoad:
				if (levelToCreate != null && levelToCreate.Count > 0)
				{
					state = State.CreatingLevel;
				}
				break;
			case State.CreatingLevel:
				//Create the level, if the levelToCreate != null
				if (levelToCreate != null)
				{
					state = State.HandlingLateCreations;
					CreateLevel(levelToCreate);
				}
				break;
			case State.HandlingLateCreations:
				//Switch to loading the test level
				HandleCreateLate();
				break;
		}
	}
}
