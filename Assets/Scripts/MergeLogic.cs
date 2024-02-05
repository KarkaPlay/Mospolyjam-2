using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MergeLogic : MonoBehaviour
{
    private ResourceData resourceData;

    private void Start()
    {
        resourceData = GetComponent<Resource>().resourceData;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Resource"))
        {
            ResourceData collidedResourceData = collision.gameObject.GetComponent<Resource>().resourceData;
            Debug.Log("Столкновение с " + collidedResourceData + " произошло");
            switch (resourceData.resourceType)
            {
                case AllResouceTypes.ResourceType.Вода:
                    switch (collidedResourceData.resourceType)
                    {
                        case AllResouceTypes.ResourceType.Вода:
                            Destroy(collision.gameObject);
                            Debug.Log("Соединились");
                            break;
                        case AllResouceTypes.ResourceType.Земля:
                            Destroy(collision.gameObject);
                            Debug.Log("Соединились");
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
