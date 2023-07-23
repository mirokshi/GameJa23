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

    private void Start()
    {
        text.text = message.ToString();
    }

    public void loadLevel()
    {
        StartCoroutine(load());
    }

    IEnumerator load()
    {
        yield return new WaitForSeconds(.1f);
        SceneManager.LoadScene(level);
    }

    public void exit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
