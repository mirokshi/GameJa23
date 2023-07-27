using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class ObstacleController : MonoBehaviour
{
    private ObstacleAction _obstacleAction;
    
    public static Action OnDeath;

    public ParticleSystem ParticleSystem;

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
            if (ParticleSystem != null)
            {
                ParticleSystem.transform.position = this.transform.position + new Vector3(0, .2f,0);
                ParticleSystem.Play();
            }

            Destroy(gameObject.GetComponent<BoxCollider2D>());
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
        OnDeath?.Invoke();
    }
}
