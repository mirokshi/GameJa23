using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Cecil.Cil;
using Unity.VisualScripting;
using UnityEngine;

public class UseItem : MonoBehaviour
{
   [SerializeField] private InventoryHand _hand;
   
   private MovementController _movementController;

   public static Action OnDeath;

   private void Start()
   {
      _movementController = GetComponent<MovementController>();
   }

   private void DeathTrigger()
   {
      OnDeath?.Invoke();
   }

   private void OnTriggerEnter2D(Collider2D collider2D)
   {
      if (collider2D.CompareTag("Obstacle"))
      {
         var obstacleAction = collider2D.gameObject.GetComponent<ObstacleAction>();
         var obstacleController = collider2D.gameObject.GetComponent<ObstacleController>();
         
         if (!_hand.CanPlaceItem() && !obstacleAction.GetItemType().Equals(ObstacleType.Weight))
         {
            var itemData = _hand.GetItemInHand();
            
            var weight = _movementController.GetCurrentWeight();
            obstacleController.OnDestroyObstacle(itemData, weight);
            
            _hand.OnDestroyItemInHand();
         }
         else if(obstacleAction.GetItemType().Equals(ObstacleType.Weight))
         {
            var itemData = _hand.GetItemInHand();
            var weight = _movementController.GetCurrentWeight();
            
            obstacleController.OnDestroyObstacle(itemData, weight);
         }
         else
         {
            DeathTrigger();
         }
      }
   }
}
