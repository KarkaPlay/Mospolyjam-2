using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//������� ��� ������
public class FireEvent : MonoBehaviour
{

    // Update is called once per frame
    void FixedUpdate()
    {
        if (true) //���� ����� ��� ���
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
        //��������� ������������� (����� ��)
        //����� ��������� (���� �������)
    }
}
