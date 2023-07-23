using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObstacle
{
    bool IsAvoided();
    void DeathTrigger();
}
