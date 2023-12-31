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
    
    [SerializeField] private InventoryPlayer inventory;
    
    public float speedReduction = 0.3f;
    public float minSpeed = 2;
    public float adjustedSpeed = 0f;
    public float weightToCut = 20f;
    
    private float _currentWeight = 0f;
    private bool _potionEffect;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        _currentWeight = inventory.GetTotalWeight();

        if (!_playerController._isDead)
        {
            if (!_potionEffect)
            {
                adjustedSpeed = baseSpeed - Mathf.Floor(_currentWeight / weightToCut * speedReduction);
                
                if (adjustedSpeed <= minSpeed)
                {
                    _rigidbody.velocity = new Vector2(minSpeed, _rigidbody.velocity.y);
                }
                else
                {
                    _rigidbody.velocity = new Vector2(adjustedSpeed, _rigidbody.velocity.y);
                }
                
                Debug.Log("Current velocity: " + _rigidbody.velocity.y);
            }
            else
            {
                _rigidbody.velocity = new Vector2(adjustedSpeed, _rigidbody.velocity.y);
            }
        }
    }

    public float GetCurrentWeight()
    {
        return _currentWeight;
    }
    
    private void OnUsePotion(ItemData itemData)
    {
        if (!_potionEffect)
        {
            if (itemData.itemType.Equals(ItemType.Potion))
            {
                _potionEffect = true;
                adjustedSpeed = itemData.potionSpeed;
                StartCoroutine(EndPotionEffect(itemData.potionDuration));
            }
        }
    }
    
    private IEnumerator EndPotionEffect(float duration)
    {
        yield return new WaitForSeconds(duration);
        
        _potionEffect = false;
    }

    private void OnEnable()
    {
        InventoryHand.OnUsePotion += OnUsePotion;
    }

    private void OnDisable()
    {
        InventoryHand.OnUsePotion -= OnUsePotion;
    }
}
