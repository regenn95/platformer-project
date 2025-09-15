using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// class that defines functions for the menu in-game
public class GameMenuBehavior : MonoBehaviour
{
   public void ExitToStartMenu()
    {
        SceneManager.LoadScene("Start Menu");
    }
    
    // todo: ensure quit via yes/no buttons and add new screen
}
