using Platformer.Mechanics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// this class manages music playback and volume
public class MusicManager : MonoBehaviour
{

    public AudioSource AudioSource;
    public static MusicManager Instance;

    void Awake()
    {
        // music persist through scenes
        if (Instance == null)
        {
            Instance = this; 
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            AudioSource.enabled = true;
        }
        else AudioSource.enabled = false;

        if (SceneManager.GetActiveScene().buildIndex == 5)        
            AudioSource.mute = true;
        else AudioSource.mute = false;            

        // handles pitch down/up in dark mode
        if (DataStorage.IsCurrentlyDark && AudioSource.pitch > 0.88f)
        {
            AudioSource.pitch -= 0.004f;
        }
        else if(!DataStorage.IsCurrentlyDark && AudioSource.pitch < 1)
        {
            AudioSource.pitch += 0.004f;
        }

    }


}
