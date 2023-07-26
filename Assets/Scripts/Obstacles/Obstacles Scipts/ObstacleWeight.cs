using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleWeight : ObstacleAction
{
    [SerializeField] private GameObject bridge;

    public static Action OnDeath;
    
    public override void DoAction(ItemData itemData, float weight)
    {
        if (weight <= obstacleData.weight)
        {
            bridge.SetActive(true);
        }
        else
        {
            OnDeath?.Invoke();
        }
    }
}
