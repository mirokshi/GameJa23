using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private CircleCollider2D _circleCollider;
    
    void Start()
    {
        _circleCollider = GetComponent<CircleCollider2D>();
    }
}
