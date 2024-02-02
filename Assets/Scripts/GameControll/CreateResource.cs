using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class CreateResource : MonoBehaviour
{
    public GameObject prefab;
    public Canvas popup;
    public TMP_Text textMeshPro;
    bool IsFirstTime = true; //��� ���� ���-�� �������, ����� ������� ����������� ���������� � ��������? ���� ����������� ����� ��� ������� �������?
    double time = 0;
    

    // Update is called once per frame
    public void Update()
    {
        CountDown();
        //�� ������� �����
        if (Input.GetMouseButtonDown(0))
        {
            if (IsFirstTime)
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
        IsFirstTime = false;
        
    }

    //������ ������
    private void SpawnPrefab()
    {
        Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Instantiate(prefab, target, Quaternion.identity);
    }

}
