using UnityEngine;

namespace Events
{
    public class BoatEvent : MonoBehaviour
    {
        private void Start()
        {
            Survivor.Instance.ChangeParameter(5, 30);
        }
    }
}