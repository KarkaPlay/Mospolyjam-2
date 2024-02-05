using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Новый ресурс", menuName = "Ресурс", order = 51)]
public class ResourceData : ScriptableObject
{
    [SerializeField] public string resourceName;
    [TextArea(1,5)]
    public List<string> descriptions;
    [SerializeField] private int resourceID;
    public AllResouceTypes.ResourceType resourceType;
    
    /// <summary>
    /// Сколько единиц ресурса нужно игроку
    /// </summary>
    public int survivorNeed;
}
