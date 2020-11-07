using System.Collections.Generic;
using UnityEngine;

public class EditorController : MonoBehaviour
{
	private const int LEFT_CLICK                  = 0;
	private const int RIGHT_CLICK                 = 1;
	private const int MIDDLE_CLICK                = 2;
	private const float MOUSE_DOUBLE_CLICK_TIME   = 0.5f;
	private const float MOUSE_WORLD_DRAG_DISTANCE = 2.5f;

	private const KeyCode PICK   = KeyCode.Q;
	private const KeyCode MOVE   = KeyCode.W;
	private const KeyCode ROTATE = KeyCode.E;
	private const KeyCode SCALE  = KeyCode.R;
	private const KeyCode PAN    = KeyCode.Space;
	private const KeyCode PAINT  = KeyCode.B;
	private const KeyCode ENTITY = KeyCode.N;
	private const KeyCode DELETE = KeyCode.Delete;

	public static readonly Vector3 SELECTION_POS_OFFSET = new Vector3(-0.15f, 0.15f, 0.0f);
	public static readonly Vector3 SELECTION_ROT_OFFSET = new Vector3(0.0f, 0.0f, 5.0f);

	public enum EditorState
	{
		Disabled = 0,
		Enabled,
		Testing
	}

	public enum ToolMode
	{
		Pick = 0,
		Move,
		Rotate,
		Scale,
		Pan,
		Paint,
		Entity,
		Delete
	}

	[System.Flags]
	private enum Modifiers
	{
		None  = 0,
		Shift = 1 << 0,
		Ctrl  = 1 << 1,
		Alt   = 1 << 2
	}

	private List<ButtEntity> selectedItems         = new List<ButtEntity>();
	[SerializeField] private EditorView editorView = null;
	private FrEdNodeScript node                    = null;
	private CameraScript cameraScript              = null;
	private Camera mainCamera                      = null;

	private Vector3 initialMouseDownPos            = Vector3.negativeInfinity;
	private Vector3 initialMouseDownWorldPos       = Vector3.negativeInfinity;
	private Vector3 lastFrameMousePos              = Vector3.zero;
	private Vector3 currentMouseWorldPos           = Vector3.negativeInfinity;
	private Vector3 currentMouseGridPos            = Vector3.negativeInfinity;
	private Vector3 totalDragDistance              = Vector3.zero;
	private float initialMouseDownTime             = -1.0f;
	private float doubleClickDownTime1             = -1.0f;
	private bool doubleClickedThisFrame            = false;
	private bool dragging                          = false;

	private static EditorState state               = EditorState.Disabled;
	private ToolMode tool                          = ToolMode.Pick;
	private GameObject selectionMarquee            = null;
	private bool toolChanged                       = false;

	public FrEdLibrary.LibraryItem SelectedEntity
	{
		get; set;
	}

	public bool CursorIsOverUIElement()
	{
		return this.editorView.CursorIsOverElement();
	}

	private GameObject SelectionMarquee
	{
		get
		{
			if (this.selectionMarquee == null) {
				this.selectionMarquee = GameObject.CreatePrimitive(PrimitiveType.Quad);
				Renderer r = this.selectionMarquee.GetComponent<Renderer>();
				r.material.mainTexture = Texture2D.whiteTexture;
				r.material.shader = Shader.Find("Custom/Texture_Alpha");
				r.material.color = new Color32(255, 255, 255, 150);
			}
			return this.selectionMarquee;
		}
	}

	private void Awake()
	{
		this.node = GameObject.Find("FrEdNode").GetComponent<FrEdNodeScript>();
		Debug.Assert(this.node != null);
		Debug.Assert(this.editorView != null);
	}

	// used to disable entities (like frogs shooting stuff)
	public static bool IsEditorActive()
	{
		return state == EditorState.Enabled;
	}

	public static void SetEditorState(EditorState newState)
	{
		state                              = newState;
		bool editorEnabled                 = newState == EditorState.Enabled;
		Cursor.visible                     = editorEnabled;
		xa.allowPlayerInput                = !editorEnabled;
		xa.freezePlayer                    = editorEnabled;
		xa.cheat_invinciblePlayer          = editorEnabled;
		za.dontKillPlayerForBeingOffscreen = editorEnabled;
	}

	public ToolMode CurrentTool
	{
		set {
			this.tool = value;
			this.toolChanged = true;
		}
		get
		{
			return this.tool;
		}
	}

	private Vector3 WorldToGridPos(Vector3 worldPos)
	{
		return new Vector3(Mathf.RoundToInt(worldPos.x),
		                   Mathf.RoundToInt(worldPos.y),
		                   Mathf.RoundToInt(worldPos.z));
	}

	private void LateUpdate()
	{
		if (!IsEditorActive()) return;

		Vector2 mouseScroll       = Input.mouseScrollDelta;
		Vector3 currentMousePos   = Input.mousePosition; // Pixel coordinate from bottom left
		Vector3 mouseDiff         = currentMousePos - this.lastFrameMousePos;
		this.currentMouseWorldPos = this.mainCamera.ScreenToWorldPoint(currentMousePos);
		this.currentMouseGridPos  = this.WorldToGridPos(this.currentMouseWorldPos);
		bool clearDrag            = false;

		// Set dragging state
		if (this.dragging || !this.editorView.CursorIsOverElement()) {
			if (Input.GetMouseButtonDown(LEFT_CLICK)) {
				this.initialMouseDownTime     = Time.timeSinceLevelLoad;
				this.initialMouseDownPos      = currentMousePos;
				this.initialMouseDownWorldPos = this.currentMouseWorldPos;
			} else if (Input.GetMouseButtonUp(LEFT_CLICK) || Input.GetMouseButtonDown(RIGHT_CLICK)) {
				clearDrag = true;
			} else if (!this.dragging &&
			           Input.GetMouseButton(LEFT_CLICK) &&
			           currentMousePos != this.initialMouseDownPos)
			{
				this.totalDragDistance = currentMousePos - this.initialMouseDownPos;
				if (this.totalDragDistance.x > MOUSE_WORLD_DRAG_DISTANCE ||
					this.totalDragDistance.y > MOUSE_WORLD_DRAG_DISTANCE ||
					this.totalDragDistance.x < -MOUSE_WORLD_DRAG_DISTANCE ||
					this.totalDragDistance.y < -MOUSE_WORLD_DRAG_DISTANCE)
				{
					this.dragging = true;
				}
			}
		}

		// Set double click state for this frame
		if (Input.GetMouseButtonDown(LEFT_CLICK)) {
			if (this.doubleClickDownTime1 < 0.0f) {
				this.doubleClickDownTime1 = Time.timeSinceLevelLoad;
			} else {
				if (!this.dragging) {
					float doubleClickDownTime2 = Time.timeSinceLevelLoad;
					if ((doubleClickDownTime2 - this.doubleClickDownTime1) < MOUSE_DOUBLE_CLICK_TIME) {
						this.doubleClickedThisFrame = true;
					}
				}
				this.doubleClickDownTime1 = -1.0f;
			}
		}

		Modifiers keyModifiers  = Modifiers.None;
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
			keyModifiers |= Modifiers.Shift;
		}
		if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) {
			keyModifiers |= Modifiers.Ctrl;
		}
		if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt) || Input.GetKey(KeyCode.AltGr)) {
			keyModifiers |= Modifiers.Alt;
		}

		// Set tool
		if (!Input.GetMouseButton(LEFT_CLICK)) {
			if (Input.GetKeyDown(PICK)) {
				this.CurrentTool = ToolMode.Pick;
			} else if (Input.GetKeyDown(MOVE)) {
				this.CurrentTool = ToolMode.Move;
			} else if (Input.GetKeyDown(ROTATE)) {
				this.CurrentTool = ToolMode.Rotate;
			} else if (Input.GetKeyDown(SCALE)) {
				this.CurrentTool = ToolMode.Scale;
			} else if (Input.GetKeyDown(PAINT)) {
				this.CurrentTool = ToolMode.Paint;
			} else if (Input.GetKeyDown(ENTITY)) {
				this.CurrentTool = ToolMode.Entity;
			} else if (Input.GetKeyDown(DELETE)) {
				this.node.DeleteEntities(this.selectedItems);
				this.selectedItems.Clear();
			}
		}

		// Pan
		if (this.cameraScript != null) {
			this.cameraScript.UpdateEditorCamera(this.GetPanDistance(mouseDiff, mouseScroll));
		}

		if (this.toolChanged) {
			this.editorView.UpdateToolText(this.tool.ToString());
		} else {
			switch (this.tool) {
			case ToolMode.Pick:
				this.HandlePick(keyModifiers);
				break;
			case ToolMode.Move:
				this.HandleMove(keyModifiers);
				break;
			case ToolMode.Rotate:
				this.HandleRotate(keyModifiers);
				break;
			case ToolMode.Scale:
				this.HandleScale(keyModifiers);
				break;
			case ToolMode.Paint:
				this.HandlePaint(keyModifiers);
				break;
			case ToolMode.Entity:
				this.HandleEntity(keyModifiers);
				break;
			}
		}

		if (clearDrag) {
			this.initialMouseDownTime = -1.0f;
			this.initialMouseDownPos = Vector3.negativeInfinity;
			this.initialMouseDownWorldPos = Vector3.negativeInfinity;
			this.dragging = false;
		}

		this.editorView.UpdateCursorPos(string.Format("{0}, {1}", this.currentMouseGridPos.x, this.currentMouseGridPos.y));
		this.lastFrameMousePos = currentMousePos;
		this.toolChanged = false;
		this.doubleClickedThisFrame = false;
	}

	private void ResizeMarquee(Modifiers keys)
	{
		if (this.SelectionMarquee.activeSelf) {
			Vector3 pos = new Vector3(this.initialMouseDownWorldPos.x + this.currentMouseWorldPos.x, this.initialMouseDownWorldPos.y + this.currentMouseWorldPos.y) / 2.0f;
			pos.z = xa.GetLayer(xa.layers.exploOvertop1);
			this.SelectionMarquee.transform.position = pos;
			this.SelectionMarquee.transform.localScale = new Vector3(Mathf.Abs(this.initialMouseDownWorldPos.x - this.currentMouseWorldPos.x),
			                                                         Mathf.Abs(this.initialMouseDownWorldPos.y - this.currentMouseWorldPos.y), 1.0f);
		}
	}

	private void HandlePick(Modifiers keyModifiers)
	{
		bool add = keyModifiers.HasFlag(Modifiers.Shift);
		bool remove = keyModifiers.HasFlag(Modifiers.Alt);
		
		this.SelectionMarquee.SetActive(this.dragging);

		if (this.dragging && Input.GetMouseButton(LEFT_CLICK)) {
			this.ResizeMarquee(keyModifiers);
		}

		if (Input.GetMouseButtonUp(LEFT_CLICK)) {
			this.SelectionMarquee.SetActive(false);
			if (float.IsNegativeInfinity(this.initialMouseDownWorldPos.x) &&
			    float.IsNegativeInfinity(this.initialMouseDownWorldPos.y))
			{
				return;
			}

			Vector3 gridDownPos = this.WorldToGridPos(this.initialMouseDownWorldPos);
			if (remove) {
				// remove item(s) at grid location
				if (gridDownPos == this.currentMouseGridPos) {
					// we clicked and released the same grid location.
					// do this based on grid pos
					this.RemoveFromSelection(gridDownPos, gridDownPos);
				} else {
					// we selected a group of items to remove from selection.
					// do this based on real pos
					this.RemoveFromSelection(this.initialMouseDownWorldPos, this.currentMouseWorldPos);
				}
			} else {
				List<ButtEntity> newItems = new List<ButtEntity>();
				if (!add) {
					List<ButtEntity> list = new List<ButtEntity>(this.selectedItems);
					this.RemoveSelectionOffset(list);
					this.selectedItems.Clear();
				}

				if (gridDownPos == this.currentMouseGridPos) {
					// we clicked and released the same grid location.
					newItems = this.AddToSelection(gridDownPos, gridDownPos);
				} else {
					// we selected a group of items to remove from selection.
					// do this based on real pos
					newItems = this.AddToSelection(this.initialMouseDownWorldPos, this.currentMouseWorldPos);
				}
				this.AddSelectionOffset(newItems);
			}
			//this.initialMouseDownWorldPos = Vector3.negativeInfinity;
		}
	}

	private void HandleMove(Modifiers keyModifiers)
	{
		Vector3 gridDownPos = this.WorldToGridPos(this.initialMouseDownWorldPos);
		Vector3 moveOffset = gridDownPos - this.currentMouseGridPos;

		if (Input.GetMouseButtonUp(LEFT_CLICK)) {
			if (gridDownPos == this.currentMouseGridPos) {
				List<ButtEntity> underCursor = this.AddToSelection(gridDownPos, gridDownPos);
				if (underCursor.Count > 0) {
					this.RemoveSelectionOffset(this.selectedItems);
					this.AddSelectionOffset(underCursor);
				}
			}
		}

		if (Input.GetMouseButtonDown(RIGHT_CLICK)) {
			for (int i = 0; i < this.selectedItems.Count; ++i) {
				if (this.selectedItems[i].sceneReference &&
				    !float.IsNegativeInfinity(this.initialMouseDownWorldPos.x) &&
				    !float.IsNegativeInfinity(this.initialMouseDownWorldPos.y))
				{
					Transform t = this.selectedItems[i].sceneReference.transform;
					t.position += moveOffset;
				}
			}
		}

		if (!float.IsNegativeInfinity(this.initialMouseDownWorldPos.x) &&
			!float.IsNegativeInfinity(this.initialMouseDownWorldPos.y))
		{
			for (int i = 0; i < this.selectedItems.Count; ++i) {
				if (this.selectedItems[i].sceneReference) {
					Transform t = this.selectedItems[i].sceneReference.transform;
					t.position -= this.WorldToGridPos(this.mainCamera.ScreenToWorldPoint(this.lastFrameMousePos)) - this.currentMouseGridPos;
					this.selectedItems[i].pos = t.position - SELECTION_POS_OFFSET;
				}
			}
		}
	}

	private void HandleRotate(Modifiers keyModifiers)
	{
		float deltaTheta = 0.0f;
		float targetRotation = 0.0f;
		if (this.dragging && this.selectedItems.Count > 0) {
			// Normalize mouse to position of first selected entity.
			Transform t          = this.selectedItems[0].sceneReference.transform;
			Vector3 firstMouse   = this.initialMouseDownWorldPos - t.position;
			Vector3 lastMouse    = this.mainCamera.ScreenToWorldPoint(this.lastFrameMousePos) - t.position;
			Vector3 currentMouse = this.currentMouseWorldPos - t.position;

			float currentTheta = Mathf.Rad2Deg * Mathf.Atan2(currentMouse.y, currentMouse.x);
			if (!keyModifiers.HasFlag(Modifiers.Shift)) {
				// Free-rotate
				float lastTheta = Mathf.Rad2Deg * Mathf.Atan2(lastMouse.y, lastMouse.x);
				deltaTheta      = currentTheta - lastTheta;
			} else {
				// Snap to 45 degrees
				float firstTheta     = Mathf.Rad2Deg * Mathf.Atan2(firstMouse.y, firstMouse.x);
				deltaTheta           = currentTheta - firstTheta;

				float angle          = 45.0f;
				targetRotation = Mathf.Floor(((t.rotation.z + deltaTheta) % 360 + angle / 2.0f) / angle) * angle;
			}

			// Update visuals
			for (int i = 0; i < this.selectedItems.Count; ++i) {
				if (this.selectedItems[i].sceneReference) {
					t = this.selectedItems[i].sceneReference.transform;
					if (!keyModifiers.HasFlag(Modifiers.Shift)) {
						t.Rotate(0.0f, 0.0f, deltaTheta);
					} else {
						Quaternion rot  = t.rotation;
						Vector3 angles  = rot.eulerAngles;
						angles.z        = targetRotation;
						rot.eulerAngles = angles;
						t.rotation      = rot;
					}

					if (Input.GetMouseButtonUp(LEFT_CLICK)) {
						// write data
						this.selectedItems[i].tilt = t.rotation.eulerAngles.z;
					}
				}
			}

		}
	}

	private void HandleScale(Modifiers keyModifiers)
	{
		Vector3 gridDownPos = this.WorldToGridPos(this.initialMouseDownPos);
		Vector3 scaleOffset = gridDownPos - this.currentMouseGridPos;

		if (Input.GetMouseButtonDown(RIGHT_CLICK)) {
			for (int i = 0; i < this.selectedItems.Count; ++i) {
				if (this.selectedItems[i].sceneReference &&
				    !float.IsNegativeInfinity(this.initialMouseDownWorldPos.x) &&
				    !float.IsNegativeInfinity(this.initialMouseDownWorldPos.y))
				{
					Transform t = this.selectedItems[i].sceneReference.transform;
					t.localScale = this.selectedItems[i].scale;
					Vector3 pos = this.selectedItems[i].pos;
					pos.z = t.position.z;
					t.position = pos;
				}
			}
		}

		if (!float.IsNegativeInfinity(this.initialMouseDownWorldPos.x) &&
			!float.IsNegativeInfinity(this.initialMouseDownWorldPos.y))
		{
			bool growFromCentre = keyModifiers.HasFlag(Modifiers.Alt);
			float multiplier = 1.0f;
			if (growFromCentre) {
				multiplier = 2.0f;
			}

			for (int i = 0; i < this.selectedItems.Count; ++i) {
				if (this.selectedItems[i].sceneReference) {
					Transform t = this.selectedItems[i].sceneReference.transform;

					Vector3 scaleDiff = (this.WorldToGridPos(this.mainCamera.ScreenToWorldPoint(this.lastFrameMousePos)) - this.currentMouseGridPos) * multiplier;
					Vector3 scale = t.localScale - scaleDiff;
					scale.x = Mathf.Max(1.0f, scale.x);
					scale.y = Mathf.Max(1.0f, scale.y);
					scale.z = Mathf.Max(1.0f, scale.z);
					if (!growFromCentre && t.localScale != scale) {
						Vector3 posDiff = scaleDiff / 2.0f;
						Vector3 pos = t.position - posDiff;
						pos.x = Mathf.Max(pos.x, this.selectedItems[i].pos.x);
						pos.y = Mathf.Max(pos.y, this.selectedItems[i].pos.y);
						t.position = pos;
					}
					t.localScale = scale;

					if (Input.GetMouseButtonUp(LEFT_CLICK)) {
						this.selectedItems[i].pos = t.position;
						this.selectedItems[i].scale = t.localScale;
					}
				}
			}
		}
	}

	private void HandlePaint(Modifiers keyModifiers)
	{
		if (!this.CursorIsOverUIElement() && Input.GetMouseButton(LEFT_CLICK) && !this.node.EntityExistsAtLocation(this.currentMouseGridPos)) {
			ButtEntity block = new ButtEntity();
			block.type = FrEdLibrary.Type.Block;
			block.Initialize();
			block.pos = this.currentMouseGridPos;
			if (this.node.AddEntity(block)) {
				this.node.CreateItem(block);
			}
		}
	}

	private void HandleEntity(Modifiers keyModifiers)
	{
		if (!this.CursorIsOverUIElement() && Input.GetMouseButtonDown(LEFT_CLICK) && !this.node.EntityExistsAtLocation(this.currentMouseGridPos)) {
			FrEdLibrary.Type type = this.SelectedEntity.type;
			switch (type) {
			default:
				ButtEntity ent = new ButtEntity();
				ent.type = type;
				ent.Initialize();
				ent.pos = this.currentMouseGridPos;
				if (this.node.AddEntity(ent)) {
					this.node.CreateItem(ent);
				}
			break;
			case FrEdLibrary.Type.PlayerSpawn:
				
			break;
			}
		}
	}

	private void RemoveSelectionOffset(List<ButtEntity> list)
	{
		for (int i = 0; i < list.Count; ++i) {
			if (list[i].sceneReference != null) {
				Transform t = list[i].sceneReference.transform;
				Vector3 rot = t.localEulerAngles;
				rot.z -= 5.0f;
				t.localEulerAngles = rot;
				Vector3 pos = t.position;
				pos.x += 0.15f;
				pos.y -= 0.15f;
				t.position = pos;
			}
		}
	}

	private void AddSelectionOffset(List<ButtEntity> list)
	{
		for (int i = 0; i < list.Count; ++i) {
			if (list[i].sceneReference != null) {
				Transform t = list[i].sceneReference.transform;
				Vector3 rot = t.localEulerAngles + SELECTION_ROT_OFFSET;
				t.localEulerAngles = rot;
				Vector3 pos = t.position + SELECTION_POS_OFFSET;
				t.position = pos;
			}
		}
	}

	private List<ButtEntity> AddToSelection(Vector3 start, Vector3 end)
	{
		List<ButtEntity> list = this.node.GetButtsInArea(start, end);
		for (int i = 0; i < list.Count; ++i) {
			if (this.selectedItems.Contains(list[i])) {
				list.Remove(list[i--]);
			}
		}
		this.selectedItems.AddRange(list);
		return list;
	}

	private List<ButtEntity> RemoveFromSelection(Vector3 start, Vector3 end)
	{
		List<ButtEntity> list = new List<ButtEntity>();
		Rect box = Utils.RectFromPoints(start, end);
		for (int i = 0; i < this.selectedItems.Count; ++i) {
			ButtEntity ent = this.selectedItems[i];
			if (box.Contains(ent.pos))
			{
				list.Add(ent);
				this.selectedItems.RemoveAt(i--);
				if (ent.sceneReference != null) {
					Transform t = ent.sceneReference.transform;
					Vector3 rot = t.localEulerAngles - SELECTION_ROT_OFFSET;
					t.localEulerAngles = rot;
					Vector3 pos = t.position + SELECTION_POS_OFFSET;
					t.position = pos;
				}
			}
		}
		return list;
	}

	private Vector3 GetPanDistance(Vector3 mouseDiff, Vector2 mouseScroll)
	{
		Vector3 panAmount = Vector3.zero;
		if (Input.GetMouseButton(MIDDLE_CLICK) || (Input.GetKey(PAN) && Input.GetMouseButton(LEFT_CLICK))) {
			// Pan the camera
			// TODO: Somehow scale sensitivity with window size.
			float aspect = (float)Camera.main.scaledPixelHeight / (float)Camera.main.scaledPixelWidth;
			float cameraScale = Camera.main.orthographicSize / 100.0f;
			panAmount -= mouseDiff * aspect * cameraScale;
		}

		if (mouseScroll.y > float.Epsilon || mouseScroll.y < float.Epsilon) {
			if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
				panAmount.x += mouseScroll.y;
			} else {
				panAmount.y += mouseScroll.y;
			}
		}

		if (mouseScroll.x > float.Epsilon || mouseScroll.x < float.Epsilon) {
			panAmount.x += mouseScroll.x;
		}

		return panAmount;
	}

	private void OnEnable()
	{
		SetEditorState(EditorState.Enabled);
		this.mainCamera = Camera.main;
		this.cameraScript = this.mainCamera.transform.parent.gameObject.GetComponent<CameraScript>();
	}

	private void OnDisable()
	{
		this.RemoveSelectionOffset(this.selectedItems);
		this.selectedItems.Clear();
		SetEditorState(EditorState.Disabled);
	}
}
