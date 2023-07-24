using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float velocity;
    private Rigidbody2D _rigidbody;
    public  PlayerController _playerController;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (_playerController._isDead == false)
        {
            _rigidbody.velocity = new Vector2(velocity, _rigidbody.velocity.y);
        }
    }
}
