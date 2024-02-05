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
            Debug.Log("������������ � " + collidedResourceData + " ���������");
            switch (resourceData.resourceType)
            {
                case AllResouceTypes.ResourceType.����:
                    switch (collidedResourceData.resourceType)
                    {
                        case AllResouceTypes.ResourceType.����:
                            Destroy(collision.gameObject);
                            Debug.Log("�����������");
                            break;
                        case AllResouceTypes.ResourceType.�����:
                            Destroy(collision.gameObject);
                            Debug.Log("�����������");
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
