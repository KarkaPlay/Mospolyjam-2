using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class CreateResource : MonoBehaviour
{
    public GameObject prefab;
    public Canvas popup;
    public TMP_Text textMeshPro;
    bool IsFirstTime = true; //Это надо где-то хранить, может создать статическую переменную к ресурсам? либо наследовать класс для каждого ресурса?
    double time = 0;
    

    // Update is called once per frame
    public void Update()
    {
        CountDown();
        //по нажатию мышки
        if (Input.GetMouseButtonDown(0))
        {
            if (IsFirstTime)
            {
                CreatePopup();
            }
            SpawnPrefab();
        }
    }

    //считает прошедшее время и выключает окно
    private void CountDown()
    {
        time -= Time.deltaTime;
        if (time < 0)
        {
            popup.gameObject.SetActive(false);
        }
    }

    //создаёт всплывающее окно
    private void CreatePopup()
    {
        textMeshPro.text = "Было создано дерево"; //текст к каждому действию должен быть разный, переменную в ресурсе, куча if либо наследовать класс для каждого ресурса? 
        popup.gameObject.SetActive(true);
        //Поле исчезает через 10 секунд, сделать по нажатию?
        time = 10;
        IsFirstTime = false;
        
    }

    //создаёт ресурс
    private void SpawnPrefab()
    {
        Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Instantiate(prefab, target, Quaternion.identity);
    }

}
