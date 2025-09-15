using Platformer.Gameplay;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// Marks a trigger as a VictoryZone, usually used to end the current game level.
    /// </summary>
    public class VictoryZone : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D collider)
        {
            var p = collider.gameObject.GetComponent<PlayerController>();
            if (p != null)
            {
                var ev = Schedule<PlayerEnteredVictoryZone>();
                ev.victoryZone = this;
                StartCoroutine(FinishGame());
            }
        }

        IEnumerator FinishGame() // wait 5 seconds in game and then go to start menu
        {
            yield return new WaitForSeconds(5f);
            SceneManager.LoadScene("Start Menu");
        }
    }
}