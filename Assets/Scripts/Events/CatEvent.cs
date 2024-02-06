using System;
using UnityEngine;

namespace Events
{
    public class CatEvent : MonoBehaviour
    {
        private void Start()
        {
            Survivor.Instance.ChangeParameter(5, 30);
        }
    }
}