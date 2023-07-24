using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRock : ObstacleAction
{
    public override void DoAction(ItemData itemData)
    {
        Debug.Log("Success! (Rock)");
    }
}
