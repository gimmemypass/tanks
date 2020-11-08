using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    [SerializeField] private AudioMixer am;
    // Start is called before the first frame update
    void Start()
    {
        Screen.fullScreen = false;
        am.SetFloat("masterVolume", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FullScreenToggle()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
    public void AudioVolume(float sliderValue)
    {
        am.SetFloat("masterVolume", sliderValue);
    }
}
