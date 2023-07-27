using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private CircleCollider2D _circleCollider;
    public Transform _playerDetector;

    public float radius;
    
    void Start()
    {
        _circleCollider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_playerDetector.position, radius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                collider.GetComponent<PlayerController>().DeathTrigger();
            }
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_playerDetector.position, radius);
    }
}