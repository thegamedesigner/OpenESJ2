using Holoville.HOTween;
using UnityEngine;
using UnityEngine.UI;

public class EditorView : MonoBehaviour
{
	[SerializeField] private EditorController controller = null;
	[SerializeField] private EntityDrawerItem drawerItem = null;
	[SerializeField] private Transform drawerContainer   = null;
	[SerializeField] private UIPanel entityDrawer        = null;
	[SerializeField] private UIPanel toolsPanel          = null;
	[SerializeField] private Text toolText               = null;
	[SerializeField] private Text cursorText             = null;
	[SerializeField] private Button pickButton           = null;
	[SerializeField] private Button moveButton           = null;
	[SerializeField] private Button scaleButton          = null;
	[SerializeField] private Button rotateButton         = null;
	[SerializeField] private Button blockButton          = null;
	[SerializeField] private Button entityButton         = null;
	private Button currentlySelectedButton               = null;
	private bool entityDrawerIsAnimating                 = false;
	private TweenParms hideDrawer                        = null;
	private TweenParms showDrawer                        = null;

	private void Awake()
	{
		// setup entity drawer tween props
		this.currentlySelectedButton = this.pickButton;
		Vector3 hidePos = new Vector3(0.0f, -110.0f, 0.0f);
		this.hideDrawer = new TweenParms()
		                .Ease(EaseType.EaseInSine)
		                .OnComplete(
			                () => {
				                this.entityDrawerIsAnimating = false;
			                }
		                )
		                .Prop("localPosition", hidePos, true);

		Vector3 showPos = new Vector3(0.0f, 110.0f, 0.0f);
		this.showDrawer = new TweenParms()
		                .Ease(EaseType.EaseInSine)
		                .OnComplete(
			                () => {
				                this.entityDrawerIsAnimating = false;
			                }
		                )
		                .Prop("localPosition", showPos, true);


		// populate entity drawer
		for (int i = 0; i < (int)FrEdLibrary.Type.End; ++i) {
			FrEdLibrary.LibraryItem item = null;
			switch ((FrEdLibrary.Type)i) {
			default:
				item = FrEdLibrary.instance.GetLibraryItem((FrEdLibrary.Type)i);
			break;

			// Ignore markers, blocks, projectiles, invisible things
			case FrEdLibrary.Type.None:
			case FrEdLibrary.Type.End:
			case FrEdLibrary.Type.Block:
			case FrEdLibrary.Type.Animation:
			case FrEdLibrary.Type.Particles:
			case FrEdLibrary.Type.SetMusic:
			case FrEdLibrary.Type.SetSky:
			case FrEdLibrary.Type.NinjaStar:
			case FrEdLibrary.Type.RoundBullet:
			case FrEdLibrary.Type.HomingMissile:
			case FrEdLibrary.Type.SpikyHomingMissile:
			case FrEdLibrary.Type.DetailBlock:
			case FrEdLibrary.Type.Decoration:
			case FrEdLibrary.Type.PlayerSpawn:
			break;
			}

			if (item != null) {
				EntityDrawerItem drawerItem = GameObject.Instantiate<EntityDrawerItem>(this.drawerItem, this.drawerContainer);
				drawerItem.Initialize(this.controller, item);
			}
		}
	}

	public bool CursorIsOverElement()
	{
		return this.entityDrawer.IsOverElement || this.toolsPanel.IsOverElement;
	}

	public void UpdateToolText(string str)
	{
		this.toolText.text = str;
	}

	public void UpdateCursorPos(string str)
	{
		this.cursorText.text = str;
	}

	public void HandlePickButton()
	{
		if (!this.entityDrawerIsAnimating) {
			this.HideEntityDrawer();
			this.controller.CurrentTool = EditorController.ToolMode.Pick;
			this.currentlySelectedButton = this.pickButton;
			this.Update();
		} else {
			Debug.LogError("wat!?");
		}
	}

	public void HandleMoveButton()
	{
		if (!this.entityDrawerIsAnimating) {
			this.HideEntityDrawer();
			this.controller.CurrentTool = EditorController.ToolMode.Move;
			this.currentlySelectedButton = this.moveButton;
			this.Update();
		}
	}

	public void HandleScaleButton()
	{
		if (!this.entityDrawerIsAnimating) {
			this.HideEntityDrawer();
			this.controller.CurrentTool = EditorController.ToolMode.Scale;
			this.currentlySelectedButton = this.scaleButton;
			this.Update();
		}
	}

	public void HandleRotateButton()
	{
		if (!this.entityDrawerIsAnimating) {
			this.HideEntityDrawer();
			this.controller.CurrentTool = EditorController.ToolMode.Rotate;
			this.currentlySelectedButton = this.rotateButton;
			this.Update();
		}
	}

	public void HandleBlockButton()
	{
		if (!this.entityDrawerIsAnimating) {
			this.HideEntityDrawer();
			this.controller.CurrentTool = EditorController.ToolMode.Paint;
			this.currentlySelectedButton = this.blockButton;
			this.Update();
		}
	}

	public void HandleEntityButton()
	{
		if (!this.entityDrawerIsAnimating && this.controller.CurrentTool != EditorController.ToolMode.Entity) {
			this.ShowEntityDrawer();
			this.controller.CurrentTool = EditorController.ToolMode.Entity;
			this.currentlySelectedButton = this.entityButton;
			this.Update();
		}
	}

	public void ShowEntityDrawer()
	{
		if (!this.entityDrawerIsAnimating && this.controller.CurrentTool != EditorController.ToolMode.Entity) {
			this.entityDrawerIsAnimating = true;
			HOTween.To(this.entityDrawer.gameObject.transform, 0.2f, this.showDrawer);
		}
	}

	public void HideEntityDrawer()
	{
		if (!this.entityDrawerIsAnimating && this.controller.CurrentTool == EditorController.ToolMode.Entity) {
			this.entityDrawerIsAnimating = true;
			HOTween.To(this.entityDrawer.gameObject.transform, 0.2f, this.hideDrawer);
		}
	}

	public void Update()
	{
		if (this.pickButton != this.currentlySelectedButton) this.pickButton.animator.SetTrigger("Normal");
		if (this.moveButton != this.currentlySelectedButton) this.moveButton.animator.SetTrigger("Normal");
		if (this.scaleButton != this.currentlySelectedButton) this.scaleButton.animator.SetTrigger("Normal");
		if (this.rotateButton != this.currentlySelectedButton) this.rotateButton.animator.SetTrigger("Normal");
		if (this.blockButton != this.currentlySelectedButton) this.blockButton.animator.SetTrigger("Normal");
		if (this.entityButton != this.currentlySelectedButton) this.entityButton.animator.SetTrigger("Normal");
		this.currentlySelectedButton.animator.SetTrigger("Pressed");
	}
}
