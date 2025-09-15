using Platformer.Core;
using Platformer.Gameplay;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;
using System;


// Fired only at the start of each level when the player spawns
public class PlayerStartSpawn : Simulation.Event<PlayerStartSpawn>
{
    public StartSpawnPoint StartSpawn;
    PlatformerModel model = Simulation.GetModel<PlatformerModel>();
    public static event Action OnStartSpawned;  

    public override void Execute()
    {
        OnStartSpawned?.Invoke();

        var player = model.player;
        if (!player.isActiveAndEnabled)
            player.enabled = true;
        player.controlEnabled = false;
        if (player.audioSource && player.respawnAudio)
            player.audioSource.PlayOneShot(player.respawnAudio);
        player.jumpState = PlayerController.JumpState.Grounded;
        player.animator.SetTrigger("start");
        Simulation.Schedule<EnablePlayerInput>(2f);
    }

}
