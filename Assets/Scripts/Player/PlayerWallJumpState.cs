using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.PlaySFX(6);
        stateTimer = player.wallJumpTime;
        player.SetVelocity(player.moveSpeed * (-player.direction), player.jumpSpeed);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        

        if (player.IsWallDetected())
        {
            stateMachine.ChangeState(player.wallSlideState);
        }
        if (player.IsGroundDetected())
        {
            player.Dust_FX_Landing();
            stateMachine.ChangeState(player.idleState);
        }

        if (stateTimer < 0 && xInput != 0)
        {
            stateMachine.ChangeState(player.airState);
        }

        if (player.jumpInput || Input.GetButtonDown("Jump"))
        {
            if (player.canDoubleJump && !player.IsWallDetected() && player.isDoubleJumpActive)
            {
                player.canDoubleJump = false;
                stateMachine.ChangeState(player.doubleJumpState);
            }
            else
            {
                player.bufferJumpTimer = player.bufferJumpTime;
            }
            player.jumpInput = false;
        }
    }
}
