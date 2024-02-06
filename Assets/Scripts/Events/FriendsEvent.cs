using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Событие деревянных человечков
public class FriendsEvent : MonoBehaviour
{
    private void Start()
    {
        Survivor.Instance.ChangeParameter(5, -10);
    }
}
