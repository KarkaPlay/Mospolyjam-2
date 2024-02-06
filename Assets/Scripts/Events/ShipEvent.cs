using UnityEngine;
using UnityEngine.SceneManagement;

namespace Events
{
    public class ShipEvent : MonoBehaviour
    {
        double time = 2;
        private void Start()
        {
            Survivor.Instance.ChangeParameter(5, 30);
        }

        private void Update()
        {
            time -= 1 * Time.deltaTime;
            if (time < 0)
            {
                SceneManager.LoadScene("VictoryScreen");
            }
        }
    }
}