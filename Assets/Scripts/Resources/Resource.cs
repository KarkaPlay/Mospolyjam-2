using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Resource : MonoBehaviour
{
    public ResourceData resourceData;

    /// <summary>
    /// Сколько еще раз можно добыть ресурс. Если его меньше 0, то игровой объект уничтожается
    /// </summary>
    [FormerlySerializedAs("Count")] [Tooltip("Сколько еще раз можно добыть ресурс")]
    public int count;

    /// <summary>
    /// Сколько единиц ресурса человек может взять за раз
    /// </summary>
    [Tooltip("Сколько единиц ресурса человек может взять за раз")]
    public int oneTake;

    /// <summary>
    /// Уменьшает количество ресурса у объекта
    /// </summary>
    public void CollectResource()
    {
        count--;
    }

    private void Start()
    {
        if (transform.parent != ResourceObjects.Instance.gameObject.transform)
            transform.SetParent(ResourceObjects.Instance.gameObject.transform);
        
        NavMeshBaker.Instance.Bake();
        ResourceObjects.Instance.AddToList(this);
    }

    

    private void OnDestroy()
    {
        NavMeshBaker.Instance.Bake();
        ResourceObjects.Instance.RemoveFromList(this);
    }
}
