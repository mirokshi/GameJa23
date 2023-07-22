using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Load_level : MonoBehaviour
{
    public int level;
    public TextMeshProUGUI text;

    private void Start()
    {
        text.text = level.ToString();
    }

    public void loadLevel()
    {
        SceneManager.LoadScene("level " + level.ToString());
    }
}
