using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class AllResouceTypes
{
    [SerializeField]
    public List<ResourceData> allResourceDatas;
    
    public enum ResourceType
    {
        Wood,
        Stone,
        Water,
        Food
    }
}