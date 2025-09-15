using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// Updates volume of specified track based on the value of the respective slider
public class SliderVolume : MonoBehaviour
{

    public Slider Slider;
    public MusicManager MusicManager;
    public void AdjustVolume()
    {
        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.AudioSource.volume = Slider.value;
        }
    }

}
