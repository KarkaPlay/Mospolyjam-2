using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������� ������������
public class ToolEvent : MonoBehaviour
{
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Progress.Instance.achievements.Find(achievement => achievement.achName == "����������").GetStatus())
        {
            int random = Random.Range(1, 100);
            if (random <= 10)
            {
                CallToolEvent();
            }
        }
    }
    public void CallToolEvent()
    {
        //��������� ������������� (����� ��)
        //����� ��������� (���� ��������� ������������)
    }
}
