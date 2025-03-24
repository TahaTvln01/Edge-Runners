using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    
    public override void Enter()
    {
        base.Enter();
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
            player.SetVelocity(0, 0);
            player.Dust_FX_Landing();
            stateMachine.ChangeState(player.idleState);
        }
        player.SetVelocity(player.moveSpeed * xInput, rb.velocity.y);


        if (player.jumpInput || Input.GetButtonDown("Jump"))
        {
            if (player.cayoteJumpTimer > 0 && player.canHaveCayoteJump)
            {
                if (player.isItWallCayote)
                {
                    player.Flip();
                    stateMachine.ChangeState(player.wallJumpState);
                }
                else
                    stateMachine.ChangeState(player.jumpState);
                player.canHaveCayoteJump = false;
                
            }
            else if (player.canDoubleJump && !player.IsWallDetected() && player.isDoubleJumpActive)
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
