using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Survivor : MonoBehaviour
{
    public int hp;
    public int water;
    public int food;
    public int sleep;
    public int sanity;
    public int temperature;

    public Inventory inventory;
    
    // Start is called before the first frame update
    void Start()
    {
        FindResource(AllResouceTypes.ResourceType.Wood);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FindResource(AllResouceTypes.ResourceType _resourceType)
    {
        ResourceObjects.Instance.FindResources(_resourceType);
    }

    /*private void FindResourcesByData(ResourceData _resourceData)
    {
        List<Resource> needResources = ResourceObjects.Instance.FindResources(_resourceData);
        Debug.Log(needResources);
    }*/
}
