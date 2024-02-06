using System;
using UnityEngine;

namespace Events
{
    public class LifeEvent : MonoBehaviour
    {
        private void Start()
        {
            Survivor.Instance.ChangeParameter(1, 15);
        }
    }
}