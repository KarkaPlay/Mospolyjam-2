using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeOfDay : MonoBehaviour
{
    public TMPro.TextMeshProUGUI timeOfDayText;

    private float time; // в "часах"

    void Start()
    {
        time = 0f;
        StartCoroutine(TimeCounter());
    }

    IEnumerator TimeCounter()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            time += 3f / 60;
            if (time >= 24f)
            {
                time = 0f;
            }

            // Вывод текста
            if (time >= 4f && time < 12f)
            {
                timeOfDayText.text = "Утро";
            }
            else if (time >= 12f && time < 18f)
            {
                timeOfDayText.text = "День";
            }
            else if (time >= 18f && time < 23f)
            {
                timeOfDayText.text = "Вечер";
            }
            else
            {
                timeOfDayText.text = "Ночь";
            }
        }
    }
}
