using Platformer.Core;
using Platformer.Gameplay;
using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// handles dark relic companion movement and targeting
public class DarkRelicBehavior : MonoBehaviour
{

    public PlayerController Player;
    SpriteRenderer spriteRenderer;
    Animator animator;
    public PlayerSpawn PlayerSpawn;
    public PlayerStartSpawn PlayerStartSpawn;
    private bool IsSpawning;
 
    public Transform CurrentTarget;
    public Transform ForwardTarget;
    public Transform BackwardTarget;
    float moveSpeed = 5f;

    void OnEnable()
    {
        PlayerStartSpawn.OnStartSpawned += HandleStartSpawn;
        PlayerSpawn.OnRespawned += HandleRespawn;
    }

    void OnDisable()
    {
        PlayerStartSpawn.OnStartSpawned -= HandleStartSpawn;
        PlayerSpawn.OnRespawned -= HandleRespawn;
    }

    void HandleStartSpawn()
    {
        IsSpawning = true;
    }

    void HandleRespawn()
    {
        IsSpawning = true;
    }

    void Start()
    {
        spriteRenderer = Player.GetComponent<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        
        if (DataStorage.DarkFollow && IsSpawning)  
            TeleportTo(ForwardTarget);
        
    }

    void Update()
    {
        // follows to player position
        // goes to other side when player flips sprite
        CurrentTarget = spriteRenderer.flipX ? BackwardTarget : ForwardTarget;

        if (!DataStorage.DarkFollow) return;

        if (Player.controlEnabled)
            IsSpawning = false;

        // handles behavior to follow or teleport near player, based on if currently is spawning
        if (!IsSpawning)
            Follow(CurrentTarget);
        else TeleportTo(CurrentTarget);

    }
    
    public void Follow(Transform Target)
    {
        if (DataStorage.DarkFollow)
        {
            if (Target != null)
                transform.position = Vector2.MoveTowards(transform.position, Target.position, moveSpeed * Time.deltaTime);
           
        }
    }

    public void TeleportTo(Transform Target)
    {
        if (DataStorage.DarkFollow)
        {
            if (Target != null)
                transform.position = Target.position;

        }
    }
}
