using System.Collections;
using System.Collections.Generic;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.EventSystems;

public class ElementPanelItem : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private GameObject itemBeingDragged;
    private GameObject itemDragged;
    public Canvas canvas;
    //public GameObject resourceObjects;
    public GameObject itemElement;

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
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        worldPosition += new Vector3(0, 0, 10);
        itemDragged = Instantiate(itemElement, worldPosition, Quaternion.identity);
        //itemDragged.transform.SetParent(resourceObjects.transform);
        Debug.Log(", position: " + itemBeingDragged.transform.position);
        Destroy(itemBeingDragged);
        
    }
}