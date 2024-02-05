using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllResourceDatas : MonoBehaviour
{
    [SerializeField]
    public List<ResourceData> allResourceDatas;

    public static AllResourceDatas Instance;

    private void Awake()
    {
        if (Instance == null) 
        { 
            Instance = this;
        } 
        else
        { 
            Destroy(gameObject); 
        } 
        
        DontDestroyOnLoad(gameObject);
    }

    public ResourceData Find(AllResouceTypes.ResourceType resourceTypeToFind)
    {
        return allResourceDatas.Find(data => data.resourceType == resourceTypeToFind);
    }
    
    public ResourceData Find(string resourceNameToFind)
    {
        return allResourceDatas.Find(resource => resource.resourceName == resourceNameToFind);
    }
}
