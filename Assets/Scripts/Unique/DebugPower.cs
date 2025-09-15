using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPower : MonoBehaviour
{
    // This script just sets all powers in storage to "true" for testing purposes
    void Start()
    {
        DataStorage.DoubleJump = true;
        DataStorage.DarkPower = true;
        DataStorage.DarkFollow = true;
    }


}
