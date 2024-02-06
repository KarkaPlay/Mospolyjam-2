using UnityEngine;

namespace Events
{
    public class RopeEvent : MonoBehaviour
    {
        private void Start()
        {
            Survivor.Instance.ChangeParameter(1, -20);
        }
    }
}