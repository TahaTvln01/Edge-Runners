using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrampolineJumpState : PlayerState
{
    public PlayerTrampolineJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    private bool canIdle;
    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.PlaySFX(14);
        canIdle = false;
        player.canDoubleJump = false;
        player.SetVelocity(player.jumpDirection.x * player.jumpForce, player.jumpDirection.y*player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(player.canMove)
            player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);

        if (player.IsWallDetected())
            stateMachine.ChangeState(player.wallSlideState);

        if (!player.IsGroundDetected())
            canIdle = true;
        if (player.IsGroundDetected() && canIdle)
        {
            stateMachine.ChangeState(player.idleState);
            player.Dust_FX_Landing();
        }
     
        if (rb.velocity.y < 0 && xInput != 0 && player.jumpDirection.x * player.jumpForce < player.moveSpeed)
            stateMachine.ChangeState(player.airState);

        if (Input.GetButtonDown("Jump") || player.jumpInput)
        {
            player.jumpInput = false;
            player.bufferJumpTimer = player.bufferJumpTime;
        }
    }
}
