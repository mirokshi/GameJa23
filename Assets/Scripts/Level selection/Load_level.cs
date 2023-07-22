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
        SceneManager.LoadScene(level);
    }

    public void exit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
