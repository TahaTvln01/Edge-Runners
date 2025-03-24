using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoubleJumpState : PlayerState
{
    public PlayerDoubleJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.PlaySFX(6);
        player.SetVelocity(rb.velocity.x, player.jumpSpeed*0.8f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);
        if (rb.velocity.y < 0)
            stateMachine.ChangeState(player.airState);
        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);

        if (Input.GetButtonDown("Jump") || player.jumpInput)
        {
            player.jumpInput = false;
            player.bufferJumpTimer = player.bufferJumpTime;
        }
    }
}
