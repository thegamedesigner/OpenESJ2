using UnityEngine;
using UnityEngine.EventSystems;

public class UIPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public bool IsOverElement {
		get; set;
	}

	public void OnPointerEnter(PointerEventData pointerEventData)
	{
		this.IsOverElement = true;
	}

	public void OnPointerExit(PointerEventData pointerEventData)
	{
		this.IsOverElement = false;
	}
}
