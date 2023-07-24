using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntuationTest : MonoBehaviour
{
    private int score = 0;

    public void addScore()
    {
        score += 1;

        PlayerPrefs.SetInt("Score", score);
    }
}
