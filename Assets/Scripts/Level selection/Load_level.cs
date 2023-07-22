using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load_level : MonoBehaviour
{
    public int level;

    void loadLevel()
    {
        SceneManager.LoadScene(level.ToString());
    }
}
