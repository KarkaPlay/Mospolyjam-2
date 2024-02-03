using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceObjects : MonoBehaviour
{
    public static ResourceObjects Instance;

    public List<Resource> allResourceObjects;
    public AllResouceTypes allResourceTypes;
    
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        SetAllObjects();
    }

    /// <summary>
    /// 
    /// </summary>
    private void SetAllObjects()
    {
        allResourceObjects = GetComponentsInChildren<Resource>().ToList();
    }

    public List<Resource> FindResources(ResourceData resourceData)
    {
        List<Resource> returnList = new List<Resource>();
        returnList = allResourceObjects.FindAll(resource => resource.resourceData == resourceData);
        
        Debug.Log("Все ресурсы: "+ returnList.ToString());
        return returnList;

        /*foreach (var resourceObject in allResourceObjects)
        {
            if (resourceObject.resourceData == resourceData)
            {
                returnList.Add(resourceObject);
            }
        }

        return returnList;*/
    }
    
    // TODO: Если нужных ресурсов не найдено, показывается иконка
    public List<Resource> FindResources(AllResouceTypes.ResourceType resourceType)
    {
        List<Resource> returnList = new List<Resource>();
        returnList = allResourceObjects.FindAll(resource => resource.resourceData.resourceType == resourceType);
        
        return returnList;
    }
}
