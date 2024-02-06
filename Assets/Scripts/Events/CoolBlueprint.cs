using UnityEngine;

namespace Events
{
    public class CoolBlueprint : MonoBehaviour
    {
        private void Start()
        {
            Survivor.Instance.ChangeParameter(5, 50);
        }
    }
}