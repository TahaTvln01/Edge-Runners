using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.Dust_FX_Play();
        AudioManager.instance.PlaySFX(6);
        player.SetVelocity(rb.velocity.x, player.jumpSpeed);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);

        if (rb.velocity.y <= 0)
            stateMachine.ChangeState(player.airState);
        
        if(player.jumpInput || Input.GetButtonDown("Jump"))
        {
            if (player.canDoubleJump && player.isDoubleJumpActive)
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
