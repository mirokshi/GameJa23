using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject button;
    public GameObject[] tutorialTexts;
    private int currentStep = 0;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShowNextStep();
            Time.timeScale = 0;
        }
    }

    public void ShowNextStep()
    {
        
        if (currentStep < tutorialTexts.Length)
        {
            ShowCurrentStep();
        }
        else
        {
            // Tutorial finished, hide all texts or handle tutorial completion
            HideAllTutorialTexts();
            Destroy(button);
            Time.timeScale = 1;

        }
        currentStep++;
    }

    private void ShowCurrentStep()
    {
        HideAllTutorialTexts();
        button.SetActive(true);
        tutorialTexts[currentStep].SetActive(true);
        
    }

    private void HideAllTutorialTexts()
    {
        foreach (GameObject text in tutorialTexts)
        {
            text.SetActive(false);
        }
    }
    
    
}
