using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


//Событие при пожаре
public class FireEvent : MonoBehaviour
{
    private bool isOnTree;

    private void Start()
    {
        InvokeRepeating(nameof(TryCallFireEvent), 5, 2);
        Survivor.Instance.ChangeParameter(6, 20);
        if (isOnTree)
        {
            Survivor.Instance.ChangeParameter(5, -20);
        }
    }

    private void TryCallFireEvent()
    {
        int random = Random.Range(1,100);
        if (random <= 10)
        {
            CallFireEvent();
        }
    }
    
    public void CallFireEvent()
    {
        Survivor.Instance.ChangeParameter(1, 30);
        //вывод сообщения (Роби обжегся)
    }
}
