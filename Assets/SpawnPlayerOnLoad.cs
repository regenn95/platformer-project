using Platformer.Core;
using Platformer.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayerOnLoad : MonoBehaviour
{
    PlatformerModel model = Simulation.GetModel<PlatformerModel>();
    void Start()
    {
        var player = model.player;
        //player.enabled = false;
        Simulation.Schedule<PlayerStartSpawn>();
    }

}
