using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Cecil.Cil;
using Unity.VisualScripting;
using UnityEngine;

public class UseItem : MonoBehaviour
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

   private void OnTriggerEnter2D(Collider2D collider)
   {
      if (collider.CompareTag("Obstacle"))
      {
         if (_hand.IsItemInInventory())
         {
            Debug.Log(_hand.GetItemHand());
            // _hand.GetItem();
         }
      }
   }

}
