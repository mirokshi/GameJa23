using System;
using System.Collections;
using System.Collections.Generic;
using Inventory;
using UnityEngine;
using UnityEngine.Serialization;

public class MovementController : MonoBehaviour
{
    public float baseSpeed;
    private Rigidbody2D _rigidbody;
    private PlayerController _playerController;
    
    [SerializeField] private ItemGrid inventory;
    
    public float speedMultiplier = 0.3f;
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
        
        if (!_potionEffect)
        {
            adjustedSpeed = baseSpeed - speedMultiplier * (int) (_currentWeight / weightToCut);
        }

        if (!_playerController._isDead)
        {
            if (adjustedSpeed < minSpeed)
            {
                _rigidbody.velocity = new Vector2(minSpeed, _rigidbody.velocity.y);
            }
            else if(adjustedSpeed > minSpeed)
            {
                _rigidbody.velocity = new Vector2(adjustedSpeed, _rigidbody.velocity.y);
            }
            else if(_potionEffect)
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
        ItemGrid.OnUsePotion += OnUsePotion;
    }

    private void OnDisable()
    {
        ItemGrid.OnUsePotion -= OnUsePotion;
    }
}
