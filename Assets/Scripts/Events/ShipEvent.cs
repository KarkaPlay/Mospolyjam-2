using UnityEngine;

namespace Events
{
    public class ShipEvent : MonoBehaviour
    {
        private void Start()
        {
            Survivor.Instance.ChangeParameter(5, 30);
        }
    }
}