﻿using System;
using UnityEngine;

namespace Events
{
    public class GroundEvent : MonoBehaviour
    {
        private void Start()
        {
            Survivor.Instance.ChangeParameter(5, 10);
        }
    }
}