using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

//Событие инструментов
public class ToolEvent : MonoBehaviour
{
    public void TryCallToolEvent()
    {
        int random = Random.Range(1, 100);
        if (random <= 10)
        {
            CallToolEvent();
        }
    }
    
    private void CallToolEvent()
    {
        Survivor.Instance.ChangeParameter(1, 30);
        Debug.LogWarning("Роби поранился инструментом");
        //TODO: вывод сообщения (Роби поранился инструментом)
    }
}
