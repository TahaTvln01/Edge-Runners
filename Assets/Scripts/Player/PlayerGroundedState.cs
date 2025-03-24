using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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
        player.bufferJumpTimer = 0;
    }

    public override void Update()
    {
        base.Update();

        if (!player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.airState);
        }else
        {
            player.canHaveCayoteJump = true;
            player.cayoteJumpTimer = player.cayoteJumpTime;
        }

        if (player.IsGroundDetected() && (Input.GetButtonDown("Jump") || player.jumpInput))
        {
            player.jumpInput = false;
            stateMachine.ChangeState(player.jumpState);
        }

        if(player.bufferJumpTimer > 0)
        {
            stateMachine.ChangeState(player.jumpState);
        }
    }
}
