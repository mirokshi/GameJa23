using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObstacleAction : MonoBehaviour
{
    [SerializeField] protected ObstacleData obstacleData;

    public ObstacleType GetItemType()
    {
        return obstacleData.obstacleType;
    }

    public abstract void DoAction(ItemData itemData);
    
    
}
