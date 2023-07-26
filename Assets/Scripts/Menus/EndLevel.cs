using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    public string escena;
    [SerializeField] private ItemGrid itemGrid;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerController>()._isDead == false)
            {
                SceneManager.LoadScene(escena);
                PlayerPrefs.SetInt("Score", CalculateScore());
            }
            
        }
    }

    private int CalculateScore()
    {
        return itemGrid.GetTotalValue();
    }
}
