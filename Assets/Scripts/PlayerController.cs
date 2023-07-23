using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CapsuleCollider2D _capsuleCollider;
    public bool _isDead;
    
    void Start()
    {
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
    }
}
