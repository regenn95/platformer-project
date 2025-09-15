using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// class that executes playback sound whenever the volume on the slider is changed
public class PlaysoundManager : MonoBehaviour
{
    public AudioSource PlaySound;
    public Slider Slider;
    public Type type;

    public enum Type 
        {
        Overworld,
        SFX
        }

    private void Awake()
    {
        // values are kept between scenes
        if (DataStorage.HasChangedSFXValue)
            Slider.value = DataStorage.SliderSFXValue;
        
    }
    void Start()
    {
        PlaySound = GetComponentInChildren<AudioSource>();
        Slider = GetComponentInChildren<Slider>();

    }

    public void SaveValueToData()
    {
        if (this.type == Type.SFX)
        {
            DataStorage.SliderSFXValue = Slider.value;
            DataStorage.HasChangedSFXValue = true;
        }
    }

    public void MuteAfter3Sec()
    {
        StartCoroutine(Delay3SecondsThenMute());
    }
    IEnumerator Delay3SecondsThenMute()
    {
        yield return new WaitForSecondsRealtime(3f);
        PlaySound.mute = true;
    }
   

}
