using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class EntityDrawerItem : MonoBehaviour
{
	private EditorController controller      = null;
	private Button button                    = null;
	private Image image                      = null;
	private FrEdLibrary.LibraryItem item     = null;
	private static System.Action DeselectAll = null;

	private void Awake()
	{
		this.button = this.gameObject.GetComponent<Button>();
		this.image  = this.gameObject.transform.GetChild(0).GetComponent<Image>();
	}

	public void Initialize(EditorController controller, FrEdLibrary.LibraryItem libraryItem)
	{
		this.controller   = controller;
		this.item         = libraryItem;
		this.image.sprite = this.item.sprite;
		DeselectAll      += this.Deselect;
	}

	private void OnDestroy()
	{
		DeselectAll -= this.Deselect;
	}

	public void Deselect()
	{
		//this.button.animator.SetTrigger("Normal");
	}

	public void HandleSelect()
	{
		DeselectAll();
		//this.button.animator.SetTrigger("Pressed");
		this.controller.SelectedEntity = this.item;
	}
}
