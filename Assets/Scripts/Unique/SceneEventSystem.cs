using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEventSystem : MonoBehaviour
{
    public static SceneEventSystem eventSystem;

    private void Awake()
    {
        eventSystem = this;
    }

    public event Action onSceneStart;

    public void SceneStart()
    {
        if (onSceneStart != null) onSceneStart();
    }
}
