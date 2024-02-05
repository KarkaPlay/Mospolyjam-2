using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Событие шляп
public class HatEvent : MonoBehaviour
{
    bool ThreeHats = false;
    bool SevenHats = false;
    bool ManyHats = false;
    void FixedUpdate()
    {
        int numberOfHats = AllResourceDatas.Instance.allResourceDatas.FindAll(x => x.resourceType == AllResouceTypes.ResourceType.Шляпа).Count;
        if (numberOfHats == 3 && !ThreeHats)
        {
            ThreeHats = true;
            //вывод сообщения (У Роби теперь большой выбор шляп)
        }
        if (numberOfHats == 7 && !SevenHats)
        {
            SevenHats= true;
            //вывод сообщения (Роби не знает что надеть)
        }
        if (numberOfHats == 20 && !ManyHats)
        {
            ManyHats= true;
            //вывод сообщения (Маскарад шляп! Роби занял первое место и выиграл 100 денег)
        }
    }
}
