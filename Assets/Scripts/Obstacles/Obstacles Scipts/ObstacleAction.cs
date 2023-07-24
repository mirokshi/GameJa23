using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObstacleAction : MonoBehaviour
{
    [SerializeField] private ObstacleData obstacleData;

    public ItemType GetItemType()
    {
        return obstacleData.itemType;
    }

    public abstract void DoAction();
}
