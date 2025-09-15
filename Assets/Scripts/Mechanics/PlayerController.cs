using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;
using Cinemachine;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This is the main class used to implement control of the player.
    /// It is a superset of the AnimationController class, but is inlined to allow for any kind of customisation.
    /// </summary>
    public class PlayerController : KinematicObject
    {
        public AudioClip jumpAudio;
        public AudioClip respawnAudio;
        public AudioClip ouchAudio;

        /// <summary>
        /// Max horizontal speed of the player.
        /// </summary>
        public float maxSpeed = 7;
        /// <summary>
        /// Initial jump velocity at the start of a jump.
        /// </summary>
        public float jumpTakeOffSpeed = 8;

        public JumpState jumpState = JumpState.Grounded;
        public bool stopJump;
        /*internal new*/ public Collider2D collider2d;
        /*internal new*/ public AudioSource audioSource;
        public Health health;
        public bool controlEnabled;

        bool jump;
        Vector2 move;
        SpriteRenderer spriteRenderer;
        internal Animator animator;
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public CinemachineVirtualCamera MainCamera;
        public CinemachineVirtualCamera LookUpCamera;
        public CinemachineVirtualCamera LookDownCamera;
        public bool stop;

        public Bounds Bounds => collider2d.bounds;

        public bool doubleJumpReady;

        void Awake()
        {
            health = GetComponent<Health>();
            audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            //DataStorage.DoubleJump = true;
        }

        protected override void Update()
        {
            
            if (controlEnabled)
            {
                move.x = Input.GetAxis("Horizontal");
                // jump controls
                if (jumpState == JumpState.Grounded && Input.GetButtonDown("Jump"))
                    jumpState = JumpState.PrepareToJump;      
                else if (Input.GetButtonUp("Jump"))
                {
                    stopJump = true;
                    Schedule<PlayerStopJump>().player = this;
                }
                else if (jumpState == JumpState.InFlight && Input.GetButtonDown("Jump") && stopJump && doubleJumpReady)
                    jumpState = JumpState.PrepareToJump;

                // look up/down
                if (stop && Input.GetButton("Vertical"))
                {
                    MainCamera.gameObject.SetActive(false);
                    if (Input.GetAxis("Vertical") > 0f) // up
                    {
                        LookDownCamera.gameObject.SetActive(false);
                        LookUpCamera.gameObject.SetActive(true);   
                    }      
                    else // down
                    {
                        LookUpCamera.gameObject.SetActive(false);
                        LookDownCamera.gameObject.SetActive(true);
                    }
                        
                } // set main camera back
                else MainCamera.gameObject.SetActive(true);
            
               
            }
            else
            {
                move.x = 0;
            }
            UpdateJumpState();
            base.Update();
        }

        void UpdateJumpState()
        {
            jump = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    jumpState = JumpState.Jumping;
                    jump = true;
                    if (DataStorage.DoubleJump)
                        doubleJumpReady = true;
                    stopJump = false;
                    break;
                case JumpState.Jumping:
                    if (!IsGrounded)
                    {
                        Schedule<PlayerJumped>().player = this;
                        jumpState = JumpState.InFlight;
                    }
                    break;
                case JumpState.InFlight:
                    if (IsGrounded)
                    {
                        Schedule<PlayerLanded>().player = this;
                        jumpState = JumpState.Landed;
                    }
                    break;
                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
                    doubleJumpReady = false;
                    break;
            }
        }

        protected override void ComputeVelocity()
        {
            if (jump)
            {
                velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                if (!IsGrounded)
                    doubleJumpReady = false;
                jump = false;
            }
            else if (stopJump)
            {
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * model.jumpDeceleration;
                }
                if (IsGrounded)
                    stopJump = false;
            }

            if (move.x > 0.01f)
                spriteRenderer.flipX = false;
            else if (move.x < -0.01f)
                spriteRenderer.flipX = true;

            if (move.x == 0f && move.y == 0f && IsGrounded) stop = true;
            else stop = false;

            animator.SetBool("grounded", IsGrounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;
        }

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed
        }
    }
}