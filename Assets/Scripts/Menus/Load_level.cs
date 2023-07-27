using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Load_level : MonoBehaviour
{
    public string level;
    public TextMeshProUGUI text;
    public string message;

    public Animator Animator;

    [SerializeField] private List<Inventory> _inventories;

    private void Start()
    {
        if (text is not null)
        {
            text.text = message.ToString();    
        }
    }

    public void loadLevel()
    {
        foreach (var inventory in _inventories)
        {
            inventory.CleanUp();
        }
        
        StartCoroutine(load());
    }

    IEnumerator load()
    {
        Animator.SetTrigger("start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(level);
    }

    public void exit()
    {
        Application.Quit();
    }
}
