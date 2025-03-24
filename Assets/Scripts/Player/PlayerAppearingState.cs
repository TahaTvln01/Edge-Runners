using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAppearingState : PlayerState
{
    private float defaultGravityScale;
    public PlayerAppearingState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(0, 0);
        defaultGravityScale = player.rb.gravityScale;
        player.rb.gravityScale = 0;
    }

    public override void Exit()
    {
        base.Exit();
        player.rb.gravityScale = defaultGravityScale;

    }

    public override void Update()
    {
        base.Update();
        player.jumpInput = false;
        player.SetVelocity(0, 0);
    }
}
