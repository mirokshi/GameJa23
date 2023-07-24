using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRock : ObstacleAction
{
    
    public override void DoAction(ItemData itemData, float weight)
    {
        Debug.Log("Success! (Rock)");
        Destroy(gameObject);
    }
}
