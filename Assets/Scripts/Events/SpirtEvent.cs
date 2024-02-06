using System;
using UnityEngine;

namespace Events
{
    public class SpirtEvent : MonoBehaviour
    {
        private void Start()
        {
            Survivor.Instance.ChangeParameter(1, 10);
            Survivor.Instance.ChangeParameter(5, -20);
        }
    }
}