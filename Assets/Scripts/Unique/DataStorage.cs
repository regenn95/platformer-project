using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this is Datastorage where certain parameters are saved between levels
public static class DataStorage
{
    public static bool DoubleJump { get; set; }
    public static bool DarkPower { get; set; }
    public static bool HasHadFirstConversation { get; internal set; }
    public static bool DarkFollow { get; internal set; }

    public static bool IsCurrentlyDark { get; set; }

    public static float SliderSFXValue { get; set; }

    public static bool HasChangedSFXValue { get; set; }
}
