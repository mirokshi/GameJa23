using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "obstacleName",menuName = "Object/Obstacle", order = 0)]
public class ObstacleData : ScriptableObject
{
    public new string name;
    public ItemType itemType;
    public Sprite sprite;
}
