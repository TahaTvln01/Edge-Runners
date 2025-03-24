using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }

    private void GoIdleState()
    {
        player.stateMachine.ChangeState(player.idleState);
    }

    private void CheckIfDying()
    {
        if (player.Hp <= 0)
        {
            player.stateMachine.ChangeState(player.dyingState);
        }
    }

    private void KillPlayer_AT()
    {
        PlayerManager.instance.KillPlayer();
    }
}
