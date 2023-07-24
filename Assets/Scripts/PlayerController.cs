using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CapsuleCollider2D _capsuleCollider;
    public bool _isDead = false;

    void Start()
    {
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
        PlayerPrefs.SetInt("Score", 0);
    }

    public void DeathTrigger()
    {
        Debug.Log("Player is dead");
        _isDead = true;
    }

    private void OnEnable()
    {
        ObstacleController.OnDeath += DeathTrigger;
        UseItem.OnDeath += DeathTrigger;
        ObstacleWeight.OnDeath += DeathTrigger;
    }

    private void OnDisable()
    {
        ObstacleController.OnDeath -= DeathTrigger;
        UseItem.OnDeath -= DeathTrigger;
        ObstacleWeight.OnDeath -= DeathTrigger;
    }
}
