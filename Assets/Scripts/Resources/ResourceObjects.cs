using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceObjects : MonoBehaviour
{
    public static ResourceObjects Instance;

    [SerializeField] private List<Resource> allResourceObjects;
    public AllResouceTypes allResourceTypes;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SetAllObjects();
    }

    private void SetAllObjects()
    {
        allResourceObjects = GetComponentsInChildren<Resource>().ToList();
    }
    
    // TODO: Если нужных ресурсов не найдено, показывается иконка
    public List<Resource> FindResources(AllResouceTypes.ResourceType resourceType)
    {
        List<Resource> returnList = new List<Resource>();
        returnList = allResourceObjects.FindAll(resource => resource.resourceData.resourceType == resourceType);
        
        return returnList;
    }

    public void AddToList(Resource resourceToAdd)
    {
        Debug.Log($"Добавляем ресурс {resourceToAdd.resourceData.resourceName} в список");
        if (!allResourceObjects.Find(resource => resource.resourceData == resourceToAdd.resourceData))
        {
            Progress.Instance.UnlockAchievement(resourceToAdd.resourceData.resourceType.ToString());
        }
        
        allResourceObjects.Add(resourceToAdd);
    }

    public void RemoveFromList(Resource resourceToRemove)
    {
        if (allResourceObjects.Contains(resourceToRemove))
        {
            allResourceObjects.Remove(resourceToRemove);
        }
        else
        {
            Debug.LogAssertion("Попытка удалить ресурс, которого нет в списке");
        }
    }
}
