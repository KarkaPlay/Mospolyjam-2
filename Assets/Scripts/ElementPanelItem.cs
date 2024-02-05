using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ElementPanelItem : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private GameObject itemBeingDragged;
    public Canvas canvas;
    public string itemElement;

    public void OnPointerDown(PointerEventData eventData)
    {
        itemBeingDragged = Instantiate(gameObject, transform.position, Quaternion.identity);
        itemBeingDragged.transform.SetParent(canvas.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        itemBeingDragged.transform.position = Input.mousePosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log(itemElement + ", position: " + itemBeingDragged.transform.position);
        Destroy(itemBeingDragged);
    }
}