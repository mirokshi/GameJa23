using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    public static DontDestroy Instance;
    public AudioSource[] _AudioSources;
    public Scene activeScene;

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

    private void Stop(int audioOrigen)
    {
        for (int i = _AudioSources.Length - 1; i >= 0; i--)
        {
            if (i != audioOrigen)
            {
                _AudioSources[i].Stop();
            }
        }
    }

    public void Update()
    {
        if ((!activeScene.Equals(SceneManager.GetActiveScene()) && SceneManager.GetActiveScene().name.Equals("MainMenu")) &&
            (!activeScene.Equals(SceneManager.GetActiveScene()) && SceneManager.GetActiveScene().name.Equals("Credits")) &&
            (!activeScene.Equals(SceneManager.GetActiveScene()) && SceneManager.GetActiveScene().name.Equals("EndPuntuation")) &&
            (!activeScene.Equals(SceneManager.GetActiveScene()) && SceneManager.GetActiveScene().name.Equals("Level-Selector")) &&
            (!activeScene.Equals(SceneManager.GetActiveScene()) && SceneManager.GetActiveScene().name.Equals("Options")) &&
            (!activeScene.Equals(SceneManager.GetActiveScene()) && SceneManager.GetActiveScene().name.Equals("ShadowLev")))
        {
            Debug.Log("Menu");
            Stop(0);
            _AudioSources[0].Play();
            activeScene = SceneManager.GetActiveScene();
        }
        
        if (!activeScene.Equals(SceneManager.GetActiveScene()) && SceneManager.GetActiveScene().name.Equals("Tutorial")){
            Debug.Log("Tutorial");
            Stop(1);
            _AudioSources[1].Play();
            activeScene = SceneManager.GetActiveScene();
        }
        
        if (!activeScene.Equals(SceneManager.GetActiveScene()) && SceneManager.GetActiveScene().name.Equals("Jungla")){
            Debug.Log("Jungla");
            Stop(2);
            _AudioSources[2].Play();
            activeScene = SceneManager.GetActiveScene();
        }
        
        if (!activeScene.Equals(SceneManager.GetActiveScene()) && SceneManager.GetActiveScene().name.Equals("Desierto")){
            Debug.Log("Desierto");
            Stop(3);
            _AudioSources[3].Play();
            activeScene = SceneManager.GetActiveScene();
        }
        
        if (!activeScene.Equals(SceneManager.GetActiveScene()) && SceneManager.GetActiveScene().name.Equals("Moon")){
            Debug.Log("Moon");
            Stop(4);
            _AudioSources[4].Play();
            activeScene = SceneManager.GetActiveScene();
        }
        
        if (!activeScene.Equals(SceneManager.GetActiveScene()) && SceneManager.GetActiveScene().name.Equals("City")){
            Debug.Log("City");
            Stop(5);
            _AudioSources[5].Play();
            activeScene = SceneManager.GetActiveScene();
        }
        
        if (!activeScene.Equals(SceneManager.GetActiveScene()) && SceneManager.GetActiveScene().name.Equals("City")){
            Debug.Log("City");
            Stop(6);
            _AudioSources[6].Play();
            activeScene = SceneManager.GetActiveScene();
        }
        
    }

    public void changeVolume(float volume)
    {
        AudioListener.volume = volume;
    }
}
