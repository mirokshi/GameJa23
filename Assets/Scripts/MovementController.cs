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
    
    [SerializeField] private ItemGrid _inventory;

    public float currentWeight = 0f;
    public float minusSpeed = 0.3f;
    public float adjustedSpeed = 0f;
    
    private bool _potionEffect;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        var minSpeed = baseSpeed - 2;
        currentWeight = _inventory.GetTotalWeight();
        if (!_potionEffect)
        {
            adjustedSpeed = baseSpeed - minusSpeed * (int) (currentWeight / 20);
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
        
        Debug.Log("Potion Effect: " + _potionEffect);
    }

    public float GetCurrentWeight()
    {
        Debug.Log("Weight: " + currentWeight);
        return currentWeight;
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
