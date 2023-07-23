using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour, IObstacle
{
    [SerializeField] private static ItemGrid _hand;

    public static Action OnDeath;
    
    public bool IsAvoided()
    {
        if (!_hand.IsItemInInventory())
        {
            return false;
        }
        
        
        return false;
    }

    public void DeathTrigger()
    {
        OnDeath?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(!IsAvoided())
                DeathTrigger();
            else
            {
                // Animation de sobrevivir
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        throw new NotImplementedException();
    }
}
