using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "obstacleName",menuName = "Object/Obstacle", order = 0)]
public class ObstacleData : ScriptableObject
{
    public new string name;
    public ObstacleType obstacleType;
    public int weight;
    public Sprite sprite;
}
