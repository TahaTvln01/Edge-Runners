using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.canDoubleJump = true;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        player.Dust_FX_WallSlide_Always();

        //if (xInput != 0 && player.direction != xInput)
        //{
        //    player.isItWallCayote = true;
        //    player.canHaveCayoteJump = true;
        //    player.cayoteJumpTimer = player.cayoteJumpTime*2;
        //    stateMachine.ChangeState(player.idleState);
        //}
        if (yInput < 0)
        {
            rb.velocity = new Vector2(10 * player.direction, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(10 * player.direction, -player.wallSlideSpeed);
        }

        if (player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.idleState);
        }
        if (!player.IsWallDetected())
        {
            stateMachine.ChangeState(player.idleState);
        }

        if(Input.GetButtonDown("Jump") || player.jumpInput)
        {
            if (player.IsWallDetected())
            {
                stateMachine.ChangeState(player.wallJumpState);
            }
            player.jumpInput = false;
        }
    }
}
