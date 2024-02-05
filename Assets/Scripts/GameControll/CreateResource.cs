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
    double time = 0;
    string achievementName;


    public void Start()
    {
        achievementName = prefab.GetComponent<Resource>().resourceData.resourceType.ToString();
    }
    // Update is called once per frame
    public void Update()
    {
        CountDown();
        //�� ������� �����
        if (Input.GetMouseButtonDown(0))
        {
            if (!Progress.Instance.achievements.Find(achievement => achievement.achName == achievementName).GetStatus())
            {
                Progress.Instance.UnlockAchievement(achievementName);
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
        textMeshPro.text = Progress.Instance.achievements.Find(achievement => achievement.achName == achievementName).message;
        popup.gameObject.SetActive(true);
        //���� �������� ����� 10 ������, ������� �� �������?
        time = 10;       
    }

    //������ ������
    private void SpawnPrefab()
    {
        Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Instantiate(prefab, target, Quaternion.identity);
    }

}
