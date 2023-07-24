using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Cecil.Cil;
using Unity.VisualScripting;
using UnityEngine;

public class UseItem : MonoBehaviour
{
   [SerializeField] private ItemGrid _hand;

   public static Action OnDeath;
   public static Action<ItemData> OnDestroyObstacle;

   private bool HasItemInHand()
   {
      if (!_hand.IsItemInInventory())
      {
         return false;
      }

      return true;
   }

   private void DeathTrigger()
   {
      OnDeath?.Invoke();
   }

   private void OnTriggerEnter2D(Collider2D collider)
   {
      if (collider.CompareTag("Obstacle"))
      {
         if (HasItemInHand())
         {
            var itemData = _hand.GetItemInHand();
            Debug.Log("Usar Objeto");
            OnDestroyObstacle?.Invoke(itemData);
         }
         else
         {
            DeathTrigger();
         }
      }
   }
}
