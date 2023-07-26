using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
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
            if (_currentSong != 0 && !nextScene.name.Equals("EndPuntuation"))
            {
                Stop();
            }
            else
            {
                Pause();
            }
        }
        
        if ((!_currentScene.Equals(nextScene) && nextScene.name.Equals("MainMenu")) ||
            (!_currentScene.Equals(nextScene) && nextScene.name.Equals("Credits")) ||
            (!_currentScene.Equals(nextScene) && nextScene.name.Equals("Level-Selector")) ||
            (!_currentScene.Equals(nextScene) && nextScene.name.Equals("Options")) ||
            (!_currentScene.Equals(nextScene) && nextScene.name.Equals("ShadowLev")))
        {
            SetMusic(0);
        }

        if (!_currentScene.Equals(nextScene) && (nextScene.name.Equals("Tutorial") || nextScene.name.Equals("TutorialSH"))){
            SetMusic(1);
        }
        
        if (!_currentScene.Equals(nextScene) && (nextScene.name.Equals("Jungla") || nextScene.name.Equals("JunglaSH"))){
            SetMusic(5);
        }
        
        if (!_currentScene.Equals(nextScene) && (nextScene.name.Equals("Desierto") || nextScene.name.Equals("DesiertoSH"))){
            SetMusic(2);
        }
        
        if (!_currentScene.Equals(nextScene) && (nextScene.name.Equals("Moon") || nextScene.name.Equals("MoonSH"))){
            SetMusic(4);
        }
        
        if (!_currentScene.Equals(nextScene) && (nextScene.name.Equals("City") || nextScene.name.Equals("CitySH"))){
            SetMusic(3);
        }
        
        if (!_currentScene.Equals(nextScene) && (nextScene.name.Equals("Templo") || nextScene.name.Equals("TemploSH"))){
            SetMusic(6);
        }
    }

    public void changeVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    private void SetMusic(int index)
    {
        _AudioSources[index].Play();
        _currentScene = SceneManager.GetActiveScene();
        _currentSong = index;
    }
    
    private void Stop()
    {
        _AudioSources[_currentSong].Stop();
    }

    private void Pause()
    {
        _AudioSources[0].Pause();
    }
}
