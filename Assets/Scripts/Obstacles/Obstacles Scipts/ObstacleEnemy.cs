using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleEnemy : ObstacleAction
{
    public override void DoAction(ItemData itemData, float weight)
    {
        Destroy(gameObject);
    }
}
