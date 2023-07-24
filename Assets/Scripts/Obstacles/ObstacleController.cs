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

    public void OnDestroyObstacle(ItemData itemData, float weight)
    {
        if (!_obstacleAction.GetItemType().Equals(ObstacleType.Weight) && !IsAvoided(itemData))
        {
            DeathTrigger();
        }
        
        else
        {
            Debug.Log("Player avoids the Obstacle");
            _obstacleAction.DoAction(itemData, weight);
        }
    }

    private bool IsAvoided(ItemData itemData)
    {
        if (_obstacleAction.GetItemType().Equals(itemData.obstacleType))
        {
            return true;
        }

        return false;
    }

    private void DeathTrigger()
    {
        Debug.Log("Dead");
        OnDeath?.Invoke();
    }
}
