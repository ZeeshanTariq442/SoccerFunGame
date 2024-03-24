using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource[] audioSources;
    // Start is called before the first frame update
    void Start()
    {
        SetSound();
    }

    public void SetSound()
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].mute = PlayerPrefs.GetString("Sound") != "ON";
        }
    }
}
