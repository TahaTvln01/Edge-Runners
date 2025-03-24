using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelFinishedState : PlayerState
{
    public PlayerLevelFinishedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(0, rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        player.jumpInput = false;
    }
}
