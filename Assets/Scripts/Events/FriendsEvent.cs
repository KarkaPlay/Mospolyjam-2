using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//������� ���������� ����������
public class FriendsEvent : MonoBehaviour
{
    bool FiveFriends = false;
    bool ManyFriends = false;
    public int numberOfFriends=0;
    // Update is called once per frame
    void FixedUpdate()
    {
        
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
