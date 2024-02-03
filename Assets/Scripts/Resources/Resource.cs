using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public ResourceData resourceData;
    
    /// <summary>
    /// Общее количество этого ресурса в этом объекте
    /// </summary>
    [Tooltip("Общее количество этого ресурса в этом объекте")]
    public int count;
    
    /// <summary>
    /// Сколько единиц ресурса человек может взять за раз
    /// </summary>
    [Tooltip("Сколько единиц ресурса человек может взять за раз")]
    public int oneTake;

    public bool playerIsNear = true;

    /// <summary>
    /// Собирает ресурс этого объекта и дает его человеку
    /// </summary>
    /// <param name="survivorResource">Ресурс человека, который будет пополняться</param>
    /// <returns><para>0 Если человек не рядом</para>
    /// 1 Если пытается взять больше чем ему надо
    /// <para>2 Если успешно взял ресурс</para>
    /// </returns>
    public int CollectResource(ref float survivorResource)
    {
        if (!playerIsNear)
            return 0;

        if (survivorResource > resourceData.survivorNeed)
            return 1;
        
        survivorResource += oneTake;
        return 2;
    }
}
