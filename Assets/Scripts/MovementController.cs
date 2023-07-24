using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float velocity;
    private Rigidbody2D _rigidbody;
    
    [SerializeField] private Vector3 launchDirection = Vector3.up;
    [SerializeField] private float launchForce = 10f;
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _rigidbody.velocity = new Vector2(velocity, _rigidbody.velocity.y);
    }

    private void OnJump()
    {
        _rigidbody.AddForce(launchDirection * launchForce, ForceMode2D.Impulse);
    }

    private void OnEnable()
    {
        LaunchPad.OnJump += OnJump;
    }

    private void OnDisable()
    {
        LaunchPad.OnJump -= OnJump;
    }
}
