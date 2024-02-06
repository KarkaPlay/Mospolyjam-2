using System;
using UnityEngine;

namespace Events
{
    public class TreeEvent : MonoBehaviour
    {
        private void Start()
        {
            Survivor.Instance.ChangeParameter(1, 10);
        }
    }
}