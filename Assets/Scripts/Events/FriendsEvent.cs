using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//������� ���������� ����������
public class FriendsEvent : MonoBehaviour
{
    private void Start()
    {
        Survivor.Instance.ChangeParameter(5, -10);
        Progress.Instance.AddFriend();
    }

    private void OnDestroy()
    {
        Progress.Instance.numOfFriends--;
    }
}
