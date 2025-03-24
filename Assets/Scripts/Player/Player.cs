using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Particles")]
    public ParticleSystem dustFX_Right;
    public ParticleSystem dustFX_Left;

    [SerializeField] private float dustFXTime = 0.1f;
    private float dustFXTimer;

    [Header("Player Stats")]
    public int Hp = 2;

    [Header("Controlls")]
    public bool testingOnPC = true;
    public bool jumpInput;
    public float ButtonXInput;
    public float ButtonYInput;

    [Header("Move Info")]
    public float moveSpeed = 4f;
    public float jumpSpeed = 10f;
    public float wallJumpTime = 1f;
    public float jumpForce = 30;
    public float wallSlideSpeed = 1f;
    public bool isDoubleJumpActive = true;
    public float bufferJumpTime = 0.2f;
    public float cayoteJumpTime;
    [HideInInspector] public bool canDoubleJump = true;
    [HideInInspector] public float bufferJumpTimer;
    [HideInInspector] public float cayoteJumpTimer;
    [HideInInspector] public bool canHaveCayoteJump;
    [HideInInspector] public int direction = 1;
    [HideInInspector] private bool isFacingRight = true;
    [HideInInspector] public Vector2 jumpDirection;
    [HideInInspector] public bool canMove;
    [HideInInspector] public bool isItWallCayote;

    [Header("Collision Info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsWall;

    [Header("Knockback Info")]
    public float knockbackTime;
    public Vector2 knockbackDirection;
    [HideInInspector] public int knockbackDir;
    [HideInInspector] public float justKnocked;

    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    #endregion

    #region States
    public PlayerStateMachine stateMachine {  get; private set; }
    
    public PlayerIdleState idleState {  get; private set; } 
    public PlayerMoveState moveState {  get; private set; }
    public PlayerAirState airState {  get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerJumpState jumpState {  get; private set; }
    public PlayerDoubleJumpState doubleJumpState { get; private set; }
    public PlayerKnockbackState knockbackState { get; private set; }
    public PlayerTrampolineJumpState trampolineJumpState { get; private set; }
    public PlayerLevelFinishedState levelFinishedState { get; private set; }
    public PlayerAppearingState appearingState { get; private set; }
    public PlayerDyingState dyingState { get; private set; }

    #endregion

    private void Awake()
    {
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "InAir");
        airState  = new PlayerAirState (this, stateMachine, "InAir");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "InAir");
        doubleJumpState = new PlayerDoubleJumpState(this, stateMachine, "InAir");
        knockbackState = new PlayerKnockbackState(this, stateMachine, "Knockback");
        trampolineJumpState = new PlayerTrampolineJumpState(this, stateMachine, "InAir");
        levelFinishedState = new PlayerLevelFinishedState(this, stateMachine, "Idle");
        appearingState = new PlayerAppearingState(this, stateMachine, "Appear");
        dyingState = new PlayerDyingState(this, stateMachine, "Dying");
    }

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        stateMachine.Initialize(appearingState);
        SetAnimationLayer();
        Hp = 2;
    }

    private void SetAnimationLayer()
    {
        int skinIndex = PlayerManager.instance.choosenSkinID;
        for (int i = 0; i < anim.layerCount; i++)
        {
            anim.SetLayerWeight(i, 0);
        }
        anim.SetLayerWeight(skinIndex, 1);
    }
    private void Update()
    {
        stateMachine.currentState.Update();

        if (!IsGroundDetected())
        {
            bufferJumpTimer -= Time.deltaTime;
            cayoteJumpTimer -= Time.deltaTime;
        }else
        {
            isItWallCayote = false;
        }

        justKnocked -= Time.deltaTime;
    }

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity,_yVelocity);
        FlipController(_xVelocity);
    }

    public void Flip()
    {
        direction = direction * -1;
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180, 0);
    }

    public void Jump()
    {
        jumpInput = true;
    }

    public void FlipController(float _x)
    {
        if (_x > 0 && !isFacingRight)
            Flip();
        else if (_x < 0 && isFacingRight)
            Flip();
    }

    public void TakeDamage(int _knockbackDir)
    {
        if(justKnocked < 0)
        {
            Hp--;
            knockbackDir = _knockbackDir;
            if (knockbackDir == direction)
                Flip();
            justKnocked = knockbackTime * 1.5f;
            stateMachine.ChangeState(knockbackState);
            PlayerManager.instance.ScreenShake(-direction);
        }
    }
    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    public bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * direction, wallCheckDistance , whatIsWall);
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance * direction, wallCheck.position.y));
    }
    public void DustFX_Always()
    {
        dustFXTimer -= Time.deltaTime;
        if(dustFXTimer < 0)
        {
            dustFXTimer = dustFXTime;
            Dust_FX_Play();
        }
    }
    public void Dust_FX_Play()
    {
        if(direction == 1)
        {
            dustFX_Right.Play();
        }else
        {
            dustFX_Left.Play();
        }
    }
    public void Dust_FX_WallSlide_Always()
    {
        dustFXTimer -= Time.deltaTime;
        if (dustFXTimer < 0)
        {
            dustFXTimer = dustFXTime;
            if (direction == 1)
            {
                dustFX_Left.Play();
            }
            else
            {
                dustFX_Right.Play();
            }
        }
    }
    public void Dust_FX_Landing()
    {
        dustFX_Right.Play();
        dustFX_Left.Play();
        dustFX_Right.Play();
        dustFX_Left.Play();
    }
}
