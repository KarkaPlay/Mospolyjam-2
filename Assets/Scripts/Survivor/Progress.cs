using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

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

    public bool GetStatus()
    {
        return status;
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

    // TODO: Сделать вывод окон сообщений
    public TMPro.TextMeshProUGUI achText;
    public Image achBG;
    double time = 0;

    // Прогресс по шляпам
    public int numOfHats;
    private bool threeHats = false, sevenHats = false, TwentyHats = false;
    public void AddHat()
    {
        numOfHats++;
        
        if (numOfHats == 3 && !threeHats)
        {
            threeHats = true;
            Debug.LogAssertion("У Роби теперь большой выбор шляп");
            //вывод сообщения (У Роби теперь большой выбор шляп)
        }
        if (numOfHats == 7 && !sevenHats)
        {
            sevenHats = true;
            Debug.LogAssertion("Роби не знает что надеть");
            //вывод сообщения (Роби не знает что надеть)
        }
        if (numOfHats == 20 && !TwentyHats)
        {
            TwentyHats = true;
            Debug.LogAssertion("Маскарад шляп! Роби занял первое место и выиграл 100 денег");
            //вывод сообщения (Маскарад шляп! Роби занял первое место и выиграл 100 денег)
        }
    }
    
    
    // Прогресс по деревянным друзьям
    public int numOfFriends;
    private bool oneFriend = false, fiveFriends = false, twentyFriends = false;
    public void AddFriend()
    {
        numOfFriends++;
        
        if (numOfFriends == 1 && !oneFriend)
        {
            oneFriend = true;
            Debug.LogAssertion(AllResourceDatas.Instance.Find(AllResouceTypes.ResourceType.ДеревянныйДруг).descriptions[0]);
            
            //вывод сообщения
        }
        if (numOfFriends == 5 && !fiveFriends)
        {
            fiveFriends = true;
            Debug.LogAssertion(AllResourceDatas.Instance.Find(AllResouceTypes.ResourceType.ДеревянныйДруг).descriptions[1]);
            //вывод сообщения
        }
        if (numOfFriends == 20 && !twentyFriends)
        {
            twentyFriends = true;
            Debug.LogAssertion(AllResourceDatas.Instance.Find(AllResouceTypes.ResourceType.ДеревянныйДруг).descriptions[2]);
            //вывод сообщения
        }
    }
    

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
        achText.text = achievements.Find(achievement => achievement.achName == achievementName).message;
        achBG.gameObject.SetActive(true);
        time = 5;
        //Debug.LogAssertion($"Разблокирован элемент {achievementName}");
        //Debug.LogAssertion(achievements.Find(achievement => achievement.achName == achievementName).message);
    }

    public void Update()
    {
        if (achBG.IsActive())
            time -= 1 * Time.deltaTime;
        if (time < 0)
            achBG.gameObject.SetActive(false);
    }
}
