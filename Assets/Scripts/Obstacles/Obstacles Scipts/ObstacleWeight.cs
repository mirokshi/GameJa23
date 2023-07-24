using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleWeight : ObstacleAction
{
    [SerializeField] private GameObject bridge;
    public override void DoAction(ItemData itemData)
    {
        Debug.Log("Success! (Weight)");
        bridge.SetActive(true);
    }
}
