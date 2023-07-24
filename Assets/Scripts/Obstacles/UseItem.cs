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
      Debug.Log("You are Dead");
   }

   private void OnTriggerEnter2D(Collider2D collider)
   {
      if (collider.CompareTag("Obstacle"))
      {
         if (HasItemInHand())
         {
            var itemData = _hand.GetItemInHand();
            Debug.Log("Usar Objeto");
            collider.gameObject.GetComponent<ObstacleController>().OnDestroyObstacle(itemData);
            _hand.OnDestroyItemInHand();
         }
         else
         {
            DeathTrigger();
         }
      }
   }
}
