using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private bool isPlayed = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            GetComponent<Animator>().SetTrigger("activate");
            PlayerManager.instance.respawnPoint = transform;
            if (!isPlayed )
            {
                AudioManager.instance.PlaySFX(3);
                isPlayed = true;
            }
        }
    }
}
