using Platformer.Core;
using Platformer.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapCollisionBehavior : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) // activates player death when colliding with spikes
    {
        if (collision.CompareTag("Spikes"))
        {
            Simulation.Schedule<PlayerDeath>();

        }
    }
}
