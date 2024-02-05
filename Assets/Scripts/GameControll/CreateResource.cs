using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;


public class CreateResource : MonoBehaviour
{
    public GameObject prefab;
    public Canvas popup;
    public TMP_Text textMeshPro;
    Resource resource;
    double time = 0;


    public void Start()
    {
        resource = prefab.GetComponent<Resource>();
    }
    // Update is called once per frame
    public void Update()
    {
        CountDown();
        //по нажатию мышки
        if (Input.GetMouseButtonDown(0))
        {
            if (resource.count==0)
            {
                CreatePopup();
            }
            SpawnPrefab();
        }
    }

    //считает прошедшее врем€ и выключает окно
    private void CountDown()
    {
        time -= Time.deltaTime;
        if (time < 0)
        {
            popup.gameObject.SetActive(false);
        }
    }

    //создаЄт всплывающее окно
    private void CreatePopup()
    {
        textMeshPro.text = "Ѕыло создано дерево"; //текст к каждому действию должен быть разный, переменную в ресурсе, куча if либо наследовать класс дл€ каждого ресурса? 
        popup.gameObject.SetActive(true);
        //ѕоле исчезает через 10 секунд, сделать по нажатию?
        time = 10;       
    }

    //создаЄт ресурс
    private void SpawnPrefab()
    {
        resource.count += 1;
        Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Instantiate(prefab, target, Quaternion.identity);
    }

}
