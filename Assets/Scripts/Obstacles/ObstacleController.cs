using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ObstacleController : MonoBehaviour, IObstacle
{
    [SerializeField] private ItemType itemType;
    
    public static Action OnDeath;
    
    public void OnDestroyObstacle(ItemData itemData)
    {
        if (!IsAvoided(itemData))
        {
            DeathTrigger();
        }
        else
        {
            Debug.Log("Animación de destruir obstaculo");
            Destroy(gameObject);
        }
    }

    public bool IsAvoided(ItemData itemData)
    {
        if (itemType.Equals(itemData.itemType))
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
