using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//������� ����
public class HatEvent : MonoBehaviour
{
    bool ThreeHats = false;
    bool SevenHats = false;
    bool ManyHats = false;
    void FixedUpdate()
    {
        int numberOfHats = AllResourceDatas.Instance.allResourceDatas.FindAll(x => x.resourceType == AllResouceTypes.ResourceType.�����).Count;
        if (numberOfHats == 3 && !ThreeHats)
        {
            ThreeHats = true;
            //����� ��������� (� ���� ������ ������� ����� ����)
        }
        if (numberOfHats == 7 && !SevenHats)
        {
            SevenHats= true;
            //����� ��������� (���� �� ����� ��� ������)
        }
        if (numberOfHats == 20 && !ManyHats)
        {
            ManyHats= true;
            //����� ��������� (�������� ����! ���� ����� ������ ����� � ������� 100 �����)
        }
    }
}
