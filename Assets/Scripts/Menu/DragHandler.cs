using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public GameObject group;

	private Vector3 curMouse;
	private Vector3 preMouse;
	private RectTransform rt;

	public bool IsDragging()
	{
		return !(preMouse == curMouse);
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		rt = group.GetComponent<RectTransform>();
	}
	public void OnDrag(PointerEventData eventData)
	{
		preMouse = curMouse;
		curMouse = Input.mousePosition;
		
		if (preMouse != Vector3.zero)
		{
			float dif = curMouse.x - preMouse.x;
			if (rt.offsetMin.x + dif > 0.0f)
				dif = -rt.offsetMin.x;
			else if(rt.offsetMax.x + dif < 0.0f)
				dif = -rt.offsetMax.x;
			group.transform.Translate(new Vector3(dif, 0.0f, 0.0f));
		}
	}
	public void OnEndDrag(PointerEventData eventData)
	{
		preMouse = Vector3.zero;
		curMouse = Vector3.zero;
	}
}
