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
        //�� ������� �����
        if (Input.GetMouseButtonDown(0))
        {
            if (resource.count==0)
            {
                CreatePopup();
            }
            SpawnPrefab();
        }
    }

    //������� ��������� ����� � ��������� ����
    private void CountDown()
    {
        time -= Time.deltaTime;
        if (time < 0)
        {
            popup.gameObject.SetActive(false);
        }
    }

    //������ ����������� ����
    private void CreatePopup()
    {
        textMeshPro.text = "���� ������� ������"; //����� � ������� �������� ������ ���� ������, ���������� � �������, ���� if ���� ����������� ����� ��� ������� �������? 
        popup.gameObject.SetActive(true);
        //���� �������� ����� 10 ������, ������� �� �������?
        time = 10;       
    }

    //������ ������
    private void SpawnPrefab()
    {
        resource.count += 1;
        Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Instantiate(prefab, target, Quaternion.identity);
    }

}
