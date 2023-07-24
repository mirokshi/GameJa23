using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowStars : MonoBehaviour
{
    public int OneStarScore;
    public GameObject Star1;

    public int TwoStarsScore;
    public GameObject Star2;

    public int ThreeStarsScore;
    public GameObject Star3;

    private int totalScore;

    private void Start()
    {
        totalScore= PlayerPrefs.GetInt("Score");
        showStars();
    }

    public void showStars()
    {
        if (totalScore >= OneStarScore) Star1.SetActive(true);
        if (totalScore >= TwoStarsScore) Star2.SetActive(true);
        if (totalScore >= ThreeStarsScore) Star3.SetActive(true);
    }
}
