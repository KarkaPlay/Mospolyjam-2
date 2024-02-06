using UnityEngine;

namespace Events
{
    public class PlotEvent : MonoBehaviour
    {
        private void Start()
        {
            Survivor.Instance.ChangeParameter(5, 30);
        }
    }
}