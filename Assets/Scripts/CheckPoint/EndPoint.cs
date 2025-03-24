using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    private InGame_UI inGame_UI;
    private bool isTriggered = false;

    private void Start()
    {
        inGame_UI = GameObject.Find("Canvas").GetComponent<InGame_UI>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            if (isTriggered)
                return;
            isTriggered = true;
            AudioManager.instance.PlaySFX(3);
            Player player = collision.GetComponent<Player>();
            GetComponent<Animator>().SetTrigger("activate");
            inGame_UI.OnLevelFinished();
            GameManager.instance.SaveCollectedFruits();
            GameManager.instance.SaveLevelInfo();
            player.stateMachine.ChangeState(player.levelFinishedState);
        }
    }
}
