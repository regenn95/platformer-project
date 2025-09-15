using Platformer.Gameplay;
using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Platformer.Core.Simulation;

public class StartSpawnPoint : MonoBehaviour
{

    public bool start;
    void OnTriggerEnter2D(Collider2D collider)
    {
        var p = collider.gameObject.GetComponent<PlayerController>();
        if (p != null && start == true)
        {
            var ev = Schedule<PlayerStartSpawn>();
            ev.StartSpawn = this;
            start = false;
        }
    }
}

