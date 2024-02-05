using System;
using System.Collections;
using System.Collections.Generic;
using NavMeshPlus.Components;
using UnityEngine;

public class NavMeshBaker : MonoBehaviour
{
    public static NavMeshBaker Instance { get; private set; }
    private NavMeshSurface _navMeshSurface;
    
    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else
        { 
            Instance = this; 
        } 
        
        DontDestroyOnLoad(gameObject);
        
        _navMeshSurface = GetComponent<NavMeshSurface>();
    }

    private void Start()
    {
        _navMeshSurface.BuildNavMeshAsync();
    }

    public void Bake()
    {
        _navMeshSurface.UpdateNavMesh(_navMeshSurface.navMeshData);
    }
}
