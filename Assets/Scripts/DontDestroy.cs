using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class DontDestroy : MonoBehaviour
{
    public static DontDestroy Instance;
    public AudioSource[] _AudioSources;
    private Scene _currentScene;
    private int _currentSong;

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

        _currentSong = 0;
    }

    private void Start()
    {
        _currentScene = SceneManager.GetActiveScene();
        if(_currentScene.name.Equals("MainMenu"))
        {
            _AudioSources[0].Play();
        }
    }

    public void Update()
    {
        var nextScene = SceneManager.GetActiveScene();
        
        if (!_currentScene.Equals(nextScene))
        {
            if (_currentSong != 0)
            {
                Stop(_currentSong);
            }
            else
            {
                Pause(_currentSong);
            }
        }
        
        if ((!_currentScene.Equals(nextScene) && nextScene.name.Equals("MainMenu")) ||
            (!_currentScene.Equals(nextScene) && nextScene.name.Equals("Credits")) ||
            (!_currentScene.Equals(nextScene) && nextScene.name.Equals("Level-Selector")) ||
            (!_currentScene.Equals(nextScene) && nextScene.name.Equals("Options")) ||
            (!_currentScene.Equals(nextScene) && nextScene.name.Equals("ShadowLev")) ||
            (!_currentScene.Equals(nextScene) && nextScene.name.Equals("EndPuntuation")))
        {
            Debug.Log("Menu");
            _AudioSources[0].UnPause();
            _currentScene = SceneManager.GetActiveScene();
            _currentSong = 0;
        }

        if (!_currentScene.Equals(nextScene) && nextScene.name.Equals("Tutorial")){
            Debug.Log("Tutorial");
            _AudioSources[1].Play();
            _currentScene = SceneManager.GetActiveScene();
            _currentSong = 1;
        }
        
        if (!_currentScene.Equals(nextScene) && nextScene.name.Equals("Jungla")){
            Debug.Log("Jungla");
            _AudioSources[2].Play();
            _currentScene = SceneManager.GetActiveScene();
            _currentSong = 2;
        }
        
        if (!_currentScene.Equals(nextScene) && nextScene.name.Equals("Desierto")){
            Debug.Log("Desierto");
            _AudioSources[3].Play();
            _currentScene = SceneManager.GetActiveScene();
            _currentSong = 3;
        }
        
        if (!_currentScene.Equals(nextScene) && nextScene.name.Equals("Moon")){
            Debug.Log("Moon");
            _AudioSources[4].Play();
            _currentScene = SceneManager.GetActiveScene();
            _currentSong = 4;
        }
        
        if (!_currentScene.Equals(nextScene) && nextScene.name.Equals("City")){
            Debug.Log("City");
            _AudioSources[5].Play();
            _currentScene = SceneManager.GetActiveScene();
            _currentSong = 5;
        }
        
        if (!_currentScene.Equals(nextScene) && nextScene.name.Equals("City")){
            Debug.Log("City");
            _AudioSources[6].Play();
            _currentScene = SceneManager.GetActiveScene();
            _currentSong = 6;
        }
        
    }

    public void changeVolume(float volume)
    {
        AudioListener.volume = volume;
    }
    
    private void Stop(int audioIndex)
    {
        for (int i = _AudioSources.Length - 1; i >= 0; i--)
        {
            if (i != audioIndex)
            {
                _AudioSources[i].Stop();
            }
        }
    }

    private void Pause(int audioIndex)
    {
        for (int i = _AudioSources.Length - 1; i >= 0; i--)
        {
            if (i != audioIndex)
            {
                _AudioSources[i].Pause();
            }
        }
    }

    private bool EndPunctuationCheck()
    {
        string[] sceneNames = new []{"MainMenu","Credits","Level-Selector","Options","ShadowLev"};

        foreach (var scene in sceneNames)
        {
            if (_currentScene.name.Equals(scene))
            {
                return true;
            }
        }

        return false;
    }
}
