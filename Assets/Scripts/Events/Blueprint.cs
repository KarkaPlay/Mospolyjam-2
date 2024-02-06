using UnityEngine;

namespace Events
{
    public class Blueprint : MonoBehaviour
    {
        private void Start()
        {
            Survivor.Instance.ChangeParameter(5, 30);
        }
    }
}