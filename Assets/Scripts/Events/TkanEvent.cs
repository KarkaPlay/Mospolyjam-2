using UnityEngine;

namespace Events
{
    public class TkanEvent : MonoBehaviour
    {
        private void Start()
        {
            Survivor.Instance.ChangeParameter(5, 20);
        }
    }
}