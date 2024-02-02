using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Новый ресурс", menuName = "Ресурс", order = 51)]
public class ResourceData : ScriptableObject
{
    [SerializeField] private string resourceName;
    [SerializeField] private string description;
    [SerializeField] private int resourceID;
    [SerializeField] public AllResouceTypes.ResourceType resourceType;
}
