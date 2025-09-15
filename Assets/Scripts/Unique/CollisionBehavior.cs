using Platformer.Mechanics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Platformer.Model;
using Platformer.Core;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using TMPro;
using DialogueEditor;


// this class handles collisions of different tags with the player to enable/disable certain objects and parameters
public class CollisionBehavior : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;
    public PlayerController PlayerController;
    readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();

    public GameObject SpawnPoints;
    public int NumOfSpawnPoints;

    public GameObject GreenRelic;
    public GameObject DarkRelic;
    public GameObject SpeechBubble;
    public TextMeshPro DoubleJumpText;
    public TextMeshPro UpDownText;
    public TextMeshPro DarkPowerText;
    public TextMeshPro DarkPowerTurnOffText;

    public GameObject VictoryZone;
    
    public bool isInDarkTrigger;

    void Start()
    {
        NumOfSpawnPoints = SpawnPoints.transform.childCount;

        if (DoubleJumpText != null )
            DoubleJumpText.enabled = false;
        if (DarkPowerText != null )
            DarkPowerText.enabled = false;
        if (DarkPowerTurnOffText != null )
            DarkPowerTurnOffText.enabled = false;

        isInDarkTrigger = false;
        DataStorage.HasHadFirstConversation = false;
        if (SpeechBubble != null )
            SpeechBubble.SetActive(false);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("DarkRelic"))
        {
            isInDarkTrigger = false;
            if (SpeechBubble != null)
                SpeechBubble.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) // upon player enters trigger with tag
    {
        if (collision.CompareTag("NextLevel"))
        {
            // load next level
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }

        else if (collision.CompareTag("UDText"))
        {
            UpDownText.enabled = false;
        }

        else if (collision.CompareTag("DJText"))
        {
            DoubleJumpText.enabled = false;
        }

        else if (collision.CompareTag("GreenRelic"))
        {
            DataStorage.DoubleJump = true;
            GreenRelic.SetActive(false);
            DoubleJumpText.enabled = true;
        }

        else if (collision.CompareTag("DarkRelic"))
        {      
            if (!DataStorage.DarkFollow)
            {
                isInDarkTrigger = true;
                if (SpeechBubble != null)
                    SpeechBubble.SetActive(true);
            }          
        }

        else if (collision.CompareTag("DPTurnOffNotice"))
        {
            DarkPowerTurnOffText.enabled = true;
        }

        else if (collision.CompareTag("VictoryZone"))
        {
            VictoryZone.SetActive(false);
        }


        else if (collision.CompareTag("Respawn"))
        {
            SpawnPoint newSpawn = collision.GetComponent<SpawnPoint>();

            for (int i = 0; i < NumOfSpawnPoints; i++)
            {
                if (collision.gameObject.transform == SpawnPoints.transform.GetChild(i)) // which spawnpoint did you trigger?
                {
                    if (i == 0) { // if start spawnpoint
                        // do nothing
                    }
                        
                    else // update spawnpoint, set all others before it to non active (to avoid resetting to a previous spawnpoint)
                    {
                        if (SpawnPoints.transform.GetChild(i).gameObject.activeSelf == true)
                            model.spawnPoint.transform.position = newSpawn.gameObject.transform.position;
                        for (int j = 0; j < i; j++) 
                        {     
                            SpawnPoints.transform.GetChild(j).gameObject.SetActive(false);
                        }
                    }
                }
            }
        }
    }
}
