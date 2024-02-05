using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Событие деревянных человечков
public class FriendsEvent : MonoBehaviour
{
    bool FiveFriends = false;
    bool ManyFriends = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        int numberOfFriends = AllResourceDatas.Instance.allResourceDatas.FindAll(x => x.resourceType == AllResouceTypes.ResourceType.ДеревянныйДруг).Count;
        if(numberOfFriends == 5 && !FiveFriends)
        {
            FiveFriends = true;
            //Создание сообщения (Роби вырезал много друзей)
        }
        if (numberOfFriends == 20 && !ManyFriends)
        {
            ManyFriends = true;
            //Создание сообщения (У Роби в пещере тысячи людей)
        }
    }
}
