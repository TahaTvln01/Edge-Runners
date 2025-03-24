using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    [SerializeField] private Transform resPoint;

    private void Start()
    {
        PlayerManager.instance.respawnPoint = resPoint;
        PlayerManager.instance.RespawnPlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            if(collision.transform.position.x < transform.position.x-1)
            {
                GetComponent<Animator>().SetTrigger("touch");
            }
        }
    }
}
