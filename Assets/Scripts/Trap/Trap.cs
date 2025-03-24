using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private bool isSpikeTrap = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            Player player = collision.GetComponent<Player>();
            GiveDamage(player);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            Player player = collision.GetComponent<Player>();
            GiveDamage(player);
        }
    }

    private void GiveDamage(Player player)
    {
        if (isSpikeTrap)
        {
            player.TakeDamage(0);
        }
        else if (player.transform.position.x > transform.position.x)
        {
            player.TakeDamage(1);
        }
        else
        {
            player.TakeDamage(-1);
        }
    }
}
