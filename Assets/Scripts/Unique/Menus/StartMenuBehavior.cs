using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


// this class contains public methods for the start menu
public class StartMenuBehavior : MonoBehaviour
{

    private void Start()
    {
        // deletes game progress from previous save
        DataStorage.DoubleJump = false;
        DataStorage.DarkPower = false;
        DataStorage.DarkFollow = false;
        DataStorage.HasHadFirstConversation = false;
    }
    public void StartNewGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void QuitApplication()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
