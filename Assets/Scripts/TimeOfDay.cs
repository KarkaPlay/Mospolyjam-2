using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeOfDay : MonoBehaviour
{
    public TMPro.TextMeshProUGUI timeOfDayText;

    [SerializeField] private float time; // � "�����"
    public enum DayTime
    {
        Morning,
        Day,
        Evening,
        Night
    }
    public static DayTime currentDayTime;
    [Tooltip("��� ������, ��� ������� ���� ����� (������� ����� �������� �� �������)")]
    public float timeSpeed = 0.1f; 

    public static TimeOfDay Instance;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        time = 0f;
        StartCoroutine(TimeCounter());
    }

    IEnumerator TimeCounter()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            time += timeSpeed;
            if (time >= 24f)
            {
                time = 0f;
            }

            switch (time)
            {
                // ����� ������� �����
                case >= 4f and < 12f:
                    timeOfDayText.text = "����";
                    currentDayTime = DayTime.Morning;
                    break;
                case >= 12f and < 18f:
                    timeOfDayText.text = "����";
                    currentDayTime = DayTime.Day;
                    break;
                case >= 18f and < 23f:
                    timeOfDayText.text = "�����";
                    currentDayTime = DayTime.Evening;
                    break;
                default:
                    timeOfDayText.text = "����";
                    currentDayTime = DayTime.Night;
                    break;
            }
        }
    }
}
