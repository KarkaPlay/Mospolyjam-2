using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//������� ����
public class HatEvent : MonoBehaviour
{
    private void Start()
    {
        Progress.Instance.AddHat();
    }

    private void OnDestroy()
    {
        Progress.Instance.numOfHats--;
    }
}
