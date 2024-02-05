using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//������� ���������� ����������
public class FriendsEvent : MonoBehaviour
{
    bool FiveFriends = false;
    bool ManyFriends = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        int numberOfFriends = AllResourceDatas.Instance.allResourceDatas.FindAll(x => x.resourceType == AllResouceTypes.ResourceType.��������������).Count;
        if(numberOfFriends == 5 && !FiveFriends)
        {
            FiveFriends = true;
            //�������� ��������� (���� ������� ����� ������)
        }
        if (numberOfFriends == 20 && !ManyFriends)
        {
            ManyFriends = true;
            //�������� ��������� (� ���� � ������ ������ �����)
        }
    }
}
