using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MovementController : MonoBehaviour
{
    public float baseSpeed;
    private Rigidbody2D _rigidbody;
    private PlayerController _playerController;
    
    [SerializeField] private ItemGrid _inventory;

    public float currentWeight = 0f;
    public float minusSpeed = 0.3f;
    public float adjustedSpeed = 0f;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        var minSpeed = baseSpeed - 2;
        currentWeight = _inventory.GetTotalWeight();
        adjustedSpeed = baseSpeed - minusSpeed * (int) (currentWeight / 20);
        
        if (!_playerController._isDead)
        {
            if (adjustedSpeed < minSpeed)
            {
                _rigidbody.velocity = new Vector2(minSpeed, _rigidbody.velocity.y);
            }
            else
            {
                _rigidbody.velocity = new Vector2(adjustedSpeed, _rigidbody.velocity.y);
            }
            
        }
    }

    public float GetCurrentWeight()
    {
        Debug.Log("Weight: " + currentWeight);
        return currentWeight;
    }
}
