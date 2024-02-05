using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[Serializable]
public class Achievement
{
    public string achName;
    public string message;
    [SerializeField] private bool status = false;
    
    public Achievement([NotNull] string name, bool status = false)
    {
        this.achName = name ?? throw new ArgumentNullException(nameof(name));
        
        this.message = AllResourceDatas.Instance.Find(name).descriptions[0];
        
        this.status = status;
    }

    public void ChangeStatus()
    {
        status = true;
    }
}

public class Progress : MonoBehaviour
{
    public List<Achievement> achievements;

    public static Progress Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        achievements = new List<Achievement>()
        {
            new("Вода"),
            new("Огонь"),
            new("Земля"),
            new("Воздух"),
            new("Металл"),
            new("Глина"),
            new("Дерево"),
            new("Спирт"),
            new("Инструмент"),
            new("Костер"),
            new("Доска"),
            new("Бумага"),
            new("Жизнь"),
            new("Плод"),
            new("Деньга"),
            new("Деревянный друг"),
            new("Кошка")
        };
    }

    public void UnlockAchievement(string achievementName)
    {
        achievements.Find(achievement => achievement.achName == achievementName).ChangeStatus();
        Debug.Log($"Разблокирован элемент {achievementName}");
    }
}
