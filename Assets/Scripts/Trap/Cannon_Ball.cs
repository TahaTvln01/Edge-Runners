using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cannon_Ball : MonoBehaviour
{
    public float Speed;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;

    private bool willDisappear;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rb.velocity = new Vector2(Speed, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            if (collision.GetComponent<Player>().justKnocked < 0)
            {
                GiveDamage(collision.GetComponent<Player>());
                Disappear();
            }
        }
    }
    private void Update()
    {
        if (IsGroundDetected())
        {
            Disappear();
        }
    }
    private void GiveDamage(Player player)
    {
        if (player.transform.position.x > transform.position.x)
        {
            player.TakeDamage(1);
        }
        else
        {
            player.TakeDamage(-1);
        }
    }

    public void SetSpeed(float _Speed)
    {
        Speed = _Speed;
    }

    private void Disappear()
    {
        if (willDisappear)
            return;

        rb.velocity = Vector3.zero;
        anim.SetTrigger("Disappear");
    }

    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.right, groundCheckDistance, whatIsGround);
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x + groundCheckDistance, groundCheck.position.y));
    }

    public void DestroyThis()
    {
        Destroy(gameObject);
    }
}
