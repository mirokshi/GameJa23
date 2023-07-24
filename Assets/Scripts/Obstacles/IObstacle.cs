using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObstacle
{
    void OnDestroyObstacle(ItemData itemData);
    bool IsAvoided(ItemData itemData);
    void DeathTrigger();
}
