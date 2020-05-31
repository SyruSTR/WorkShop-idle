using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollRectDragFunction : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private AddItemsOnBoard script;
    private void Awake()
    {
        script = transform.GetChild(0).GetComponentInChildren<AddItemsOnBoard>();
    }
    private Vector2 lastPos;
    public void OnDrag(PointerEventData data)
    {
        Vector2 distance = lastPos - data.position;
        lastPos = data.position;
        if (Mathf.Abs(distance.y) > 10.0f)
            script.DragActive = true;
        Debug.Log("distance " + distance.y);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        script.DragActive = false;
    }
}
