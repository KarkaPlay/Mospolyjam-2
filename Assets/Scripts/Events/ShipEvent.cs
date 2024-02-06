using UnityEngine;
using UnityEngine.SceneManagement;

namespace Events
{
    public class ShipEvent : MonoBehaviour
    {
        private void Start()
        {
            Survivor.Instance.ChangeParameter(5, 30);
            //Запуск победной сцены, если сделать не моментально после создания, то надо переместить
            SceneManager.LoadScene("VictoryScreen");
        }
    }
}