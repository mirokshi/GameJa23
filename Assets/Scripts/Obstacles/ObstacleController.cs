using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ObstacleController : MonoBehaviour
{
    private ObstacleAction _obstacleAction;
    
    public static Action OnDeath;

    private void Awake()
    {
        _obstacleAction = GetComponent<ObstacleAction>();
    }

    public void OnDestroyObstacle(ItemData itemData)
    {
        if (!IsAvoided(itemData))
        {
            DeathTrigger();
        }
        else
        {
            Debug.Log("Player avoids the Hole");
            _obstacleAction.DoAction();
        }
    }

    public bool IsAvoided(ItemData itemData)
    {
        if (_obstacleAction.GetItemType().Equals(itemData.itemType))
        {
            return true;
        }

        return false;
    }

    public void DeathTrigger()
    {
        OnDeath?.Invoke();
    }
    
    private void OnEnable()
    {
        UseItem.OnDestroyObstacle += OnDestroyObstacle;
    }
}
