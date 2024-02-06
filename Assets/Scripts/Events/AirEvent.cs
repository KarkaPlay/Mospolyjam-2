using System;
using UnityEngine;

namespace Events
{
    public class AirEvent : MonoBehaviour
    {
        private void Start()
        {
            Survivor.Instance.ChangeParameter(6, -25);
        }
    }
}