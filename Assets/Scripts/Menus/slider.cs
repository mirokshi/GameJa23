using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slider : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private void Awake()
    {
        _slider.value = AudioListener.volume;
    }

    private void Start()
    {
        _slider.onValueChanged.AddListener(val => MusicManager.Instance.changeVolume(val));
    }
}
