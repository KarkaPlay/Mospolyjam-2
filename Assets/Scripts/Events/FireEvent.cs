using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Событие при пожаре
public class FireEvent : MonoBehaviour
{

    // Update is called once per frame
    void FixedUpdate()
    {
        if (true) //есть пожар или нет
        {
            int random = Random.Range(1,100);
            if (random <= 10)
            {
                CallFireEvent();
            }
        }
    }
    public void CallFireEvent()
    {
        //Изменение характеристик (минус хп)
        //вывод сообщения (Роби обжегся)
    }
}
