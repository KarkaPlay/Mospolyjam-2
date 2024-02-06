using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivorUI : MonoBehaviour
{
    private Survivor _survivor;

    [SerializeField] private RectTransform HPScale;
    [SerializeField] private RectTransform WaterScale; 
    [SerializeField] private RectTransform FoodScale; 
    [SerializeField] private RectTransform SanityScale; 
    
    // Start is called before the first frame update
    void Start()
    {
        _survivor = GetComponent<Survivor>();
    }

    // Update is called once per frame
    void Update()
    {
        HPScale.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _survivor.HP * 3);
        WaterScale.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _survivor.Water * 3);
        FoodScale.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _survivor.Food * 3);
        SanityScale.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _survivor.Sanity * 3);
    }
}
