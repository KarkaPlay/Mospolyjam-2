using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivorUI : MonoBehaviour
{
    private Survivor _survivor;

    [SerializeField] private RectTransform HPScale;
    [SerializeField] private RectTransform WaterScale; 
    [SerializeField] private RectTransform FoodScale; 
    [SerializeField] private RectTransform SleepScale; 
    [SerializeField] private RectTransform SanityScale; 
    
    // Start is called before the first frame update
    void Start()
    {
        _survivor = GetComponent<Survivor>();
    }

    // Update is called once per frame
    void Update()
    {
        HPScale.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _survivor.hp * 3);
        WaterScale.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _survivor.water * 3);
        FoodScale.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _survivor.food * 3);
        SleepScale.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _survivor.sleep * 3);
        SanityScale.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _survivor.sanity * 3);
    }
}
