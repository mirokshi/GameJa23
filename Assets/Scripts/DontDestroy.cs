using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    public static DontDestroy Instance;
    public AudioSource[] AudioSources;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    public void Update()
    {
        if (SceneManager.GetActiveScene().name.Equals("MsinMenu"))
        {
            AudioSources[0].Play();
        }
    }

    public void changeVolume(float volume)
    {
        AudioListener.volume = volume;
    }
}
