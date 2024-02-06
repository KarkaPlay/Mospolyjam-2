using UnityEngine;

public class Drag : MonoBehaviour
{
    private bool dragging = false;
    private Vector3 offset;
    private Material originalMaterial;
    private Material highlightMaterial;

    private void Start()
    {
        originalMaterial = GetComponent<Renderer>().material;

        highlightMaterial = new Material(originalMaterial);
        highlightMaterial.color = Color.black;
    }

    void Update()
    {
        if (dragging)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }
    }

    private void OnMouseDown()
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragging = true;
    }

    private void OnMouseUp()
    {
        dragging = false;
    }

    private void OnMouseEnter()
    {
        GetComponent<Renderer>().material = highlightMaterial;
    }

    private void OnMouseExit()
    {
        GetComponent<Renderer>().material = originalMaterial;
    }
}