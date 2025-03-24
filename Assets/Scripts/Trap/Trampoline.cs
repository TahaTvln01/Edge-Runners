using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Trampoline : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform directionPoint;
    private bool canMove;
    private Vector2 direction;

    private void Start()
    {
        canMove = true;
        direction = new Vector2(directionPoint.position.x - transform.position.x, directionPoint.position.y - transform.position.y);
        if (transform.rotation.z != 0)
        {
            canMove = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            Player player = collision.GetComponent<Player>();
            GetComponent<Animator>().SetTrigger("activate");
            player.jumpForce = jumpForce;
            player.jumpDirection = direction;
            player.canMove = canMove;
            player.stateMachine.ChangeState(player.trampolineJumpState);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(directionPoint.position.x, directionPoint.position.y));
    }
}
