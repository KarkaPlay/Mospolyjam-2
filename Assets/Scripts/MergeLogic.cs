using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MergeLogic : MonoBehaviour
{
    private ResourceData resourceData;
    public bool hasCollided;

    private void Start()
    {
        hasCollided = false;
        resourceData = GetComponent<Resource>().resourceData;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Resource") && !hasCollided)
        {
            collision.gameObject.GetComponent<MergeLogic>().hasCollided = true;
            ResourceData collidedResourceData = collision.gameObject.GetComponent<Resource>().resourceData;
            GameObject result = resourceData.recipes.Find(x => x.addedItem == collidedResourceData).result;
            Destroy(collision.gameObject);
            Destroy(gameObject);
            Instantiate(result, transform.position, Quaternion.identity);
        }
    }
}
