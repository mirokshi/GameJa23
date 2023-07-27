using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class EndLevel : MonoBehaviour
{
    public string escena;
    [SerializeField] private InventoryPlayer inventoryPlayer;

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
        return inventoryPlayer.GetTotalValue();
    }
}
