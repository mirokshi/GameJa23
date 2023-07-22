using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CapsuleCollider2D _capsuleCollider;
    
    void Start()
    {
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
    }
}
