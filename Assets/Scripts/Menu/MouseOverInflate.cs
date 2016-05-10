using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MouseOverInflate : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public float incAmount = 1.5f;

    private Vector3 prevScale;
	bool mouseOver = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        prevScale = transform.localScale;
        Vector3 scale = transform.localScale;
        scale.x *= incAmount;
        scale.y *= incAmount;
        transform.localScale = scale;
		mouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = prevScale;
		mouseOver = false;
    }

	void OnDisable()
	{
		if(mouseOver)
			transform.localScale = prevScale;
		mouseOver = false;
	}
}
