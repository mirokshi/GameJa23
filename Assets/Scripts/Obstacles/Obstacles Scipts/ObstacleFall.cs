using System;
using System.Collections;
using System.Collections.Generic;
using Inventory;
using UnityEngine;
using UnityEngine.Serialization;

public class ObstacleFall : ObstacleAction
{
    [SerializeField] private GameObject board;
    [SerializeField] private GameObject launchPad;

    public override void DoAction(ItemData itemData)
    {
        Debug.Log("Success! (Fall)");
        if (itemData.itemType == ItemType.Board)
        {
            board.SetActive(true);
        }

        if (itemData.itemType == ItemType.LaunchPad)
        {
            launchPad.SetActive(true);
        }
    }
}
