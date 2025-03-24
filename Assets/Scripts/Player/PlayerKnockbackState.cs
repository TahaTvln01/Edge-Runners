using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockbackState : PlayerState
{
    public PlayerKnockbackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.PlaySFX(10);
        rb.velocity = new Vector2(player.knockbackDirection.x * player.knockbackDir, player.knockbackDirection.y);
        stateTimer = player.knockbackTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        stateTimer -= Time.deltaTime;
        player.jumpInput = false;
        if (stateTimer < 0)
        {
            if (xInput != 0)
            {
                stateMachine.ChangeState(player.airState);
            }
            if (player.IsGroundDetected())
            {
                stateMachine.ChangeState(player.idleState);
            }
        }
    }
}
