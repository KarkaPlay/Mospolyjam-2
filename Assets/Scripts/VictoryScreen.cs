using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class VictoryScreen : MonoBehaviour
{
    public TMP_Text text;
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Quit()
    {
        Application.Quit();
    }

}
